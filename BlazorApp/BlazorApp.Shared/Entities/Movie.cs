using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Shared;

namespace BlazorApp.Shared.Entities
{
    public class Movie : DeletableEntity
    {
        public string Title { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string Image { get; set; }
        public DateTime PremiereDate { get; set; }
        public int LengthMinutes { get; set; } = 120;
        public List<ActorMovie> Actors { get; set; }

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
