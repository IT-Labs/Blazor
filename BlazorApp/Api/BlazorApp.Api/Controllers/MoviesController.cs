using BlazorApp.Contracts.Managers;
using BlazorApp.Shared.DTOs.Requests;
using BlazorApp.Shared.Entities;
using BlazorApp.Shared.Requests;
using BlazorApp.Shared.Requests.Movie;
using BlazorApp.Shared.Response;
using Core.Framework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
        }

        [HttpGet]
        public List<Movie> GetMultiple([FromQuery] GetMoviesRequest request)
        {
            return _movieManager.GetMultiple(request).Payload.ToList();
        }

        [HttpPost]
        public int Save([FromBody] SaveMovieRequest request)
        {
            //TODO: add save logic here
            return 0;
        }

        [HttpGet("{id}")]
        public Movie Get([FromRoute] IdRequest request)
        {
            return _movieManager.Get(request).Payload;
        }

        [HttpPut("{id}")]
        public long Update([FromRoute] IdRequest idRequest, [FromBody]SaveMovieRequest request) => Get(idRequest).Id;
    }
}