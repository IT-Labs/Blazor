using BlazorApp.Shared;
using BlazorApp.Shared.Repository;
using BlazorApp.Shared.Requests;
using Core.Framework.Extensions;

namespace Core.Framework.Repository.Commands
{
    public class DeleteEntityCommand<T> : BaseCommand<IdRequest, bool>
        where T : AuditableEntity
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

            dataContext.Delete<T>(x => x.Id == Request.Id);

            return new Result<bool>
            {
                Value = true
            };
        }
    }
}