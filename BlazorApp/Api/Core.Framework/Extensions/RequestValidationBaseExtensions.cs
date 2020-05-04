using Core.Shared.Interfaces;
using Core.Shared.Validation;
using System;
using System.Net;

namespace Core.Framework.Extensions
{
    /// <summary>
    ///     Base class for validating requests
    /// </summary>
    public static class RequestValidationBaseExtensions
    {
        /// <summary>
        ///     Defines if the request is valid.
        ///     This method implements ValidatorFactory.GetValidator of the request to validate the request using its concrete
        ///     implementation
        /// </summary>
        /// <typeparam name="T">IRequest</typeparam>
        /// <typeparam name="TY">IResponse</typeparam>
        /// <param name="request">Input request</param>
        /// <param name="response">The response that is generated from this request</param>
        /// <param name="userContext"></param>
        /// <returns></returns>
        public static bool IsRequestInvalid<T, TY>(this T request, TY response, IUserContextInfo userContext = null)
            where T : IRequest
            where TY : IResponse
        {
            try
            {
                var validator = ContainerFactory.GetValidator<T>();

                if (validator == null)
                    return false;

                if (validator is ValidatorBase<T> contextValidator && userContext != null)
                {
                    contextValidator.UserContext = userContext;
                }

                var validationResult = validator.Validate(request);

                if (validationResult.IsValid)
                {
                    return false;
                }

                response.Merge(validationResult);
                return true;
            }
            catch (Exception ex)
            {
                response.Status = HttpStatusCode.InternalServerError;
                response.Messages.Add(ex.GetExceptionResponse());
                return true;
            }
        }
    }
}