using BlazorApp.Shared.Entities;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Client.Pages.Movies
{
    public partial class SingleMovie
    {
        [Parameter] public Movie Movie { get; set; }
        [Parameter] public bool ShowGrid { get; set; }
    }
}
