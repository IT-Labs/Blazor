using BlazorApp.Shared.Entities;
using Core.Shared.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlazorApp.Repository.Filters.Movies
{
    public class MoviesFilter : IQueryFilter<Movie>
    {
        private readonly string _title;

        public MoviesFilter(string title)
        {
            _title = title;
        }

        public IQueryable<Movie> Filter(IQueryable<Movie> query)
        {
            if (!string.IsNullOrWhiteSpace(_title))
                query = query.Where(x => EF.Functions.Like(x.Title, $"%{_title}%"));

            return query;
        }
    }
}
