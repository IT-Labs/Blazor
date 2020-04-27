using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorApp.Shared.DTOs.Requests
{
    public class SaveMovieRequest
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string Image { get; set; }
    }
}
