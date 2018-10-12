using System.ComponentModel.DataAnnotations.Schema;
using DotNetCore2018.Core;

namespace DotNetCore2018.Data.Entities
{
    public sealed class Product : IHasId<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        [ForeignKey(nameof(Supplier))]
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
    }
}