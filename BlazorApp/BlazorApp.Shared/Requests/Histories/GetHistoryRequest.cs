using BlazorApp.Shared.Interfaces;

namespace BlazorApp.Shared.Requests.Histories
{
    public class GetHistoryRequest : IRequest
    {
        public int Id { get; set; }
    }
}