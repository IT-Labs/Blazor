using System.Collections.Generic;
using BlazorApp.Shared.Interfaces;

namespace BlazorApp.Shared.Repository
{
    /// <summary>
    ///     Definition of ListResult. Implements IResult and IPageable
    /// </summary>
    public interface IListResult : IResult, IPageable
    {
        /// <summary>
        ///     Total number of items
        /// </summary>
        int Total { get; set; }

        /// <summary>
        ///     Total unique number of items
        /// </summary>
        int UniqueTotal { get; set; }
    }

    /// <summary>
    ///     Definition of ListResult. Implements IListResult
    /// </summary>
    /// <typeparam name="TValue">List item type</typeparam>
    public interface IListResult<TValue> : IListResult
    {
        /// <summary>
        ///     List of items
        /// </summary>
        List<TValue> Values { get; set; }
    }
}