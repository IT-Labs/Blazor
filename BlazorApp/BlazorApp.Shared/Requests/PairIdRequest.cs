using System;

namespace BlazorApp.Shared.Requests
{
    public class PairIdRequest : IdRequest
    {
        public PairIdRequest() { }
        public PairIdRequest(long id, long pairId) : base(id)
        {
            PairId = pairId;
        }
        public long PairId { get; set; }
    }
}