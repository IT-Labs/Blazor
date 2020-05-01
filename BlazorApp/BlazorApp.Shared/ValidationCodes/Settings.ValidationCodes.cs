using System.ComponentModel;

namespace BlazorApp.Shared.ValidationCodes
{
    public static partial class ValidationCodes
    {
        public enum Settings
        {
            [Description("Setting does not exist or is set to non-editable!")]
            Set001
        }
    }
}
