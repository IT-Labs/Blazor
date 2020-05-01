using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BlazorApp.Shared;
using BlazorApp.Shared.Interfaces;
using BlazorApp.Shared.Repository;
using BlazorApp.Shared.Requests;
using Core.Framework.Extensions;

namespace Core.Framework.Repository.Queries
{
    public class GetMultipleWithFuncQuery<TRequest, TEntity, TSortColumn> : BaseListQuery<TRequest, TEntity>
        where TRequest : SortablePageableRequest<TSortColumn>, IRequest
        where TEntity : AuditableEntity
        where TSortColumn : struct
    {

        public GetMultipleWithFuncQuery(Expression<Func<TEntity, bool>> filter)
        {
            Filter = filter;
        }
        private Expression<Func<TEntity, bool>> Filter { get; }
        public List<IQueryFilter<TEntity>> QueryFilters { get; set; } = new List<IQueryFilter<TEntity>>();
        public IQueryInclude<TEntity> QueryInclude { get; set; }
        public Dictionary<TSortColumn, Expression<Func<TEntity, object>>> SortDictionary { get; set; }

        public override IListResult<TEntity> Execute(IContext dataContext)
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
            if (Filter != null)
                query = query.Where(Filter);


            var total = query.Count();
            var data = query.SortAndPage(SortDictionary, Request);

            return new ListResult<TEntity>(data.ToList(), Request)
            {
                Total = total
            };
        }
    }
}