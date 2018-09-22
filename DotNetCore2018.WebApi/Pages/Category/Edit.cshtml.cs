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
        private readonly ICategoryService _categoryService;

        [BindProperty]
        public DbCategory Category { get; set; }

        public EditModel(ICategoryService categoryService)
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
            Category = category;
            return Page();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                _categoryService.Update(Category);
                return RedirectToAction("Index", "Category");
            }
            return Page();
        }
    }
}