using System;
using Core.Shared.Interfaces;

namespace Core.Shared.Requests
{
    public abstract class SaveRequest : IRequest
    {
        public long? Id { get; set; }
        public bool IsNew => !Id.HasValue;
    }
}