using System;
using System.Linq;
using DotNetCore2018.Business.Services.Interfaces;
using DotNetCore2018.Business.Specifications;
using DotNetCore2018.Data.Entities;
using DotNetCore2018.WebApi.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DotNetCore2018.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    public class CategoryController : Controller
    {
        private readonly IDataService _dataService;
        private readonly ILogger<CategoryController> _logger;
        private readonly IConfiguration _configuration;

        public CategoryController(
            IDataService categoryService,
            ILogger<CategoryController> logger,
            IConfiguration configuration)
        {
            _dataService = categoryService;
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            var data = _dataService.GetAll<Category>()
                .OrderBy(x => x.Id)
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
                _logger.LogInformation($"File size is: {model.Image?.Length}");

                Guid? imageName = null;
                if (model.Image != null)
                {
                    imageName = Guid.NewGuid();
                    using (var writer = System.IO.File.OpenWrite($"./wwwroot/images/{imageName}.jpeg"))
                    {
                        model.Image.CopyTo(writer);
                    }
                }

                var category = new Category() { Name = model.Name, Image = imageName };
                _dataService.Add(category);

                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpGet]
        public IActionResult Image(int id)
        {
            _logger.LogInformation($"Category image requested. Category id: {id}");

            var category = _dataService.GetBy(new IdSpecification<Category>(id));
            if (category != null && category.Image != null)
            {
                var image = System.IO.File.OpenRead($"./wwwroot/images/{category.Image}.jpeg");
                return File(image, "image/jpeg");
            }

            var noImage = System.IO.File.OpenRead($"./wwwroot/images/NoImage.jpg");
            return File(noImage, "image/jpeg");
        }
    }
}