using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BlazorApp.Shared.Interfaces;
using BlazorApp.Shared.Response;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Core.Framework.Extensions
{
    public static class HttpClientExtensions
    {

        public static Task<U> HandlePost<T, U>(this HttpClient httpClient, ApiModel<T> model, ILogger logger, FileParameter file = null) where T : IRequest, new()
            where U : IResponse, new()
        {
            return file == null ? HandleException(PostFunct<T, U>, model, httpClient, logger)
                : HandleException(PostFileFunct<T, U>, model, httpClient, logger, file);

        }



        public static Task<Response<Guid>> HandlePost<T>(this HttpClient httpClient, ApiModel<T> model, ILogger logger) where T : IRequest, new()

        {
            return HandleException(PostFunct<T, Response<Guid>>, model, httpClient, logger);

        }

        public static Task<U> HandlePut<T, U>(this HttpClient httpClient, ApiModel<T> model, ILogger logger) where T : IRequest, new()
            where U : IResponse, new()
        {
            return HandleException(PutFunct<T, U>, model, httpClient, logger);

        }

        public static Task<Response<Guid>> HandlePut<T>(this HttpClient httpClient, ApiModel<T> model, ILogger logger) where T : IRequest, new()

        {
            return HandleException(PutFunct<T, Response<Guid>>, model, httpClient, logger);
        }

        public static Task<U> HandleGet<T, U>(this HttpClient httpClient, ApiModel<T> model, ILogger logger) where T : IRequest, new()
            where U : IResponse, new()
        {
            return HandleException(GetFunct<T, U>, model, httpClient, logger);

        }
        public static Task<U> HandleDelete<T, U>(this HttpClient httpClient, ApiModel<T> model, ILogger logger) where T : IRequest, new()
            where U : IResponse, new()
        {
            return HandleException(DeleteFunct<T, U>, model, httpClient, logger);

        }

        public static Task<Response<bool>> HandleDelete<T>(this HttpClient httpClient, ApiModel<T> model, ILogger logger) where T : IRequest, new()

        {
            return HandleException(DeleteFunct<T, Response<bool>>, model, httpClient, logger);

        }

        private static async Task<T> ReadAsJsonAsync<T>(this HttpContent content, HttpStatusCode actionResultStatusCode) where T : IResponse, new()
        {
            var dataAsString = string.Empty;
            try
            {
                dataAsString = await content.ReadAsStringAsync();
            }
            catch
            {
                //Passing error to Ui as info
            }
            if (string.IsNullOrWhiteSpace(dataAsString) || dataAsString.Contains("Under Construction!") || (int)actionResultStatusCode >= (int)HttpStatusCode.InternalServerError)
            {
                return new T() { Status = actionResultStatusCode, Messages = new List<ResponseMessage>() { new ResponseMessage() { Message = dataAsString } } };
            }
            return JsonConvert.DeserializeObject<T>(dataAsString);
        }

        private static string GetQueryString(this object obj)
        {
            var keyValueContent = obj.ToKeyValue() ?? new Dictionary<string, string>();
            var formUrlEncodedContent = new FormUrlEncodedContent(keyValueContent);
            var urlEncodedString = formUrlEncodedContent.ReadAsStringAsync().Result;

            return $"?{urlEncodedString}";
        }

        public static IDictionary<string, string> ToKeyValue(this object metaToken)
        {
            if (metaToken == null)
            {
                return null;
            }

            JToken token = metaToken as JToken;
            if (token == null)
            {
                return ToKeyValue(JObject.FromObject(metaToken));
            }

            if (token.HasValues)
            {
                var contentData = new Dictionary<string, string>();
                foreach (var child in token.Children().ToList())
                {
                    var childContent = child.ToKeyValue();
                    if (childContent != null)
                    {
                        contentData = contentData.Concat(childContent)
                            .ToDictionary(k => k.Key, v => v.Value);
                    }
                }

                return contentData;
            }

            var jValue = token as JValue;
            if (jValue?.Value == null)
            {
                return null;
            }

            var value = jValue?.Type == JTokenType.Date ?
                jValue?.ToString("o", CultureInfo.InvariantCulture) :
                jValue?.ToString(CultureInfo.InvariantCulture);

            return new Dictionary<string, string> { { token.Path, value } };
        }
        private static Task<U> PostFunct<T, U>(HttpClient httpClient, ApiModel<T> model, FileParameter file) where U : IResponse, new()
        {
            var dataAsString = JsonConvert.SerializeObject(model.Request);
            var content = new StringContent(dataAsString);
            PrepareHeaders(httpClient, model, content);

            var actionResult = httpClient.PostAsync(model.Url, content).Result;
            var responseTask = actionResult.HandleResult<T, U>();
            return responseTask;
        }

        private static Task<U> PostFileFunct<T, U>(HttpClient httpClient, ApiModel<T> model, FileParameter file) where U : IResponse, new()
        {
            var content = new MultipartFormDataContent();
            var fileContent = new StreamContent(file.Data);
            fileContent.Headers.Add("Content-Disposition", $"form-data; name=\"file\"; filename=\"{file.FileName ?? "file"}\"");

            content.Add(fileContent, "\"file\"", $"\"{file.FileName ?? "file"}\"");

            if (!string.IsNullOrWhiteSpace(model.Token))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", model.Token.Replace("Bearer ", "").Trim());
            }

            var url = model.Url;
            if (model.Request != null)
            {
                url += model.Request.GetQueryString();
            }

            var actionResult = httpClient.PostAsync(url, content).Result;
            var responseTask = actionResult.HandleResult<T, U>();
            return responseTask;
        }

        private static Task<U> HandleResult<T, U>(this HttpResponseMessage actionResult) where U : IResponse, new()
        {
            var responseTask = actionResult.Content.ReadAsJsonAsync<U>(actionResult.StatusCode);
            responseTask.Result.Status = actionResult.StatusCode;


            return responseTask;
        }

        private static async Task<U> HandleException<T, U>(this Func<HttpClient, ApiModel<T>,
            Task<U>> funct,
            ApiModel<T> model, HttpClient httpClient, ILogger logger, FileParameter file = null) where T : IRequest, new()
            where U : IResponse, new()
        {
            try
            {
                Console.WriteLine($"Api Url :{model.Url}");
                var taskresponse = await funct.Invoke(httpClient, model);
                return taskresponse;
            }
            catch (Exception e)
            {
                logger?.LogError(new EventId(), e, e.Message);
                var response = new U
                {
                    Status = HttpStatusCode.InternalServerError,
                    Messages = new List<ResponseMessage>() { new ResponseMessage() { Message = e.Message } }
                };

                return response;


            }

        }

        private static async Task<U> HandleException<T, U>(this Func<HttpClient, ApiModel<T>, FileParameter,
                Task<U>> funct,
            ApiModel<T> model, HttpClient httpClient, ILogger logger, FileParameter file = null) where T : IRequest, new()
            where U : IResponse, new()
        {
            try
            {
                Console.WriteLine($"Api Url :{model.Url}");
                var taskresponse = await funct.Invoke(httpClient, model, file);
                return taskresponse;
            }
            catch (Exception e)
            {
                logger?.LogError(new EventId(), e, e.Message);
                var response = new U
                {
                    Status = HttpStatusCode.InternalServerError,
                    Messages = new List<ResponseMessage>() { new ResponseMessage() { Message = e.Message } }
                };

                return response;


            }

        }

        private static Task<U> PutFunct<T, U>(this HttpClient httpClient, ApiModel<T> model) where U : IResponse, new()
        {
            var dataAsString = JsonConvert.SerializeObject(model.Request);
            var content = new StringContent(dataAsString);
            PrepareHeaders(httpClient, model, content);

            var actionResult = httpClient.PutAsync(model.Url, content).Result;
            var responseTask = actionResult.HandleResult<T, U>();
            return responseTask;
        }

        private static Task<U> GetFunct<T, U>(this HttpClient httpClient, ApiModel<T> model) where U : IResponse, new()
        {
            var content = new StringContent("");
            PrepareHeaders(httpClient, model, content);
            var url = model.Url;
            if (model.Request != null)
            {
                url += model.Request.GetQueryString();
            }
            var actionResult = httpClient.GetAsync(url).Result;
            var responseTask = actionResult.HandleResult<T, U>();
            return responseTask;


        }

        private static Task<U> DeleteFunct<T, U>(this HttpClient httpClient, ApiModel<T> model) where U : IResponse, new()
        {
            var content = new StringContent("");
            PrepareHeaders(httpClient, model, content);
            var url = model.Url;
            if (model.Request != null)
            {
                url += model.Request.GetQueryString();
            }

            var actionResult = httpClient.DeleteAsync(url).Result;
            var responseTask = actionResult.HandleResult<T, U>();
            return responseTask;
        }

        private static void PrepareHeaders<T>(HttpClient httpClient, ApiModel<T> model, HttpContent content)
        {
            content.Headers.ContentType = new MediaTypeHeaderValue(model.ContentType);
            httpClient.DefaultRequestHeaders.Referrer = model.Referrer;

            if (!string.IsNullOrWhiteSpace(model.Token))
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", model.Token.Replace("Bearer ", "").Trim());
        }

        public static void AddEspHeader(this HttpClient client, string method, string url, string key, string secret)
        {
            Uri uri = new Uri(url.ToLower());
            string path = uri.GetLeftPart(UriPartial.Path);
            var requestUri = System.Web.HttpUtility.UrlEncode(path);
            var requestHttpMethod = method;

            //Calculate UNIX time
            var epochStart = new DateTime(1970, 01, 01, 0, 0, 0, 0, DateTimeKind.Utc);
            var timeSpan = DateTime.UtcNow - epochStart;
            var requestTimeStamp = Convert.ToUInt64(timeSpan.TotalSeconds).ToString();

            //create random nonce for each request
            var nonce = Guid.NewGuid().ToString("N");

            //Creating the raw signature string
            var signatureRawData = $"{key}{requestHttpMethod}{requestUri}{requestTimeStamp}{nonce}";
            var secretKeyByteArray = Convert.FromBase64String(secret);
            var signature = Encoding.UTF8.GetBytes(signatureRawData);

            using (var hmac = new HMACSHA256(secretKeyByteArray))
            {
                var signatureBytes = hmac.ComputeHash(signature);
                var requestSignatureBase64String = Convert.ToBase64String(signatureBytes);

                //Setting the values in the Authorization header using custom scheme (amx)
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("amx",
                    $"{key}:{requestSignatureBase64String}:{nonce}:{requestTimeStamp}");
            }
        }
    }

    public class ApiModel<T>
    {
        public string ApiUrl { get; set; }
        public string Controller { get; set; }
        public string Token { get; set; }
        public T Request { get; set; }
        public string Url
        {
            get
            {
                string url;
                if (!string.IsNullOrEmpty(Route))
                {
                    if (Route.Contains("~/"))
                    {
                        url = $"{ApiUrl}{Route.Replace("~/", "/")}";
                    }
                    else
                    {
                        url = $"{ApiUrl}/{Controller}/{Route}";
                    }
                }
                else
                {
                    url = $"{ApiUrl}/{Controller}";
                }
                return url;
            }
        }

        public string ContentType { get; set; } = "application/json";
        public string Route { get; set; }
        public Uri Referrer { get; set; }
    }

    public class FileParameter
    {
        public Stream Data { get; set; }
        public string FileName { get; set; }
    }
}