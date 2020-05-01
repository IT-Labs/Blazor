using System;
using BlazorApp.Shared.Enums;
using BlazorApp.Shared.Interfaces;
using BlazorApp.Shared.Repository;
using BlazorApp.Shared.Requests;

namespace Core.Framework.Repository.Queries
{
    public abstract class BaseQuery<TRequest, TResult> : IQuery<TRequest, TResult> where TRequest : IRequest
    {
        public ContextRequest<TRequest> ContextRequest { get; set; }
        public TRequest Request => ContextRequest.Request;
        public long? UserId => ContextRequest.UserId;
        public string Username => ContextRequest.Username;
        public abstract IResult<TResult> Execute(IContext dataContext);
    }   
    
}