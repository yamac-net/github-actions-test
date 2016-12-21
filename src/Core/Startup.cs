using Core.Example;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core
{
    public abstract class Startup
    {
        public IConfigurationRoot Configuration { get; protected set;  }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSingleton<IExampleService, ExampleService>();
        }
    }
}
