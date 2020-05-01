using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorApp.Shared.ConfigurationValues
{
    public class EncryptionSettings
    {
        public string Key { get; set; }
        public string InitializationVector { get; set; }
    }
}
