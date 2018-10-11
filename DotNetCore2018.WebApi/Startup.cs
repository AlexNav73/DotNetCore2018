using System;
using System.IO;
using DotNetCore2018.Business.Services;
using DotNetCore2018.Business.Services.Interfaces;
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

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<Data.AppContext>(options => 
            {
                options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddScoped<IDataService, DataService>();
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

            app.UseStaticFiles();
            app.UseMvc(ConfigureRoutes);
        }

        private void ConfigureRoutes(IRouteBuilder routeBuilder)
        {
            var fileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"));

            routeBuilder.MapRoute("default", "{controller=Home}/{action=Index}");
            routeBuilder.MapGet("{controller}/{filename}.wasm", context =>
            {
                var filename = context.GetRouteValue("filename");
                context.Response.ContentType = "application/wasm";
                return context.Response.SendFileAsync(fileProvider.GetFileInfo($"{filename}.wasm"));
            });
        }

        private void OnApplicationStart(object sender)
        {
            var logger = (Logger<Startup>)sender;
            logger.LogInformation($"Application started in {Directory.GetCurrentDirectory()}");
        }
    }
}
