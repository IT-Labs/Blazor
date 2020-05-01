using System.Net;
using BlazorApp.Shared.Interfaces;
using BlazorApp.Shared.Repository;
using BlazorApp.Shared.Requests;
using Core.Framework.Repository.Commands;
using Microsoft.EntityFrameworkCore;

namespace Core.Framework.Repository
{
    public class BatchInsert<T> : BaseCommand<EnumerableRequest<T>, bool> where T : class, IEntity
    {
        public override IResult<bool> Execute(IContext dataContext)
        {
            var context = dataContext as DbContext;
            context.Set<T>().AddRange(Request.Payload);
            context.SaveChanges();

            return new Result<bool> { Status = HttpStatusCode.OK };
        }
    }
}
