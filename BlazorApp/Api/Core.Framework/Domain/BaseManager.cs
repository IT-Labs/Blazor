using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.Shared;
using Core.Shared.Enums;
using Core.Shared.Interfaces;
using Core.Shared.Managers;
using Core.Shared.Repository;
using Core.Shared.Requests;
using Core.Shared.Requests.Histories;
using Core.Shared.Response;
using Core.Framework.Extensions;
using Core.Framework.Repository;
using Core.Framework.Repository.Commands;
using Core.Framework.Repository.Filters;
using Core.Framework.Repository.Queries;
using Core.Framework.Repository.Queries.Histories;
using Microsoft.Extensions.Logging;

namespace Core.Framework.Domain
{
    public abstract class BaseManager : IManager
    {
        protected readonly DomainRepository Repository;
        public ILogger<BaseManager> Logger { get; }

        protected BaseManager(DomainRepository repository, ILogger<BaseManager> logger)
        {
            Repository = repository;
            Logger = logger;
            ContextInfo = new UserContextInfo();
        }

        public long? UserId => ContextInfo.UserId;
        public string Username => ContextInfo.Username;

        public IUserContextInfo ContextInfo { get; private set; }

        public void SetContextInfo(IUserContextInfo contextInfo)
        {
            ContextInfo = contextInfo;
        }

        public Response<bool> SetStatus<T>(IdRequest request, bool isActive) where T : DeletableEntity
        {
            var setActive = new SetActiveStatusRequest<T> { Id = request.Id, IsActive = isActive };

            return SetStatus(setActive);
        }
        public virtual Response<bool> SetStatus<T>(SetActiveStatusRequest<T> request) where T : DeletableEntity
        {
            Logger.LogTrace($"Entering SetStatus for {typeof(T).Name} for id: {request.Id} set it to {request.IsActive}. By user {Username}");
            var response = new Response<bool>();
            if (request.IsRequestInvalid(response, ContextInfo))
            {
                return response;
            }

            var command = new SetEntityIsActiveCommand<T>
            {
                WrappedRequest = WrapRequest(request)
            };
            var result = Repository.ExecuteCommand(command);
            response.Merge(result);

            return response;
        }

        public Response<long> Save<T, TRequest>(TRequest request, IQueryInclude<T> includeQuery = null)
        where T : AuditableEntity, new()
        where TRequest : SaveRequest
        {

            Logger.LogTrace($"{(request.IsNew ? $"Entering Save new {typeof(T).Name} " : $"Entering Save {typeof(T).Name} with id: {request.Id}")}. By user {Username}");
            var response = new Response<long>();

            if (request.IsRequestInvalid(response, ContextInfo))
            {
                return response;
            }
            var mapper = ContainerFactory.GetMap<T, ContextRequest<TRequest>>()
                ?? new DefaultMapping<T, TRequest>();


            var command = new SaveEntityCommand<T, TRequest>
            {
                WrappedRequest = WrapRequest(request),
                IncludeQuery = includeQuery,
                MapAction = (x, y) => mapper.Map(x, y)

            };
            var result = Repository.ExecuteCommand(command);
            response.Merge(result);

            return response;
        }

        public Response<T> Get<T>(IdRequest request, IQueryInclude<T> includeQuery = null) where T : DeletableEntity
        {
            Logger.LogTrace($"Entering Get {typeof(T).Name} by id: {request.Id}. By user {Username}");
            var query = new GetQuery<T, IdRequest>
            {
                IncludeQuery = includeQuery
            };

            query.QueryFilters.Add(new IsActiveAndByIdFilter<T>(request.Id, true));

            return Get(request, query);
        }

        public Response<T> Get<T>(IdRequest request, GetQuery<T, IdRequest> query)
            where T : class, IEntity
        {
            var response = new Response<T>();
            if (request.IsRequestInvalid(response, ContextInfo))
            {
                return response;
            }

            query.ContextRequest = WrapRequest(request);
            var result = Repository.ExecuteQuery(query);
            response.Merge(result);
            return response;
        }

        public PagedResponse<TResult> ExecuteCustomListQuery<TQuery, TRequest, TResult>(TRequest request)
            where TRequest : IRequest
            where TQuery : BaseListQuery<TRequest, TResult>, new()
        {
            Logger.LogTrace($"Entering execute of custom list query: {typeof(TQuery).Name}. By user {Username}");
            var response = new PagedResponse<TResult>();
            if (request.IsRequestInvalid(response, ContextInfo))
            {
                return response;
            }
            var query = new TQuery { ContextRequest = WrapRequest(request) };
            var result = Repository.ExecuteQuery(query);
            response.Merge(result);

            return response;
        }
        public PagedResponse<TEntity> ExecuteCustomListQuery<TEntity, TRequest, TSortColumn>(TRequest request,
            Dictionary<TSortColumn, Expression<Func<TEntity, object>>> sortDictionary,
            Expression<Func<TEntity, bool>> func = null,
            IQueryInclude<TEntity> include = null)
            where TEntity : AuditableEntity
            where TRequest : SortablePageableRequest<TSortColumn>, IRequest
            where TSortColumn : struct
        {
            Logger.LogTrace($"Entering execute of custom list query: {typeof(TEntity).Name}. By user {Username}");
            var response = new PagedResponse<TEntity>();
            if (request.IsRequestInvalid(response, ContextInfo))
            {
                return response;
            }
            var query = new GetMultipleWithFuncQuery<TRequest, TEntity, TSortColumn>(func)
            { 
                ContextRequest = WrapRequest(request), 
                SortDictionary = sortDictionary,
                QueryInclude = include
            };
            var result = Repository.ExecuteQuery(query);
            response.Merge(result);

            return response;
        }

