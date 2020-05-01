using System;

namespace BlazorApp.Shared.Managers
{
    public interface ICloudFrontManager 
    {
        string GetPreAssignedUrl(string src, string version = null);
    }
}
