using System.ComponentModel.DataAnnotations;

namespace DotNetCore2018.WebApi.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        [Required, MinLength(3), MaxLength(255)]
        public string Name { get; set; }
        public CategoryViewModel Category { get; set; }
        public SupplierViewModel Supplier { get; set; }
    }
}