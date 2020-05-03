using System;

namespace Core.Shared.Interfaces
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
