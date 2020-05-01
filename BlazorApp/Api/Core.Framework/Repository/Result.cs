using System;
using System.Collections.Generic;
using System.Net;
using BlazorApp.Shared.Enums;
using BlazorApp.Shared.Repository;
using BlazorApp.Shared.Response;
using Core.Framework.Extensions;

namespace Core.Framework.Repository
{
    /// <summary>
    ///     Result class of the Repository response
    /// </summary>
    /// <typeparam name="TValue">type of the Result class</typeparam>
    public class Result<TValue> : IResult<TValue>
    {
       
        /// <summary>
        ///     Base constructor. It sets new List of ResponseMessage and Status to HttpStatusCode.OK
        /// </summary>
        public Result()
        {
            Errors = new List<ResponseMessage>();
            Status = HttpStatusCode.OK;
        }

        public Result(TValue v) : this()
        {
            Value = v;
        }
        public Result(HttpStatusCode status = HttpStatusCode.NotFound, Enum errorCode = null, string entityName = "", string propertyName = "", ResponseMessageType type = ResponseMessageType.Validation, params object[] args)
        {
            Status = status;
            Errors = new List<ResponseMessage>();
            if (errorCode != null)
            {
                Errors.Add(new ResponseMessage
                {
                    Code = errorCode.ToString(),
                    Message = errorCode.GetDescription().Replace("{EntityName}", entityName.SplitCamelCase()).Replace("{PropertyName}", propertyName.SplitCamelCase()),
                    Args = args ?? new object[0]

                }
                );
            }
           
        }
        /// <summary>
        ///     Extended constructor
        /// </summary>
        /// <param name="status">Http status code of the result</param>
        /// <param name="code">Code of the ResponseMessage</param>
        /// <param name="message">Message of the ResponseMessage</param>
        /// <param name="type">Type of the ResponseMessage</param>
        /// <param name="args">Arguments of the ResponseMessage</param>
        public Result(HttpStatusCode status, string code = null, string message = null, ResponseMessageType? type = null,params object[] args  )
        {
            Status = status;
            Errors = new List<ResponseMessage>();

            if (type.HasValue && !string.IsNullOrEmpty(code) && !string.IsNullOrEmpty(message))
            {
                Errors = new List<ResponseMessage>
                {
                    new ResponseMessage
                    {
                        Code = code,
                        Message = message,
                        Type = type.Value,
                        Args = args ?? new object[0]
                    }
                };
            }
        }

        /// <summary>
        ///     Gets or sets the Status of the result
        /// </summary>
        public HttpStatusCode Status { get; set; }

        /// <summary>
        ///     Get or sets the list of ResponseMessages of the result
        /// </summary>
        public List<ResponseMessage> Errors { get; set; }

        /// <summary>
        ///     Gets or sets Value of the result
        /// </summary>
        public TValue Value { get; set; }
    }
}