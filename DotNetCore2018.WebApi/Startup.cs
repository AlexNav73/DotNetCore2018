using System.IO;
using DotNetCore2018.Business.Services;
using DotNetCore2018.Business.Services.Interfaces;
using DotNetCore2018.WebApi.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;

namespace DotNetCore2018.WebApi
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration, ILogger<Startup> logger)
        {
            _configuration = configuration;

            var logConfig = configuration.GetSection("Logging");
            var logLevelConfig = logConfig.GetSection("LogLevel");
            var defaultLogLevel = logLevelConfig.GetValue<string>("Default");

            logger.LogTrace("Logging configuration: {0}", defaultLogLevel);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<Data.AppContext>(options => 
            {
                options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddScoped<IDataService, DataService>();
            services.AddScoped<IFileService, FileService>();
            services.AddMvc();
        }

        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env,
            IApplicationLifetime appLifetime,
            ILoggerFactory loggerFactory)
        {
            appLifetime.ApplicationStarted.Register(OnApplicationStart, loggerFactory.CreateLogger<Startup>());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/home/error");
            }

            app.CacheImageFiles();
            app.UseStaticFiles();
            app.UseMvc(ConfigureRoutes);
        }

        private void ConfigureRoutes(IRouteBuilder routeBuilder)
        {
            var wwwroot = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            if (!Directory.Exists(Path.Combine(wwwroot, "images")))
            {
                Directory.CreateDirectory(Path.Combine(wwwroot, "images"));
            }

            var fileProvider = new PhysicalFileProvider(wwwroot);
            routeBuilder.MapGet("{filename}.wasm", context => 
            {
                var filename = context.GetRouteValue("filename");
                context.Response.ContentType = "application/wasm";
                return context.Response.SendFileAsync(fileProvider.GetFileInfo($"{filename}.wasm"));
            });
            routeBuilder.MapRoute("images", "images/{id:int}", defaults: new { controller = "Category", action = "Image" });
            routeBuilder.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
        }

        private void OnApplicationStart(object sender)
        {
            var logger = (Logger<Startup>)sender;
            logger.LogInformation($"Application started in {Directory.GetCurrentDirectory()}");
        }
    }
}
