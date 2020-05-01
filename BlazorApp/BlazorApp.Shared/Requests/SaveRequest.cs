using System;
using BlazorApp.Shared.Interfaces;

namespace BlazorApp.Shared.Requests
{
    public abstract class SaveRequest : IRequest
    {
        public long? Id { get; set; }
        public bool IsNew => !Id.HasValue;
    }
}