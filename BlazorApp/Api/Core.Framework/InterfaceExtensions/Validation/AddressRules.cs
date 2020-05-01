using System.Collections.Generic;
using BlazorApp.Shared.Interfaces;
using BlazorApp.Shared.Validation;
using Core.Framework.Validation;

namespace Core.Framework.InterfaceExtensions.Validation
{
    public class AddressRules<TEntity>
        where TEntity : IHaveAddress
    {


        public AddressRules(List<IValidationRule<TEntity>> rules)
        {
            Rules = rules;
        }
        public List<IValidationRule<TEntity>> Rules { get; }
    }
}