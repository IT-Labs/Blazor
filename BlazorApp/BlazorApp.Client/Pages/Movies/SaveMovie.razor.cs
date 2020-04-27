using BlazorApp.Client.Models;
using BlazorApp.Shared;
using BlazorApp.Shared.DTOs.Requests;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Client.Pages.Movies
{
    public partial class SaveMovie
    {
        [Parameter]
        public int? Id { get; set; }
        [Inject]
        public HttpService HttpService { get; set; }
        SaveMovieRequest SaveMovieRequest { get; set; } = new SaveMovieRequest();

        public void CreateMovie()
        {
            //pass data to api
            Console.WriteLine($"{SaveMovieRequest.Title}");
        }

        protected async override Task OnInitializedAsync()
        {
            if(Id.HasValue)
                SaveMovieRequest = await HttpService.Get<SaveMovieRequest>($"/movies/{Id}");
        }
    }
}
