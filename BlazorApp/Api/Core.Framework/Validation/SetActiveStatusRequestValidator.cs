using Core.Shared;
using Core.Shared.Requests;
using Core.Shared.Validation;
using Core.Shared.ValidationCodes;
using Core.Framework.Repository;
using Core.Framework.Repository.Queries.Entities;
using FluentValidation;
using Core.Framework.Repository.Queries;
using System.Net;
using System.Linq.Expressions;
using System;
using Core.Shared.Repository;

namespace Core.Framework.Validation
{
    public class SetActiveStatusRequestValidator<TEntity> : ValidatorBase<SetActiveStatusRequest<TEntity>> where TEntity :  DeletableEntity, new()
    {
        private readonly DomainRepository _repository;
        public SetActiveStatusRequestValidator(DomainRepository repository)
        {
            _repository = repository;

            RuleFor(x => x)
                .Must(x => NotAny(_repository, QueryPredicates.ChangedStatus<TEntity>(x.Id, x.IsActive)))
                .WithError(ValidationCodes.Common.Cmn044, values: typeof(TEntity).Name);
        }

        protected bool NotAny<T>(DomainRepository repository, Expression<Func<T, bool>> predicate, IQueryInclude<T> queryInclude = null) where T : AuditableEntity, new()
        {
            var query = new CheckPredicateQuery<T>(true)
            {
                ContextRequest = new ContextRequest<EmptyRequest>(UserContext),
                Predicate = predicate,
                QueryInclude = queryInclude
            };
            var queryResult = repository.ExecuteQuery(query);

            return queryResult.Status == HttpStatusCode.OK && queryResult.Value;
        }
    }
}

