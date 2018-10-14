using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DotNetCore2018.Business.Services.Interfaces;
using DotNetCore2018.Business.Specifications;
using DotNetCore2018.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.FileProviders;

namespace DotNetCore2018.WebApi.Middleware
{
    public class ImageCacheMiddleware
    {
        private const string CacheFolder = "cache";

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
            _fileProvider = new PhysicalFileProvider(Path.Combine(_wwwroot, CacheFolder));
        }

        public async Task InvokeAsync(HttpContext context, IDataService dataService)
        {
            if (context.Request.Path.StartsWithSegments("/category/image"))
            {
                if (await ReturnCachedFile(context, dataService))
                {
                    return;
                }
            }

            await _next(context);

            if (context.Response.ContentType == "image/jpeg")
            {
                await SaveFileToCache(context, dataService);
            }
        }

        private async Task SaveFileToCache(HttpContext context, IDataService dataService)
        {

        }

        private async Task<bool> ReturnCachedFile(HttpContext context, IDataService dataService)
        {
            var id = context.Request.Query["id"].ToArray().FirstOrDefault();
            if (id != null && int.TryParse(id, out int idValue))
            {
                var category = dataService.GetBy(new IdSpecification<Category>(idValue));
                if (category?.Image != null)
                {
                    var fileInfo = _fileProvider.GetFileInfo($"{category.Image}.jpeg");
                    if (fileInfo.Exists)
                    {
                        await context.Response.SendFileAsync(fileInfo);
                        return true;
                    }
                }
            }
            return false;
        }
    }
}