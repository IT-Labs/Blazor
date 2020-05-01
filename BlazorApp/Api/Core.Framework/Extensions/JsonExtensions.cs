using Newtonsoft.Json.Linq;
using System.Linq;
using System.Net;

namespace Core.Framework.Extensions
{
    public static class JsonExtensions
    {
        public static string LoadJsonFromUrl(this string url)
        {
            try
            {
                using (WebClient wc = new WebClient())
                {
                    return wc.DownloadString(url);
                }
            }
            catch { }

            return string.Empty;
        }

        public static T DeserializeJsonString<T>(this string json, bool ignoreRoot) where T : class
        {
            if (string.IsNullOrWhiteSpace(json))
                return default(T);

            try
            {
                return ignoreRoot
                    ? JObject.Parse(json)?.Properties()?.First()?.Value?.ToObject<T>()
                    : JObject.Parse(json)?.ToObject<T>();
            }
            catch { }

            return default(T);
        }
    }
}
