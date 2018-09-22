using System.ComponentModel.DataAnnotations;

namespace DotNetCore2018.WebApi.ViewModels
{
    public sealed class CategoryViewModel
    {
        public int Id { get; set; }
        [Required, MaxLength(255)]
        public string Name { get; set; }
    }
}