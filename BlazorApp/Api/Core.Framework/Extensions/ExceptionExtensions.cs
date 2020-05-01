using System;
using System.Text;

namespace Core.Framework.Extensions
{
    /// <summary>
    ///     Extends Exception class
    /// </summary>
    public static class ExceptionExtensions
    {
        /// <summary>
        ///     Encapsulated FlattenEXception function when there is no message
        /// </summary>
        /// <param name="exception">input exception that is extended</param>
        /// <returns>Full exception string</returns>
        public static string FlattenExceptionMessage(this Exception exception)
        {
            return exception.FlattenException(null);
        }


        /// <summary>
        ///     Adds custom message to the Exception
        /// </summary>
        /// <param name="exception">input exception that is extended</param>
        /// <param name="message">Custom message tgat needs to be added into the exception</param>
        /// <returns>Full exception including message as string</returns>
        public static string FlattenException(this Exception exception, string message)
        {
            var stringBuilder = new StringBuilder();
            var localException = exception;

            if (!string.IsNullOrEmpty(message))
            {
                stringBuilder.AppendLine(message);
            }

            while (localException != null)
            {
                stringBuilder.AppendLine(localException.Message);
                localException = localException.InnerException;
            }

            return stringBuilder.ToString();
        }
    }


   
}