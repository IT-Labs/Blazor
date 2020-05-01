using System;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using Core.Framework.Extensions;

namespace Core.Framework
{
    public class DateTimeConverter : DateTimeConverterBase
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime) || objectType == typeof(DateTime?);
        }
        
        //if the datetime property in the body of the request is utc return the property
        //else convert that property to utc before returning it
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader?.Value != null)
            {
                var datetime = reader.Value as DateTime?;
                return !datetime.HasValue 
                       ? reader?.Value 
                       : datetime.Value.Kind != DateTimeKind.Utc 
                       ? datetime.Value.ToUniversalTime()
                       : datetime.Value;
            }

            return reader?.Value;
        }
        
        //converts all datetimes in the responses from utc to est
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value != null && DateTime.TryParse(value.ToString(), out DateTime result))
            {
                writer.WriteValue(result.FromUtcToEst());
                writer.Flush();
            }
        }
    }
}