using Core.Shared.Dto;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace Core.Framework.Extensions
{
    public static class TokenExtensions
    {
        public static TokenData<CurrentUserAuthDetails> ValidateToken(this string token, int expityTolerance = 0)
        {
            var response = new TokenData<CurrentUserAuthDetails>();
            token = token.Substring("Bearer ".Length).Trim();

            try
            {
                var jwtSecurityToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
                var claims = jwtSecurityToken.Claims.ToList();
                var json = claims
                    .FirstOrDefault(_ => _.Type == ClaimTypes.UserData)
                    .Value;
                response = JsonConvert.DeserializeObject<TokenData<CurrentUserAuthDetails>>(json);
            }
            catch
            {
                return default;
            }

            if (DateTimeOffset.UtcNow.ToUnixTimeSeconds() - response.Expiration > expityTolerance)
                return default;

            return response;
        }
    }
}
