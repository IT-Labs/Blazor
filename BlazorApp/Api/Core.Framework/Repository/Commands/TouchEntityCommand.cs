using System.Linq;
using System.Net;
using Core.Shared;
using Core.Shared.Repository;
using Core.Shared.Requests;
using Core.Shared.ValidationCodes;
using Core.Framework.Extensions;

namespace Core.Framework.Repository.Commands
{
    /// <summary>
    /// Command for touching an entity (Updating auditable properties)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TouchEntityCommand<T> : BaseCommand<IdRequest, T> where T : AuditableEntity
    {
        public override IResult<T> Execute(IContext dataContext)
        {
            var entity = dataContext.AsQueryable<T>().FirstOrDefault(x => x.Id == Request.Id);
            if (entity == null)
                return new Result<T>(errorCode: ValidationCodes.Common.Cmn008, entityName: typeof(T).Name.SplitCamelCase());

            var updateResponse = Update(dataContext, entity);
            if (updateResponse.Status != HttpStatusCode.OK)
                return new Result<T>(HttpStatusCode.InternalServerError, ValidationCodes.Common.Cmn046);

            return new Result<T>(entity);
        }
    }
}