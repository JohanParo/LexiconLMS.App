using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using IdentityModel;
using LexiconLMS.App.Server.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace LexiconLMS.App.Server.Services
{
    public class AvatarProfileService : IProfileService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AvatarProfileService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = await _userManager.GetUserAsync(context.Subject);
            var roleClaims = context.Subject.FindAll(JwtClaimTypes.Role);

            if (!string.IsNullOrEmpty(user.Avatar))
            {
                context.IssuedClaims.Add(new Claim("Avatar", user.Avatar));
            }
            if (!string.IsNullOrEmpty(user.FirstName) && !string.IsNullOrEmpty(user.LastName))
            {
                context.IssuedClaims.Add(new Claim("FullName", $"{user.FirstName} {user.LastName}"));
            }
            if (user.CourseId is not null)
            {
                context.IssuedClaims.Add(new Claim("CourseId", user.CourseId.ToString()));
            }
            
            context.IssuedClaims.AddRange(roleClaims);
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var user = await _userManager.GetUserAsync(context.Subject);

            context.IsActive = user != null;
        }
    }

}
