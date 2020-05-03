using BlazorApp.Shared.Entities;
using BlazorApp.Shared.Enums.Sort;
using BlazorApp.Shared.Requests.Movies;
using Core.Shared.Managers;

namespace BlazorApp.Contracts.Managers
{
    public interface IMovieManager : IManager<Movie, GetMoviesRequest, SortColumnCodes.Movies>
    {
        
    }
}
