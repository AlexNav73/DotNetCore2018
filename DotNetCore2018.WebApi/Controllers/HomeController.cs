using DotNetCore2018.Core.Breadcrumbs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DotNetCore2018.WebApi.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Breadcrumb]
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

        [AllowAnonymous]
        public IActionResult Error()
        {
            var error = HttpContext.Features.Get<IExceptionHandlerFeature>().Error;
            _logger.LogError(error, "Caught in global error handling routine: {}", error.Message);

            return View();
        }
    }
}