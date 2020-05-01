using BlazorApp.Shared.Interfaces;
using BlazorApp.Shared.Repository;
using BlazorApp.Shared.Requests;
using BlazorApp.Shared.Response;

namespace BlazorApp.Shared.Managers
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