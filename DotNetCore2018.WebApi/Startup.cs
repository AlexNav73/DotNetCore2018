using System;
using System.IO;
using DotNetCore2018.Business.Services;
using DotNetCore2018.Business.Services.Interfaces;
using DotNetCore2018.Business.Services.Models;
using DotNetCore2018.Data;
using DotNetCore2018.Data.Entities;
using DotNetCore2018.WebApi.Middleware;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using NSwag.AspNetCore;

namespace DotNetCore2018.WebApi
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _env;

        public Startup(IConfiguration configuration, ILogger<Startup> logger, IHostingEnvironment env)
        {
            _configuration = configuration;
            _env = env;

            var defaultLogLevel = _configuration.GetValue<string>("Logging:LogLevel:Default");

            logger.LogTrace("Logging configuration: {0}", defaultLogLevel);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<Data.AppContext>(options => 
            {
                options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddScoped<IAppContext, Data.AppContext>();
            services.AddScoped<IDataService, DataService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<SignInManager<User>>();
            services.AddScoped<RoleManager<UserRole>>();
            services.AddSingleton<IEmailSender, EmailSender>();
            services.Configure<AuthMessageSenderOptions>(_configuration);
            services.AddSingleton(f => new MemoryCache(new MemoryCacheOptions()
            {
                SizeLimit = 30
            }));
            services.AddSwagger();
            services.AddSession();
            if (!_env.IsDevelopment())
            {
                services.Configure<MvcOptions>(o => o.Filters.Add(new RequireHttpsAttribute()));
            }

            services.AddIdentity<User, UserRole>(o =>
            {
                if (_env.IsDevelopment())
                {
                    o.SignIn.RequireConfirmedEmail = false;
                    o.SignIn.RequireConfirmedPhoneNumber = false;

                    o.Password.RequireDigit = false;
                    o.Password.RequiredLength = 5;
                    o.Password.RequiredUniqueChars = 0;
                    o.Password.RequireLowercase = false;
                    o.Password.RequireNonAlphanumeric = false;
                    o.Password.RequireUppercase = false;

                    o.User.RequireUniqueEmail = false;
                }
            })
            .AddRoles<UserRole>()
            .AddEntityFrameworkStores<Data.AppContext>()
            .AddDefaultTokenProviders()
                .AddTokenProvider<EmailTokenProvider<User>>(TokenOptions.DefaultEmailProvider);

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddAuthentication(options =>
            {
                //options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                //options.DefaultScheme = AzureADDefaults.AuthenticationScheme;

                //options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                //options.DefaultAuthenticateScheme = AzureADDefaults.AuthenticationScheme;

                //options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                //options.DefaultChallengeScheme = AzureADDefaults.AuthenticationScheme;

                //options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                //options.DefaultSignInScheme = AzureADDefaults.AuthenticationScheme;

                //options.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                //options.DefaultSignOutScheme = AzureADDefaults.AuthenticationScheme;

                /* Local login
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                */

                /* Azure AD Authentication
                options.DefaultChallengeScheme = AzureADDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = AzureADDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = AzureADDefaults.AuthenticationScheme;
                options.DefaultSignOutScheme = AzureADDefaults.AuthenticationScheme;
                */
            })
            .AddAzureAD(options => _configuration.Bind("AzureAd", options))
            .AddCookie(o => 
            {
                o.LoginPath = "/Authentication/Login";
                o.LogoutPath = "/Authentication/Logout";
                o.AccessDeniedPath = "/Authenticated/AccessDenied";
            });

            services.Configure<OpenIdConnectOptions>(AzureADDefaults.OpenIdScheme, options =>
            {
                options.Authority = options.Authority + "/v2.0/";
                options.TokenValidationParameters.ValidateIssuer = false;
            });

            services.AddMvc()
                .AddSessionStateTempDataProvider();
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
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/home/error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseSwaggerUi3WithApiExplorer();
            app.CacheImageFiles(new ImageCacheOptions()
            {
                CacheTo = "cache",
                ExpireAfter = TimeSpan.FromMinutes(30)
            });
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseSession();
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
