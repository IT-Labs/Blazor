using System;

namespace BlazorApp.Shared.Dto
{
    public class OldNewValueResponse
    {
        public DateTime Date { get; set; }
        public string User { get; set; }
        public string Field { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public string Action { get; set; }
    }
}
