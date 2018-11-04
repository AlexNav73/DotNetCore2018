using System;
using System.Linq;
using System.Threading.Tasks;
using DotNetCore2018.Data.Entities;
using DotNetCore2018.WebApi.Extensions;
using DotNetCore2018.WebApi.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DotNetCore2018.WebApi.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class AuthenticationController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(
            UserManager<User> userManager,
            ILogger<AuthenticationController> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user == null)
                {
                    var result = await _userManager.CreateAsync(new User()
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserName = model.UserName
                    }, model.Password);

                    if (result.Errors.Any())
                    {
                        _logger.LogWarning($"User [{model.UserName}] was not registered");
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(error.Code, error.Description);
                            return View();
                        }
                    }

                    _logger.LogInformation($"User [{model.UserName}] was registered");
                }
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    await HttpContext.Login(user);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Invalid UserName or Password");
            }
            return View();
        }
    }
}