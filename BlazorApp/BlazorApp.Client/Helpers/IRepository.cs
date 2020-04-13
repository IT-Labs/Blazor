using BlazorApp.Shared;
using System.Collections.Generic;

namespace BlazorApp.Client.Helpers
{
	public interface IRepository
	{
		List<Movie> GetMovies();
	}
}