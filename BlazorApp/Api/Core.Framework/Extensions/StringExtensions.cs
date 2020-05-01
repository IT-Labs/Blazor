using Humanizer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace Core.Framework.Extensions
{
    public static class StringExtensions
    {
        public static string SplitCamelCase(this string value)
        {
            var result = string.Empty;

            foreach (var ch in value)
            {
                if (char.IsLower(ch))
                { result += ch.ToString(); }
                else if (string.IsNullOrEmpty(result))
                { result += ch.ToString(); }
                else if (string.IsNullOrWhiteSpace(ch.ToString()))
                { result += ch.ToString(); }
                else
                { result += " " + ch.ToString(); }
            }

            return result;
        }

        public static List<string> SplitCamelCaseToList(this string value)
        {
            var result = string.Empty;
            var values = new List<string>();
            foreach (var ch in value)
            {
                if (char.IsLower(ch))
                { result += ch.ToString(); }
                else if (string.IsNullOrEmpty(result))
                {
                    result += ch.ToString();
                }
                else
                {
                    if (!string.IsNullOrEmpty(result))
                    {
                        values.Add(result);
                        result = string.Empty;
                    }

                    result += ch.ToString();
                }
            }
            if (!string.IsNullOrEmpty(result) && !values.Contains(result))
            {
                values.Add(result);
            }

            return values;
        }

        public static string UpTo(this string value, int length = 100)
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length <= length) return value ?? string.Empty;

            return value.Substring(0, length);
        }

        /// <summary>
        /// Get string value after [last] a.
        /// </summary>
        public static string StringAfter(this string value, string pattern)
        {
            int posA = value.LastIndexOf(pattern);
            if (posA == -1)
            {
                return "";
            }
            int adjustedPosA = posA + pattern.Length;
            if (adjustedPosA >= value.Length)
            {
                return "";
            }
            return value.Substring(adjustedPosA);
        }

        public static bool ContainsIgnoreCase(this string source, string toCheck)
        {
            return source?.IndexOf(toCheck, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        public static string ToTitleCase(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;

            return text.Humanize(LetterCasing.Title);
        }
    }
}