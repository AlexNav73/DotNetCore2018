using System;
using System.ComponentModel.DataAnnotations;
using DotNetCore2018.Core;

namespace DotNetCore2018.Data.Entities
{
    public sealed class Category : IHasId<int>
    {
        public int Id { get; set; }
        [Required, MaxLength(255)]
        public string Name { get; set; }
        public Guid? Image { get; set; }
    }
}