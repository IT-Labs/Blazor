using BlazorApp.Shared.Interfaces;
using BlazorApp.Shared.Requests;

namespace BlazorApp.Shared.Repository
{
    /// <summary>
    ///     Definition of Query
    /// </summary>
    /// <typeparam name="TResult">type of the query response</typeparam>
    /// <typeparam name="TRequest">type of the query request</typeparam>
    public interface IQuery<TRequest, TResult> where TRequest : IRequest
    {
        /// <summary>
        ///     Execute
        /// </summary>
        /// <param name="dataContext">Database context</param>
        /// <returns>Definition of IResult</returns>
        IResult<TResult> Execute(IContext dataContext);

        /// <summary>
        /// Query request
        /// </summary>
        TRequest Request { get;  }

        ContextRequest<TRequest> ContextRequest { get; set; }
    }
}