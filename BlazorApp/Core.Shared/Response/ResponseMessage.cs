using Core.Shared.Enums;

namespace Core.Shared.Response
{
    /// <summary>
    ///     Definition of the response message
    /// </summary>
    public class ResponseMessage
    {
        public ResponseMessage() { }
        public ResponseMessage(ResponseMessageType type = ResponseMessageType.Validation)
        {
            Type = type;
        }

        /// <summary>
        ///     Gets or sets type of the response message
        /// </summary>
        public ResponseMessageType Type { get; set; }

        /// <summary>
        ///     Gets or sets code of the response message
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        ///     Gets or sets the message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///     Gets mesage arguments 
        /// </summary>
        public object[] Args { get; set; }
    }
}