using System;

namespace BlazorApp.Shared.Dto
{
    public class HistoryResponse<T> where T : class
    {
        public Int64 Id { get; set; }
        public DateTime Date { get; set; }
        public T Item { get; set; }
    }
}
