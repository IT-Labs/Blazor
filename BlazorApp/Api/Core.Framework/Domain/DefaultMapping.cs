using BlazorApp.Shared;
using BlazorApp.Shared.Interfaces;
using BlazorApp.Shared.Repository;
using BlazorApp.Shared.Requests;
using Omu.ValueInjecter;


namespace Core.Framework.Domain
{
    public class DefaultMapping<T, TRequest> : IMap<T, ContextRequest<TRequest>>
        where T : AuditableEntity
        where TRequest : IRequest
    {

        public void Map(T entity, ContextRequest<TRequest> request)
        {
            var baseRequest = request.Request;
            entity?.InjectFrom(baseRequest);
        }
    }
}
