using System;

namespace Core.Shared.Interfaces
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