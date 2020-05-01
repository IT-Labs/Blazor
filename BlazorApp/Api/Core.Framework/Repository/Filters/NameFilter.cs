using BlazorApp.Shared.Interfaces;
using BlazorApp.Shared.Repository;
using Core.Framework.Repository.Queries.Entities;
using System.Linq;

namespace Core.Framework.Repository.Filters
{
    public class NameFilter<T> : IQueryFilter<T> where T : IHaveName, IHaveFirstName, IHaveLastName
    {
        private readonly string _keyword;

        public NameFilter(string keyword)
        {
            _keyword = keyword;
        }

        public IQueryable<T> Filter(IQueryable<T> query)
        {
            if (string.IsNullOrEmpty(_keyword)) return query;

            return query.Where(QueryPredicates.NameFilterPredicate<T>(_keyword));
        }
    }
}
