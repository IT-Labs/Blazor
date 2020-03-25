using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.Authorization;
using BlazorApp.Client.Util;

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

            await builder.Build().RunAsync();
        }
    }
}
