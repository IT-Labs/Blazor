using BlazorApp.Shared.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Framework.Extensions
{
    public static partial class FunctionalExtensions
    {
        public static PagedResponse<TEntity> BindPayload<TEntity>(this PagedResponse<TEntity> response, Func<IEnumerable<TEntity>, IEnumerable<TEntity>> func)
        {
            if (response.NotOk)
                return response;

            response.Payload = func(response.Payload);

            return response;
        }

        public static Response<TEntity> BindPayload<TEntity>(this Response<TEntity> response, Func<TEntity, TEntity> func)
        {
            if (response.NotOk)
                return response;

            response.Payload = func(response.Payload);

            return response;
        }

        public static Response<TEntity> BindResponse<TEntity>(this Response<TEntity> response, Action<Response<TEntity>> func)
        {
            if (response.NotOk)
                return response;

            func(response);

            return response;
        }

        public static PagedResponse<TEntity> BindResponse<TEntity>(this PagedResponse<TEntity> response, Action<PagedResponse<TEntity>> func)
        {
            if (response.NotOk)
                return response;

            func(response);

            return response;
        }
    }
}
