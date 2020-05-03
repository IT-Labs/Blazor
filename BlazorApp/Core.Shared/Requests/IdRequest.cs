using System;
using Core.Shared.Interfaces;
using Newtonsoft.Json;

namespace Core.Shared.Requests
{
    public class IdRequest : IRequest
    {
        public IdRequest() { }
        public IdRequest(long id)
        {
            Id = id;
        }

        [JsonProperty("id")]
        public long Id { get; set; }   
    }
}