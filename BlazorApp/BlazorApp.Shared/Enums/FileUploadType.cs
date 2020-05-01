using System.ComponentModel;

namespace BlazorApp.Shared.Enums
{
    public enum FileUploadType
    {
        [Description("jpeg,jpg,gif,png")]
        Image,
        [Description("pdf")]
        Document
    }
}
