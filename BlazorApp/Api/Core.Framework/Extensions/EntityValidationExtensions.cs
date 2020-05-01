using BlazorApp.Shared.Enums;
using BlazorApp.Shared.ValidationCodes;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Core.Framework.Extensions
{
    public static class EntityValidationExtensions
    {
        private static readonly Dictionary<InputValidationSet, string> RegexStrings = new Dictionary<InputValidationSet, string>
        {
            {InputValidationSet.AlphaNumeric ,@"^[a-zA-Z0-9]*$" },
            {InputValidationSet.AlpaNumericWithSpace,@"^[a-zA-Z0-9\s]*$" },
            {InputValidationSet.Numbers,@"^[0-9]*$" },
            {InputValidationSet.PostalCode,@"^(\d{5}(-?\d{4})?)$" },
            {InputValidationSet.Address,@"^[A-Za-z0-9 _,\s()&\/.\-#]{1,100}$" },
            {InputValidationSet.City,@"^[A-Za-z0-9-()&#/.,\s]{1,100}$" },
            {InputValidationSet.Username,@"^[^;:|=, \+\*\?<>\\\/\[\]\s]*$" },
            {InputValidationSet.OneDigitMinimum, @"\d" },
            {InputValidationSet.LowerCharacter,@"[a-z]" },
            {InputValidationSet.UpperCharacter,@"[A-Z]" },
            {InputValidationSet.SpecialCharacter,@"[!,@,#,$,%,^,&,*,?,_,~,-,£,(,)]" },
            {InputValidationSet.UccNumber, @"^[0-9, x, X]*$"}
        };

        public static bool InRangeAndRequired(this string value, int lowerRange = 0, int upperRange = 100, bool isRequired = true)
        {
            value = value ?? string.Empty;
            return string.IsNullOrWhiteSpace(value.Trim())
              ? !isRequired
              : value.Length.InRange(lowerRange, upperRange);
        }

        public static bool InRangeRequiredAndValidChars(this string value, int lowerRange = 0, int upperRange = 100, bool isRequired = true)
        {
            value = value ?? string.Empty;
            return InRangeAndRequired(value, lowerRange, upperRange, isRequired && HasValidChars(value));
        }

        public static bool HasValidChars(this string text)
        {
            return (text ?? string.Empty).ToCharArray().All(x => !EntityValidationMessages.NotAllowedCharsArray.Contains(x));
        }

        public static bool IsValid(this string value, InputValidationSet validationSet)
        {
            if (string.IsNullOrEmpty(value))
                return true;

            if (!RegexStrings.ContainsKey(validationSet))
                return false;

            return Regex.Match(value, RegexStrings[validationSet]).Success;
        }

        public static bool InRangeMatchRegularExpressionAndIsRequired(this InputValidationSet validationSet, string value, int lowerRange = 0, int upperRange = 100, bool isRequired = true)
        {
            return string.IsNullOrWhiteSpace(value)
                ? !isRequired
                : value.Length.InRange(lowerRange, upperRange) && RegexStrings.ContainsKey(validationSet) && Regex.Match(value, RegexStrings[validationSet]).Success;
        }

        public static bool IsValidEmail(this string emailaddress)
        {
            if (string.IsNullOrWhiteSpace(emailaddress)) return true;
            try
            {
                var email = new MailAddress(emailaddress);
                return email.Address == emailaddress;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsValidUsername(this string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return false;

            return Regex.IsMatch(username.Trim(), @"^[A-Za-z0-9_\-.]+$");
        }

        public static bool IsValidLink(this string link, bool isRequired = true)
        {

            var isEmpty = string.IsNullOrWhiteSpace(link);

            if (isRequired && isEmpty) return false;

            if (!isRequired && isEmpty) return true;

            return Uri.IsWellFormedUriString(link, UriKind.RelativeOrAbsolute);
        }

        public static string GetDefaultDescription(this string field, bool isRequired = true, int minRange = 0,
            int maxRange = 100)
        {
            var requiredText = isRequired ? " is required and " : string.Empty;
            var lowerRange = minRange != 0 ? $" between {minRange} and " : "up to ";
            return $"{field}{requiredText} needs to be {lowerRange}{maxRange} characters";
        }

        public static bool MaxDecimalPlaces(this decimal number, int decimalPlaces = 2)
        {
            var modulValue = number - Math.Truncate(number);
            return decimalPlaces >= modulValue.ToString(CultureInfo.InvariantCulture).Length - 2;
        }

        public static bool IsValidFile(this IFormFile file, FileUploadType fileType, bool isRequired = true)
        {
            if (!isRequired && file == null)
                return true;

            if (file == null || file?.Length == 0)
                return false;

            var extension = Path.GetExtension(file.FileName).Replace(".", "").ToLower();
            List<string> allowedTypes = fileType.GetDescription()?.Split(',')?.ToList() ?? new List<string>();
            return allowedTypes.Any(x => x == extension);
        }

        public static bool IsValidFileSize(this IFormFile file, int maxSizeMB = 20, bool isRequired = true)
        {
            if (!isRequired && file == null)
                return true;

            if (file == null || file?.Length == 0)
                return false;

            if (file.Length <= maxSizeMB * 1048576)
                return true;

            return false;
        }
    }
}
