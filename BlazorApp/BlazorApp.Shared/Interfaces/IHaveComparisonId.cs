using System;

namespace BlazorApp.Shared.Interfaces
{
    public interface IHaveComparisonId
    {
        Guid CompareId { get; }
    }
}