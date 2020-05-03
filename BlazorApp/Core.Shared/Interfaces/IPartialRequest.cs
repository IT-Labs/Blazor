using System.Collections.Generic;

namespace Core.Shared.Interfaces
{
    public interface IPartialRequest
    {
        List<string> Includes { get; set; }
        List<string> Fields { get; set; }
    }

   
}