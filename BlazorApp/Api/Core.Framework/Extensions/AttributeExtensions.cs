using BlazorApp.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Core.Framework.Extensions
{
    public static class AttributeExtensions
    {

        public static bool HasAttribute<T>(this Enum enumValue) where T : Attribute
        {
            if (enumValue == null)
                return false;

            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString(), BindingFlags.Public | BindingFlags.Static);
            return fieldInfo != null && fieldInfo.CustomAttributes.Any(x => x.AttributeType == typeof(T));
        }

        public static bool HasAttribute(this Enum enumValue, Type attrType)
        {
            if (enumValue == null)
                return false;

            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString(), BindingFlags.Public | BindingFlags.Static);
            return fieldInfo != null && fieldInfo.CustomAttributes.Any(x => x.AttributeType == attrType);
        }


        public static bool HasAttribute<T>(this PropertyInfo propertyInfo) where T : Attribute
        {
            if (propertyInfo == null)
                return false;
            return propertyInfo.CustomAttributes.Any(x => x.AttributeType == typeof(T));
        }

        public static bool HasAttribute(this PropertyInfo propertyInfo, Type attrType)
        {
            if (propertyInfo == null)
                return false;

            return propertyInfo.CustomAttributes.Any(x => x.AttributeType == attrType);
        }

        public static T GetAttribute<T>(this Enum enumValue) where T : Attribute
        {
            return enumValue?.GetType().GetMember(enumValue.ToString()).FirstOrDefault()?.GetCustomAttributes(typeof(T), false).FirstOrDefault() as T;
        }

        public static Attribute GetAttribute(this Enum enumValue, Type attrType)
        {
            return enumValue?.GetType().GetMember(enumValue.ToString()).FirstOrDefault()?.GetCustomAttributes(attrType, false).FirstOrDefault() as Attribute;
        }

        public static T GetAttribute<T>(this PropertyInfo propertyInfo) where T : Attribute
        {
            return propertyInfo?.GetCustomAttributes(typeof(T), false).FirstOrDefault() as T;
        }

        public static Attribute GetAttribute(this PropertyInfo propertyInfo, Type attrType)
        {
            return (Attribute)propertyInfo?.GetCustomAttributes(attrType, false).FirstOrDefault();
        }

        public static string GetDescription(this PropertyInfo propertyInfo)
        {
            if (propertyInfo == null)
                return null;

            var attribute = propertyInfo.GetCustomAttributes(typeof(DescriptionAttribute), false)
                .OfType<DescriptionAttribute>().FirstOrDefault();

            return attribute == null ? propertyInfo.Name.SplitCamelCase() : attribute.Description;
        }

        public static List<Attribute> GetEnumAttributes<Attribute, T>(this T value) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
                return new List<Attribute>();

            var attribues = value.GetType().GetMember(value.ToString()).FirstOrDefault()?
                .GetCustomAttributes(typeof(Attribute), false).OfType<Attribute>().ToList();

            return attribues;
        }
    }
}