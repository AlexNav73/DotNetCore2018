using System;
using DotNetCore2018.Business.Services.Interfaces;
using DotNetCore2018.Business.Specifications;
using DotNetCore2018.Core.Breadcrumbs;
using DotNetCore2018.WebApi.Controllers;
using DotNetCore2018.WebApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using DbCategory = DotNetCore2018.Data.Entities.Category;

namespace DotNetCore2018.WebApi.Pages.Category
{
    [Breadcrumb("Edit", Parent = typeof(CategoryController))]
    public sealed class EditModel : PageModel
    {
        private readonly IDataService _categoryService;
        private readonly IFileService _fileService;

        [BindProperty]
        public CategoryViewModel Category { get; set; }

        public EditModel(IDataService categoryService, IFileService fileService)
        {
            _categoryService = categoryService;
            _fileService = fileService;
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
                _categoryService.Update(new DbCategory()
                {
                    Id = Category.Id,
                    Name = Category.Name,
                    Image = _fileService.SaveFile(Category.Image.OpenReadStream())
                });
                return RedirectToAction("Index", "Category");
            }
            return Page();
        }
    }
}