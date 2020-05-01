using System.Collections.Generic;
using BlazorApp.Shared.Enums;

namespace BlazorApp.Shared.Dto
{
    public class CurrentUserAuthDetails
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public List<string> Permissions { get; set; }
    }
}