namespace BlazorApp.Shared.Validation
{
    /// <summary>
    ///     Definition of validation rule
    /// </summary>
    /// <typeparam name="TEntity">Entity on which this validation rule is implemented</typeparam>
    public interface IValidationRule<in TEntity>
    {
        /// <summary>
        ///     Get or sets the name of the validatio nrule
        /// </summary>
        string Name { get; }

        /// <summary>
        ///     Gets or sets the messages of the validation rule
        /// </summary>
        string Message { get; }

        /// <summary>
        ///     Runs the validatio nrule
        /// </summary>
        /// <param name="entity">Entity on which this validation rule is implemented</param>
        /// <param name="validationResult">Result of the validation</param>
        /// <returns>Validation is ok or not</returns>
        bool Run(TEntity entity, ValidationResult validationResult);
    }
}