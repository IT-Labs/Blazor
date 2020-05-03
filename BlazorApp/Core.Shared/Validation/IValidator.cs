namespace Core.Shared.Validation
{
    /// <summary>
    ///     Definition of Validator
    /// </summary>
    public interface IValidator
    {
    }

    /// <summary>
    ///     Extended definition of Validator
    /// </summary>
    /// <typeparam name="TEntity">Entity on which this validation rule is implemented</typeparam>
    public interface IValidator<in TEntity> : IValidator
    {
        /// <summary>
        ///     Function to validate the rule
        /// </summary>
        /// <param name="entity">Entity on which this validation rule is implemented</param>
        /// <returns>ValidationResult value</returns>
        ValidationResult ExecuteValidation(TEntity entity);
    }
}