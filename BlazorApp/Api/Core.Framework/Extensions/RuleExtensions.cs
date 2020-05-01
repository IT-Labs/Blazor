using System;
using System.Collections.Generic;
using BlazorApp.Shared.Validation;
using Core.Framework.Validation;

namespace Core.Framework.Extensions
{
    public static class RulesExtensions
    {
        public static void AddRule<TEntity>(this IList<IValidationRule<TEntity>> rules, Enum code,
            Predicate<TEntity> rule, string propertyName = "", string numberOfCharacters = "", string propertyValue = "")
        {
            var description = code.GetDescription().Replace("{EntityName}", typeof(TEntity).Name.SplitCamelCase()).
                Replace("{PropertyName}", propertyName.SplitCamelCase()).
                 Replace("{NumberOfCharacters}", numberOfCharacters).
                   Replace("{PropertyValue}", propertyValue);

            rules.Add(new ValidationRule<TEntity>(code.ToString(), rule, description));
        }

        public static void AddRule<TEntity>(this IList<IValidationRule<TEntity>> rules, Predicate<TEntity> rule, 
            Enum code, string propertyName = "", string numberOfCharacters = "", string propertyValue = "")
        {
            var description = code.GetDescription().Replace("{EntityName}", typeof(TEntity).Name.SplitCamelCase()).
                Replace("{PropertyName}", propertyName.SplitCamelCase()).
                 Replace("{NumberOfCharacters}", numberOfCharacters).
                   Replace("{PropertyValue}", propertyValue);

            rules.Add(new ValidationRule<TEntity>(code.ToString(), rule, description));
        }
    }
}
