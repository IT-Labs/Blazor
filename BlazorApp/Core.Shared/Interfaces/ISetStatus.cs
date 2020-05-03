using Core.Shared.Requests;
using Core.Shared.Response;

namespace Core.Shared.Interfaces
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