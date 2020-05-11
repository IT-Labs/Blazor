using Core.Shared;
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
        public List<ActorMovie> Movies { get; set; }
    }
}
