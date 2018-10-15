using System;
using System.Linq;
using DotNetCore2018.Business.Services.Interfaces;
using DotNetCore2018.Business.Specifications;
using DotNetCore2018.Data.Entities;
using DotNetCore2018.WebApi.Filters;
using DotNetCore2018.WebApi.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DotNetCore2018.WebApi.Controllers
{
    [LogAction]
    [Route("[controller]")]
    public class CategoryController : Controller
    {
        private readonly IDataService _dataService;
        private readonly ILogger<CategoryController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IFileService _fileService;

        public CategoryController(
            IDataService categoryService,
            ILogger<CategoryController> logger,
            IConfiguration configuration,
            IFileService fileService)
        {
            _dataService = categoryService;
            _logger = logger;
            _configuration = configuration;
            _fileService = fileService;
        }

        [HttpGet]
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

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                _logger.LogInformation("Post message received by create category method");

                var category = new Category() 
                { 
                    Name = model.Name,
                    Image = _fileService.SaveFile(model.Image?.OpenReadStream())
                };
                _dataService.Add(category);

                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpGet("image/{id}")]
        public IActionResult Image(int id)
        {
            _logger.LogInformation($"Category image requested. Category id: {id}");

            var category = _dataService.GetBy(new IdSpecification<Category>(id));
            if (category?.Image != null)
            {
                return File(_fileService.OpenFile(category.Image.Value), "image/jpeg");
            }

            return File(_fileService.NoImageFile(), "image/jpeg");
        }
    }
}