using BlazorApp.Shared.Interfaces;

namespace BlazorApp.Shared.Requests
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