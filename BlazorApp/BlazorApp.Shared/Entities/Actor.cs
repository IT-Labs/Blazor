using Core.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorApp.Shared.Entities
{
    public class Actor : DeletableEntity
    {
        public string FullName { get; set; }
        public string Image { get; set; }
        public string Bio { get; set; }
        [JsonIgnore]
        public List<ActorMovie> ActorMovies { get; set; } = new List<ActorMovie>();
    }
}
