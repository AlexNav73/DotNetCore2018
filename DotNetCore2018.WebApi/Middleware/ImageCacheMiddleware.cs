using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using DotNetCore2018.Business.Services.Interfaces;
using DotNetCore2018.Business.Specifications;
using DotNetCore2018.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;

namespace DotNetCore2018.WebApi.Middleware
{
    public class ImageCacheMiddleware
    {
        private const string CategoryImage = "/images";

        private readonly MemoryCache _cache;
        private readonly RequestDelegate _next;
        private readonly IFileProvider _fileProvider;
        private readonly ImageCacheOptions _options;

        private string _wwwroot;

        public ImageCacheMiddleware(ImageCacheOptions options, RequestDelegate next, MemoryCache cache)
        {
            _options = options;
            _next = next;
            _cache = cache;
            _wwwroot = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            if (!Directory.Exists(Path.Combine(_wwwroot, options.CacheTo)))
            {
                Directory.CreateDirectory(Path.Combine(_wwwroot, options.CacheTo));
            }
            _fileProvider = new PhysicalFileProvider(_wwwroot);
        }

        public async Task InvokeAsync(HttpContext context, IDataService dataService, ILogger<ImageCacheMiddleware> logger)
        {
            if (context.Request.Path.StartsWithSegments(CategoryImage))
            {
                var imagePath = FromCache(GetImageId(context));
                if (imagePath != null)
                {
                    var file = _fileProvider.GetFileInfo(imagePath);
                    if (file != null && file.Exists)
                    {
                        logger.LogTrace("Return image from cache");
                        await context.Response.SendFileAsync(file);
                        return;
                    }
                }
            }

            await _next(context);

            if (context.Response.ContentType == "image/jpeg")
            {
                logger.LogTrace("Save image to cache");
                var id = GetImageId(context);
                await CacheImage(id, dataService);
                AddToCache(id, logger);
            }
        }

        private void AddToCache(string id, ILogger<ImageCacheMiddleware> logger)
        {
            if (!_cache.TryGetValue(id, out string path))
            {
                var expirationToken = new CancellationChangeToken(new CancellationTokenSource(_options.ExpireAfter).Token);
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(_options.ExpireAfter)
                    // Expiration token allows to clean cache even no requests comming
                    // https://github.com/aspnet/Caching/issues/248#issuecomment-258571802
                    .AddExpirationToken(expirationToken)
                    .RegisterPostEvictionCallback(onDropCacheEntry, logger)
                    .SetSize(1);

                var imagePath = _fileProvider.GetFileInfo($"/{_options.CacheTo}/{id}.jpeg").PhysicalPath;
                _cache.Set(id, imagePath, cacheEntryOptions);
            }
        }

        private string FromCache(string id)
            => _cache.TryGetValue(id, out string path) ? path : null;

        private void onDropCacheEntry(object key, object value, EvictionReason reason, object state)
        {
            var logger = (ILogger<ImageCacheMiddleware>)state;
            logger.LogInformation($"Image [{value}] will be removed [{reason}] from cache");
            File.Delete((string)value);
        }

        private async Task CacheImage(string id, IDataService dataService)
        {
            if (id != null && int.TryParse(id, out int idValue))
            {
                var category = dataService.GetBy(new IdSpecification<Category>(idValue));
                if (category?.Image != null)
                {
                    var fileInfo = _fileProvider.GetFileInfo($"/images/{category.Image}.jpeg");
                    if (fileInfo.Exists)
                    {
                        using (var reader = fileInfo.CreateReadStream())
                        {
                            var cacheFilePath = _fileProvider.GetFileInfo($"/{_options.CacheTo}/{id}.jpeg").PhysicalPath;
                            using (var writer = File.OpenWrite(cacheFilePath))
                            {
                                await reader.CopyToAsync(writer);
                            }
                        }
                    }
                }
            }
        }

        private string GetImageId(HttpContext context)
        {
            return context.Request.Path
                .ToUriComponent()
                .Replace(CategoryImage + "/", string.Empty);
        }
    }

    public class ImageCacheOptions
    {
        public string CacheTo { get; set; }
        public TimeSpan ExpireAfter { get; set; }
    }
}