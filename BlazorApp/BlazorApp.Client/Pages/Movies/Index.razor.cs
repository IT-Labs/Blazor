using BlazorApp.Client.Models;
using BlazorApp.Shared;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorApp.Client.Pages.Movies
{
    public partial class Index
    {
        [Inject] public HttpService HttpService { get; set; }
        private bool _showGrid { get; set; }
        private List<Movie> _movies;

        protected async override Task OnInitializedAsync()
        {
            _showGrid = false;
            _movies = await HttpService.Get<List<Movie>>("/movies");
        }

        void HandleToogle(ChangeEventArgs e)
        {
            _showGrid = (bool)e.Value;
        }
    }
}