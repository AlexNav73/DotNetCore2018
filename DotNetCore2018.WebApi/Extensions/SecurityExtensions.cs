using System;
using System.Security.Claims;
using System.Threading.Tasks;
using DotNetCore2018.Data.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace DotNetCore2018.WebApi.Extensions
{
    public static class SecurityExtensions
    {
        public static async Task Login(this HttpContext self, User user)
        {
            var identity = new ClaimsIdentity(Constants.Security.Cookie);
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
            identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));

            await self.SignInAsync(Constants.Security.Cookie, new ClaimsPrincipal(identity));
        }

        public static async Task<bool> IsLoggedIn(this HttpContext self, UserManager<User> userManager)
        {
            var id = self.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier);
            if (id != null)
            {
                return await userManager.FindByIdAsync(id.Value) != null;
            }
            return false;
        }
    }
}