using BlazorApp.Client.Interfaces;
using BlazorApp.Shared.Entities;
using Core.Shared.Requests;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Sotsera.Blazor.Toaster;
using System.Threading.Tasks;

namespace BlazorApp.Client.Pages.Movies
{
    public partial class SingleMovie
    {
        [Inject] protected IToaster Toaster { get; set; }
        [Inject] public IJSRuntime IJSRuntime { get; set; }
        [Inject] public IMoviesService MoviesService { get; set; }
        [Parameter] public Movie Movie { get; set; }
        [Parameter] public bool ShowGrid { get; set; }
        [Parameter] public EventCallback<long> OnMovieDeleted { get; set; }

        public async Task Delete_Movie()
        {
            bool confirmed = await IJSRuntime.InvokeAsync<bool>("confirm", $"Are you shure you want to delete {Movie.Title}?");
            if (confirmed)
            {

                var response = await MoviesService.Delete(new IdRequest(Movie.Id));
                if (response.Ok)
                {
                    Toaster.Success("Movie deleted successfully");
                    await OnMovieDeleted.InvokeAsync(Movie.Id);
                }
                else
                {
                    Toaster.Success("Movie not deleted. Something went wrong");
                }
            }
        }
    }
}
