using DotNetCore2018.Data.Entities;
using DotNetCore2018.WebApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DotNetCore2018.WebApi.Controllers
{
    [Authorize(Roles = Constants.Roles.Administrator)]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class UserManagementController : Controller
    {
        private readonly UserManager<User> _userManager;

        public UserManagementController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var users = _userManager.Users.ToArray();
            var models = users
                .Select(x => new UserViewModel()
                {
                    UserId = x.Id,
                    UserName = x.UserName,
                    Roles = string.Join(", ", _userManager.GetRolesAsync(x).GetAwaiter().GetResult())
                });
            return View(models);
        }
    }
}
