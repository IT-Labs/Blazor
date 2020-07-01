using BlazorApp.Shared.Entities;
using BlazorApp.Shared.Requests.Movies;
using Core.Shared.Response;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace BlazorApp.Client.Pages.Movies
{
    public partial class MoviesList
    {
        [Parameter] public bool ShowGrid { get; set; }
        [Parameter] public PagedResponse<Movie> MoviesResponse { get; set; }
        [Parameter] public EventCallback<int> OnPageChange { get; set; }
        [Parameter] public EventCallback<GetMoviesRequest> OnSearch { get; set; }

        [Parameter] public EventCallback OnMovieDeleted { get; set; }

        protected async Task MovieDeleted()
        {
            await OnMovieDeleted.InvokeAsync(0);
        }
    }
}
