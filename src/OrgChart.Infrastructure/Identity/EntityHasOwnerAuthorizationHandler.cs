using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using OrgChart.Core.Entities;
using System.Threading.Tasks;

namespace OrgChart.Infrastructure.Identity
{
    public class EntityAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, Entity>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public EntityAuthorizationHandler(UserManager<ApplicationUser> userManager) => _userManager = userManager;

        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            OperationAuthorizationRequirement requirement,
            Entity resource)
        {
            if (_userManager.GetUserId(context.User) == resource.UserId &&
                (requirement == Operations.Create
                || requirement == Operations.Read
                || requirement == Operations.Update
                || requirement == Operations.Delete))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
