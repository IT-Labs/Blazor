using BlazorApp.Shared;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Client.Shared
{
    public partial class SingleMovie
    {
        [Parameter] public Movie Movie { get; set; }
        [Parameter] public bool ShowGrid { get; set; }
    }
}