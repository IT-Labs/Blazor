using System;
using Core.Shared.Interfaces;
using Core.Shared.Validation;

namespace Core.Framework.Validation
{
    /// <summary>
    ///     Custom validator base class inherits. Extends ValidatorBase
    /// </summary>
    /// <typeparam name="TEntity">type of the Entity that is validated</typeparam>
    public abstract class CustomValidatorBase<TEntity> : ValidatorBase<TEntity>
    {  
        /// <summary>
        ///     Validates the entity
        /// </summary>
        /// <param name="entity">type of entiti that is being validated</param>
        /// <returns>Result of validation</returns>
        public override ValidationResult ExecuteValidation(TEntity entity)
        {
            var results = base.ExecuteValidation(entity);
            if (results.State == ValidationStatus.Valid)
            {
                RunCustomValidation(entity, results);
            }

            return results;
        }

        /// <summary>
        ///     Enables custom validation procedures to be run by this function
        /// </summary>
        /// <param name="entity">Validating entity</param>
        /// <param name="result">Validaiton result</param>
        protected abstract void RunCustomValidation(TEntity entity, ValidationResult result);
    }
}