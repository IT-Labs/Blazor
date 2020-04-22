using BlazorApp.Client.Models;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace BlazorApp.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddBaseAddressHttpClient();
            builder.Services.AddMsalAuthentication(options =>
            {
                var authentication = options.ProviderOptions.Authentication;
                authentication.Authority = "https://login.microsoftonline.com/common";
                authentication.ClientId = "13705342-1d5a-4558-89ae-5b8025b29d34";
                authentication.RedirectUri = "https://localhost:44375/signin-oidc";
            });

            ConfigureServices(builder.Services);

            await builder.Build().RunAsync();
        }
        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<HttpService>();
        }
    }
}
