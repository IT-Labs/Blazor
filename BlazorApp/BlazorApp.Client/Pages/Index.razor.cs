using BlazorApp.Client.Models;
using BlazorApp.Shared;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorApp.Client.Pages
{
    public partial class Index
    {
        [Inject] public HttpService HttpService { get; set; }

        private List<Movie> Movies;

        protected async override Task OnInitializedAsync()
        {
            Movies = await HttpService.Get<List<Movie>>("/movies");
        }
    }
}