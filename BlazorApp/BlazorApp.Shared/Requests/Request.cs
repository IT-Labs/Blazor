using BlazorApp.Shared.Interfaces;

namespace BlazorApp.Shared.Requests
{
    public class Request<T> : IRequest
    {
        public T Payload { get; set; }
    }
}
