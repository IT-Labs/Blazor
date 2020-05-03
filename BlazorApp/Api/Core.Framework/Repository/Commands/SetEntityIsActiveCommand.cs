using Core.Shared;
using Core.Shared.Repository;
using Core.Shared.Requests;
using Core.Framework.Extensions;

namespace Core.Framework.Repository.Commands
{
    public class SetEntityIsActiveCommand<T> : BaseCommand<SetActiveStatusRequest<T>, bool> where T : DeletableEntity
    {
        public override IResult<bool> Execute(IContext dataContext)
        {
            var entity = dataContext.Get<T>(x => x.Id == Request.Id);

            if (entity == null)
            {
                return new Result<bool>(
                    // errorCode: ValidationCodes.Common.Cmn021, 
                    entityName: typeof(T).Name.SplitCamelCase());
            }

            if (!Request.SetParent && (entity.IsActive) == Request.IsActive)
            {
                return new Result<bool>(
                    //errorCode: Request.IsActive ? ValidationCodes.Common.Cmn022 : ValidationCodes.Common.Cmn023,
                    entityName: typeof(T).Name.SplitCamelCase());
            }
        
             entity.IsActive = Request.IsActive;        
            
            entity.UpdateAuditableProperties(
                Request.IsActive
                    ? AuditableEntity.AuditableAction.Reactivate
                    : AuditableEntity.AuditableAction.Delete, UserId);
            dataContext.Update(entity);

            return new Result<bool>
            {
                Value = true
            };
        }
    }
}