using System;

namespace BlazorApp.Shared.Interfaces
{
    public interface IHaveOwner
    {
        Guid OwnerId { get; }
    }
}