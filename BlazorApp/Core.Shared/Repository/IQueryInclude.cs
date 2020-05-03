using System.Linq;

namespace Core.Shared.Repository
{
    public interface IQueryInclude<TEntity>
        where TEntity : class
    {
        IQueryable<TEntity> Include(IQueryable<TEntity> query);
    }

   
}