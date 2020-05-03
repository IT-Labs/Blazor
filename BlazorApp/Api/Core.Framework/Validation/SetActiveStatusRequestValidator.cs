using Core.Shared;
using Core.Shared.Requests;
using Core.Shared.Validation;
using Core.Shared.ValidationCodes;
using Core.Framework.Repository;
using Core.Framework.Repository.Queries.Entities;
using FluentValidation;

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
    }
}

