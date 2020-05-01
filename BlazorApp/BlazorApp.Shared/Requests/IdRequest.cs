using System;
using BlazorApp.Shared.Interfaces;
using Newtonsoft.Json;

namespace BlazorApp.Shared.Requests
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