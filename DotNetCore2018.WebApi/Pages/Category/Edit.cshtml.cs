using System;
using DotNetCore2018.Business.Services.Interfaces;
using DotNetCore2018.Business.Specifications;
using DotNetCore2018.WebApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using DbCategory = DotNetCore2018.Data.Entities.Category;

namespace DotNetCore2018.WebApi.Pages.Category
{
    public sealed class EditModel : PageModel
    {
        private readonly IDataService _categoryService;

        [BindProperty]
        public CategoryViewModel Category { get; set; }

        public EditModel(IDataService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult OnGet(int id)
        {
            var category = _categoryService.GetBy(new IdSpecification<DbCategory>(id));
            if (category == null)
            {
                return RedirectToAction("Index", "Category");
            }
            Category = new CategoryViewModel()
            {
                Id = category.Id,
                Name = category.Name,
            };
            return Page();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                Guid? imageName = null;
                if (Category.Image != null)
                {
                    imageName = Guid.NewGuid();
                    using (var writer = System.IO.File.OpenWrite($"./wwwroot/images/{imageName}.jpeg"))
                    {
                        Category.Image.CopyTo(writer);
                    }
                }
                _categoryService.Update(new DbCategory()
                {
                    Id = Category.Id,
                    Name = Category.Name,
                    Image = imageName
                });
                return RedirectToAction("Index", "Category");
            }
            return Page();
        }
    }
}