namespace BlazorApp.Shared.ValidationCodes
{
    public static class EntityValidationMessages
    {
        public const string IsRequiredText = " is required";
        public const string RequiredText = " is required and";
        public const string RangeText = " cannot be more than 100 characters";
        public const string RangeTextArea = " cannot be more than 1000 characters";
        public const string DoesNotExist = " does not exist";
        public const string IsAlreadyActive = " is already active";
        public const string IsAlreadyDeleted = " is already inactive"; 
        public const string PositiveNumber = " needs to be positive number";
        public const string NotAllowedChars = "€‚ƒ„…†‡ˆ‰Š‹ŒŽ‘’“”•——˜™š›œžŸ";
        public const string ValidNumber = " is not a valid number";
        public const string ValidBool = " is not a valid boolean";
        public const string IsNotAValid = " is not a valid ";
        public static char[] NotAllowedCharsArray = NotAllowedChars.ToCharArray();
        public const string CannotBeModified = " cannot be modified";
    }
}
