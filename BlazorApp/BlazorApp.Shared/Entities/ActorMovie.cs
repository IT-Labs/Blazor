using Core.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorApp.Shared.Entities
{
    public class ActorMovie : DeletableEntity
    {
        public long MovieId { get; set; }
        public long ActorId { get; set; }
        public Movie Movie { get; set; }
        public Actor Actor { get; set; }
    }
}
