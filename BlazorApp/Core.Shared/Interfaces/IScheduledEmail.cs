using Core.Shared.ESB;
using System;

namespace Core.Shared.Interfaces
{
    public interface IScheduledEmail<T> : IMessage
    {
        T EmailTemplateType { get; set; }
        long? UserId { get; set; }
        string ToEmail { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        bool SendImmediately { get; set; }
    }
}
