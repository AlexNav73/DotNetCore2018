using System;
using System.Linq;
using System.Threading.Tasks;
using DotNetCore2018.Business.Services.Interfaces;
using DotNetCore2018.Data.Entities;
using DotNetCore2018.WebApi.ViewModels;
using Microsoft.AspNetCore.Authentication;
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
        private string UserId = "userId";
        private string PasswordResetToken = "passwordResetToken";

        private readonly IEmailSender _emailSender;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<UserRole> _roleManager;
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(
            IEmailSender emailSender,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<UserRole> roleManager,
            ILogger<AuthenticationController> logger)
        {
            _emailSender = emailSender;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
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
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user == null)
                {
                    user = new User()
                    {
                        Id = Guid.NewGuid(),
                        UserName = model.UserName,
                        Email = model.Email
                    };
                    var result = await _userManager.CreateAsync(user, model.Password);

                    if (result.Errors.Any())
                    {
                        _logger.LogWarning($"User [{model.UserName}] was not registered");
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                            return View();
                        }
                    }

                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Action(
                        nameof(ConfirmEmail),
                        "Authentication",
                        values: new { userId = user.Id, code },
                        protocol: Request.Scheme);

                    await _emailSender.SendConfirmEmailAsync(model.Email, callbackUrl);

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "User already exists");
            }
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync(Constants.Roles.Administrator))
                {
                    await _roleManager.CreateAsync(new UserRole(Constants.Roles.Administrator));
                }
                await _userManager.AddToRoleAsync(user, Constants.Roles.Administrator);
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home");
            }
            return Forbid();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult PasswordReset()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> PasswordReset(PasswordResetViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var callbackUrl = Url.Action(
                        nameof(PasswordResetSubmit),
                        "Authentication",
                        values: new { userId = user.Id, code },
                        protocol: Request.Scheme);

                    await _emailSender.SendPasswordResetAsync(model.Email, callbackUrl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User with that Email does not exists");
                }
            }
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult PasswordResetSubmit(string userId, string code)
        {
            TempData[UserId] = userId;
            TempData[PasswordResetToken] = code;

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> PasswordResetSubmit(PasswordResetConfirmedViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = (string)TempData[UserId];
                var code = (string)TempData[PasswordResetToken];

                var user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    var result = await _userManager.ResetPasswordAsync(user, code, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong. Try to resend Email");
                }
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
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
                if (result.Succeeded)
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
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}