using BlazorApp.Shared.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Framework.Extensions
{
    public static partial class FunctionalExtensions
    {
        public static Response<K> OnOk<TEntity, K>(this Response<TEntity> response, Func<Response<K>> func)
        {
            if (response.NotOk)
            {
                var other = new Response<K>();
                other.Merge(response);
                return other;
            }

            return func();
        }

        public static PagedResponse<K> OnOk<TEntity, K>(this PagedResponse<TEntity> response, Func<PagedResponse<K>> func)
        {
            if (response.NotOk)
            {
                var other = new PagedResponse<K>();
                other.Merge(response);
                return other;
            }

            return func();
        }

        public static Response<TEntity> OnOk<TEntity>(this Response<TEntity> response, Func<Response<TEntity>> func)
        {
            if (response.NotOk)
                return response;

            return func();
        }

        public static PagedResponse<TEntity> OnOk<TEntity>(this PagedResponse<TEntity> response, Func<PagedResponse<TEntity>> func)
        {
            if (response.NotOk)
                return response;

            return func();
        }

        public static Response<TEntity> OnOk<TEntity>(this Response<TEntity> response, Action func)
        {
            if (response.NotOk)
                return response;

            func();

            return response;
        }

        public static PagedResponse<TEntity> OnOk<TEntity>(this PagedResponse<TEntity> response, Action func)
        {
            if (response.NotOk)
                return response;

            func();

            return response;
        }

        public static Response<TEntity> OnOk<TEntity>(this Response<TEntity> response, Action<TEntity> func)
        {
            if (response.NotOk)
                return response;

            func(response.Payload);

            return response;
        }

        public static PagedResponse<TEntity> OnOk<TEntity>(this PagedResponse<TEntity> response, Action<IEnumerable<TEntity>> func)
        {
            if (response.NotOk)
                return response;

            func(response.Payload);

            return response;
        }
    }
}
