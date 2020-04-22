using BlazorApp.Client.Models;
using BlazorApp.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorApp.Client.Pages
{
	public partial class FetchData
    {
		[Inject] public HttpService HttpService { get; set; }

		[CascadingParameter]
		private Task<AuthenticationState> _authenticationStateTask { get; set; }
		private List<WeatherForecast> _forecasts { get; set; }

		protected override async Task OnInitializedAsync()
		{
			var authState = await _authenticationStateTask;

			if (authState.User.Identity != null && authState.User.Identity.IsAuthenticated)
			{
				_forecasts = await HttpService.Get<List<WeatherForecast>>("/weatherforecast");
			}
		}
	}
}
