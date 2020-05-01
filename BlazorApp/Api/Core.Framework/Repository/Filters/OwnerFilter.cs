using System;
using System.Linq;
using BlazorApp.Shared.Interfaces;
using BlazorApp.Shared.Repository;
using Core.Framework.Repository.Queries;

namespace Core.Framework.Repository.Filters
{
    public class OwnerFilter<T> : IQueryFilter<T> where T : class, IHaveOwner
    {
        private readonly Guid? _ownerId;

        public OwnerFilter(Guid? ownerId)
        {
            _ownerId = ownerId;

        }

        public IQueryable<T> Filter(IQueryable<T> query)
        {
            if (!_ownerId.HasValue) return query;

            return Queryable.Where(query, x => x.OwnerId == _ownerId.Value);
        }
    }
}