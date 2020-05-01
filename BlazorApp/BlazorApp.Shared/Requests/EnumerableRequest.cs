using System.Collections.Generic;

namespace BlazorApp.Shared.Requests
{
    public class EnumerableRequest<T> : Request<IEnumerable<T>>
    {
        public new IEnumerable<T> Payload { get; set; }
    }
}