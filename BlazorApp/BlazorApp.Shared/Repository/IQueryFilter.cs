using System.Linq;

namespace BlazorApp.Shared.Repository
{
    /// <summary>
    /// Definition of filter query
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IQueryFilter<TEntity>
    {
        /// <summary>
        /// Filter
        /// </summary>
        /// <param name="query">Query</param>
        /// <returns>Filtered query</returns>      
        IQueryable<TEntity> Filter(IQueryable<TEntity> query);
    }
}
