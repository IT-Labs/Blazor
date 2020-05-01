using BlazorApp.DataContext;
using BlazorApp.Repository;
using BlazorApp.Shared.Repository;
using Core.Framework;
using Core.Framework.Repository;
using Lamar;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorApp.Api
{
    public class Startup : StartupConfiguration
    {
        private IWebHostEnvironment _environment { get; set; }
        public Startup(IWebHostEnvironment environment) : base("BlazorApp.Api", environment)
        {
            _environment = environment;
        }

        public override ServiceRegistry ConfigureAdditionalServices(ServiceRegistry services)
        {
            Configuration.ConfigureDatabase<BlazorApiContext>(services);
            services.AddTransient<DomainRepository, BlazorApiRepository>();
            services.AddTransient<IContext, BlazorApiContext>();

            return services;
        }
    }
}
