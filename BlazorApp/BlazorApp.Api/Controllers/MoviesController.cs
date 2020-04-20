using BlazorApp.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace BlazorApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : Controller
    {
        [HttpGet]
        public List<Movie> GetMultiple()
        {
			return new List<Movie>()
			{
				new Movie()
				{
					Title = "Spider-Man: Far From Home",
					ReleaseDate = new DateTime(2019, 7, 2),
					Image = "https://m.media-amazon.com/images/M/MV5BMGZlNTY1ZWUtYTMzNC00ZjUyLWE0MjQtMTMxN2E3ODYxMWVmXkEyXkFqcGdeQXVyMDM2NDM2MQ@@._V1_SY1000_CR0,0,674,1000_AL_.jpg"
				},
				new Movie()
				{
					Title = "Moana",
					ReleaseDate = new DateTime(2016, 11, 23),
					Image = "https://m.media-amazon.com/images/M/MV5BMjI4MzU5NTExNF5BMl5BanBnXkFtZTgwNzY1MTEwMDI@._V1_SY1000_CR0,0,674,1000_AL_.jpg"
				},
				new Movie()
				{
					Title = "Inception",
					ReleaseDate = new DateTime(2010, 7, 16),
					Image = "https://m.media-amazon.com/images/M/MV5BMjAxMzY3NjcxNF5BMl5BanBnXkFtZTcwNTI5OTM0Mw@@._V1_SY1000_CR0,0,675,1000_AL_.jpg"
				}
			};
		}
    }
}