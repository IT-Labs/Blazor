using Core.Shared.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Framework.Extensions
{
    public static partial class FunctionalExtensions
    {
        public static Response<K> Map<TEntity, K>(this Response<TEntity> response, Func<TEntity, K> func)
        {
            var dtoResponse = new Response<K>();
            dtoResponse.Merge(response);
            if (dtoResponse.NotOk)
                return dtoResponse;

            dtoResponse.Payload = func(response.Payload);

            return dtoResponse;
        }

        public static PagedResponse<K> Map<TEntity, K>(this PagedResponse<TEntity> response, Func<IEnumerable<TEntity>, IEnumerable<K>> func)
        {
            var dtoResponse = new PagedResponse<K>();
            dtoResponse.Merge(response);
            if (dtoResponse.NotOk)
                return dtoResponse;

            dtoResponse.Payload = func(response.Payload);

            return dtoResponse;
        }
    }
}
