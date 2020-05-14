using BlazorApp.Client.Interfaces;
using BlazorApp.Client.Services;
using BlazorApp.Shared.Entities;
using BlazorApp.Shared.Requests.Movies;
using BlazorApp.Shared.Validation.Movies;
using Core.Shared.Requests;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BlazorApp.Client.Pages.Movies
{
    public partial class SaveMovie
    {
        [Parameter] public int? Id { get; set; }
        [Inject] public IMoviesService MoviesService { get; set; }
        [Inject] public NavigationManager Nvm { get; set; }

        private SaveMovieRequest _saveMovieRequest { get; set; } = new SaveMovieRequest();
        private SaveMovieRequestValidator _saveMovieRequestValidator { get; set; } = new SaveMovieRequestValidator();
        private Movie _movie;

        public async Task CreateOrUpdateMovie()
        {
            var response = await MoviesService.Save(_saveMovieRequest);
            if (!_saveMovieRequest.Id.HasValue && response.Ok)
            {
                Nvm.NavigateTo("/");
            }
        }

        protected async override Task OnInitializedAsync()
        {
            if (Id.HasValue)
            {
                var response = await MoviesService.Get(new IdRequest(Id.Value));
                if (response.Ok)
                {
                    _movie = response.Payload;
                    _saveMovieRequest = new SaveMovieRequest
                    {
                        Id = _movie.Id,
                        Image = _movie.Image,
                        ReleaseDate = _movie.ReleaseDate,
                        Title = _movie.Title
                    };
                }
            } 
        }
    }
}
