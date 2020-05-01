using BlazorApp.Shared.ConfigurationValues;
using Microsoft.Extensions.Configuration;

namespace BlazorApp.Shared.Interfaces
{
    public interface IStartupConfiguration
    {
        string CorsPolicy { get; }
        IConfigurationRoot Configuration { get; }

        //EncryptionSettings EncryptionSettings { get; set; }
    }
}
