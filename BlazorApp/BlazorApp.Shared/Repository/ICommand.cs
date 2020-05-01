using BlazorApp.Shared.Interfaces;
using BlazorApp.Shared.Requests;
using System.Collections.Generic;

namespace BlazorApp.Shared.Repository
{
    /// <summary>
    ///     Definition of Command
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TRequest"></typeparam>
    public interface ICommand<TRequest, TResult> where TRequest : IRequest
    {
        /// <summary>
        /// Command Request
        /// </summary>
        TRequest Request { get; set; }

        /// <summary>
        /// Wrapped Request
        /// </summary>
        ContextRequest<TRequest> WrappedRequest { get; set; }

        /// <summary>
        ///     Execute
        /// </summary>
        /// <param name="dataContext">Database context</param>
        /// <returns>Definition of Result</returns>
        IResult<TResult> Execute(IContext dataContext);

        /// <summary>
        ///     Update Entity
        /// </summary>
        /// <param name="dataContext">Database context</param>
        /// <param name="T">Entity to update</param>
        /// <returns>IResult from bool</returns>
        IResult<long> Update<T>(IContext dataContext, T entity) where T : AuditableEntity;

        /// <summary>
        ///     Insert Entity
        /// </summary>
        /// <param name="dataContext">Database context</param>
        /// <param name="T">Entity to update</param>
        /// <returns>IResult from bool</returns>
        IResult<long> Insert<T>(IContext dataContext, T entity) where T : AuditableEntity;

        /// <summary>
        ///     Update Multiple Entity
        /// </summary>
        /// <param name="dataContext">Database context</param>
        /// <param name="T">Entity to update</param>
        /// <returns>IResult from bool</returns>
        IResult<bool> UpdateMultiple<T>(IContext dataContext, List<T> entities) where T : AuditableEntity;
    }
}