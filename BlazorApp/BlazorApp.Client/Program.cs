using BlazorApp.Client.Interfaces;
using BlazorApp.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sotsera.Blazor.Toaster.Core.Models;
using System.Reflection;
using System.Threading.Tasks;

namespace BlazorApp.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");
            var configuration = new ConfigurationBuilder()
                .AddJsonStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("BlazorApp.Client.appsettings.json"))
                .Build();
            builder.Services.AddSingleton(configuration);

            builder.Services.AddBaseAddressHttpClient();
            builder.Services.AddMsalAuthentication(options =>
            {
                var authentication = options.ProviderOptions.Authentication;
                var azureConfig = configuration.GetSection("AzureAD");
                authentication.Authority = azureConfig["Authority"].ToString();
                authentication.ClientId = azureConfig["ClientId"].ToString();
                authentication.RedirectUri = azureConfig["RedirectUri"].ToString();
            });

            ConfigureServices(builder.Services);

            await builder.Build().RunAsync();
        }
        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddToaster(config =>
            {
                config.PositionClass = Defaults.Classes.Position.TopRight;
                config.ToastTitleClass = $"{Defaults.Classes.ToastTitle} {Defaults.Classes.TextPosition.Left}";
                config.ToastMessageClass = $"{Defaults.Classes.ToastMessage} {Defaults.Classes.TextPosition.Left}";
                config.PreventDuplicates = false;
                config.NewestOnTop = true;
            });

            services.AddScoped<ToasterService>();
            services.AddScoped<HttpService>();
            //services.AddScoped<MoviesService>();
            services.AddScoped<IMoviesService, MoviesService>();
            services.AddDevExpressBlazor();
        }
    }
}
