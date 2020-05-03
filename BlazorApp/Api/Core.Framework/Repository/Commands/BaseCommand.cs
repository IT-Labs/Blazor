using Core.Shared;
using Core.Shared.Interfaces;
using Core.Shared.Repository;
using Core.Shared.Requests;
using System.Collections.Generic;

namespace Core.Framework.Repository.Commands
{
    public abstract class BaseCommand<TRequest, TResult> : ICommand<TRequest, TResult> where TRequest : IRequest
    {
        public ContextRequest<TRequest> WrappedRequest { get; set; }
        public TRequest Request => WrappedRequest.Request;
        public long? UserId => WrappedRequest.UserId;
        public string Username => WrappedRequest.Username;
        TRequest ICommand<TRequest, TResult>.Request { get; set; }

        public abstract IResult<TResult> Execute(IContext dataContext);

        public IResult<long> Insert<T>(IContext dataContext, T entity) where T : AuditableEntity
        {
            entity.UpdateAuditableProperties(AuditableEntity.AuditableAction.Insert, UserId);
            dataContext.InsertWithChildEntities(entity);
            return new Result<long>(entity.Id);
        }

        public IResult<long> Update<T>(IContext dataContext, T entity) where T : AuditableEntity
        {
            entity.UpdateAuditableProperties(AuditableEntity.AuditableAction.Update, UserId);
            dataContext.Update(entity);
            return new Result<long>(entity.Id);
        }

        public IResult<bool> UpdateMultiple<T>(IContext dataContext, List<T> entities) where T : AuditableEntity
        {
            entities.ForEach(x => x.UpdateAuditableProperties(AuditableEntity.AuditableAction.Update, UserId));
            dataContext.UpdateMultiple(entities);
            return new Result<bool>(true);
        }
    }
}