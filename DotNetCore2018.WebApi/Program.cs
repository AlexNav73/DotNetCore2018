using System.Net;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace DotNetCore2018.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel(o => 
                {
                    o.Listen(IPAddress.Loopback, 5000);
                    o.Listen(IPAddress.Loopback, 5001, lo =>
                    {
                        lo.UseHttps("localhost.pfx", "1234");
                    });
                })
                .UseStartup<Startup>()
                .ConfigureLogging(logging => 
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(LogLevel.Trace);
                })
                .UseNLog()
                .Build();
    }
}
