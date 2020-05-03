using System.Collections.Generic;
using Core.Shared.Enums;

namespace Core.Shared.Dto
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