using System.Threading.Tasks;
using DotNetCore2018.Data.Entities;
using DotNetCore2018.WebApi.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCore2018.WebApi.ViewComponents
{
    public class Navigation : ViewComponent
    {
        private readonly UserManager<User> _userManager;

        public Navigation(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("Index", await HttpContext.IsLoggedIn(_userManager));
        }
    }
}