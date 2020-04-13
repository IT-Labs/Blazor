using BlazorApp.Client.Helpers;
using BlazorApp.Shared;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorApp.Client.Pages
{
    public partial class Index
    {
        [Inject]
        IRepository repository { get; set; }

        private List<Movie> Movies;
        protected async override Task OnInitializedAsync()
        {
            Movies = repository.GetMovies();
        }
    }
}