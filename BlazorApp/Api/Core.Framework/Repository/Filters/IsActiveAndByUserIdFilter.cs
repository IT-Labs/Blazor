using System;
using System.Linq;
using BlazorApp.Shared;
using BlazorApp.Shared.Interfaces;

namespace Core.Framework.Repository.Filters
{
    public class IsActiveAndByUserIdFilter<T> : IsActiveFilter<T> where T : DeletableEntity, IHaveUserId
    {
        private readonly Guid _id;

        public IsActiveAndByUserIdFilter(Guid id, bool? isActive) : base(isActive)
        {
            _id = id;
        }
        public override IQueryable<T> Filter(IQueryable<T> query)
        {
            query = base.Filter(query);
            return Queryable.Where(query, x => x.UserId == _id);
        }
    }
}