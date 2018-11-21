using System;
using System.Linq;
using System.Threading.Tasks;
using DotNetCore2018.Business.Services.Interfaces;
using DotNetCore2018.Business.Services.Models;
using DotNetCore2018.Data.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace DotNetCore2018.Business.Services
{
    public class AuthenticationService : Interfaces.IAuthenticationService
    {
        private readonly IEmailSender _emailSender;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<AuthenticationService> _logger;

        public AuthenticationService(
            IEmailSender emailSender,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<AuthenticationService> logger)
        {
            _emailSender = emailSender;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        public async Task<AuthenticationLoginResult> RegisterUser(string userName, string email, string password)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                var result = await _userManager.CreateAsync(new User()
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = userName,
                    Email = email
                }, password);

                if (result.Errors.Any())
                {
                    _logger.LogWarning($"User [{userName}] was not registered");
                    return new AuthenticationLoginResult()
                    {
                        Errors = result.Errors
                            .Select(x => new AuthenticationLoginError()
                            {
                                Code = x.Code,
                                Description = x.Description
                            })
                            .ToArray()
                    };
                }

                await _signInManager.PasswordSignInAsync(userName, password, false, false);

                _logger.LogInformation($"User [{userName}] was registered");

                return new AuthenticationLoginResult() { Success = true };
            }

            return new AuthenticationLoginResult()
            {
                Errors = new[] 
                {
                    new AuthenticationLoginError()
                    {
                        Code = string.Empty,
                        Description = "User already exists"
                    }
                }
            };
        }

        public async Task<bool> LoginUser(string userName, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(userName, password, false, false);
            return result.Succeeded;
        }

        public async Task LogoutUser(HttpContext context)
            => await context.SignOutAsync(IdentityConstants.ApplicationScheme);

        private async Task SendEmailAsync(string email, string loginUrl)
            => await _emailSender.SendEmailAsync(email, Subject(), Body(loginUrl));

        private string Body(string loginUrl)
            => $"To complete registration, follow <a href=\"{loginUrl}\">this link</a>";

        private string Subject()
            => "Complete registration on DotNetCore2018 site";
    }
}