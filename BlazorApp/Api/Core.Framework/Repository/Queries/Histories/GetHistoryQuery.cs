using Core.Shared;
using Core.Shared.Repository;
using Core.Shared.Requests.Histories;
using Core.Framework.Extensions;
using System.Linq;
using System.Net;

namespace Core.Framework.Repository.Queries.Histories
{
    public class GetHistoryQuery<T> : BaseQuery<GetHistoryRequest, HistoryData> where T : DeletableEntity
    {
        public override IResult<HistoryData> Execute(IContext dataContext)
        {
            string tableName = typeof(T).Name.ToPostgreConvention().ToPlural();
            var result = dataContext.AsQueryable<HistoryData>().Where(x => x.TableName == tableName && x.Id == Request.Id).FirstOrDefault();
            return new Result<HistoryData>(result) { Status = result == null ? HttpStatusCode.NotFound : HttpStatusCode.OK };
        }
    }
}
