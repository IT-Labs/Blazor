using Core.Shared.Interfaces;

namespace Core.Shared.Repository
{
    /// <summary>
    ///     Definition of Repository
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        ///     This function implementation needs to be used when executing command
        /// </summary>
        /// <typeparam name="TResult">type of the command response</typeparam>
        /// <typeparam name="TRequest">type of the command request</typeparam>
        /// <param name="command">uses ICommant having same output type as the return value</param>
        /// <returns>Definition of IResult</returns>
        IResult<TResult> ExecuteCommand<TRequest, TResult>(ICommand<TRequest, TResult> command) where TRequest : IRequest;

        /// <summary>
        ///     This function imnplementation needs to be used when executing query
        /// </summary>
        /// <typeparam name="TResult">type of the query response</typeparam>
        /// <typeparam name="TRequset">type of the query request</typeparam>
        /// <param name="query">uses IQuery having same output type as the return value</param>
        /// <returns>Definition of IResult</returns>
        IResult<TResult> ExecuteQuery<TRequest, TResult>(IQuery<TRequest, TResult> query) where TRequest : IRequest;

        /// <summary>
        ///     This function implementation needs to be used when executing query
        /// </summary>
        /// <typeparam name="TResult">type of the query</typeparam>
        /// <typeparam name="TRequest"></typeparam>
        /// <param name="query">uses IListQuery having same output type as the return value</param>
        /// <returns>Definition of IListResult</returns>
        IListResult<TResult> ExecuteQuery<TRequest, TResult>(IListQuery<TRequest, TResult> query) where TRequest : IRequest;

    }
}