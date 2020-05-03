using Core.Shared.Interfaces;
using Core.Shared.Repository;
using Core.Shared.Requests;
using Core.Shared.Response;

namespace Core.Shared.Managers
{
    public interface IGet
    {
        Response<T> Get<T>(IdRequest request, IQueryInclude<T> includeQuery = null) where T : DeletableEntity;
    }
    public interface IGet<T, in TV> : IGet
        where T : class where TV : IRequest
    {
        Response<T> Get(IdRequest request);
        PagedResponse<T> GetMultiple(TV request);
    }
}