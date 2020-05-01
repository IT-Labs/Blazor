using System;
using BlazorApp.Shared.Enums;
using BlazorApp.Shared.Interfaces;
using BlazorApp.Shared.Repository;
using BlazorApp.Shared.Requests;

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