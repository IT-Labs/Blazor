using Core.Shared.Interfaces;

namespace Core.Shared.Requests
{
    public class EmailRequest : IRequest, IHaveEmail
    {
        public EmailRequest() { }
        public EmailRequest(string email)
        {
            Email = email;
        }
        public string Email { get; set; }
    }
}