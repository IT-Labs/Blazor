using System.Collections.Generic;
using System.Net;

namespace BlazorApp.Shared.Response
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedResponse<T> : ListResponse<T>
    {
        /// <summary>
        /// 
        /// </summary>
        public Meta Meta { get; set; } = new Meta();

        public static new PagedResponse<T> Forbidden()
        {
            return new PagedResponse<T>
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

        public static PagedResponse<T> BadRequest(string message = null)
        {
            return new PagedResponse<T>
            {
                Status = HttpStatusCode.BadRequest,
                Messages = new List<ResponseMessage> { new ResponseMessage
                {
                    Message = message ?? "Invalid request",
                    Code = "Cmn002",
                    Type = Enums.ResponseMessageType.Validation
                } }
            };
        }
    }
}