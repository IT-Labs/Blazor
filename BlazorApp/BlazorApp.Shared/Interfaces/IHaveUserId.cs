using System;

namespace BlazorApp.Shared.Interfaces
{
    public interface IHaveUserId
    {
        Guid UserId { get; set; }
    }
}