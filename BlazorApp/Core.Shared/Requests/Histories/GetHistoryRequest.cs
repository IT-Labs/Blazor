using Core.Shared.Interfaces;

namespace Core.Shared.Requests.Histories
{
    public class GetHistoryRequest : IRequest
    {
        public int Id { get; set; }
    }
}