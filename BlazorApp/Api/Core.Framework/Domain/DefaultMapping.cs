using Core.Shared;
using Core.Shared.Interfaces;
using Core.Shared.Repository;
using Core.Shared.Requests;
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
