using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCore2018.WebApi.ViewComponents
{
    public class UserSignInOptions : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return View("AuthorizedUser");
            }
            return View("Anonymous");
        }
    }
}