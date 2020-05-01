using BlazorApp.Shared.Entities;
using BlazorApp.Shared.Enums.Sort;
using BlazorApp.Shared.Managers;
using BlazorApp.Shared.Requests.Movie;

namespace BlazorApp.Contracts.Managers
{
    public interface IMovieManager : IManager<Movie, GetMoviesRequest, SortColumnCodes.Movies>
    {
        
    }
}
