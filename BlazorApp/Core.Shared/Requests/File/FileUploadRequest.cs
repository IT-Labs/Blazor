using Core.Shared.Enums;
using Microsoft.AspNetCore.Http;

namespace Core.Shared.Requests.File
{
    public class FileUploadRequest : SaveRequest
    {
        public IFormFile File { get; set; }
        public FileUploadType FileUploadType { get; set; } = FileUploadType.Image;
    }
}
