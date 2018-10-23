using DotNetCore2018.Business.Services.Interfaces;
using DotNetCore2018.Business.Specifications;
using DbCategory = DotNetCore2018.Data.Entities.Category;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Linq;
using DotNetCore2018.WebApi.ViewModels;
using System;
using Microsoft.AspNetCore.Http;

namespace DotNetCore2018.WebApi.Api.Category
{
    [Produces("application/json")]
    [Route("api/v1/categories")]
    public sealed class CategoryApiController : ControllerBase
    {
        private readonly IDataService _dataService;
        private readonly IFileService _fileService;
        private readonly ILogger<CategoryApiController> _logger;

        public CategoryApiController(
            IDataService dataService,
            IFileService fileService,
            ILogger<CategoryApiController> logger)
        {
            _dataService = dataService;
            _fileService = fileService;
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
                _fileService.Delete(category.Image);
                return Accepted();
            }

            return NotFound();
        }

        [HttpGet]
        public CategoryViewModel[] GetAll()
        {
            return _dataService.GetAll<DbCategory>()
                .Select(x => new CategoryViewModel()
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToArray();
        }

        [HttpGet("image_file")]
        public IActionResult ImageFile([FromQuery] int id)
        {
            var category = _dataService.GetBy(new IdSpecification<DbCategory>(id));
            if (category != null && category.Image != null)
            {
                return File(_fileService.OpenFile(category.Image.Value), "image/jpeg");
            }
            return NotFound();
        }

        [HttpPost("update_image"), DisableRequestSizeLimit]
        public IActionResult UpdateImage([FromQuery] int id, [FromForm] IFormFile file)
        {
            try
            {
                if (file == null)
                {
                    return StatusCode(422);
                }
                var category = _dataService.GetBy(new IdSpecification<DbCategory>(id));
                if (file != null && category != null)
                {
                    category.Image = _fileService.SaveFile(file.OpenReadStream());
                    _dataService.Update(category);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while uploading file");
                return StatusCode(500);
            }
        }
    }
}