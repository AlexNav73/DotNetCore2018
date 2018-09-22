using System.Linq;
using DotNetCore2018.Business.Services.Interfaces;
using DotNetCore2018.Business.Specifications;
using DotNetCore2018.Data;
using DotNetCore2018.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotNetCore2018.Business.Services
{
    public sealed class CategoryService : ICategoryService
    {
        private readonly AppContext _context;

        public CategoryService(AppContext context)
        {
            _context = context;
        }

        public void Add(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public Category[] GetAll() => _context.Categories.ToArray();

        public Category[] GetAllBy(Specification<Category> specification)
        {
            return _context.Categories
                .Where(specification.Expr)
                .ToArray();
        }

        public Category GetBy(Specification<Category> specification)
            => _context.Categories.FirstOrDefault(specification.Expr);

        public void Update(Category category)
        {
            _context.Attach(category).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}