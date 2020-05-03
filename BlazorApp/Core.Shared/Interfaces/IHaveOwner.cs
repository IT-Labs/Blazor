using System;

namespace Core.Shared.Interfaces
{
    public interface IHaveOwner
    {
        Guid OwnerId { get; }
    }
}