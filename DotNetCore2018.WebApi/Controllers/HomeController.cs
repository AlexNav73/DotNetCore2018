using DotNetCore2018.Business.Services.Interfaces;
using DotNetCore2018.Business.Specifications;
using DotNetCore2018.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCore2018.WebApi
{
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly ICategoryService _categoryService;

        public HomeController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [Route("[action]/{id?}")]
        public IActionResult Index(string id)
        {
            return View(_categoryService.GetBy(new IdSpecification<Category>(0)));
        }
    }
}