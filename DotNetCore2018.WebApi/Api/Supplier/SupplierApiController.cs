using DotNetCore2018.Business.Services.Interfaces;
using DotNetCore2018.Business.Specifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DbSupplier = DotNetCore2018.Data.Entities.Supplier;

namespace DotNetCore2018.WebApi.Api.Supplier
{
    [Produces("application/json")]
    [Route("api/v1/[controller]/[action]")]
    public sealed class SupplierApiController : ControllerBase
    {
        private readonly IDataService _dataService;
        private readonly ILogger<SupplierApiController> _logger;

        public SupplierApiController(
            IDataService dataService,
            ILogger<SupplierApiController> logger)
        {
            _dataService = dataService;
            _logger = logger;
        }

        [HttpDelete, ActionName("delete")]
        [ProducesResponseType(202)]
        public void DeleteById(int id)
        {
            _logger.LogWarning($"Delete supplier by id: {id}");
            var supplier = _dataService.GetBy(new IdSpecification<DbSupplier>(id));
            if (supplier != null)
            {
                _logger.LogWarning("Supplier found");
                _dataService.Delete(supplier);
            }
        }
    }
}