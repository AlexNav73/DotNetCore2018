using System.ComponentModel.DataAnnotations;

namespace DotNetCore2018.WebApi.ViewModels
{
    public class NewProductViewModel
    {
        [Required, MinLength(3), MaxLength(255)]
        public string Name { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public int SupplierId { get; set; }
    }
}