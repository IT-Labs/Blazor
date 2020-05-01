using BlazorApp.Shared.Interfaces;
using BlazorApp.Shared.Requests;

namespace BlazorApp.Shared.Managers
{
    /// <summary>
    /// 
    /// </summary>

    public interface IManager<TEntity, TRequest, TSortColumn> : IManager,
        ISave<TEntity>,
        IGet<TEntity, TRequest>,
        ISetStatus<TEntity>
        where TRequest : SortablePageableRequest<TSortColumn>, IRequest
        where TEntity : DeletableEntity, new()
        where TSortColumn : struct
    {

    }
    /// <summary>
    /// 
    /// </summary>
    public interface IManager : ISetStatus, ISave, IGet
    {
        void SetContextInfo(IUserContextInfo contextInfo);
    }
}