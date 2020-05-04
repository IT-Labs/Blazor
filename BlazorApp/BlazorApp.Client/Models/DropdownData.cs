using System;
using System.Collections.Generic;
using System.Linq;

namespace BlazorApp.Client.Models
{
    public class DropdownData<T> where T : Enum
    {
        public IEnumerable<EnumDropdown<T>> Data => Enum.GetValues(typeof(T)).Cast<T>().Select(x => new EnumDropdown<T>(x));

        public string Text { get; set; }
        public EnumDropdown<T> Item { get; set; }
    }
}
