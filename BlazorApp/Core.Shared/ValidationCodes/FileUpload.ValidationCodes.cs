﻿using System.ComponentModel;

namespace Core.Shared.ValidationCodes
{
    public static partial class ValidationCodes
    {
        public enum FileUpload
        {
            [Description("Request cannot be null!")]
            Fu001,
            Fu002,
            Fu003,
            Fu004
        }
    }
}
