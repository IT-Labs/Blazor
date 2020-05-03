using Core.Shared.Interfaces;
using Microsoft.Extensions.Logging;

namespace Core.Framework.Consumer
{
    public abstract class BaseConsumer
    {
        protected readonly ILogger<BaseConsumer> Logger;

        protected BaseConsumer(ILogger<BaseConsumer> logger)
        {
            Logger = logger;
        }
    }
}
