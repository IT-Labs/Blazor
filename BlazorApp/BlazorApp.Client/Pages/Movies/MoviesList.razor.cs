using BlazorApp.Shared.Entities;
using Core.Shared.Response;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Client.Pages.Movies
{
    public partial class MoviesList
    {
        [Parameter] public bool ShowGrid { get; set; }
        [Parameter] public PagedResponse<Movie> MoviesResponse { get; set; }
        [Parameter] public EventCallback<int> OnPageChange { get; set; }
        [Parameter] public EventCallback<string> OnSearch { get; set; }
    }
}
