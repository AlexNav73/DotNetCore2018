using System.Linq;
using DotNetCore2018.Business.Services.Interfaces;
using DotNetCore2018.Data.Entities;
using DotNetCore2018.WebApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DotNetCore2018.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    public class ProductController : Controller
    {
        private readonly IDataService _dataService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(
            IDataService dataService,
            ILogger<ProductController> logger)
        {
            _dataService = dataService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var products = _dataService.GetAll<Product>()
                .Include(x => x.Category)
                .Include(x => x.Supplier)
                .AsEnumerable()
                .Select(x => new ProductViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Category = new CategoryViewModel()
                    {
                        Id = x.Category != null ? x.Category.Id : 0,
                        Name = x.Category?.Name
                    },
                    Supplier = new SupplierViewModel()
                    {
                        Id = x.Supplier != null ? x.Supplier.Id : 0,
                        Name = x.Supplier?.Name
                    }
                })
                .ToArray();

            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = _dataService.GetAll<Category>()
                    .Select(x => new SelectListItem()
                    {
                        Value = x.Id.ToString(),
                        Text = x.Name
                    })
                    .ToArray();
            ViewBag.Suppliers = _dataService.GetAll<Supplier>()
                .Select(x => new SelectListItem()
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                })
                .ToArray();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(NewProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                _logger.LogInformation("Post message received by create product method");

                var product = new Product() 
                { 
                    Name = model.Name,
                    SupplierId = model.SupplierId,
                    CategoryId = model.CategoryId
                };
                _dataService.Add(product);

                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }
}