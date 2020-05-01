using System;
using BlazorApp.Shared.Enums;
using BlazorApp.Shared.Response;

namespace Core.Framework.Extensions
{
    public static class ResponseMessageExtensions
    {

        public static ResponseMessage ResponseMessage(this Enum enumCode,
            ResponseMessageType type = ResponseMessageType.Validation,
            string entityName = "",
            string propertyName = "", params object[] args)
        {
            return new ResponseMessage
            {
                Code = enumCode.ToString(),
                Message = enumCode.GetDescription().Replace("{EntityName}", entityName.SplitCamelCase()).Replace("{PropertyName}", propertyName.SplitCamelCase()),
                Args = args ?? new object[0]
            };
        }
    }
    
}
