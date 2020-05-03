namespace Core.Shared.Requests
{
    public class TokenRequest
    {
        public string Code { get; set; }
        public string RedirectUri { get; set; }
    }
}
