using Microsoft.Extensions.Configuration;

namespace Core.Shared.Interfaces
{
    public interface IStartupConfiguration
    {
        string CorsPolicy { get; }
        IConfigurationRoot Configuration { get; }

        //EncryptionSettings EncryptionSettings { get; set; }
    }
}
