using System.Linq;
using System.Threading.Tasks;
using DotNetCore2018.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCore2018.WebApi.ViewComponents
{
    [AllowAnonymous]
    public class Navigation : ViewComponent
    {
        private readonly SignInManager<User> _signInManager;

        public Navigation(SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("Index", _signInManager.IsSignedIn(HttpContext.User));
        }
    }
}