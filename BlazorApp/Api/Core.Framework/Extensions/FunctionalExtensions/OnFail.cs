using BlazorApp.Shared.Response;
using System;

namespace Core.Framework.Extensions
{
    public static partial class FunctionalExtensions
    {
        public static Response<TEntity> OnFail<TEntity>(this Response<TEntity> response, Action<Response<TEntity>> func)
        {
            if (!response.NotOk)
                return response;

            func(response);

            return response;
        }
    }
}
