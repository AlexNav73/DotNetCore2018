using System.Threading.Tasks;

namespace DotNetCore2018.Business.Services.Interfaces
{
    public interface IEmailSender
    {
        Task SendConfirmEmailAsync(string email, string loginUrl);
        Task SendPasswordResetAsync(string email, string passwordResetUrl);
    }
}