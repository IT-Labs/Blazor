using BlazorApp.Client.Services;
using BlazorApp.Shared.Entities;
using BlazorApp.Shared.Requests.Movies;
using Core.Shared.Response;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BlazorApp.Client.Pages.Movies
{
    public partial class Movies
    {
        [Inject] public MoviesService MoviesService { get; set; }

        private bool _showGrid { get; set; }
        private PagedResponse<Movie> _moviesResponse;
        private GetMoviesRequest _request;

        protected async override Task OnInitializedAsync()
        {
            _showGrid = false;
            _request = new GetMoviesRequest { All = true };
            _moviesResponse = await MoviesService.GetMultiple(_request);
        }

        void HandleToogle(ChangeEventArgs e)
        {
            _showGrid = (bool)e.Value;
        }
    }
}
