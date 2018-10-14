using System.Linq;
using DotNetCore2018.Business.Services.Interfaces;
using DotNetCore2018.Data.Entities;
using DotNetCore2018.WebApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DotNetCore2018.WebApi.Controllers
{
    [Route("[controller]")]
    public class SupplierController : Controller
    {
        private readonly IDataService _dataService;
        private readonly ILogger<SupplierController> _logger;

        public SupplierController(
            IDataService dataService,
            ILogger<SupplierController> logger)
        {
            _dataService = dataService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var suppliers = _dataService.GetAll<Supplier>()
                .Select(x => new SupplierViewModel()
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToArray();
            return View(suppliers);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SupplierViewModel model)
        {
            if (ModelState.IsValid)
            {
                _logger.LogInformation("Post message received by create supplier method");

                var supplier = new Supplier() { Name = model.Name };
                _dataService.Add(supplier);

                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }
}