using DotNetCore2018.Data;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCore2018.WebApi
{
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly IDatabaseContext _context;

        public HomeController(IDatabaseContext context)
        {
            _context = context;
        }

        [Route("[action]/{id?}")]
        public IActionResult Index(string id)
        {
            return View();
        }
    }
}