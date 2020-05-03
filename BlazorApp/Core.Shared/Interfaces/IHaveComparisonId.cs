using System;

namespace Core.Shared.Interfaces
{
    public interface IHaveComparisonId
    {
        Guid CompareId { get; }
    }
}