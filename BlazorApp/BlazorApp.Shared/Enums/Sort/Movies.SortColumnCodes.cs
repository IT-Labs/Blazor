using BlazorApp.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BlazorApp.Shared.Enums.Sort
{
    public partial class SortColumnCodes
    {
        public enum Movies
        {
            CreatedAt,
            Title,
            ReleaseDate
        }

        public static Dictionary<Movies, Expression<Func<Movie, object>>> MoviesSort = new Dictionary<Movies, Expression<Func<Movie, object>>>()
        {
            {Movies.CreatedAt, x => x.CreatedAt},
            {Movies.Title, x => x.Title},
            {Movies.ReleaseDate, x => x.ReleaseDate}
        };
    }
}
