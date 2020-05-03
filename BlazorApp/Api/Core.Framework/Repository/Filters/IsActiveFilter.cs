using Core.Shared.Interfaces;
using Core.Shared.Repository;
using System.Linq;

namespace Core.Framework.Repository.Filters
{
    public class IsActiveFilter<T> : IQueryFilter<T> where T : IHaveIsActive
    {
        protected readonly bool? IsActive;

        public IsActiveFilter(bool? isActive)
        {
            IsActive = isActive;
        }

        public virtual IQueryable<T> Filter(IQueryable<T> query)
        {
            return !IsActive.HasValue
                ? query
                : query.Where(
                    x =>
                        IsActive.Value && x.IsActive
                        ||
                        !IsActive.Value && !(x.IsActive
                        ));
        }
    }
}
