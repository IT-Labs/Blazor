using System;

namespace Core.Shared.Dto
{
    public class TokenData<T>
    {
        public int Expiration { get; set; }
        public T Data { get; set; }
        public bool RefreshToken(int expirationSeconds)
            => Expiration - DateTimeOffset.UtcNow.ToUnixTimeSeconds() < expirationSeconds / 2;
    }
}