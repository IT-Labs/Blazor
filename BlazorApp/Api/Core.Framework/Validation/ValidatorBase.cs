using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using Core.Shared;
using Core.Shared.Enums;
using Core.Shared.Interfaces;
using Core.Shared.Repository;
using Core.Shared.Requests;
using Core.Shared.Validation;
using Core.Framework.Extensions;
using Core.Framework.Repository;
using Core.Framework.Repository.Queries;
using FluentValidation;

namespace Core.Framework.Validation
{
    /// <summary>
    ///     Basic validation description
    /// </summary>
    /// <typeparam name="TEntity">Entity on which this validation rule is implemented</typeparam>
    public abstract class ValidatorBase<TEntity> : AbstractValidator<TEntity>, Core.Shared.Validation.IValidator<TEntity>
    {
        public IUserContextInfo UserContext { get; set; }
        public long? UserId => UserContext?.UserId;
        /// <summary>
        ///     List of validation rules
        /// </summary>
        //protected IList<IValidationRule<TEntity>> Rules = new List<IValidationRule<TEntity>>();

        /// <summary>
        ///     Function to validate the rules
        /// </summary>
        /// <param name="entity">Entity on which this validation rule is implemented</param>
        /// <returns>Result of the validation</returns>
        public virtual ValidationResult ExecuteValidation(TEntity entity)
        {
            var results = new ValidationResult();
            results.Merge(base.Validate(entity));
            return results;
        }

        protected override bool PreValidate(ValidationContext<TEntity> context, FluentValidation.Results.ValidationResult result)
        {
            if (context.InstanceToValidate == null)
            {
                result.Errors.Add(new FluentValidation.Results.ValidationFailure("", "Please ensure a model was supplied."));
                return false;
            }
            return true;
        }

        /// <summary>
        ///     Adds a validation error to the result.
        /// </summary>
        /// <param name="errorName">The name of the validation error.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="args">The error message arguments.</param>
        public void AddValidationError(ValidationResult result, string errorName, string errorMessage, params object[] args)
        {
            result.SetDirty();
            result.Messages.Add(new ValidationMessage(errorName, errorMessage, args));
        }
        public void AddValidationError(ValidationResult result, Enum errorCode, string entityName = "",string propertyName ="", params object[] args)
        {
            var description = errorCode.GetDescription().Replace("{EntityName}", entityName.SplitCamelCase()).Replace("{PropertyName}", propertyName.SplitCamelCase());
            result.SetDirty();
            result.Messages.Add(new ValidationMessage(errorCode.ToString(), description, args));
        }

        public void AddValidationError<T>(ValidationResult result, DomainRepository repository, Expression<Func<T, bool>> predicate, Enum enumCode, bool invertCondition = false, IQueryInclude<T> queryInclude = null, string entityName = null)
            where T : AuditableEntity, new()
        {
            var query = new CheckPredicateQuery<T>(invertCondition)
            {
                ContextRequest = new ContextRequest<EmptyRequest>(UserContext),
                Predicate = predicate,
                QueryInclude = queryInclude
            };
            var queryResult = repository.ExecuteQuery(query);

            if (queryResult.Status == HttpStatusCode.OK && queryResult.Value)
            {
                result.AddValidationError(enumCode, entityName ?? typeof(T).Name.SplitCamelCase());
            }
        }
        /// <summary>
        ///     Gets or Sets definition of Validation messages
        /// </summary>
        public List<ValidationMessage> Messages { get; set; } = new List<ValidationMessage>();

        /// <summary>
        ///     Gets whether the validation result is valid.
        /// </summary>
       // public ValidationStatus State => Messages.Any() ? ValidationStatus.Invalid : ValidationStatus.Valid;

        protected IResult<T> Get<T>(DomainRepository repository, Expression<Func<T, bool>> filter) where T : class, IEntity
            => repository.ExecuteQuery(new GetWithFuncQuery<T, EmptyRequest>(filter));

        protected bool Any<T>(DomainRepository repository, Expression<Func<T, bool>> predicate, IQueryInclude<T> queryInclude = null) where T : AuditableEntity, new()
        {
            var query = new CheckPredicateQuery<T>()
            {
                ContextRequest = new ContextRequest<EmptyRequest>(UserContext),
                Predicate = predicate,
                QueryInclude = queryInclude
            };
            var queryResult = repository.ExecuteQuery(query);

            return queryResult.Status == HttpStatusCode.OK && queryResult.Value;
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