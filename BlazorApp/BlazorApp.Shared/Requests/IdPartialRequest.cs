using System.Collections.Generic;
using BlazorApp.Shared.Interfaces;

namespace BlazorApp.Shared.Requests
{
    public class IdPartialRequest : IdRequest, IPartialRequest
    {
        public List<string> Includes { get; set; }
        public List<string> Fields { get; set; }
    }
}