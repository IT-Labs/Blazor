using System;
using System.Linq.Expressions;
using BlazorApp.Shared.Validation;

namespace Core.Framework.Validation
{
    /// <summary>
    ///     Definition of Validation Rule
    /// </summary>
    /// <typeparam name="TEntity">Entity on which this validation rule is implemented</typeparam>
    public class ValidationRule<TEntity> : IValidationRule<TEntity>
    {
        private readonly Predicate<TEntity> _rule;

        /// <summary>
        ///     Constructor of the ValidationRule
        /// </summary>
        /// <param name="name">Name of the validation rule</param>
        /// <param name="rule">delegate of the rule used for linq queries</param>
        /// <param name="message">message of the validation rule</param>
        public ValidationRule(string name, Predicate<TEntity> rule, string message)
        {
            Name = name;
            Message = message;
            _rule = rule;
        }

        /// <summary>
        ///     Gets the name of the validation rule
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///     Gets the message of the validation rule
        /// </summary>
        public string Message { get; }

        /// <summary>
        ///     Execute the validation rule
        /// </summary>
        /// <param name="entity">Entity on which this validation rule is implemented</param>
        /// <param name="result">Validaiont result that is built with the rule</param>
        /// <returns>Wheter the rule is OK or not</returns>
        public bool Run(TEntity entity, ValidationResult result)
        {
            if (_rule(entity))
            {
                return false;
            }
            result.AddValidationError(Name, Message);
            return true;
        }
    }


    /// <summary>
    ///     Extends Validation Rule
    /// </summary>
    /// <typeparam name="TEntity">Entity on which this validation rule is implemented</typeparam>
    /// <typeparam name="TProperty">Property that needs to be validated against</typeparam>
    public class ValidationRule<TEntity, TProperty> : ValidationRule<TEntity>
    {
        /// <summary>
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="rule"></param>
        /// <param name="message"></param>
        public ValidationRule(Expression<Func<TEntity, TProperty>> expression, Predicate<TEntity> rule, string message)
            : base(ValidationExtensions.GetPropertyName(expression), rule, message)
        {
        }
    }
}