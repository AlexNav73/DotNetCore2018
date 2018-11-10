using System.Threading.Tasks;

namespace DotNetCore2018.Business.Services.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string body);
    }
}