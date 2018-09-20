using DotNetCore2018.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCore2018.WebApi
{
    [Route("[controller]")]
    public class HomeController : Controller
    {
        [Route("[action]/{id?}")]
        public IActionResult Index(string id)
        {
            var test = new TestModel() { Name = "Test" };
            return View(test);
        }
    }
}