using System;
using System.Linq;
using Core.Shared;
using Core.Shared.Repository;
using Core.Shared.Requests;
using Core.Framework.Extensions;

namespace Core.Framework.Repository.Commands
{
    public class SaveEntityCommand<T, TRequest> : BaseCommand<TRequest, long>
        where T : AuditableEntity, new()
        where TRequest : SaveRequest
    {
        public IQueryInclude<T> IncludeQuery { get; set; } = null;

        public override IResult<long> Execute(IContext dataContext)
        {
            var query = dataContext.AsQueryable<T>();
            if (IncludeQuery != null)
            {
                query = IncludeQuery.Include(query);
            }

            var entity = !Request.IsNew ?
                Queryable.FirstOrDefault(query, x => x.Id == Request.Id)
                : new T();

            if (entity == null)
            {
                return new Result<long>(
                // errorCode: ValidationCodes.Common.Cmn021, 
                entityName: typeof(T).Name.SplitCamelCase());
            }

            MapAction?.Invoke(entity, WrappedRequest);

            entity.UpdateAuditableProperties(Request.IsNew
                ? AuditableEntity.AuditableAction.Insert
                : AuditableEntity.AuditableAction.Update, UserId);

            //if (entity.AlternateId==0)
            if (Request.IsNew)
            {
                dataContext.InsertWithChildEntities(entity);
            }
            else
            {
                dataContext.Update(entity);
            }

            return new Result<long>
            {
                Value = entity.Id
            };
        }

        public Action<T, ContextRequest<TRequest>> MapAction { get; set; } = null;

    }
}