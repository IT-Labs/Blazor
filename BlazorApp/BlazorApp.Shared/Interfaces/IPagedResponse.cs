using System.Collections.Generic;

namespace BlazorApp.Shared.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPagedResponse<T> : IResponse<IEnumerable<T>>
    {
        /// <summary>
        /// 
        /// </summary>
        Meta Meta { get; set; }
    }
}