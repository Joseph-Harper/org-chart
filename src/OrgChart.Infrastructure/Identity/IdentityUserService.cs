using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using OrgChart.Core.Interfaces;
using System.Security.Claims;

namespace OrgChart.Infrastructure.Identity
{
    public class IdentityUserProvider : IUserProvider
    {
        private UserManager<ApplicationUser> _userManager;
        private IHttpContextAccessor _httpContextAccessor;

        public IdentityUserProvider(UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserId()
        {
            // User is null when database sample data is seeded during startup.
            var user = _httpContextAccessor?.HttpContext?.User ?? new ClaimsPrincipal();
            return _userManager.GetUserId(user);
        }
    }
}
