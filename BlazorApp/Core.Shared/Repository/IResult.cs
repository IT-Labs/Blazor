using System.Collections.Generic;
using System.Net;
using Core.Shared.Response;

namespace Core.Shared.Repository
{
    /// <summary>
    ///     Definition of Result
    /// </summary>
    public interface IResult
    {
        /// <summary>
        ///     Http status of the result
        /// </summary>
        HttpStatusCode Status { get; set; }

        /// <summary>
        ///     List of ResponseMessages
        /// </summary>
        List<ResponseMessage> Errors { get; set; }
    }

    /// <summary>
    ///     Definiton of Result. Implements IResult
    /// </summary>
    /// <typeparam name="TValue">Input type parameter</typeparam>
    public interface IResult<TValue> : IResult
    {
        /// <summary>
        ///     Value of the result
        /// </summary>
        TValue Value { get; set; }
    }
}