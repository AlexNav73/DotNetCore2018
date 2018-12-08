using DotNetCore2018.Core.Breadcrumbs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DotNetCore2018.WebApi.Controllers
{
    [Breadcrumb]
    [AllowAnonymous]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [Breadcrumb]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult Do()
        {
            return RedirectToAction("Index", "Product");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult Error()
        {
            var error = HttpContext.Features.Get<IExceptionHandlerFeature>().Error;
            _logger.LogError(error, "Caught in global error handling routine: {}", error.Message);

            return View();
        }
    }
}