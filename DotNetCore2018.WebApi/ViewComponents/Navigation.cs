using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCore2018.WebApi.ViewComponents
{
    [AllowAnonymous]
    public class Navigation : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("Index", HttpContext.User.Identity.IsAuthenticated);
        }
    }
}