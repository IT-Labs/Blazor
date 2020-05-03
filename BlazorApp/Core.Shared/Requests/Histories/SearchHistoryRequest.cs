using Core.Shared.Interfaces;
using System;
using System.Collections.Generic;

namespace Core.Shared.Requests.Histories
{
    public class SearchHistoryRequest : PageableRequest, IRequest
    {
        public Guid? RecordId { get; set; }
        public DateTime? Date { get; set; }
        public Dictionary<string, string> NewValParameters { get; set; } = new Dictionary<string, string>();
    }
}
