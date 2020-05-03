using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core.Shared;
using Core.Shared.Interfaces;
using Core.Shared.Repository;
using Core.Shared.Requests;
using Core.Framework.Extensions;

namespace Core.Framework.Repository.Queries
{
    public class SearchQuery<TRequest, TEntity, TSortColumn, TResult> : BaseListQuery<TRequest, TResult>
        where TRequest : SortablePageableRequest<TSortColumn>, IRequest
        where TEntity : AuditableEntity
        where TSortColumn : struct
        where TResult : class
    {
        public List<IQueryFilter<TEntity>> QueryFilters { get; set; } = new List<IQueryFilter<TEntity>>();
        public IQueryInclude<TEntity> QueryInclude { get; set; }
        public Func<TEntity, TResult> ToSearchContract { get; set; }
        public Dictionary<TSortColumn, Expression<Func<TEntity, object>>> SortDictionary { get; set; }

        public override IListResult<TResult> Execute(IContext dataContext)
        {
            var query = dataContext.AsQueryable<TEntity>();
            if (QueryInclude != null)
            {
                query = QueryInclude.Include(query);
            }

            foreach (var queryFilter in QueryFilters)
            {
                query = queryFilter.Filter(query);
            }

            var total = query.Count();
            var data = query.SortAndPage(SortDictionary, Request).Select(x => ToSearchContract(x));
            return new ListResult<TResult>(data.ToList(), Request)
            {
                Total = total
            };
        }


    }
}