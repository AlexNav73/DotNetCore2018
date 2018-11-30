using System.ComponentModel.DataAnnotations;

namespace DotNetCore2018.WebApi.ViewModels
{
    public class PasswordResetViewModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }
    }
}
