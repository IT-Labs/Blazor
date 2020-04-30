using BlazorApp.Shared;
using BlazorApp.Shared.DTOs.Requests;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading;

namespace BlazorApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : Controller
    {
        [HttpGet]
        public List<Movie> GetMultiple()
        {
            Thread.Sleep(2000);
            return new List<Movie>()
            {
                 new Movie()
                {
                    Title = "Captain Marvel",
                    ReleaseDate = new DateTime(2019, 7, 2),
                    Image = "https://m.media-amazon.com/images/M/MV5BMTE0YWFmOTMtYTU2ZS00ZTIxLWE3OTEtYTNiYzBkZjViZThiXkEyXkFqcGdeQXVyODMzMzQ4OTI@._V1_SY1000_CR0,0,675,1000_AL_.jpg"
                },
                  new Movie()
                {
                    Title = "Avengers: Age of Ultron",
                    ReleaseDate = new DateTime(2019, 7, 2),
                    Image = "https://m.media-amazon.com/images/M/MV5BMTM4OGJmNWMtOTM4Ni00NTE3LTg3MDItZmQxYjc4N2JhNmUxXkEyXkFqcGdeQXVyNTgzMDMzMTg@._V1_SY1000_SX675_AL_.jpg"
                },
                   new Movie()
                {
                    Title = "Black Panther",
                    ReleaseDate = new DateTime(2019, 7, 2),
                    Image = "https://m.media-amazon.com/images/M/MV5BMTg1MTY2MjYzNV5BMl5BanBnXkFtZTgwMTc4NTMwNDI@._V1_SY1000_CR0,0,674,1000_AL_.jpg"
                },
                    new Movie()
                {
                    Title = "Thor: Ragnarok",
                    ReleaseDate = new DateTime(2019, 7, 2),
                    Image = "https://m.media-amazon.com/images/M/MV5BMjMyNDkzMzI1OF5BMl5BanBnXkFtZTgwODcxODg5MjI@._V1_SY1000_CR0,0,674,1000_AL_.jpg"
                },
                     new Movie()
                {
                    Title = "Guardians of the Galaxy",
                    ReleaseDate = new DateTime(2019, 7, 2),
                    Image = "https://m.media-amazon.com/images/M/MV5BMTAwMjU5OTgxNjZeQTJeQWpwZ15BbWU4MDUxNDYxODEx._V1_SY1000_CR0,0,674,1000_AL_.jpg"
                },
                new Movie()
                {
                    Title = "Wonder Woman 1984",
                    ReleaseDate = new DateTime(2019, 7, 2),
                    Image = "https://m.media-amazon.com/images/M/MV5BYzAyOGJkMzUtMmRjYS00NGJmLWExNGEtYzI2YjVmMmQzMzFiXkEyXkFqcGdeQXVyMTkxNjUyNQ@@._V1_SY1000_SX675_AL_.jpg"
                },
                    new Movie()
                {
                    Title = "Wonder Woman",
                    ReleaseDate = new DateTime(2019, 7, 2),
                    Image = "https://m.media-amazon.com/images/M/MV5BNDFmZjgyMTEtYTk5MC00NmY0LWJhZjktOWY2MzI5YjkzODNlXkEyXkFqcGdeQXVyMDA4NzMyOA@@._V1_SY1000_SX675_AL_.jpg"
                },
                        new Movie()
                {
                    Title = "Black Widow",
                    ReleaseDate = new DateTime(2019, 7, 2),
                    Image = "https://m.media-amazon.com/images/M/MV5BMzFiODE0ZDUtN2IxNC00OTI5LTg4OWItZTE2MjU4ZTk2NjM5XkEyXkFqcGdeQXVyNDYzODU1ODM@._V1_SY1000_CR0,0,674,1000_AL_.jpg"
                },
                            new Movie()
                {
                    Title = "Eternals",
                    ReleaseDate = new DateTime(2019, 7, 2),
                    Image = "https://m.media-amazon.com/images/M/MV5BOTYzMTlhM2ItMmFkYi00ZTJhLTg3MWQtNzM1NTRkM2NiNzRjXkEyXkFqcGdeQXVyNTE1NjY5Mg@@._V1_SY1000_CR0,0,737,1000_AL_.jpg"
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

        [HttpPost]
        public int Save([FromBody]SaveMovieRequest request)
        {
            //TODO: add save logic here
            return 0;
        }

        [HttpGet("{id}")]
        public Movie GetById(int id)
        {
            return new Movie()
            {
                Id = id,
                Title = "Spider-Man: Far From Home",
                ReleaseDate = new DateTime(2019, 7, 2),
                Image = "https://m.media-amazon.com/images/M/MV5BMGZlNTY1ZWUtYTMzNC00ZjUyLWE0MjQtMTMxN2E3ODYxMWVmXkEyXkFqcGdeQXVyMDM2NDM2MQ@@._V1_SY1000_CR0,0,674,1000_AL_.jpg"
            };
        }

        [HttpPut("{id}")]
        public int Update(int id, [FromBody]SaveMovieRequest request) => GetById(id).Id;
    }
}