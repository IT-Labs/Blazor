using BlazorApp.Client.Models;
using BlazorApp.Shared;
using BlazorApp.Shared.DTOs.Requests;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorApp.Client.Pages.Movies
{
    public partial class Index
    {
        [Inject] public HttpService HttpService { get; set; }

        private List<Movie> _movies;
        protected async override Task OnInitializedAsync()
        {
            _movies = await HttpService.Get<List<Movie>>("/movies");
        }

    }
}