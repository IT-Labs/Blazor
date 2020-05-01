using System.ComponentModel;

namespace BlazorApp.Shared.ValidationCodes
{
    public static partial class ValidationCodes
    {
        public enum PhoneNumber
        {
            [Description("Request cannot be null!")]
            Ph001,
            [Description("Please enter a phone number.")]
            Ph002,
            [Description("Phone number must be 10 digits.")]
            Ph003,
            [Description("Phone number can contain only numbers(0,9).")]
            Ph004,
            [Description("Phone extension can contain only numbers(0,9).")]
            Ph005,
            [Description("Phone extension cannot be more than 5 characters.")]
            Ph006,
        }
    }
}
