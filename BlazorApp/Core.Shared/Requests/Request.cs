using Core.Shared.Interfaces;

namespace Core.Shared.Requests
{
    public class Request<T> : IRequest
    {
        public T Payload { get; set; }
    }
}
