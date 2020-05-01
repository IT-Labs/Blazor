using System.IO;

namespace BlazorApp.Shared.Response
{
    public class FileDownloadResponse
    {
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public Stream Stream { get; set; }
    }
}
