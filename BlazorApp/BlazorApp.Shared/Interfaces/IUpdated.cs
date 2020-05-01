using System;

namespace BlazorApp.Shared.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUpdated
    {
        /// <summary>
        /// 
        /// </summary>

        long? UpdatedBy { get; set; }
      
        /// <summary>
        /// 
        /// </summary>
        DateTime? UpdatedAt { get; set; }
    }
}