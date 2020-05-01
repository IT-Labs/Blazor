using System.Collections.Generic;
using System.Linq;
using System.Net;
using BlazorApp.Shared.Interfaces;

namespace BlazorApp.Shared.Response
{   

    /// <summary>
    ///     Base definition of the Response
    /// </summary>
    public class Response<T> : IResponse<T>
    {
        /// <summary>
        ///     Gets or sets status of the Response
        /// </summary>
        public HttpStatusCode Status { get; set; } = HttpStatusCode.OK;
        public bool NotOk => Status != HttpStatusCode.OK;

        /// <summary>
        ///     Gets or sets List of ResponseMessage
        /// </summary>
        public List<ResponseMessage> Messages { get; set; } = new List<ResponseMessage>();

        /// <summary>
        ///     Gets or sets payload of the Response
        /// </summary>
        public T Payload { get; set; } = default(T);
        
        public static Response<T> Forbidden()
        {
            return new Response<T>
            {
                Status = HttpStatusCode.Forbidden,
                Messages = new List<ResponseMessage> { new ResponseMessage
                {
                    Message = "Forbidden action",
                    Code = "Cmn001",
                    Type = Enums.ResponseMessageType.Validation
                } }
            };
        }

        public static Response<T> BadRequest(string message = null, string code = null)
        {
            return new Response<T>
            {
                Status = HttpStatusCode.BadRequest,
                Messages = new List<ResponseMessage> { new ResponseMessage
                {
                    Message = message ?? "Invalid request",
                    Code = code ?? "Cmn002",
                    Type = Enums.ResponseMessageType.Validation
                } }
            };
        }
        
        public static Response<T> NotFound()
        {
            return new Response<T>
            {
                Status = HttpStatusCode.NotFound
            };
        }

        public string MessageSummary => Messages.Aggregate(string.Empty, (current, x) => $"{current} code: {x.Code} message: {x.Message} ");
    }
}