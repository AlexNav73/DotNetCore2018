using System.ComponentModel.DataAnnotations;

namespace DotNetCore2018.WebApi.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }
}