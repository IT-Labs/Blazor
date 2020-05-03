using Core.Shared.Managers;
using System;

namespace Core.Framework.Extensions
{
    public static class FileExtensions
    {
        public static string SetCDN(this string filename, string version = null)
        {

            if (string.IsNullOrWhiteSpace(filename))
                return string.Empty;

            var cloudFrontManager = ContainerFactory.GetInstance<ICloudFrontManager>();
            var fileUrl = cloudFrontManager.GetPreAssignedUrl(filename, version);

            return fileUrl;  
        }
    }
}
