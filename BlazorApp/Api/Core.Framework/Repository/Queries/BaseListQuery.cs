using System;
using Core.Shared.Enums;
using Core.Shared.Interfaces;
using Core.Shared.Repository;
using Core.Shared.Requests;

namespace Core.Framework.Repository.Queries
{
    public abstract class BaseListQuery<TRequest, TResult> : IListQuery<TRequest, TResult> where TRequest : IRequest
    {
        public ContextRequest<TRequest> ContextRequest { get; set; }
        public TRequest Request => ContextRequest.Request;
        public long? UserId => ContextRequest.UserId;      
        public string Username => ContextRequest.Username;
        public abstract IListResult<TResult> Execute(IContext dataContext);
    }
}