        public Response<TResult> ExecuteCustomQuery<TQuery, TRequest, TResult>(TRequest request)
            where TRequest : IRequest
            where TQuery : BaseQuery<TRequest, TResult>, new()
        {
            Logger.LogTrace($"Entering execute of custom query: {typeof(TQuery).Name}. By user {Username}");
            var response = new Response<TResult>();
            if (request.IsRequestInvalid(response, ContextInfo))
            {
                return response;
            }
            var query = new TQuery { ContextRequest = WrapRequest(request) };
            var result = Repository.ExecuteQuery(query);
            response.Merge(result);

            return response;
        }
        public Response<TEntity> ExecuteCustomQuery<TEntity, TRequest>(TRequest request, Expression<Func<TEntity, bool>> func, IQueryInclude<TEntity> include = null)
            where TRequest : IRequest
            where TEntity : AuditableEntity
        {
            Logger.LogTrace($"Entering execute of custom query: {typeof(TEntity).Name}. By user {Username}");
            var response = new Response<TEntity>();
            if (request.IsRequestInvalid(response, ContextInfo))
            {
                return response;
            }

            var query = new GetWithFuncQuery<TEntity, TRequest>(func)
            {
                ContextRequest = WrapRequest(request)
                ,
                IncludeQuery = include
            };
            var result = Repository.ExecuteQuery(query);
            response.Merge(result);

            return response;
        }

        public Response<TResult> ExecuteCustomCommand<TCommand, TRequest, TResult>(TRequest request)
            where TRequest : IRequest
            where TCommand : BaseCommand<TRequest, TResult>, new()
        {
            Logger.LogTrace($"Entering execute of custom command: {typeof(TCommand).Name}. By user {Username}");
            var response = new Response<TResult>();
            if (request.IsRequestInvalid(response, ContextInfo))
            {
                return response;
            }
            var command = new TCommand { WrappedRequest = WrapRequest(request) };
            var result = Repository.ExecuteCommand(command);
            response.Merge(result);

            return response;
        }

        public PagedResponse<TResult> GetSearchResults<TRequest, TEntity, TSortColumn, TResult>(TRequest request,
            SearchQuery<TRequest, TEntity, TSortColumn, TResult> query)
             where TRequest : SortablePageableRequest<TSortColumn>, IRequest
        where TEntity : AuditableEntity
        where TSortColumn : struct
        where TResult : class
        {
            Logger.LogTrace($"Entering GetSearchResults of {typeof(TEntity).Name}. By user {Username}");
            var response = new PagedResponse<TResult>();
            if (request.IsRequestInvalid(response, ContextInfo))
            {
                return response;
            }
            query.ContextRequest = WrapRequest(request);

            var result = Repository.ExecuteQuery(query);
            response.Merge(result);

            return response;
        }

        public PagedResponse<TEntity> GetMultiple<TRequest, TEntity, TSortColumn>(TRequest request,
            GetMultipleQuery<TRequest, TEntity, TSortColumn> query)
        where TRequest : SortablePageableRequest<TSortColumn>, IRequest
        where TEntity : AuditableEntity
        where TSortColumn : struct
        {
            Logger.LogTrace($"GetMultiple of {typeof(TEntity).Name}. By user {Username} ");
            var response = new PagedResponse<TEntity>();
            if (request.IsRequestInvalid(response, ContextInfo))
            {
                return response;
            }

            query.ContextRequest = WrapRequest(request);
            var result = Repository.ExecuteQuery(query);
            response.Merge(result);


            return response;
        }

        public ContextRequest<T> WrapRequest<T>(T request) where T : IRequest
        {
            return new ContextRequest<T>(ContextInfo)
            {
                Request = request
            };
        }

        public ContextRequest<EmptyRequest> EmptyRequest()
        {
            var request = new EmptyRequest();
            return WrapRequest(request);
        }

        public Response<bool> HardDelete<T>(IdRequest request)
      where T : AuditableEntity
        {
            Logger.LogTrace($"Hard delete of {typeof(T).Name} by id: {request.Id}. By user {Username}");
            var response = new Response<bool>();

            if (request.IsRequestInvalid(response, ContextInfo))
            {
                return response;
            }

            var command = new DeleteEntityCommand<T> { WrappedRequest = WrapRequest(request) };
            var result = Repository.ExecuteCommand(command);
            response.Merge(result);

            return response;
        }

        public Response<HistoryData> GetHistory<T>(GetHistoryRequest request) where T : DeletableEntity
        {
            return ExecuteCustomQuery<GetHistoryQuery<T>, GetHistoryRequest, HistoryData>(request);
        }

        public PagedResponse<HistoryData> SearchHistory<T>(SearchHistoryRequest request) where T : DeletableEntity
        {
            return ExecuteCustomListQuery<SearchHistoryQuery<T>, SearchHistoryRequest, HistoryData>(request);
        }
    }
}
