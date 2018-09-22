using DotNetCore2018.Business.Specifications;
using DotNetCore2018.Data.Entities;

namespace DotNetCore2018.Business.Services.Interfaces
{
    public interface ICategoryService
    {
        Category[] GetAll();
        Category[] GetBy(Specification<Category> specification);
        void Add(Category category);
    }
}