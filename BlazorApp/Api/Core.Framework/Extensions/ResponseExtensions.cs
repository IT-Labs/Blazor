using System;
using System.Linq;
using System.Net;
using BlazorApp.Shared;
using BlazorApp.Shared.Enums;
using BlazorApp.Shared.Interfaces;
using BlazorApp.Shared.Repository;
using BlazorApp.Shared.Response;
using BlazorApp.Shared.Validation;
using Core.Framework.Repository;
using Core.Framework.Validation;

namespace Core.Framework.Extensions
{
    /// <summary>
    ///     Extends Response object
    /// </summary>
    public static class ResponseExtensions
    {

        /// <summary>
        ///     Merges original response with Validaiton Result to return full response message
        /// </summary>
        /// <param name="originalResponse">Original response created from the message</param>
        /// <param name="validationResult">Validatin result with messages</param>
        public static void Merge(this IResponse originalResponse, ValidationResult validationResult)
        {

            if (validationResult == null) return;
            if (validationResult.State == ValidationStatus.Invalid)
            {
                originalResponse.Status = HttpStatusCode.BadRequest;

                foreach (var validationError in validationResult.Messages)
                {
                    originalResponse.Messages.Add(new ResponseMessage
                    {
                        Code = validationError.Name,
                        Message = validationError.Message,
                        Type = ResponseMessageType.Validation,
                        Args = validationError.Args
                    });
                }
            }
        }

        /// <summary>
        ///     Merges original response with result defined in IResult to return full response message
        /// </summary>
        /// <param name="originalResponse">Original response created from the message</param>
        /// <param name="result">Result with messages</param>
     //   [Obsolete("fix translation API and remove this method")]
        //public static void Merge(this IResponse originalResponse, IResult result)
        //{
        //    originalResponse.Status = MergeStatus(originalResponse.Status, result.Status);
        //    originalResponse.Messages.AddRange(result.Errors);
        //}

        public static void Merge(this IResponse originalResponse, IResponse response)
        {
            if (response == null) return;
            originalResponse.Status = MergeStatus(originalResponse.Status, response.Status);
            originalResponse.Messages.AddRange(response.Messages);

        }

        /// <summary>
        ///     Merges original response with result defined in IResult to return full response message
        /// </summary>
        /// <param name="originalResponse">Original response created from the message</param>
        /// <param name="result">Result with messages</param>
        public static void Merge<T>(this IResponse<T> originalResponse, IResult<T> result)
        {
            if (result == null) return;
            originalResponse.Status = MergeStatus(originalResponse.Status, result.Status);
            originalResponse.Messages.AddRange(result.Errors);

            if (result.IsValid())
            {
                originalResponse.Payload = result.Value;
            }
        }
        public static void MergeStatus<T, U>(this IResponse<T> originalResponse, IResponse<U> result)
        {

            if (result == null) return;
            originalResponse.Status = MergeStatus(originalResponse.Status, result.Status);
            originalResponse.Messages.AddRange(result.Messages);
        }

        /// <summary>
        ///     Merges original response with result defined in IResult to return full response message
        /// </summary>
        /// <param name="originalResponse">Original response created from the message</param>
        /// <param name="result">Result with messages</param>
        public static void Merge<T>(this PagedResponse<T> originalResponse, IListResult<T> result)
        {
            if (result == null) return;
            originalResponse.Status = MergeStatus(originalResponse.Status, result.Status);
            originalResponse.Messages.AddRange(result.Errors);

            if (result.IsInvalid())
            {
                return;
            }
            originalResponse.Payload = result.Values;
            originalResponse.Meta.TotalCount = result.Total;
            originalResponse.Meta.PageSize = result.PageSize;
            originalResponse.Meta.CurrentPage = result.CurrentPage;
            originalResponse.Meta.UniqueTotal = result.UniqueTotal;
        }


        /// <summary>
        ///     Merges original response with result defined in IResult to return full response message
        /// </summary>
        /// <param name="originalResponse">Original response created from the message</param>
        /// <param name="result">Result with messages</param>
        public static void Merge<T>(this ListResponse<T> originalResponse, IListResult<T> result)
        {
            if (result == null) return;
            originalResponse.Status = MergeStatus(originalResponse.Status, result.Status);
            originalResponse.Messages.AddRange(result.Errors);

            if (result.IsInvalid())
            {
                return;
            }
            originalResponse.Payload = result.Values;          
        }

        /// <summary>
        ///     Sets Http Status code of the response
        /// </summary>
        /// <param name="original">Original status code of the response</param>
        /// <param name="result">Status code of the result</param>
        /// <returns>Resposne Http status code</returns>
        private static HttpStatusCode MergeStatus(HttpStatusCode original, HttpStatusCode result)
        {
            switch (original)
            {
                case HttpStatusCode.OK:
                    return result;
                default:
                    return HttpStatusCode.BadRequest;
            }
        }

        public static ResponseMessage GetExceptionResponse(this Exception exception)
        {
            return new ResponseMessage
            {
                Message = exception.FlattenExceptionMessage(),
                Type = ResponseMessageType.Exception,
            };
        }

        public static bool IsValid<T>(this IResponse<T> originalResponse)
        {
            return originalResponse.Status == HttpStatusCode.OK;
        }
        public static bool IsInvalid<T>(this IResponse<T> originalResponse)
        {
            return !originalResponse.IsValid();
        }

        public static void Merge(this Meta originalResponse, Meta result)
        {
            originalResponse.TotalCount = result.TotalCount;
            originalResponse.PageSize = result.PageSize;
            originalResponse.CurrentPage = result.CurrentPage;
            originalResponse.UniqueTotal = result.UniqueTotal;
        }

        public static PagedResponse<BasicInfo<long>> ToPagedBasicInfoResponse<T>(this PagedResponse<T> response, string propertyName) 
            where T : IEntity
        {
            var dtoResponse = new PagedResponse<BasicInfo<long>>();
            dtoResponse.Merge(response);
            if (dtoResponse.NotOk || response.Payload == null)
                return dtoResponse;

            dtoResponse.Meta.Merge(response.Meta);
            dtoResponse.Payload = response.Payload.Select(x => new BasicInfo<long>
            {
                Id = x.Id,
                Name = typeof(T).GetProperty(propertyName).GetValue(x).ToString()
            }).OrderBy(x => x.Id);

            return dtoResponse;
        }
    }
}