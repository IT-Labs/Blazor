using System.Collections.Generic;
using Core.Shared.Interfaces;
using Core.Shared.Validation;
using Core.Framework.Validation;

namespace Core.Framework.InterfaceExtensions.Validation
{
    public class PhoneRules<TEntity>
        where TEntity : IHavePhone
    {

        public PhoneRules(List<IValidationRule<TEntity>> rules)
        {
            Rules = rules;
        }
        public List<IValidationRule<TEntity>> Rules { get; }
    }
}