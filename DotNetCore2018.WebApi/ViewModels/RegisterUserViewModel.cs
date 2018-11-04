using System.ComponentModel.DataAnnotations;

namespace DotNetCore2018.WebApi.ViewModels
{
    public class RegisterUserViewModel
    {
        [Required, MaxLength(255)]
        public string UserName { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, Compare(nameof(Password))]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}