using BlazorApp.Shared.Interfaces;
using System.Collections.Generic;
using System.Net;

namespace BlazorApp.Shared.Response
{
    public class ResponseBase : IResponse
    {
        /// <summary>
        /// Base constructor. Sets Messages to new list of ResponseMessage and Status to HttpStatusCode.OK
        /// </summary>
        public ResponseBase()
        {
            Messages = new List<ResponseMessage>();
            Status = HttpStatusCode.OK;
        }

        /// <summary>
        /// Gets or sets status of the Response
        /// </summary>
        public HttpStatusCode Status { get; set; }

        /// <summary>
        /// Gets or sets List of ResponseMessage
        /// </summary>
        public List<ResponseMessage> Messages { get; set; }
    }
}
