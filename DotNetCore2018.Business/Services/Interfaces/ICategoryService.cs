using DotNetCore2018.Business.Specifications;
using DotNetCore2018.Data.Entities;

namespace DotNetCore2018.Business.Services.Interfaces
{
    public interface ICategoryService
    {
        Category[] GetAll();
        Category[] GetAllBy(Specification<Category> specification);
        Category GetBy(Specification<Category> specification);
        void Update(Category category);
        void Add(Category category);
    }
}