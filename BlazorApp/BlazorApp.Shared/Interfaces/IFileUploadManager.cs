using BlazorApp.Shared.Managers;
using BlazorApp.Shared.Requests.File;
using BlazorApp.Shared.Response;

namespace BlazorApp.Shared.Interfaces
{
    public interface IFileUploadManager : IManager
    {
        Response<string> Upload(FileUploadRequest request, string fileName = null);
        Response<FileDownloadResponse> Download(string path);
        Response<bool> Delete(FileDeleteRequest request);
    }
}
