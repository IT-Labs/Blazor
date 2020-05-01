using System;
using BlazorApp.Shared.Enums;
using BlazorApp.Shared.Interfaces;

namespace BlazorApp.Shared.Requests
{
    public class ContextRequest<TRequest> where TRequest : IRequest
    {
        public ContextRequest(IUserContextInfo contextInfo)
        {
            UserId = contextInfo?.UserId;
            Username = contextInfo?.Username;
        }

        public TRequest Request { get; set; }
        public long? UserId { get; protected set; }
        public string Username { get; protected set; }
    }
}