using System.Linq;
using System.Net;
using BlazorApp.Shared;
using BlazorApp.Shared.Interfaces;
using BlazorApp.Shared.Repository;
using BlazorApp.Shared.Requests;

namespace Core.Framework.Repository.Queries
{
    public class GetByEmailQuery<T> : BaseQuery<EmailRequest, T> where T : DeletableEntity, IHaveEmail
    {
        public override IResult<T> Execute(IContext dataContext)
        {
            if (string.IsNullOrEmpty(Request.Email))
                return new Result<T> { Status = HttpStatusCode.BadRequest };

            string email = Request.Email.Trim().ToLowerInvariant();

            var entity = dataContext.AsQueryable<T>()
                    .FirstOrDefault(x => x.IsActive && !string.IsNullOrEmpty(x.Email) && x.Email.ToLower() == email);

            if (entity == null)
                return new Result<T> { Status = HttpStatusCode.NotFound };

            return new Result<T>(entity);
        }
    }
}
