using BlazorApp.Client.Services;
using BlazorApp.Shared.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Collections.Generic;
using System.Linq;
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
				var response = await HttpService.GetMultiple<WeatherForecast>("/weatherforecast");
				_forecasts = response.Payload.ToList();
			}
		}
	}
}
