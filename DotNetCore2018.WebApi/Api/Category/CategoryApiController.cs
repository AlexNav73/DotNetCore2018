using DotNetCore2018.Business.Services.Interfaces;
using DotNetCore2018.Business.Specifications;
using DbCategory = DotNetCore2018.Data.Entities.Category;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace DotNetCore2018.WebApi.Api.Category
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    public sealed class CategoryApiController : ControllerBase
    {
        private readonly IDataService _dataService;
        private readonly ILogger<CategoryApiController> _logger;

        public CategoryApiController(
            IDataService dataService,
            ILogger<CategoryApiController> logger)
        {
            _dataService = dataService;
            _logger = logger;
        }

        [HttpDelete("delete")]
        [ProducesResponseType(202)]
        public IActionResult DeleteById(int id)
        {
            _logger.LogWarning($"Delete category by id: {id}");

            var category = _dataService.GetBy(new IdSpecification<DbCategory>(id));
            if (category != null)
            {
                _logger.LogWarning("Category found");
                _dataService.Delete(category);
                return Accepted();
            }

            return NotFound();
        }
    }
}