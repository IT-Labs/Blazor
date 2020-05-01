using System.ComponentModel;

namespace BlazorApp.Shared.ValidationCodes
{
   public static partial class ValidationCodes
    {

        public enum Address
        {
            [Description("Address line 1  needs to be up to 100 characters")]
            Add001,
            [Description("Address line 2  needs to be up to 100 characters")]
            Ad002,
            [Description("City  needs to be up to 100 characters")]
            Ad003,
            [Description("Postal code  needs to be up to 100 characters")]
            Add004,
            [Description("Please enter a street address without special characters.")]
            Add005,
            [Description("City needs to be up to 100 characters")]
            Add006,
            [Description("City can include the following: numbers, letters, -, (), &, #, /, . and ,")]
            Add007,
            [Description("Please enter a 5 or 9 digit postal code.")]
            Add008,
            [Description("Please enter complete Address - line 1, City Postal Code, Country, State.")]
            Add009
        }
    }
}
