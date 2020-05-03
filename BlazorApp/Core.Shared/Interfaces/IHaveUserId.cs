using System;

namespace Core.Shared.Interfaces
{
    public interface IHaveUserId
    {
        Guid UserId { get; set; }
    }
}