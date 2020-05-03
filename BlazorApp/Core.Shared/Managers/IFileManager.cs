using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Core.Shared.Managers
{
    /// <summary>
    /// 
    /// </summary>
    public interface IFileManager
    {
        string Root { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="dest"></param>
        void Save(Stream stream, string dest);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        Stream Read(string path);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dest"></param>
        void Copy(string src, string dest);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dest"></param>
        void Move(string src, string dest);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        void Delete(string src);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        bool Exists(string src);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dest"></param>
        /// <returns></returns>
        Task<bool> CopyAsync(string src, string dest);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dest"></param>
        void MoveAsync(string src, string dest);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        Task DeleteAsync(string src);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        void DeleteMultipleAsync(List<string> srcs);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dest"></param>
        void CopyDirectory(string src, string dest);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        List<string> GetAllFiles(string path);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dest"></param>
        void CopyDirectoryFromLocal(string src, string dest);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        string GetPreAssignedUrl(string src);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectKey"></param>
        /// <param name="destinationPath"></param>
        /// <returns></returns>
        /// 
        void DownloadObject(string objectKey, string destinationPath);

        void DownloadObjects(string sourceFolder, string destinationFolder);
    }
}