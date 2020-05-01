using System.Net;
using BlazorApp.Shared.Repository;

namespace Core.Framework.Extensions
{
    public static class ResultExtensions
    {
        public static bool IsValid<TValue>(this IResult<TValue> originalResult)
        {
            return originalResult.Status == HttpStatusCode.OK;
        }
        public static bool IsInvalid<TValue>(this IResult<TValue> originalResult)
        {
            return !originalResult.IsValid();
        }
        public static bool IsValid<TValue>(this IListResult<TValue> originalResult)
        {
            return originalResult.Status == HttpStatusCode.OK;
        }
        public static bool IsInvalid<TValue>(this IListResult<TValue> originalResult)
        {
            return !originalResult.IsValid();
        }
    }
}
