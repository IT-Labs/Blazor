using System.Collections.Generic;
using System.Linq;
using BlazorApp.Shared.Enums;
using BlazorApp.Shared.Interfaces;
using BlazorApp.Shared.Validation;
using BlazorApp.Shared.ValidationCodes;
using Core.Framework.Extensions;
using Core.Framework.Validation;

namespace Core.Framework.InterfaceExtensions.Validation
{
    public static class ValidationRulesFactory
    {
        public static List<IValidationRule<TEntity>> GetAddressList<TEntity>(bool isRequired)
            where TEntity : IHaveAddress
        {
            var q = ContainerFactory.GetInstance<AddressRules<TEntity>>();
            if (q != null && q.Rules.Any()) return q.Rules;

            var rules = new List<IValidationRule<TEntity>>();
            rules.AddRule(ValidationCodes.Address.Add001, x => x.AddressLine1.InRangeRequiredAndValidChars(isRequired: isRequired));
            rules.AddRule(ValidationCodes.Address.Add005, x => x.AddressLine1.IsValid(InputValidationSet.Address));

            rules.AddRule(ValidationCodes.Address.Ad002, x => x.AddressLine2.InRangeRequiredAndValidChars(isRequired: false));
            rules.AddRule(ValidationCodes.Address.Add005, x => x.AddressLine2.IsValid(InputValidationSet.Address));

            rules.AddRule(ValidationCodes.Address.Ad003, x => x.City.InRangeRequiredAndValidChars(isRequired: isRequired));
            rules.AddRule(ValidationCodes.Address.Add007, x => x.City.IsValid(InputValidationSet.City));

            rules.AddRule(ValidationCodes.Address.Add004, x => x.PostalCode.InRangeRequiredAndValidChars(isRequired: isRequired));
            rules.AddRule(ValidationCodes.Address.Add008, x => x.PostalCode.IsValid(InputValidationSet.PostalCode));

            rules.AddRule(ValidationCodes.Address.Add009, x => !x.IsEntered() && string.IsNullOrWhiteSpace(x.AddressLine2) || x.IsAddressEntered());
            return rules;
        }

        public static List<IValidationRule<TEntity>> GetPhoneList<TEntity>(bool isPhoneNumberRequired, bool isPhoneExtensionRequired)
            where TEntity : IHavePhone
        {
            var q = ContainerFactory.GetInstance<PhoneRules<TEntity>>();
            if (q != null && q.Rules.Any()) return q.Rules;

            var rules = new List<IValidationRule<TEntity>>();

            rules.AddRule(ValidationCodes.PhoneNumber.Ph001, x => x != null);
            rules.AddRule(ValidationCodes.PhoneNumber.Ph002, x => isPhoneNumberRequired && !string.IsNullOrEmpty(x.PhoneNumber));
            rules.AddRule(ValidationCodes.PhoneNumber.Ph003, x => x.PhoneNumber?.Length == 10);
            rules.AddRule(ValidationCodes.PhoneNumber.Ph004, x => x.PhoneNumber.IsValid(InputValidationSet.Numbers));

            rules.AddRule(ValidationCodes.PhoneNumber.Ph005, x => x.PhoneExtension.IsValid(InputValidationSet.Numbers));
            rules.AddRule(ValidationCodes.PhoneNumber.Ph006, x => x.PhoneExtension.InRangeAndRequired(upperRange: 5, isRequired: isPhoneExtensionRequired));

            return rules;
        }

        public static List<IValidationRule<TEntity>> GetEmailList<TEntity>(bool isRequired)
            where TEntity : IHaveEmail
        {
            var q = ContainerFactory.GetInstance<EmailRules<TEntity>>();
            if (q != null && q.Rules.Any()) return q.Rules;

            var rules = new List<IValidationRule<TEntity>>();

            rules.AddRule(ValidationCodes.EmailAddress.Ea001, x => x != null);
            rules.AddRule(ValidationCodes.EmailAddress.Ea002, x => x.Email.InRangeRequiredAndValidChars(isRequired: isRequired));
            rules.AddRule(ValidationCodes.EmailAddress.Ea003, x => x.Email.IsValidEmail());

            return rules;
        }
    }
}