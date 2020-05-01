using System;

namespace BlazorApp.Shared
{
    /// <summary>Excludes an action method from the generated Swagger specification.</summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Parameter)]
    public class SwaggerIgnoreAttribute : Attribute
    {
    }
}
