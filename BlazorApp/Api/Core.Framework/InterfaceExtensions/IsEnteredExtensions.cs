using System;
using Core.Shared.Interfaces;

namespace Core.Framework.InterfaceExtensions
{
    public static class IsEnteredExtensions
    {
        public static bool IsEntered(this IHaveAddress entity)
        {
            return !string.IsNullOrWhiteSpace(entity.AddressLine1) || !string.IsNullOrWhiteSpace(entity.City) ||
                   !string.IsNullOrWhiteSpace(entity.PostalCode) ||
                   entity.StateId.HasValue;
        }
        public static bool IsAddressEntered(this IHaveAddress entity)
        {
            return !string.IsNullOrWhiteSpace(entity.AddressLine1) && !string.IsNullOrWhiteSpace(entity.City) &&
                   !string.IsNullOrWhiteSpace(entity.PostalCode);
        }
        public static bool IsPhoneNumberEntered(this IHavePhone entity)
        {
            return !string.IsNullOrWhiteSpace(entity.PhoneNumber);
        }

        public static bool IsEmailAddressEntered(this IHaveEmail entity)
        {
            return !string.IsNullOrWhiteSpace(entity.Email);
        }
        public static bool IsAddressEntered(this IMayHaveAddress entity)
        {
            return !string.IsNullOrWhiteSpace(entity.AddressLine1) && !string.IsNullOrWhiteSpace(entity.City) &&
                   !string.IsNullOrWhiteSpace(entity.PostalCode) && !entity.CountryId.HasValue;
        }
    }
}
