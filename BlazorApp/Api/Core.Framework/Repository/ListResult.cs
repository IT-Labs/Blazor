using System.Collections.Generic;
using System.Net;
using Core.Shared.Enums;
using Core.Shared.Interfaces;
using Core.Shared.Repository;
using Core.Shared.Response;

namespace Core.Framework.Repository
{
    /// <summary>
    ///     ListResult class of the Repository
    /// </summary>
    /// <typeparam name="TValue">type of the ListResult class</typeparam>
    public class ListResult<TValue> : IListResult<TValue>
    {
        /// <summary>
        ///     Extended constructor. It extends from Base constructor.
        /// </summary>
        /// <param name="values">List of result values</param>
        public ListResult(List<TValue> values)
        {
            Values = values;
            Total = Values?.Count ?? 0;
            UniqueTotal = UniqueTotal;

        }

        /// <summary>
        ///     Extended constructor. It extends from Base constructor.
        /// </summary>
        /// <param name="values">List of result values</param>
        /// <param name="request"></param>
        public ListResult(List<TValue> values, IPageable request) : this(values)
        {
            CurrentPage = request.CurrentPage;
            PageSize = request.PageSize;
        }

        /// <summary>
        ///     Extended constructor
        /// </summary>
        /// <param name="status">Http status code of the result</param>
        /// <param name="code">Code of the ResponseMessage</param>
        /// <param name="message">MEssage of the ResponseMessage</param>
        /// <param name="type">Type of the ResponseMessage</param>
        public ListResult(HttpStatusCode status, string code = null, string message = null,
            ResponseMessageType? type = null)
        {
            Status = status;

            if (type.HasValue && !string.IsNullOrEmpty(code) && !string.IsNullOrEmpty(message))
            {
                Errors = new List<ResponseMessage>
                {
                    new ResponseMessage
                    {
                        Code = code,
                        Message = message,
                        Type = type.Value
                    }
                };
            }
        }

        /// <summary>
        ///     Gets or sets Http status code
        /// </summary>
        public HttpStatusCode Status { get; set; } = HttpStatusCode.OK;

        /// <summary>
        ///     Get or sets list of error messages
        /// </summary>
        public List<ResponseMessage> Errors { get; set; } = new List<ResponseMessage>();

        /// <summary>
        ///     Get or set list of values
        /// </summary>
        public List<TValue> Values { get; set; } = new List<TValue>();

        /// <summary>
        ///     Gets or sets current page
        /// </summary>
        public int CurrentPage { get; set; } = 1;

        /// <summary>
        ///     Gets or sets page size
        /// </summary>
        public int PageSize { get; set; } = 50;

        /// <summary>
        ///     Gets or sets total number of records
        /// </summary>
        public int Total { get; set; }


        /// <summary>
        ///     Gets or sets total number of unique record
        /// </summary>
        public int UniqueTotal { get; set; }
    }
}