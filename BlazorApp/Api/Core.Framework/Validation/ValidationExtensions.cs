using System;
using System.Linq.Expressions;
using Core.Shared.Validation;
using Core.Framework.Extensions;
using FluentValidation;

namespace Core.Framework.Validation
{
    /// <summary>
    ///     Extends Validation
    /// </summary>
    public static class ValidationExtensions
    {
        public static void AddValidationError(this ValidationResult result, Enum errorCode, string entityName = "", params object[] args)
        {
            result.SetDirty();
            var description = errorCode.GetDescription().Replace("{EntityName}", entityName.SplitCamelCase());

            result.Messages.Add(new ValidationMessage(errorCode.ToString(), description, args));
        }

        public static void AddValidationError(this ValidationResult result, Enum errorCode, string propertyName, string propertyValue, params object[] args)
        {
            result.SetDirty();
            var description = errorCode.GetDescription().Replace("{PropertyName}", propertyName).Replace("{PropertyValue}", propertyValue);

            result.Messages.Add(new ValidationMessage(errorCode.ToString(), description, args));
        }

        public static void AddValidationError(this ValidationResult result, string description, Enum errorCode, params object[] args)
        {
            result.SetDirty();
            result.Messages.Add(new ValidationMessage(errorCode?.ToString(), description, args));
        }

        /// <summary>
        ///     Retrieves property name of the validation entity
        /// </summary>
        /// <typeparam name="TModel">Entity that is being validated</typeparam>
        /// <typeparam name="TProperty">Required property of the validated entity</typeparam>
        /// <param name="expression">Query parameters</param>
        /// <returns>Name of the property as string</returns>
        public static string GetPropertyName<TModel, TProperty>(Expression<Func<TModel, TProperty>> expression)
        {
            var memberExp = expression.Body as MemberExpression;
            if (memberExp != null)
            {
                return memberExp.Member.Name;
            }

            if (expression.Body.NodeType == ExpressionType.Call)
            {
                var body = (MethodCallExpression)expression.Body;
                return GetPropertyName(body).Substring(expression.Parameters[0].Name.Length + 1);
            }

            return expression.Body.ToString().Substring(expression.Parameters[0].Name.Length + 1);
        }

        /// <summary>
        ///     Retrieves property name of the validation entity
        /// </summary>
        /// <param name="expression">Linq expression</param>
        /// <returns>Name of the property as string</returns>
        public static string GetPropertyName(MethodCallExpression expression)
        {
            var expression2 = expression.Object as MethodCallExpression;

            if (expression2 != null)
            {
                return GetPropertyName(expression2);
            }

            return expression.Object.ToString();
        }

        public static void Merge(this ValidationResult results, FluentValidation.Results.ValidationResult validationResult)
        {
            if (validationResult == null) return;
            if (!validationResult.IsValid)
            {
                foreach (var validationError in validationResult.Errors)
                {
                    results.Messages.Add(new ValidationMessage(validationError.ErrorCode, validationError.ErrorMessage));
                }
            }
        }

        public static IRuleBuilderOptions<T, K> WithError<T, K>(this IRuleBuilderOptions<T, K> rule, Enum value, string entityName = "", params string[] values) 
        {
            var description = value.GetDescription();
            for (int i = 0; i < values.Length; i++)
            {
                description = description.Replace($"{{{i}}}", values[i]);
            }

            description = description.Replace("{EntityName}", entityName.SplitCamelCase());
            return rule.WithErrorCode(value.ToString()).WithMessage(description);
        }
    }
}