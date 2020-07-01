using BlazorApp.Shared.Entities;
using BlazorApp.Shared.Requests.Movies;
using Core.Shared.Requests;
using Core.Shared.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Client.Interfaces
{
	public interface IMoviesService
	{
		Task<PagedResponse<Movie>> GetMultiple(GetMoviesRequest request);

		Task<Response<Movie>> Get(IdRequest request);

		Task<Response<long>> Save(SaveMovieRequest request);
		Task<Response<bool>> Delete(IdRequest request);
	}
}
