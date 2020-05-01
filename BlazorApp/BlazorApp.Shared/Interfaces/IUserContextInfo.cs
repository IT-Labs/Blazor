using BlazorApp.Shared.Enums;
using System;

namespace BlazorApp.Shared.Interfaces
{
    public interface IUserContextInfo
    {
        long? UserId { get;  set; }
        string Token { get; set; }
        string Username { get; set; }
    }
}