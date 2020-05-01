using System.Collections.Generic;
using System.Linq;
using BlazorApp.Shared.Interfaces;

namespace Core.Framework.Extensions
{
    public static class ConversionHelper
    {
        public static string Seriliaze<T>(this T entity, IPartialRequest request)
        {
            if (!request.Fields.Any())
                return string.Empty;

            var entityType = typeof(T);
            var data = new Dictionary<string, string>();
            foreach (var info in entityType.GetProperties().Where(x => x.CanRead))
            {
                if (info.GetType().GetInterfaces().Any(y => y.IsConstructedGenericType && y.GetGenericTypeDefinition() == typeof(IList<>)))
                {

                }
                else if (request.Fields.Contains(info.Name))
                {
                    var value = info.GetValue(info.Name);
                    data.Add(info.Name, value?.ToString() ?? string.Empty);
                }
            }

            return string.Empty;
        }
    }
}