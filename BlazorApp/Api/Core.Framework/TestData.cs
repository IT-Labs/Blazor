using BlazorApp.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using static BlazorApp.Shared.AuditableEntity;

namespace Core.Framework
{
    public static class TestData
    {
        public static List<Movie> MoviesTestData()
        {
            var movies =  new List<Movie>()
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
            movies.ForEach(movie => movie.UpdateAuditableProperties(AuditableAction.Insert));
            return movies;
        }
    }
}
