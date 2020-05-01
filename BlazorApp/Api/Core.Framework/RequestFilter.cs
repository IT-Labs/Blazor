using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;

namespace Core.Framework
{
    public sealed class RequestFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid) 
            {
                context.Result = new BadRequestObjectResult(new {
                    Messages = new List<object>
                    {
                        new { Message = "Some of the values you entered are incorrect. Please correct them and try again." }
                    }});
            }
        }
    }
}