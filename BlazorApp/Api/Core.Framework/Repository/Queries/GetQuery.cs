using BlazorApp.Shared.Interfaces;

namespace Core.Framework.Repository.Queries
{
    public class GetQuery<T, TRequest> : GetWithFuncQuery<T, TRequest>
        where T : class, IEntity
        where TRequest : IRequest
    {
        public GetQuery() : base(null)
        {
        }
    }
}