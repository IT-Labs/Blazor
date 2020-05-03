using Core.Shared.Response;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.Client.Services
{
    public class HttpService
    {
        public HttpClient HttpClient;
        private ToasterService _toasterService;
        private readonly IConfigurationRoot _configuration;

        public HttpService(HttpClient httpClient, ToasterService toasterService, IConfigurationRoot configuration)
        {
            HttpClient = httpClient;
            _configuration = configuration;
            var apiUrl = _configuration.GetSection("Api")["Url"].ToString();
            HttpClient.BaseAddress = new Uri(apiUrl);
            _toasterService = toasterService;
        }

        public async Task<Response<T>> Get<T>(string route, string queryString = null)
        {
            if (!string.IsNullOrWhiteSpace(queryString))
            {
                route += "?" + queryString;
            }
            var response = await HttpClient.GetAsync($"api{route}");
            var content = await response.Content.ReadAsStringAsync();
            var responseBody = JsonConvert.DeserializeObject<Response<T>>(content);

            ShowErrorMessages(responseBody);

            return responseBody;
        }

        public async Task<PagedResponse<T>> GetMultiple<T>(string route, string queryString = null)
        {
            if (!string.IsNullOrWhiteSpace(queryString))
            {
                route += "?" + queryString;
            }

            var response = await HttpClient.GetAsync($"api{route}");
            var content = await response.Content.ReadAsStringAsync();
            var responseBody = JsonConvert.DeserializeObject<PagedResponse<T>>(content);

            ShowErrorMessages(responseBody);

            return responseBody;
        }

        public async Task<Response<T>> Post<T>(string route, object body)
        {
            var bodyContent = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
            var response = await HttpClient.PostAsync($"api{route}", bodyContent);
            var responseContent = await response.Content.ReadAsStringAsync();
            var responseBody = JsonConvert.DeserializeObject<Response<T>>(responseContent);

            ShowMessages(responseBody);

            return responseBody;
        }

        public async Task<Response<T>> Put<T>(string route, object body)
        {
            var bodyContent = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
            var response = await HttpClient.PutAsync($"api{route}", bodyContent);
            var responseContent = await response.Content.ReadAsStringAsync();
            var responseBody = JsonConvert.DeserializeObject<Response<T>>(responseContent);

            ShowMessages(responseBody);

            return responseBody;
        }

        private void ShowMessages<T>(Response<T> response)
        {
            ShowSuccessMessages(response);
            ShowErrorMessages(response);
        }

        private void ShowSuccessMessages<T>(Response<T> response)
        {
            if (response.Ok)
            {
                _toasterService.ShowSucess();
            }
        }

        private void ShowErrorMessages<T>(Response<T> response)
        {
            if (response.NotOk)
            {
                foreach (var meassge in response.Messages.Select(x => x.Message))
                {
                    _toasterService.ShowError(meassge);
                }
            }
        }
    }
}
