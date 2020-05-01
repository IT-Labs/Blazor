using Microsoft.Extensions.Logging;
using System.Net.Http;

namespace BlazorApp.Shared.Interfaces
{
    public interface IHaveServiceUrlsClient
    {
        HttpClient HttpClient { get; }
        ILogger Logger { get; }
        IUserContextInfo ContextInfo { get; }
    }
}
