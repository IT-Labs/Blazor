using BlazorApp.Contracts.Managers;
using BlazorApp.Repository.QueryInclude;
using BlazorApp.Shared.Entities;
using BlazorApp.Shared.Requests.Movies;
using Core.Framework;
using Core.Framework.Extensions;
using Core.Shared.Requests;
using Core.Shared.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading;

namespace BlazorApp.Api.Controllers
{
    [ApiController]
    public class MoviesController : BaseApiController
    {
        private readonly IMovieManager _movieManager;

        public MoviesController(IHttpContextAccessor httpContextAccessor, IMovieManager movieManager)
            : base(httpContextAccessor)
        {
            _movieManager = movieManager;
            _movieManager.SetContextInfo(GetUserContext(HttpContextAccessor));
        }

        [HttpGet]
        public PagedResponse<Movie> GetMultiple([FromQuery] GetMoviesRequest request)
        {
            var movies = _movieManager.GetMultiple(request).BindPayload(p => p.Select(x =>
            {
                x.Actors = x.ActorMovies.Select(y => y.Actor).ToList();
                return x;
            }));
            return movies;
        }

        [HttpPost]
        public Response<long> Save([FromBody] SaveMovieRequest request)
        {
            return _movieManager.Save(request);
        }

        [HttpGet("{id}")]
        public Response<Movie> Get([FromRoute] IdRequest request)
        {
            return _movieManager.Get(request, new MovieInclude()).BindPayload(x =>
            {
                x.Actors = x.ActorMovies.Select(y => y.Actor).ToList();
                return x;
            });
        }

        [HttpPut("{id}")]
        public Response<long> Update([FromRoute] IdRequest idRequest, [FromBody]SaveMovieRequest request)
        {
            request.Id = idRequest.Id;
            return _movieManager.Save(request);
        }
    }
}