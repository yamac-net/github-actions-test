using App.Example;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .UseEnvironment(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development")
                .Build();

            CommandLineApplication app = new CommandLineApplication(throwOnUnexpectedArg: false)
            {
                Name = "AspNetMain",
                FullName = "The ASP.NET Core Main application.",
                Description = "The ASP.NET Core Main application.",
            };
            app.HelpOption("-h|--help");

            // Cli
            app.Command("cli", x => { })
            .OnExecute(() =>
            {
                var service = host.Services.GetRequiredService<IExampleService>();
                Console.WriteLine(service.GetCurrentTime());
                return 0;
            });

            // Execute
            app.OnExecute(() =>
            {
                host.Run();
                return 0;
            });

            app.Execute(args);
        }
    }
}
