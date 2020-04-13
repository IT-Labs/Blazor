using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.Authorization;
using BlazorApp.Client.Util;
using BlazorApp.Client.Helpers;

namespace BlazorApp.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            // Use our CustomAuthenticationProvider as the 

            // AuthenticationStateProvider
            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationProvider>();

            // Add Authentication support
            builder.Services.AddOptions();
            builder.Services.AddAuthorizationCore();

            builder.RootComponents.Add<App>("app");

            builder.Services.AddBaseAddressHttpClient();


            ConfigureServices(builder.Services);

            await builder.Build().RunAsync();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IRepository, RepositoryInMemory>();
        }
    }
}
