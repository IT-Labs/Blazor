using Core.Shared.Interfaces;
using Core.Shared.Requests;

namespace Core.Shared.Repository
{
    /// <summary>
    ///     Definition of List Query
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TRequest"></typeparam>
    public interface IListQuery<TRequest, TResult> where TRequest : IRequest
    {
        /// <summary>
        ///     Execute
        /// </summary>
        /// <param name="dataContext">Database context</param>
        /// <returns>Definitino of ListResult</returns>
        IListResult<TResult> Execute(IContext dataContext);

        /// <summary>
        /// Query request
        /// </summary>
        TRequest Request { get; }

        ContextRequest<TRequest> ContextRequest { get; set; }
    }
}