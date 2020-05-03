using BlazorApp.Contracts.Managers;
using BlazorApp.Shared.Entities;
using BlazorApp.Shared.Enums.Sort;
using BlazorApp.Shared.Requests.Movies;
using Core.Framework.Domain;
using Core.Framework.Repository;
using Microsoft.Extensions.Logging;

namespace BlazorApp.Domain
{
    public class MovieManager : CoreManager<Movie, GetMoviesRequest, SortColumnCodes.Movies>, IMovieManager
    {
        public MovieManager(DomainRepository repository, ILogger<MovieManager> logger) 
            : base(repository, null, SortColumnCodes.MoviesSort, logger)
        {

        }
    }
}
