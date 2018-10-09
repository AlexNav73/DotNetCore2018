using System.ComponentModel.DataAnnotations;

namespace DotNetCore2018.WebApi.ViewModels
{
    public class SupplierViewModel
    {
        public int Id { get; set; }
        [Required, MinLength(3), MaxLength(255)]
        public string Name { get; set; }
    }
}