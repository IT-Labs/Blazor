using BlazorApp.Shared.Entities;
using Core.Shared.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlazorApp.Repository.QueryInclude
{
    public class MovieInclude : IQueryInclude<Movie>
    {
        public IQueryable<Movie> Include(IQueryable<Movie> query)
        {
            return query.Include(x => x.ActorMovies).ThenInclude(x => x.Actor);
        }
    }
}
