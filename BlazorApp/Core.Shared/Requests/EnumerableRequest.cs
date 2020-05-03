using System.Collections.Generic;

namespace Core.Shared.Requests
{
    public class EnumerableRequest<T> : Request<IEnumerable<T>>
    {
        public new IEnumerable<T> Payload { get; set; }
    }
}