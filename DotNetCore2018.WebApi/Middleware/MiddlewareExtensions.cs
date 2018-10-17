using Microsoft.AspNetCore.Builder;

namespace DotNetCore2018.WebApi.Middleware
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder CacheImageFiles(
            this IApplicationBuilder builder, ImageCacheOptions options)
        {
            return builder.UseMiddleware<ImageCacheMiddleware>(options);
        }
    }
}