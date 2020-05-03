using Core.Shared.Managers;
using Core.Shared.Requests.File;
using Core.Shared.Response;

namespace Core.Shared.Interfaces
{
    public interface IFileUploadManager : IManager
    {
        Response<string> Upload(FileUploadRequest request, string fileName = null);
        Response<FileDownloadResponse> Download(string path);
        Response<bool> Delete(FileDeleteRequest request);
    }
}
