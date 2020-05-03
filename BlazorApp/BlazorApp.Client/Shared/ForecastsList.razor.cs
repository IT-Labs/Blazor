using BlazorApp.Shared.Entities;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace BlazorApp.Client.Shared
{
    public partial class ForecastsList
    {
        [Parameter] public List<WeatherForecast> Forecasts { get; set; }
    }
}
