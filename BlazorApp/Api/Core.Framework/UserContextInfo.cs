using System;
using BlazorApp.Shared.Enums;
using BlazorApp.Shared.Interfaces;

namespace Core.Framework
{
    public class UserContextInfo : IUserContextInfo
    {
        public UserContextInfo()
        {
            UserId = null;
        }

        public long? UserId { get; set; }

        public string Token { get; set; }

        public string Username { get; set; }

    }
}