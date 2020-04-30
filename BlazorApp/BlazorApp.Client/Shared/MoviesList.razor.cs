using BlazorApp.Shared;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace BlazorApp.Client.Shared
{
    public partial class MoviesList
    {   [Parameter] public bool ShowGrid { get; set; }
        [Parameter] public List<Movie> Movies { get; set; }
    }
}
