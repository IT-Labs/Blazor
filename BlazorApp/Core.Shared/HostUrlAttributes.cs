using Core.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Shared
{
    public class HostUrlAttribute : Attribute
    {
        public HostType HostType { get; set; }
    }
}
