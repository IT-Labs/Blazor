using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.Shared.Enums;
using Microsoft.AspNetCore.Authorization;

namespace Core.Framework
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public PermissionRequirement(Permissions permission)
        {
            Permission = permission;
        }

        public Permissions Permission { get; protected set; }
    }

    public class PermissionRequirementHandler : AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            var requiredPermissions = requirement.Permission.ToListPermission(); 

            if (context.User.HasClaim(c => c.Type == CustomClaimTypes.Permissions))
            {
                var permissions = context.User.Claims.FirstOrDefault(x => x.Type == "custom:permissions").Value?.Split(",").ToList();
                if(permissions.Any(x => requiredPermissions.Contains(x)))
                    context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}