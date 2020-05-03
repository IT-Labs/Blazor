using Microsoft.Extensions.Logging;
using System.Net.Http;

namespace Core.Shared.Interfaces
{
    public interface IHaveServiceUrlsClient
    {
        HttpClient HttpClient { get; }
        ILogger Logger { get; }
        IUserContextInfo ContextInfo { get; }
    }
}
