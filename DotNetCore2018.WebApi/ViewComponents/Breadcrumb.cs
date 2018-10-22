using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DotNetCore2018.Core.Breadcrumbs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DotNetCore2018.WebApi.ViewComponents
{
    public class Breadcrumb : ViewComponent
    {
        private static readonly BreadcrumbNode _tree;

        private readonly ILogger<Breadcrumb> _logger;

        static Breadcrumb()
        {
            _tree = BreadcrumbCollector.Collect(Assembly.GetExecutingAssembly());
        }

        public Breadcrumb(ILogger<Breadcrumb> logger)
        {
            _logger = logger;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var controller = (string)Url.ActionContext.RouteData.Values["Controller"];
            var action = (string)Url.ActionContext.RouteData.Values["Action"];

            var page = (string)Url.ActionContext.RouteData.Values["page"];
            if (page != null)
            {
                var pageComponents = page.Split(new[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
                if (pageComponents.Length == 2)
                {
                    controller = pageComponents[0];
                    action = pageComponents[1];
                }
            }

            var path = BreadcrumbPathResolver.GetPath(_tree, controller, action);

            var links = new List<BreadcrumbLink>();
            if (path.Count >= 2)
            {
                var actionNode = path[0];
                var controllerNode = path[1];
                if (actionNode.Name.Equals("Index", StringComparison.OrdinalIgnoreCase))
                {
                    links.Add(new BreadcrumbLink(controllerNode.Name, Url.Action(actionNode.Name, controllerNode.Name)));
                }
                else
                {
                    links.Add(new BreadcrumbLink(actionNode.Name, Url.Action(actionNode.Name, controllerNode.Name)));
                    links.Add(new BreadcrumbLink(controllerNode.Name, Url.Action("Index", controllerNode.Name)));
                }
            }
            for (int i = 2; i < path.Count; i++)
            {
                links.Add(new BreadcrumbLink(path[i].Name, Url.Action("Index", path[i].Name)));
            }
            links.Reverse();

            return View("Index", links);
        }
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