using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Core.Shared;
using Newtonsoft.Json;

namespace BlazorApp.Shared.Entities
{
    public class Movie : DeletableEntity
    {
        public string Title { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string Image { get; set; }
        public DateTime PremiereDate { get; set; }
        public int LengthMinutes { get; set; } = 120;
        [JsonIgnore]
        public List<ActorMovie> ActorMovies { get; set; } = new List<ActorMovie>();

        //public List<Actor> Actors => ActorMovies.Select(x => x.Actor).ToList();

        [NotMapped]
        public List<Actor> Actors { get; set; }

        [NotMapped]
        public string TitleBrief
        {
            get
            {
                if (string.IsNullOrEmpty(Title))
                {
                    return null;
                }

                if (Title.Length > 60)
                {
                    return Title.Substring(0, 60) + "...";
                }
                else
                {
                    return Title;
                }
            }
        }
    }
}
