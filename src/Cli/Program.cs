using Core.Example;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace Cli
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
                Name = "Web App",
                FullName = "The ASP.NET Core Web application.",
                Description = "The ASP.NET Core Web application.",
            };
            app.HelpOption("-h|--help");

            // Cli
            app.Command("cli", x => { })
            .OnExecute(() =>
            {
                var exampleService = host.Services.GetRequiredService<IExampleService>();
                Console.WriteLine(exampleService.GetCurrentTime());
                return 0;
            });

            // Execute
            app.OnExecute(() =>
            {
                app.ShowHelp();
                return 0;
            });

            app.Execute(args);
        }
    }
}
