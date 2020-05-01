using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorApp.Shared.Entities
{
    public class Movie : DeletableEntity
    {
        public string Title { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string Image { get; set; }

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
