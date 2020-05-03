using System.ComponentModel;

namespace Core.Shared.Enums
{
    public enum FileUploadType
    {
        [Description("jpeg,jpg,gif,png")]
        Image,
        [Description("pdf")]
        Document
    }
}
