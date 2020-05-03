using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core.Shared.Interfaces;
using Core.Shared.Repository;
using Core.Framework.Extensions;

namespace Core.Framework.Repository.Queries
{
    public class GetWithFuncQuery<T, TRequest> : BaseQuery<TRequest, T>
        where T : class, IEntity
        where TRequest : IRequest
    {
        public GetWithFuncQuery(Expression<Func<T, bool>> filter)
        {
            Filter = filter;
        }
        public IQueryInclude<T> IncludeQuery { get; set; } = null;
        Expression<Func<T, bool>> Filter { get; }
        public List<IQueryFilter<T>> QueryFilters { get; set; } = new List<IQueryFilter<T>>();

        public override IResult<T> Execute(IContext dataContext)
        {
            var query = dataContext.AsQueryable<T>();
            if (IncludeQuery != null)
            {
                query = IncludeQuery.Include(query);
            }
            foreach (var queryFilter in QueryFilters)
            {
                query = queryFilter.Filter(query);
            }

            var data = Filter != null 
                ? query.FirstOrDefault(Filter) : query.FirstOrDefault();

            if (data == null)
            {
                return new Result<T>(
                    //errorCode: ValidationCodes.Common.Cmn021, 
                    entityName: typeof(T).Name.SplitCamelCase());
            }

            return new Result<T>(data);
        }



    }
}