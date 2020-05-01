using System;

namespace BlazorApp.Shared.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICreated
    {
        /// <summary>
        /// 
        /// </summary>
        DateTime CreatedAt { get; set; }

        long? CreatedBy { get; set; }

    }
}