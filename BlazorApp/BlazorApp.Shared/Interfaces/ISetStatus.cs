using BlazorApp.Shared.Requests;
using BlazorApp.Shared.Response;

namespace BlazorApp.Shared.Interfaces
{
    public interface ISetStatus
    {
        Response<bool> SetStatus<T>(IdRequest request, bool isActive) where T : DeletableEntity;
    }
    public interface ISetStatus<T>
        where T : DeletableEntity

    {
        Response<bool> SetStatus(SetActiveStatusRequest<T> request);
    }
}