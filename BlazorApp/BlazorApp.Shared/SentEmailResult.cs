using System;

namespace BlazorApp.Shared
{
    public class SentEmailResult
    {
        public Exception ErrorException { get; set; }
        public string MessageId { get; set; }
        public bool HasError { get; set; }

        public SentEmailResult()
        {
            HasError = false;
            ErrorException = null;
            MessageId = string.Empty;
        }
    }
}