using System;

namespace Core.Shared.Managers
{
    public interface ICloudFrontManager 
    {
        string GetPreAssignedUrl(string src, string version = null);
    }
}
