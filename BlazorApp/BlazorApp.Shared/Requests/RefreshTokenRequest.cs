namespace BlazorApp.Shared.Requests
{
    public class RefreshTokenRequest
    {
        public string RefreshToken { get; set; }
        public string RedirectUri { get; set; }
    }
}
