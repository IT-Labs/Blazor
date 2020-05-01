using BlazorApp.Shared.Requests;
using BlazorApp.Shared.Response;

namespace BlazorApp.Shared.Managers
{
    public interface IAuthManager
    {
        public Response<TokenResponse> GetToken(TokenRequest tokenRequest);
        public Response<TokenResponse> RefreshToken(RefreshTokenRequest refreshTokenRequest);
    }
}
