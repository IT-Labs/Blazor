using Core.Shared.Enums;
using System;

namespace Core.Shared.Interfaces
{
    public interface IUserContextInfo
    {
        long? UserId { get;  set; }
        string Token { get; set; }
        string Username { get; set; }
    }
}