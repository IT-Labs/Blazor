using System;

namespace BlazorApp.Shared.Interfaces
{
    public interface IDeleted
    {
        /// <summary>
        /// 
        /// </summary>
        DateTime? DeletedAt { get; set; }
        long? DeletedBy { get; set; }

    }
}
