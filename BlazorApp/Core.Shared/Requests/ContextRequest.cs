using System;
using Core.Shared.Enums;
using Core.Shared.Interfaces;

namespace Core.Shared.Requests
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