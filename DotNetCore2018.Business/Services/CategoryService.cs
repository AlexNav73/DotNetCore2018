using System.Linq;
using DotNetCore2018.Business.Services.Interfaces;
using DotNetCore2018.Business.Specifications;
using DotNetCore2018.Data;
using DotNetCore2018.Data.Entities;

namespace DotNetCore2018.Business.Services
{
    public sealed class CategoryService : ICategoryService
    {
        private readonly DatabaseContext _context;

        public CategoryService(DatabaseContext context)
        {
            _context = context;
        }

        public void Add(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public Category[] GetAll()
        {
            return _context.Categories.ToArray();
        }

        public Category[] GetBy(Specification<Category> specification)
        {
            return _context.Categories
                .Where(specification.Expr)
                .ToArray();
        }
    }
}