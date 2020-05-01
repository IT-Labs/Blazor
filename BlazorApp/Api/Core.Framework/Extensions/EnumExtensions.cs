using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Core.Framework.Extensions
{
    public static class EnumExtensions
    {
        public static T ToEnum<T>(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return default(T);
            }

            var enumType = typeof(T);

            if (!enumType.GetTypeInfo().IsEnum)
            {
                throw new InvalidOperationException($"Type {typeof(T).Name} is not a  enum.");
            }

            value = string.Join("", value.Split(' '));

            return (T)Enum.Parse(enumType, value.Replace(".", ""), true);
        }
        /// <summary>
        /// Test a value to parse to an enum
        /// </summary>
        /// <typeparam name="T">Enum Type</typeparam>
        /// <param name="valueToParse">Value to parse to enum</param>
        /// <param name="parsed">value parsed to enum</param>
        /// <returns></returns>
        public static bool TryParse<T>(string valueToParse, out T parsed)
        {
            parsed = default(T);

            if (valueToParse == null)
                return false;

            int intTest;

            //check if int value for type exists
            if (int.TryParse(valueToParse, out intTest))
            {
                return TryParse(intTest, out parsed);
            }

            //try by name
            if (Enum.IsDefined(typeof(T), valueToParse))
            {
                parsed = (T)Enum.Parse(typeof(T), valueToParse, true);
                return true;
            }

            //try by name case insensitive
            foreach (string name in Enum.GetNames(typeof(T)))
            {
                if (name.Equals(valueToParse, StringComparison.OrdinalIgnoreCase))
                {
                    parsed = (T)Enum.Parse(typeof(T), name, true);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Test a value to parse to an enum
        /// </summary>
        /// <typeparam name="T">Enum Type</typeparam>
        /// <param name="valueToParse">Value to parse to enum</param>
        /// <param name="parsed">value parsed to enum</param>
        /// <returns></returns>
        public static bool TryParse<T>(int valueToParse, out T parsed)
        {
            //set out parameter to default
            parsed = default(T);

            //check for enum with valueToParse defined
            if (Enum.IsDefined(typeof(T), valueToParse))
            {
                parsed = (T)(object)valueToParse;
                return true;
            }

            //not found
            return false;
        }

        /// <summary>
        /// Will attempt to parse the enum and if unsuccessful will return the default enum value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="valueToParse"></param>
        /// <returns></returns>
        public static T Parse<T>(int valueToParse)
        {
            T parsed;
            TryParse(valueToParse, out parsed);
            return parsed;
        }

        /// <summary>
        /// Will attempt to parse the enum and if unsuccessful will return the default enum value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="valueToParse"></param>
        /// <returns></returns>
        public static T Parse<T>(string valueToParse)
        {
            T parsed;
            TryParse(valueToParse, out parsed);
            return parsed;
        }

        public static T ParseEnum<T>(this string input)
        {
            return Parse<T>(input);
        }
        public static TEnum? ParseNullableEnum<TEnum>(this string input) where TEnum : struct
        {
            if (string.IsNullOrEmpty(input))
                return default(TEnum?);

            var enumType = typeof(TEnum);

            var exists = Enum.GetNames(enumType).Any(item => item.ToLower() == input.ToLower());

            if (exists)
            {
                return (TEnum)Enum.Parse(enumType, input, true);
            }

            return default(TEnum?);
        }

        public static T ParseEnum<T>(this int input)
        {
            return Parse<T>(input);
        }

        /// <summary>
        /// Gets the DescriptionAttribute value of an enum value
        /// </summary>
        /// <param name="value">Enum Value</param>
        /// <returns>DescriptionAttribute value if it exists, else value.ToString()</returns>
        public static string GetDescription(this Enum value)
        {
            if (value == null)
                return null;

            var fi = value.GetType().GetField(value.ToString(), BindingFlags.Public | BindingFlags.Static);

            return GetDescription(fi);
        }

        public static string[] GetDescriptions<T>()
        {
            var list = new List<string>();

            var type = typeof(T);

            foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                list.Add(GetDescription(field));
            }

            return list.ToArray();
        }

        public static List<string> GetDescriptions<T>(this List<T> enums) where T : Enum
            => enums.Select(x => x.GetDescription()).ToList();

        public static IDictionary<string, string> GetValueDescriptionMap<T>() where T : struct
        {
            var map = new Dictionary<string, string>();

            foreach (var field in typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                map.Add(field.Name, GetDescription(field));
            }

            return map;
        }

        public static IDictionary<string, string> GetValueDescriptionMap<T>(string firstValue, string firstDescription) where T : struct
        {
            var map = new Dictionary<string, string> { { firstValue, firstDescription } };
            return map.Concat(GetValueDescriptionMap<T>()).ToDictionary(x => x.Key, x => x.Value);
        }

        public static IDictionary<string, string> GetValueDescriptionMap(params Enum[] values)
        {
            return values.ToDictionary(v => v.ToString(), v => v.GetDescription());
        }

        public static string GetDescription(this FieldInfo fieldInfo)
        {
            if (fieldInfo == null)
                return null;

            var attribute = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false)
                .OfType<DescriptionAttribute>().FirstOrDefault();

            return attribute == null ? fieldInfo.Name : attribute.Description;
        }

        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }

        public static string ToLower(this Enum item)
        {
            return item.ToString().ToLower();
        }

        public static string GetDisplayName(this Enum value)
        {
            if (value == null)
                return null;

            var fi = value.GetType().GetField(value.ToString(), BindingFlags.Public | BindingFlags.Static);

            return GetDisplayName(fi);
        }

        public static string GetDisplayName(this FieldInfo fieldInfo)
        {
            if (fieldInfo == null)
                return null;

            var attribute = fieldInfo.GetCustomAttributes(typeof(DisplayNameAttribute), false)
                .OfType<DisplayNameAttribute>().FirstOrDefault();

            return attribute == null ? fieldInfo.Name : attribute.DisplayName;
        }

        /// <summary>
        /// Gets the DescriptionAttribute value of an enum value
        /// </summary>
        /// <param name="value">Enum Value</param>
        /// <returns>DescriptionAttribute value if it exists, else value.ToString()</returns>
        public static string GetEnumDescription(this Enum value) 
        {
            if (value == null)
                return null;

            var fi = value.GetType().GetField(value.ToString(), BindingFlags.Public | BindingFlags.Static);

            return GetDescription(fi);
        }
    }
}
