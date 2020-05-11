using Core.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BlazorApp.Shared.Entities
{
    public class ActorMovie : DeletableEntity
    {
        public long MovieId { get; set; }
        public long ActorId { get; set; }

        [JsonIgnore]
        public Movie Movie { get; set; }
        public Actor Actor { get; set; }
    }
}
