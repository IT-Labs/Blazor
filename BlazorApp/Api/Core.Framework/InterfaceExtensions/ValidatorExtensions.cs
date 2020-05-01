using System;
using System.Collections.Generic;
using BlazorApp.Shared.Interfaces;
using BlazorApp.Shared.Requests.File;
using BlazorApp.Shared.Validation;
using BlazorApp.Shared.ValidationCodes;
using Core.Framework.Extensions;
using Core.Framework.InterfaceExtensions.Validation;

namespace Core.Framework.InterfaceExtensions
{
    public static class ValidatorExtensions
    {
        public static IList<IValidationRule<T>> AddressValidators<T>(this IList<IValidationRule<T>> rules, bool isRequired = true)
            where T : IRequest, IHaveAddress
        {
            var validations = ValidationRulesFactory.GetAddressList<T>(isRequired);
            var rulesList = rules as List<IValidationRule<T>>;
            rulesList?.AddRange(validations);

            return rulesList;
        }

        public static IList<IValidationRule<T>> PhoneNumberValidators<T>(this IList<IValidationRule<T>> rules, bool isPhoneNumberRequired = true, bool isPhoneExtensionRequired = false)
            where T : IRequest, IHavePhone
        {
            var validations = ValidationRulesFactory.GetPhoneList<T>(isPhoneNumberRequired, isPhoneExtensionRequired);
            var rulesList = rules as List<IValidationRule<T>>;
            rulesList?.AddRange(validations);

            return rulesList;
        }

        public static IList<IValidationRule<T>> EmailAddressRequestValidator<T>(this IList<IValidationRule<T>> rules, bool isRequired = true)
            where T : IRequest, IHaveEmail
        {
            var validations = ValidationRulesFactory.GetEmailList<T>(isRequired);
            var rulesList = rules as List<IValidationRule<T>>;
            rulesList?.AddRange(validations);

            return rulesList;
        }
    }
}
