using System;
using System.Linq;
using Core.Shared;

namespace Core.Framework.Repository.Filters
{
    public class IsActiveAndByIdFilter<T> : IsActiveFilter<T> where T : DeletableEntity
    {
        private readonly long _id;

        public IsActiveAndByIdFilter(long id, bool? isActive) : base(isActive)
        {
            _id = id;
        }
        public override IQueryable<T> Filter(IQueryable<T> query)
        {
            query = base.Filter(query);
            return query.Where(x => x.Id == _id);
        }
    }
}