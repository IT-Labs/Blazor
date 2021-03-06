﻿using System.Threading.Tasks;

namespace Core.Shared.ESB
{
    public interface IBusService
    {
        Task Publish<T>(T message) where T : IMessage;
    }
}