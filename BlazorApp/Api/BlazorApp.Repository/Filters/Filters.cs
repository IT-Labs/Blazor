using BlazorApp.Repository.Filters.Movies;
using BlazorApp.Shared.Entities;
using BlazorApp.Shared.Requests.Movies;
using Core.Framework.Repository.Filters;
using Core.Shared.Repository;
using Core.Shared.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorApp.Repository.Filters
{
    public static partial class Filters
    {
        public static List<IQueryFilter<Movie>> Movies(ContextRequest<GetMoviesRequest> contextRequest)
        {
            var request = contextRequest.Request;
            return new List<IQueryFilter<Movie>>()
            {
                new MoviesFilter(request.Title),
                new IsActiveFilter<Movie>(true)
            };
        }
    }
}
