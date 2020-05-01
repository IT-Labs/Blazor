using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using BlazorApp.Shared.Enums;
using Microsoft.AspNetCore.Http;

namespace Core.Framework.Extensions
{
    public static class HttpContextExtensions
    {
        /// <summary>
        /// UserIdClaimMatch
        /// </summary>
        /// <param name="context"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static bool UserIdClaimMatch(this IHttpContextAccessor context, long userId)
        {
            var userClaimsId = context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == CustomClaimTypes.UserId)?.Value;  
            return userClaimsId != null &&  userClaimsId == userId.ToString();  
        }

        public static string Claim(this IHttpContextAccessor context, string claim)
        {
            return Claim(context.HttpContext.User.Claims.ToList(), claim);
        }
        public static string Claim(this List<Claim> claims, string claim)
        {
            return claims.FirstOrDefault(x => x.Type == claim)?.Value;
        }
        public static List<Guid> ToGuidList(this string content, char separator = ';')
        {
            if (string.IsNullOrWhiteSpace(content)) return new List<Guid>();

            return content.Split(separator).Select(x => new Guid(x)).ToList();
        }

        /// <summary>
        /// UserHasClaim
        /// </summary>
        /// <param name="context"></param>
        /// <param name="permission"></param>
        /// <returns></returns>
        public static bool UserHasClaim(this IHttpContextAccessor context, Enum permission)
        {
            return context.HttpContext.User.Claims.Any(x => x.Type == ClaimTypes.Role && x.Value.Contains(permission.ToString()));
        }

        public static UserContextInfo GetUserContext(this IHttpContextAccessor contextAccessor)
        {
            var userId = (contextAccessor.Claim(CustomClaimTypes.UserId));
            var contextInfo = new UserContextInfo
            {
                UserId = long.TryParse(userId, out long parsed) ? parsed : (long?)null,
                Token = contextAccessor.HttpContext.Request.Headers["Authorization"],
                Username = contextAccessor.Claim(ClaimTypes.Email)?.Split('@')[0]
            };

            return contextInfo;
        }
    }
}
