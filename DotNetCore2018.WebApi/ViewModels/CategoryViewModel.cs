using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace DotNetCore2018.WebApi.ViewModels
{
    public sealed class CategoryViewModel
    {
        public int Id { get; set; }
        [Required, MinLength(3), MaxLength(255)]
        public string Name { get; set; }
        public IFormFile Image { get; set; }
    }
}