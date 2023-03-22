using LexiconLMS.App.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace LexiconLMS.App.Server.Services
{
    public class AvatarClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser>
    {
        public AvatarClaimsPrincipalFactory(UserManager<ApplicationUser> userManager, IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, optionsAccessor)
        {
        }

        public async override Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
        {
            var principal = await base.CreateAsync(user);

            if (!string.IsNullOrEmpty(user.Avatar))
            {
                ((ClaimsIdentity)principal.Identity).AddClaim(new Claim("Avatar", user.Avatar));
            }

            return principal;
        }
    }
}