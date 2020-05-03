using System.ComponentModel;

namespace Core.Shared.ValidationCodes
{
    public static partial class ValidationCodes
    {
        public enum EmailAddress
        {
            [Description("Request cannot be null!")]
            Ea001,
            [Description("Email address cannot be more than 100 characters.")]
            Ea002,
            [Description("Email address isn't in a valid format!")]
            Ea003

        }
    }
}
