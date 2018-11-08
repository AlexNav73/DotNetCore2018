using System.Threading.Tasks;
using DotNetCore2018.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCore2018.WebApi.ViewComponents
{
    public class UserSignInOptions : ViewComponent
    {
        private readonly SignInManager<User> _signInManager;

        public UserSignInOptions(SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (_signInManager.IsSignedIn(HttpContext.User))
            {
                return View("AuthorizedUser");
            }
            return View("Anonymous");
        }
    }
}