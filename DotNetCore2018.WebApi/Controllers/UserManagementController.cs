using DotNetCore2018.Data;
using DotNetCore2018.Data.Entities;
using DotNetCore2018.WebApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DotNetCore2018.WebApi.Controllers
{
    [Authorize(Roles = Constants.Roles.User + ", " + Constants.Roles.Administrator)]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class UserManagementController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IAppContext _context;

        public UserManagementController(
            IAppContext context,
            UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var users = _context.Users
                .Select(x => new UserViewModel()
                {
                    UserId = x.Id,
                    UserName = x.UserName,
                    Roles = string.Join(", ", _userManager.GetRolesAsync(x).GetAwaiter().GetResult())
                });
            return View(users);
        }
    }
}
