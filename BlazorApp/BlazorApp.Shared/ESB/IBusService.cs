using System.Threading.Tasks;

namespace BlazorApp.Shared.ESB
{
    public interface IBusService
    {
        Task Publish<T>(T message) where T : IMessage;
    }
}