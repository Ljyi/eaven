using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Eaven.Ven.Host.Authorize
{
    public abstract class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (!context.User.Identity.IsAuthenticated)
            {
                context.Fail();
                return;
            }

            var userId = long.Parse(context.User.Claims.First(x => x.Type == JwtRegisteredClaimNames.Sub).Value);
            var roles = context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value;
            if (string.IsNullOrWhiteSpace(roles))
            {
                context.Fail();
                return;
            }

            var roleIds = roles.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(x => long.Parse(x));
            if (roleIds.Contains(1))
            {
                context.Succeed(requirement);
                return;
            }
            else
            {
                var attribute = (context.Resource as RouteEndpoint).Metadata.GetMetadata<PermissionAttribute>();
                var result = await CheckUserPermissions(userId, roleIds.ToArray(), attribute.Codes);
                if (result)
                {
                    context.Succeed(requirement);
                    return;
                }
            }
            context.Fail();
            return;
        }

        protected abstract Task<bool> CheckUserPermissions(long userId, long[] roleIds, string[] codes);
    }
}
