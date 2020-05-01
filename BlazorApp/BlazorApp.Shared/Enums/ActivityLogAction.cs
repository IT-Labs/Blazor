using System.ComponentModel;

namespace BlazorApp.Shared.Enums
{
    public enum ActivityLogAction
    {
        [Description("Add")]
        Create,
        [Description("Update")]
        Update,
        [Description("Soft Delete")]
        SoftDelete,
        [Description("Hard Delete")]
        HardDelete
    }
}
