using System.Collections.Generic;
using System.Linq;
using Core.Shared.Response;
using Microsoft.Extensions.Logging;

namespace Core.Framework.Extensions
{
    public static class MyLoggerExtensions
    {

        public static void LogErrors(this ILogger logger, List<ResponseMessage> messages)
        {
            if (messages == null || !messages.Any()) return;
            foreach (var message in messages)
            {
                logger.LogError(message.Message);
            }
        }
    }
}
