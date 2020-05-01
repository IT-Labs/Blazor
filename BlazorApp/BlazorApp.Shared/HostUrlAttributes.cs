using BlazorApp.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorApp.Shared
{
    public class HostUrlAttribute : Attribute
    {
        public HostType HostType { get; set; }
    }
}
