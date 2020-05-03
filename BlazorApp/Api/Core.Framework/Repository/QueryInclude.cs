using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core.Shared.Interfaces;
using Core.Shared.Repository;
using Microsoft.EntityFrameworkCore;

namespace Core.Framework.Repository
{
    public class QueryInclude<T, TProperty> : IQueryInclude<T>
        where T : class
    {
        private readonly Expression<Func<T, TProperty>> _navigationPropertyPath;

        public QueryInclude(Expression<Func<T, TProperty>> navigationPropertyPath)
        {
            _navigationPropertyPath = navigationPropertyPath;
        }
        public IQueryable<T> Include(IQueryable<T> query)
        {
            return query.Include(_navigationPropertyPath);
        }
    }

    public class QueryInclude<T> : IQueryInclude<T>
        where T : class
    {
        private IPartialRequest _request;
        public QueryInclude(IPartialRequest request)
        {
            _request = request;
        }
        public bool IsValid
        {
            get
            {
                var info = typeof(T).GetProperties();
                foreach (var item in _request.Includes)
                {
                    if (!info.Any(x => x.CanRead && x.Name.ToLower() == item
                       && x.GetType().GetInterfaces().Any(y => y.IsConstructedGenericType && y.GetGenericTypeDefinition() == typeof(IList<>))))
                        continue;

                    return true;
                }

                return false;
            }
        }

        public IQueryable<T> Include(IQueryable<T> query)
        {
            var info = typeof(T).GetProperties();

            foreach (var item in _request.Includes)
            {
                if (!info.Any(x => x.CanRead && x.Name.ToLower() == item
                   && x.GetType().GetInterfaces().Any(y => y.IsConstructedGenericType && y.GetGenericTypeDefinition() == typeof(IList<>))))
                    continue;

                query.Include(item);
            }
            return query;
        }
    }
}