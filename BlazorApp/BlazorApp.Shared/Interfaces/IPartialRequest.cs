using System.Collections.Generic;

namespace BlazorApp.Shared.Interfaces
{
    public interface IPartialRequest
    {
        List<string> Includes { get; set; }
        List<string> Fields { get; set; }
    }

   
}