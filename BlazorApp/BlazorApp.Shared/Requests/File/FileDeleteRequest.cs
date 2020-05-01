using BlazorApp.Shared.Enums;
using BlazorApp.Shared.Interfaces;

namespace BlazorApp.Shared.Requests.File
{
    public class FileDeleteRequest : IRequest
    {
        public string Path { get; set; }
        public FileUploadType FileUploadType { get; set; }
    }
}
