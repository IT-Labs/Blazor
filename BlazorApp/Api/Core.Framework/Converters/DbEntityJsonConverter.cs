using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Reflection;

namespace Core.Framework.Converters
{
    public class DbEntityJsonConverter : JsonConverter
    {
        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            if (objectType.GetTypeInfo() == typeof(string))
                return false;

            return objectType.GetTypeInfo().IsClass;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            object instance = Activator.CreateInstance(objectType);
            var properties = objectType.GetProperties();

            JObject jo = JObject.Load(reader);
            foreach (JProperty jp in jo.Properties())
            {
                string propName = RevertFromPostgreConvention(jp.Name);
                PropertyInfo prop = properties.FirstOrDefault(pi => pi.CanWrite && pi.Name.ToLowerInvariant() == propName);
                prop?.SetValue(instance, jp.Value.ToObject(prop.PropertyType, serializer));
            }

            return instance;
        }

        private string RevertFromPostgreConvention(string value, string splitter = "_")
        {
            string result = string.Empty;

            if (string.IsNullOrWhiteSpace(value))
                return result;

            var array = value.Split(splitter);
            foreach (var part in array)
            {
                result += part;
            }

            return result.ToLowerInvariant();
        }
    }
}
