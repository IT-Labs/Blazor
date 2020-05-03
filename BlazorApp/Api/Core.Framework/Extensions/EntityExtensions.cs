using Core.Shared;
using Core.Framework.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Core.Framework.Extensions
{
    public static class EntityExtensions
    {
        public static Dictionary<string, string> GetEnityPropertyValues<T>(this T entity, bool excludeDeletableProperties = false, List<string> additionalExcludedProperties = null)
            where T : DeletableEntity
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            List<string> excludedProperties = new List<string>();

            if (entity == null)
                return result;

            if (excludeDeletableProperties)
                excludedProperties.AddRange(typeof(DeletableEntity).GetProperties().Select(x => x.Name).ToList());

            if (additionalExcludedProperties != null && additionalExcludedProperties.Any())
                excludedProperties.AddRange(additionalExcludedProperties);

            var properties = typeof(T).GetProperties();
            foreach (var prop in properties.Where(x => !excludedProperties.Contains(x.Name)))
            {
                result.Add(prop.Name, prop.GetValue(entity)?.ToString() ?? string.Empty);
            }

            return result;
        }

        public static T MapDataRowToEntity<T>(this DataRow row) where T : class, new()
        {
            T entity = new T();
            var properties = (typeof(T)).GetProperties().ToList();

            foreach (var prop in properties)
            {
                var propertyValue = row[prop.Name.ToPostgreConvention()] == DBNull.Value ? null : row[prop.Name.ToPostgreConvention()];
                typeof(T).GetProperty(prop.Name).SetValue(entity, propertyValue);
            }

            return entity;
        }

        public static T DeserializeToEntity<T>(this string json) where T : DeletableEntity
        {
            if (string.IsNullOrWhiteSpace(json))
                return default(T);

            return JsonConvert.DeserializeObject<T>(json, new DbEntityJsonConverter());
        }
    }
}
