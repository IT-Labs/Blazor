using Core.Shared.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorApp.Shared.Requests.Movies
{
    public class SaveMovieRequest : SaveRequest
    {
        public string Title { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string Image { get; set; }
    }
}
