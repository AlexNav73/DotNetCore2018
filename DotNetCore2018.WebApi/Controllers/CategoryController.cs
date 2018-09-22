using System.Linq;
using DotNetCore2018.Business.Services.Interfaces;
using DotNetCore2018.Data.Entities;
using DotNetCore2018.WebApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DotNetCore2018.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(
            ICategoryService categoryService,
            ILogger<CategoryController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var data = _categoryService.GetAll()
                .Select(x => new CategoryViewModel()
                {
                    Id = x.Id,
                    Name = x.Name
                });

            return View(data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                _logger.LogInformation("Post message received by create category method");

                var category = new Category() { Name = model.Name };
                _categoryService.Add(category);

                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }
}