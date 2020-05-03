using System;
using System.Linq;
using System.Linq.Expressions;
using Core.Shared;
using Core.Shared.Repository;
using Core.Framework.Repository.Queries;

namespace Core.Framework.Repository.Filters
{
    public class ExpressionFilter<T> : IQueryFilter<T> where T : AuditableEntity
    {
        public Expression<Func<T, bool>> QueryExpression { get; set; }

        public virtual IQueryable<T> Filter(IQueryable<T> query)
        {
            return query.Where(QueryExpression);
        }
    }
}
