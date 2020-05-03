using Core.Shared.Enums;
using Core.Shared.Interfaces;

namespace Core.Shared.Requests.File
{
    public class FileDeleteRequest : IRequest
    {
        public string Path { get; set; }
        public FileUploadType FileUploadType { get; set; }
    }
}
