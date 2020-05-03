using Core.Shared;
using Core.Shared.Interfaces;
using Core.Shared.Requests;

namespace Core.Framework.Repository.Queries
{
    public class GetMultipleQuery<TRequest, TEntity, TSortColumn> : GetMultipleWithFuncQuery<TRequest, TEntity, TSortColumn>
        where TRequest : SortablePageableRequest<TSortColumn>, IRequest
        where TEntity : AuditableEntity
        where TSortColumn : struct
    {
        public GetMultipleQuery() : base(null)
        {
        }
    }
}