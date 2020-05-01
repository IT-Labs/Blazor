using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;
using System.Threading.Tasks;

namespace Core.Framework
{
    public class DateTimeModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
                throw new ArgumentNullException(nameof(bindingContext));

            var stringValue = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).FirstValue;

            if (bindingContext.ModelType == typeof(DateTime?) && string.IsNullOrEmpty(stringValue))
            {
                bindingContext.Result = ModelBindingResult.Success(null);
                return Task.CompletedTask;
            }

            bindingContext.Result = 
                DateTime.TryParse(stringValue, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out DateTime result)
                ? ModelBindingResult.Success(result.Kind == DateTimeKind.Utc ? result : result.ToUniversalTime())
                : ModelBindingResult.Failed();

            return Task.CompletedTask;
        }
    }
}