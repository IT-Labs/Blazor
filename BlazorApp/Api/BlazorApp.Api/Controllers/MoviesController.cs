using BlazorApp.Contracts.Managers;
using BlazorApp.Shared.Entities;
using BlazorApp.Shared.Requests.Movies;
using Core.Framework;
using Core.Shared.Requests;
using Core.Shared.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        }

        [HttpGet]
        public PagedResponse<Movie> GetMultiple([FromQuery] GetMoviesRequest request)
        {
            return _movieManager.GetMultiple(request);
        }

        [HttpPost]
        public Response<long> Save([FromBody] SaveMovieRequest request)
        {
            return _movieManager.Save(request);
        }

        [HttpGet("{id}")]
        public Response<Movie> Get([FromRoute] IdRequest request)
        {
            return _movieManager.Get(request);
        }

        [HttpPut("{id}")]
        public Response<long> Update([FromRoute] IdRequest idRequest, [FromBody]SaveMovieRequest request)
        {
            request.Id = idRequest.Id;
            return _movieManager.Save(request);
        }
    }
}