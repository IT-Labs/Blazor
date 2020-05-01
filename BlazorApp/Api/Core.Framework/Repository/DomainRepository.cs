using System;
using System.Net;
using BlazorApp.Shared.Interfaces;
using BlazorApp.Shared.Repository;
using Core.Framework.Extensions;
using Microsoft.Extensions.Logging;

namespace Core.Framework.Repository
{
    /// <summary>
    ///     Abstracted repository for the domain
    /// </summary>
    public abstract class DomainRepository : IRepository, IDisposable
    {
        private readonly IContext _dataContext;
        private readonly ILogger _logger;

        /// <summary>
        ///     Base constructor
        /// </summary>
        /// <param name="dataContext">Database context</param>
        /// <param name="logger">Logger</param>
        protected DomainRepository(IContext dataContext, ILogger logger)
        {
            _dataContext = dataContext;
            _logger = logger;
        }

        /// <summary>
        ///     Definition of returning result
        /// </summary>
        /// <typeparam name="TResult">Type of the returning result</typeparam>
        /// <typeparam name="TRequest">Type of the command request</typeparam>
        /// <param name="command">Executing command</param>
        /// <returns>Returning result</returns>
        public virtual IResult<TResult> ExecuteCommand<TRequest, TResult>(ICommand<TRequest, TResult> command)
            where TRequest : IRequest
        {
            IResult<TResult> result = null;

            try
            {
                result = command.Execute(_dataContext);
            }
            catch (Exception exception)
            {
                result = HandleException(result, exception, command);
            }

            return result;
        }

        /// <summary>
        ///     Definition of returning result
        /// </summary>
        /// <typeparam name="TResult">Type of the returning result</typeparam>
        /// <typeparam name="TRequest"></typeparam>
        /// <param name="query">Executing query</param>
        /// <returns>Returning result</returns>
        public virtual IResult<TResult> ExecuteQuery<TRequest, TResult>(IQuery<TRequest, TResult> query) where TRequest : IRequest
        {
            IResult<TResult> result = null;

            try
            {
                result = query.Execute(_dataContext);
            }
            catch (Exception exception)
            {
                result = HandleException(result, exception, query);
            }

            return result;
        }

        /// <summary>
        ///     Abstracting execution of the query
        /// </summary>
        /// <typeparam name="TResult">Returning result</typeparam>
        /// <typeparam name="TRequest"></typeparam>
        /// <param name="query">the execution query</param>
        /// <returns>List of results</returns>
        public virtual IListResult<TResult> ExecuteQuery<TRequest, TResult>(IListQuery<TRequest, TResult> query) where TRequest : IRequest
        {
            IListResult<TResult> result = null;

            try
            {
                result = query.Execute(_dataContext);
            }
            catch (Exception exception)
            {
                result = HandleException(result, exception, query);
            }

            return result;
        }


        private IResult<TResult> HandleException<TResult>(IResult<TResult> result, Exception exception, object command)
        {
            if (result == null)
            {
                result = new Result<TResult>();
            }

            _logger.LogError(default(EventId), exception, $"InternalServerError: {command.GetType().FullName}");

            result.Status = HttpStatusCode.InternalServerError;
            result.Errors.Add(exception.GetExceptionResponse());
            return result;
        }

        private IListResult<TResult> HandleException<TResult>(IListResult<TResult> result, Exception exception, object command)
        {
            if (result == null)
            {
                result = new ListResult<TResult>(null);
            }

            _logger.LogError(default(EventId), exception, $"InternalServerError: {command.GetType().FullName}");

            result.Status = HttpStatusCode.InternalServerError;
            result.Errors.Add(exception.GetExceptionResponse());
            return result;
        }


        public void Dispose()
        {
            if (_dataContext != null)
            {
                _dataContext.Dispose();
                _logger.LogDebug("Context disposed");
            }
        }
    }
}