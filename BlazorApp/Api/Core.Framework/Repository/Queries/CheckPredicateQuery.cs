using System;
using System.Linq;
using System.Linq.Expressions;
using Core.Shared;
using Core.Shared.Repository;
using Core.Shared.Requests;

namespace Core.Framework.Repository.Queries
{
    public class CheckPredicateQuery<T> : BaseQuery<EmptyRequest, bool>
        where T : AuditableEntity, new()
    {
        private readonly bool _invertPredicate;
        public IQueryInclude<T> QueryInclude { get; set; }

        public CheckPredicateQuery(bool invertPredicate = false)
        {
            _invertPredicate = invertPredicate;
        }

        public Expression<Func<T, bool>> Predicate { get; set; }
        public override IResult<bool> Execute(IContext dataContext)
        {
            var query = dataContext.AsQueryable<T>();
            if (QueryInclude != null)
            {
                query = QueryInclude.Include(query);
            }
            return _invertPredicate
                ? new Result<bool>(!query.Any(Predicate))
                : new Result<bool>(query.Any(Predicate));
        }
    }
}