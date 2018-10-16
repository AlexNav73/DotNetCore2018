using System.IO;
using System.Threading.Tasks;
using DotNetCore2018.Business.Services.Interfaces;
using DotNetCore2018.Business.Specifications;
using DotNetCore2018.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;

namespace DotNetCore2018.WebApi.Middleware
{
    public class ImageCacheMiddleware
    {
        private const string CacheFolder = "cache";
        private const string CategoryImage = "/category/image";

        private readonly RequestDelegate _next;
        private readonly IFileProvider _fileProvider;

        private string _wwwroot;

        public ImageCacheMiddleware(RequestDelegate next)
        {
            _next = next;
            _wwwroot = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            if (!Directory.Exists(Path.Combine(_wwwroot, CacheFolder)))
            {
                Directory.CreateDirectory(Path.Combine(_wwwroot, CacheFolder));
            }
            _fileProvider = new PhysicalFileProvider(_wwwroot);
        }

        public async Task InvokeAsync(HttpContext context, IDataService dataService)
        {
            if (context.Request.Path.StartsWithSegments(CategoryImage))
            {
                var imageName = GetImageId(context);
                var file = _fileProvider.GetFileInfo($"/{CacheFolder}/{imageName}.jpeg");
                if (file.Exists)
                {
                    await context.Response.SendFileAsync(file);
                    return;
                }
            }

            await _next(context);

            if (context.Response.ContentType == "image/jpeg")
            {
                await CacheImage(GetImageId(context), dataService);
            }
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
                            var cacheFilePath = _fileProvider.GetFileInfo($"/{CacheFolder}/{id}.jpeg").PhysicalPath;
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
}