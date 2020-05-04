using BlazorApp.Client.Services;
using BlazorApp.Shared.Entities;
using BlazorApp.Shared.Enums.Sort;
using BlazorApp.Shared.Requests.Movies;
using Core.Shared.Enums;
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
            _request = new GetMoviesRequest { PageSize = 8 };
            _moviesResponse = await MoviesService.GetMultiple(_request);
        }

        void HandleToogle(ChangeEventArgs e)
        {
            _showGrid = (bool)e.Value;
        }

        public async Task OnPageChange(int page)
        {
            _moviesResponse = null;
            _request.CurrentPage = page;
            _moviesResponse = await MoviesService.GetMultiple(_request);
        }

        public async Task OnSearch(GetMoviesRequest request)
        {
            _moviesResponse = null;
            _request.SortOrder = request.SortOrder;
            _request.OrderColumnName = request.OrderColumnName;
            _request.Title = request.Title;
            _moviesResponse = await MoviesService.GetMultiple(_request);
        }
    }
}
