using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorApp.Client.Models
{
    public class HttpService
    {
        public HttpClient HttpClient;

        public HttpService(HttpClient httpClient)
        {
            HttpClient = httpClient;
            HttpClient.BaseAddress = new Uri("https://localhost:44358/");
        }

        public async Task<T> Get<T>(string route)
        {
            return await HttpClient.GetJsonAsync<T>($"api{route}");
        }
    }
}
