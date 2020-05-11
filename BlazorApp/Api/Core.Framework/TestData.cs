using BlazorApp.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using static Core.Shared.AuditableEntity;

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

            var random = new Random();

            movies = movies.Select((movie, index) =>
            {
                movie.PremiereDate = new DateTime(2020, 5, index + 1, 18 - random.Next(1, 6), 00, 00);
                movie.UpdateAuditableProperties(AuditableAction.Insert);
                return movie;
            }).ToList();

            return movies;
        }

        public static List<Actor> ActorsTestData()
        {
            var actors = new List<Actor>()
            {
                new Actor()
                {
                    FullName = "Gal Gadot",
                    Image = "https://hips.hearstapps.com/hmg-prod.s3.amazonaws.com/images/gal-gadot-attends-the-2020-vanity-fair-oscar-party-hosted-news-photo-1584616055.jpg",
                    Bio = "Israeli actress, model, and producer",
                },
                new Actor()
                {
                    FullName = "Jennifer Lawrence",
                    Image = "https://encrypted-tbn0.gstatic.com/images?q=tbn%3AANd9GcTwwFEDWYYt9CQn7v6xzz2OLaArl8mP0vQqHg6tg6NK9nXCKC6_&usqp=CAU",
                    Bio = "Jennifer Shrader Lawrence is an American actress. The films she has acted in have grossed over $6 billion worldwide, and she was the highest-paid actress in the world in 2015 and 2016",
                },
                new Actor()
                {
                    FullName = "Scarlett Johansson",
                    Image= "https://cdn.britannica.com/59/182359-050-C6F38CA3/Scarlett-Johansson-Natasha-Romanoff-Avengers-Age-of.jpg",
                    Bio = "Scarlett Ingrid Johansson is an American actress and singer. The world's highest-paid actress since 2018, she has made multiple appearances in the Forbes Celebrity 100. Her films have grossed over $14.3 billion worldwide, making Johansson the ninth-highest-grossing box office star of all time."
                },
                new Actor()
                {
                    FullName = "Jessica Alba",
                    Image= "https://www3.pictures.stylebistro.com/gi/21st+Annual+Warner+Bros+InStyle+Golden+Globe+9n-rIDLHcELl.jpg",
                    Bio = "Jessica Marie Alba is an American actress and businesswoman. She began her television and movie appearances at age 13 in Camp Nowhere and The Secret World of Alex Mack, but rose to prominence at 19, as the lead actress of the television series Dark Angel, for which she received a Golden Globe nomination."
                },

                new Actor()
                {
                    FullName = "Megan Fox",
                    Image = "https://www.syfy.com/sites/syfy/files/styles/1200x680_hero/public/2018/10/jennifers-body.png",
                    Bio = "Megan Denise Fox is an American actress and model. She began her acting career in 2001, with several minor television and film roles, and played a regular role on the Hope & Faith television sitcom. In 2004, she made her film debut with a role in the teen comedy Confessions of a Teenage Drama Queen"
                },
                new Actor()
                {
                    FullName = "Brie Larson",
                    Image = "https://i.pinimg.com/474x/1b/b9/81/1bb9811a65302282673886912268553b.jpg",
                    Bio = "Brianne Sidonie Desaulniers, known professionally as Brie Larson, is an American actress and filmmaker. Noted for her supporting work in comedies when a teenager, she has since expanded to leading roles in independent dramas and film franchises, receiving such accolades as an Academy Award and a Golden Globe."
                },
                new Actor()
                {
                    FullName = "Cameron Diaz",
                    Image = "https://i.redd.it/ajbwgi1rpq331.jpg",
                    Bio = "Cameron Michelle Diaz is a retired American actress, author, producer and model. She has frequently appeared in comedies throughout her career, while also earning critical recognition in dramatic films"
                }
            };

            actors.ForEach(x => x.UpdateAuditableProperties());
            return actors;
        }

        public static List<Movie> ActorMoviesTestData(List<Movie> movies, List<Actor> actors)
        {
            var random = new Random();
            var ids = actors.Select(x => x.Id).ToList();
            foreach (var movie in movies)
            {
                var set = new HashSet<long>();
                for (int i = 0; i < 3;)
                {
                    var next = ids[random.Next(0, ids.Count())];
                    if (set.Add(next))
                    {
                        var actor = actors.FirstOrDefault(x => x.Id == next);
                        if (actor != null)
                        {
                            i++;
                            movie.ActorMovies.Add(new ActorMovie
                            {
                                Actor = actor
                            });
                        }
                    }
                }
            }
            return movies;
        }
    }
}
