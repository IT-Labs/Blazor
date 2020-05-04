using Core.Shared.Interfaces;
using FluentValidation;

namespace Core.Shared.Validation
{
    /// <summary>
    ///     Basic validation description
    /// </summary>
    /// <typeparam name="TEntity">Entity on which this validation rule is implemented</typeparam>
    public abstract class ValidatorBase<TEntity> : AbstractValidator<TEntity>, IValidator<TEntity>
    {
        public IUserContextInfo UserContext { get; set; }
        public long? UserId => UserContext?.UserId;

        protected override bool PreValidate(ValidationContext<TEntity> context, FluentValidation.Results.ValidationResult result)
        {
            if (context.InstanceToValidate == null)
            {
                result.Errors.Add(new FluentValidation.Results.ValidationFailure("", "Please ensure a model was supplied."));
                return false;
            }
            return true;
        }
    }
}