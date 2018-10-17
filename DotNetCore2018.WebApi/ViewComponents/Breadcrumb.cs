using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;

namespace DotNetCore2018.WebApi.ViewComponents
{
    public class Breadcrumb : ViewComponent
    {
        private readonly ILogger<Breadcrumb> _logger;

        public Breadcrumb(ILogger<Breadcrumb> logger)
        {
            _logger = logger;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var controller = (string)Url.ActionContext.RouteData.Values["Controller"];
            var action = (string)Url.ActionContext.RouteData.Values["Action"];

            return View("Index", _path[id]);
        }

        private static readonly List<List<BreadcrumbLink>> _path = new List<List<BreadcrumbLink>>()
        {
            new List<BreadcrumbLink>() { new BreadcrumbLink("Home", "") },
            new List<BreadcrumbLink>() 
            { 
                new BreadcrumbLink("Home", ""),
                new BreadcrumbLink("Category", "")
            },
            new List<BreadcrumbLink>() 
            { 
                new BreadcrumbLink("Home", ""),
                new BreadcrumbLink("Category", ""),
                new BreadcrumbLink("Create new", "")
            }
        };
    }

    public class BreadcrumbLink
    {
        public string Name { get; set; }
        public string Link { get; set; }

        public BreadcrumbLink(string name, string link)
        {
            Name = name;
            Link = link;
        }
    }
}