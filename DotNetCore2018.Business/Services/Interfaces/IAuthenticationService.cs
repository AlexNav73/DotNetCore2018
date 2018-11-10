using System.Threading.Tasks;
using DotNetCore2018.Business.Services.Models;
using Microsoft.AspNetCore.Http;

namespace DotNetCore2018.Business.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<AuthenticationLoginResult> RegisterUser(string userName, string email, string password);
        Task<bool> LoginUser(string userName, string password);
        Task LogoutUser(HttpContext context);
    }
}