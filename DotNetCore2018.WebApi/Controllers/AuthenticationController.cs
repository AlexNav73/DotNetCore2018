using System.Linq;
using System.Threading.Tasks;
using DotNetCore2018.Business.Services.Interfaces;
using DotNetCore2018.WebApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DotNetCore2018.WebApi.Controllers
{
    [Authorize]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(
            IAuthenticationService authenticationService,
            ILogger<AuthenticationController> logger)
        {
            _authenticationService = authenticationService;
            _logger = logger;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _authenticationService.RegisterUser(model.UserName, model.Email, model.Password);
                if (result.Errors.Any())
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                        return View();
                    }
                }
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (await _authenticationService.LoginUser(model.UserName, model.Password))
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Invalid UserName or Password");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _authenticationService.LogoutUser(HttpContext);
            return RedirectToAction("Index", "Home");
        }
    }
}