using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AAPZ_Backend.Models;
using Microsoft.Extensions.Logging;
using System.Net;

namespace AAPZ_Backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);
            using (var scope = host.Services.CreateScope())
            {

                var services = scope.ServiceProvider;

                try
                {
                    var serviceProvider = services.GetRequiredService<IServiceProvider>();
                    var configuration = services.GetRequiredService<IConfiguration>();
                    Seed.CreateRoles(serviceProvider, configuration).Wait();

                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }

            }
            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
             //.UseKestrel(options =>
             //{
             //    options.Listen(IPAddress.Loopback, 5000);
             //    options.Listen(IPAddress.Loopback, 5001, listenOptions =>
             //    {
             //        listenOptions.UseHttps("localhost.pfx", "1234");
             //    });
             //})
                .UseStartup<Startup>()
                .UseSetting("detailedErrors", "true")
                .CaptureStartupErrors(true)
                .Build();
    }
}
