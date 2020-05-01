using BlazorApp.Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Core.Framework
{
    /// <summary>
    /// Custom ApiControllerActionInvoker which sets response code based on GI.Contracts.IResponse
    /// </summary>

    public class ResponseFilter : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            var content = context.Result as ObjectResult;

            if (content?.Value is IResponse)
            {
                context.HttpContext.Response.StatusCode = (int)((IResponse)content.Value).Status;
            }
        }
    }
}