using System.ComponentModel;

namespace BlazorApp.Shared.ValidationCodes
{
    public static partial class ValidationCodes
    {
        public enum Common
        {
            [Description("Forbidden action")]
            Cmn001,
            [Description("Id is required or does not match")]
            Cmn002,
            [Description("Email is required")]
            Cmn003,
            [Description("Request cannot be null!")]
            Cmn004,
            [Description("Email is not in correct format")]
            Cmn005,
            [Description("This email address is already in use!")]
            Cmn006,
            [Description("Username is in use")]
            Cmn007,
            [Description("{EntityName}" + EntityValidationMessages.DoesNotExist)]
            Cmn008,
            [Description("{EntityName}" + EntityValidationMessages.IsAlreadyActive)]
            Cmn009,
            [Description("{EntityName}" + EntityValidationMessages.IsAlreadyDeleted)]
            Cmn010,
            [Description("{EntityName} name is in use")]
            Cmn011,
            [Description("{PropertyName}" + EntityValidationMessages.RangeText)]
            Cmn012,
            [Description("{PropertyName}" + EntityValidationMessages.RangeTextArea)]
            Cmn013,
            [Description("{EntityName} {PropertyName}" + EntityValidationMessages.IsRequiredText + EntityValidationMessages.RangeText)]
            Cmn014,
            [Description("{PropertyName}" + EntityValidationMessages.IsRequiredText)]
            Cmn015,
            [Description("{EntityName} Id" + EntityValidationMessages.IsRequiredText)]
            Cmn016,
            [Description("{EntityName} must be even number")]
            Cmn017,
            [Description("{PropertyName}  " + EntityValidationMessages.PositiveNumber)]
            Cmn018,
            [Description("{PropertyName} cannot be less than 0!")]
            Cmn019,
            [Description("{PropertyName}")]
            Cmn020,
            [Description("{EntityName} is in use")]
            Cmn021,
            [Description("The credit card reset link has expired")]
            Cmn022,
            [Description("Invalid {PropertyName}.")]
            Cmn023,
            [Description("{PropertyName} is in invalid range!")]
            Cmn024,
            [Description("{PropertyName}" + EntityValidationMessages.IsRequiredText + " and" + EntityValidationMessages.RangeText)]
            Cmn025,
            [Description("{PropertyName} isn't in a valid format")]
            Cmn026,
            [Description("{PropertyName} cannot include the following: " + EntityValidationMessages.NotAllowedChars)]
            Cmn027,
            [Description("Please enter a {PropertyName}")]
            Cmn028,
            [Description("Please enter an {PropertyName}")]
            Cmn029,
            [Description("Please select a {PropertyName}")]
            Cmn030,
            [Description("Please select an {PropertyName}")]
            Cmn031,
            [Description("{PropertyName}" + EntityValidationMessages.DoesNotExist)]
            Cmn032,
            [Description("{PropertyName}" + EntityValidationMessages.IsRequiredText + " and" + EntityValidationMessages.RangeTextArea)]
            Cmn033,
            [Description("{PropertyName}" + EntityValidationMessages.RangeTextArea)]
            Cmn034,
            [Description("{PropertyName}" + EntityValidationMessages.IsRequiredText + " and" + EntityValidationMessages.PositiveNumber)]
            Cmn035,
            [Description("{PropertyName}" + EntityValidationMessages.IsRequiredText + " and cannot be more than 9 characters.")]
            Cmn036,
            [Description("{PropertyName}" + EntityValidationMessages.RangeText + " and less than {NumberOfCharacters} characters ")]
            Cmn037,
            [Description("{PropertyName}" + EntityValidationMessages.ValidNumber)]
            Cmn038,
            [Description("{PropertyValue}" + EntityValidationMessages.IsNotAValid + "{PropertyName}")]
            Cmn039,
            [Description("{EntityName}" + EntityValidationMessages.CannotBeModified)]
            Cmn040,
            [Description("{PropertyName} is required")]
            Cmn041,
            [Description("Only ISO8859-1 characters are allowed for {PropertyName}")]
            Cmn042,
            [Description("{PropertyName}" + EntityValidationMessages.IsRequiredText + " and cannot be more than {0} characters.")]
            Cmn043,
            [Description("Selected {0} is already in this status!")]
            Cmn044,
            [Description("{PropertyName} cannot be more that {0} characters")]
            Cmn045,
            [Description("Error occured during saving changes. Please try again later.")]
            Cmn046,
            [Description("{PropertyName} must have {NumberOfCharacters} characters")]
            Cmn047,
        }
    }
}