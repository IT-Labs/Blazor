using Core.Shared.Response;
using Core.Framework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using BlazorApp.Shared.Entities;

namespace BlazorApp.Api.Controllers
{
    [ApiController]
    public class WeatherForecastController : BaseApiController
    {
        public WeatherForecastController(IHttpContextAccessor httpContextAccessor)
            : base(httpContextAccessor)
        {

        }

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet]
        public PagedResponse<WeatherForecast> Get()
        {
            var rng = new Random();
            var list = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
            var response = new PagedResponse<WeatherForecast>
            {
                Payload = list
            };
            return response;
        }
    }
}
