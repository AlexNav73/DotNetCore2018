using System.Linq;
using DotNetCore2018.Business.Services.Interfaces;
using DotNetCore2018.Business.Specifications;
using DotNetCore2018.Data.Entities;
using DotNetCore2018.WebApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DotNetCore2018.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    public class CategoryController : Controller
    {
        private readonly IDataService _categoryService;
        private readonly ILogger<CategoryController> _logger;
        private readonly IConfiguration _configuration;

        public CategoryController(
            IDataService categoryService,
            ILogger<CategoryController> logger,
            IConfiguration configuration)
        {
            _categoryService = categoryService;
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            var data = _categoryService.GetAll<Category>()
                .Take(_configuration.GetValue<int>("categoryNum"))
                .Select(x => new CategoryViewModel()
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToArray();

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