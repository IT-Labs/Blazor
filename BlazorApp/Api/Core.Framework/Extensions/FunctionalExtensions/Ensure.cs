using Core.Shared.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Framework.Extensions
{
    public static partial class FunctionalExtensions
    {
        public static Response<TEntity> Ensure<TEntity>(this Response<TEntity> response, Func<TEntity, bool> func, string message = "")
        {
            if (response.NotOk)
                return response;

            if (!func(response.Payload))
                return Response<TEntity>.BadRequest(message);

            return response;
        }

        public static PagedResponse<TEntity> Ensure<TEntity>(this PagedResponse<TEntity> response, Func<IEnumerable<TEntity>, bool> func, string message = "")
        {
            if (response.NotOk)
                return response;

            if (!func(response.Payload))
                return PagedResponse<TEntity>.BadRequest(message);

            return response;
        }
    }
}
