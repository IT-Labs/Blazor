using System;
using System.Text.RegularExpressions;

namespace BlazorApp.Client.Models
{
    public class EnumDropdown<T> where T : Enum
    {
        public T Value { get; set; }
        public string Description { get; set; }

        public EnumDropdown(T value)
        {
            Value = value;
            Description = Regex.Replace(Value.ToString(), "([A-Z])", " $1").Trim();
        }

        public override string ToString()
        {
            return Description;
        }
    }
}
