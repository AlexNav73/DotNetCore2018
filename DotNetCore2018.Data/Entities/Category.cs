using System.ComponentModel.DataAnnotations;

namespace DotNetCore2018.Data.Entities
{
    public sealed class Category : IHasId<int>
    {
        public int Id { get; set; }
        [Required, MaxLength(255)]
        public string Name { get; set; }
    }
}