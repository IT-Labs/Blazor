using Core.Shared;
using Core.Shared.Interfaces;
using Core.Shared.Managers;
using Core.Shared.Repository;
using Core.Shared.Requests;
using Core.Shared.Response;
using Core.Framework.Extensions;
using Core.Framework.Repository;
using Core.Framework.Repository.Queries;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Core.Framework.Domain
{
    public abstract class CoreManager<TEntity, TRequest, TSortColumn> : BaseManager,
          IManager<TEntity, TRequest, TSortColumn>
          where TRequest : SortablePageableRequest<TSortColumn>, IRequest
          where TEntity : DeletableEntity, new()
          where TSortColumn : struct
    {
        protected Func<TRequest, List<IQueryFilter<TEntity>>> QueryFiltersFunc { get; set; }
        private readonly IQueryInclude<TEntity> _queryInclude;
        private readonly Dictionary<TSortColumn, Expression<Func<TEntity, object>>> _sortDictionary;

        protected CoreManager(DomainRepository repository,
            IQueryInclude<TEntity> queryInclude, Dictionary<TSortColumn, Expression<Func<TEntity, object>>> sortDictionary,
             ILogger<CoreManager<TEntity, TRequest, TSortColumn>> logger)
             : base(repository, logger)
        {
            _queryInclude = queryInclude;
            _sortDictionary = sortDictionary;
        }

        public virtual Response<TEntity> Get(IdRequest request)
        {
            return Get(request, _queryInclude);
        }

        public virtual Response<string> GetJSON(IdPartialRequest request, IQueryInclude<TEntity> includeQuery = null)
        {
            var response = new Response<string>();
            var queryInclude = new QueryInclude<TEntity>(request);

            var result = Get(request, queryInclude.IsValid ? queryInclude : includeQuery ?? _queryInclude);
            response.Merge(result);

            return response;
        }

        public virtual Response<TEntity> Get(IdRequest request, IQueryInclude<TEntity> includeQuery)
        {
            return base.Get(request, includeQuery ?? _queryInclude);
        }

        public virtual PagedResponse<TEntity> GetMultiple(TRequest request)
        {
            return GetMultiple(request, _queryInclude);
        }

        public virtual PagedResponse<TEntity> GetMultiple(TRequest request,
            IQueryInclude<TEntity> queryInclude,
            List<IQueryFilter<TEntity>> queryFilters = null)
        {
            var query = new GetMultipleQuery<TRequest, TEntity, TSortColumn>
            {
                QueryInclude = queryInclude ?? _queryInclude,
                QueryFilters = queryFilters ?? QueryFilters(request),
                SortDictionary = _sortDictionary,
                ContextRequest = WrapRequest(request)
            };

            return GetMultiple(request, query);
        }

        public virtual Response<long> Save<TSaveRequest>(TSaveRequest request, IQueryInclude<TEntity> includeQuery = null)
            where TSaveRequest : SaveRequest
        {
            return base.Save(request, includeQuery ?? _queryInclude);
        }

        private List<IQueryFilter<TEntity>> QueryFilters(TRequest request)
        {
            List<IQueryFilter<TEntity>> filters = new List<IQueryFilter<TEntity>>();
            if (QueryFiltersFunc != null)
            {
                filters = QueryFiltersFunc(request);
            }
            return filters;
        }

        public virtual Response<bool> SetStatus(SetActiveStatusRequest<TEntity> request)
        {
            var response = new Response<bool>();
            if (request.IsRequestInvalid(response, ContextInfo))
            {
                return response;
            }

            return SetStatus<TEntity>(request);
        }


        public virtual Response<bool> SaveMultiple<TSaveRequest>(List<TSaveRequest> request, IQueryInclude<TEntity>  includeQuery = null, bool stopOnFirstInvalidRequest = false) 
           where TSaveRequest : SaveRequest
        {
            if (request == null)
                return Response<bool>.BadRequest();

            var response = new Response<bool>();

            foreach (var t in request)
            {
                t.IsRequestInvalid(response, ContextInfo);
                if (response.NotOk && stopOnFirstInvalidRequest) 
                    return response;
            }

            if (response.Messages.Any())
                return response;

            request.ForEach(x => 
            {
                response.Merge(Save(x, includeQuery));
            });
            response.Payload = !response.Messages.Any();
            return response;
        }
    }
}