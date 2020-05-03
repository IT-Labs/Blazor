using BlazorApp.Client.Extensions;
using BlazorApp.Shared.Entities;
using BlazorApp.Shared.Requests.Movies;
using Core.Shared.Requests;
using Core.Shared.Response;
using System.Threading.Tasks;

namespace BlazorApp.Client.Services
{
    public class MoviesService
    {
        private readonly HttpService _http;
        private readonly string _route = "/movies"; 

        public MoviesService(HttpService http)
        {
            _http = http;
        }

        public async Task<PagedResponse<Movie>> GetMultiple(GetMoviesRequest request)
        {
            var response = await _http.GetMultiple<Movie>(_route, request.ToQueryString());
            return response;
        }

        public async Task<Response<Movie>> Get(IdRequest request)
        {
            var response = await _http.Get<Movie>($"{_route}/{request.Id}");
            return response;
        }

        public async Task<Response<long>> Save(SaveMovieRequest request)
        {
            return request.Id.HasValue 
                ? await _http.Put<long>($"{_route}/{request.Id}", request)
                : await _http.Post<long>(_route, request);
        }
    }
}
