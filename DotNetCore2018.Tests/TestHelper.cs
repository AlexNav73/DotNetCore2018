using System.IO;
using DotNetCore2018.Business.Services.Interfaces;
using DotNetCore2018.Tests.Mocks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NUnit.Framework;

namespace DotNetCore2018.Tests
{
    internal static class TestHelper
    {
        public static IConfiguration BuildConfig()
        {
            var path = new DirectoryInfo(TestContext.CurrentContext.TestDirectory).Parent.Parent.Parent.Parent.FullName;
            var appsettings = Path.Combine(path, "DotNetCore2018.WebApi", "appsettings.json");

            return new ConfigurationBuilder()
                .AddJsonFile(appsettings)
                .Build();
        }

        public static ILogger<T> BuildLogger<T>()
        {
            return new TestLogger<T>();
        }

        public static T ExtractModel<T>(this IActionResult self) where T : class
        {
            return (self as ViewResult)?.ViewData?.Model as T;
        }

        public static IFileService BuildFileService() => new FileServiceMock();
    }
}