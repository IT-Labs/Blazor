using Core.Shared.Requests;
using Core.Shared.Response;

namespace Core.Shared.Managers
{
    public interface IAuthManager
    {
        public Response<TokenResponse> GetToken(TokenRequest tokenRequest);
        public Response<TokenResponse> RefreshToken(RefreshTokenRequest refreshTokenRequest);
    }
}
