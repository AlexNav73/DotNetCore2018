using System.Linq;
using DotNetCore2018.Business.Services.Interfaces;
using DotNetCore2018.Business.Specifications;
using DotNetCore2018.WebApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using DbProduct = DotNetCore2018.Data.Entities.Product;

namespace DotNetCore2018.WebApi.Api.Product
{
    [Produces("application/json")]
    [Route("api/v1/products")]
    public sealed class ProductApiController : ControllerBase
    {
        private readonly IDataService _dataService;
        private readonly ILogger<ProductApiController> _logger;

        public ProductApiController(
            IDataService dataService,
            ILogger<ProductApiController> logger)
        {
            _dataService = dataService;
            _logger = logger;
        }

        [HttpDelete, ActionName("delete")]
        [ProducesResponseType(202)]
        public IActionResult DeleteById(int id)
        {
            _logger.LogWarning($"Delete product by id: {id}");
            var product = _dataService.GetBy(new IdSpecification<DbProduct>(id));
            if (product != null)
            {
                _logger.LogWarning("Product found");
                _dataService.Delete(product);
                return Accepted();
            }

            return NotFound();
        }

        [HttpGet]
        public ProductViewModel[] GetAll()
        {
            return _dataService.GetAll<DbProduct>()
                .Include(x => x.Category)
                .Include(x => x.Supplier)
                .Select(x => new ProductViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Category = new CategoryViewModel()
                    {
                        Id = x.Category.Id,
                        Name = x.Category.Name
                    },
                    Supplier = new SupplierViewModel()
                    {
                        Id = x.Supplier.Id,
                        Name = x.Supplier.Name
                    }
                })
                .ToArray();
        }
    }
}