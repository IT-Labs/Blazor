using System.Collections.Generic;
using System.Net;
using Core.Shared.Response;

namespace Core.Shared.Interfaces
{
    /// <summary>
    ///     Definition of the Response
    /// </summary>
    public interface IResponse
    {
        /// <summary>
        ///     Gets or sets https status of the response
        /// </summary>
        HttpStatusCode Status { get; set; }

        /// <summary>
        ///     Gets or sets list of messages of the response
        /// </summary>
        List<ResponseMessage> Messages { get; set; }
    }


    /// <summary>
    ///     Definition of the Response
    /// </summary>
    public interface IResponse<T> : IResponse
    {
        /// <summary>
        /// </summary>
        T Payload { get; set; }
    }
}