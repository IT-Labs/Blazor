using System.Collections.Generic;
using Core.Shared.Interfaces;

namespace Core.Shared.Requests
{
    public class IdPartialRequest : IdRequest, IPartialRequest
    {
        public List<string> Includes { get; set; }
        public List<string> Fields { get; set; }
    }
}