using System;
using System.IO;

namespace Core.Framework.Extensions
{
    public static class UrlExtensions
    {
        public static string GetFileNameFromUrl(this string url)
        {
            try
            {
                if (!string.IsNullOrEmpty(url))
                {
                    Uri uri = new Uri(url);
                    return Path.GetFileName(uri.LocalPath);
                }

                return url;
            }
            catch
            {
                return url;
            }
        }

        public static string GetHostNameFromUrl(this string url)
        {
            if (string.IsNullOrWhiteSpace(url) || !Uri.TryCreate(url, UriKind.Absolute, out Uri uri))
                return null;

            if (uri.Authority.Contains("www."))
                return uri.Authority.Replace("www.", string.Empty);
            return uri.Authority;
        }
    }
}