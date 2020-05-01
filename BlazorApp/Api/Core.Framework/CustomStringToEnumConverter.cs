using System;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System.Linq;

namespace Core.Framework
{
    public class CustomStringToEnumConverter : StringEnumConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (string.IsNullOrEmpty(reader.Value?.ToString()))
            {
                return null;
            }
            
            var type = !objectType.GenericTypeArguments.Any() ? objectType : objectType.GenericTypeArguments[0];

            return Enum.TryParse(type, reader.Value.ToString(), true, out object parsedEnumValue)
                   ? parsedEnumValue
                   : null;
        }
    }
}