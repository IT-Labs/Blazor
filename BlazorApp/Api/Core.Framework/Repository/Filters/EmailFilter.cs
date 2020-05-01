using System.Linq;
using BlazorApp.Shared.Interfaces;
using BlazorApp.Shared.Repository;
using Core.Framework.Repository.Queries.Entities;

namespace Core.Framework.Repository.Filters
{
    public class EmailFilter<T> : IQueryFilter<T> where T : class, IHaveEmail
    {
        private readonly string _keyword;

        public EmailFilter(string keyword)
        {
            _keyword = keyword;
        }

        public IQueryable<T> Filter(IQueryable<T> query)
        {
            if (string.IsNullOrEmpty(_keyword)) return query;

            return query.Where(QueryPredicates.EmailFilterPredicate<T>(_keyword));


        }
    }
}
