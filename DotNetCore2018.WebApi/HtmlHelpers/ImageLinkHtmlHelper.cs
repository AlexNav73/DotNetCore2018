using DotNetCore2018.WebApi.Helpers;
using DotNetCore2018.WebApi.ViewModels;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;

namespace DotNetCore2018.WebApi.HtmlHelpers
{
    public static class ImageLinkHtmlHelper
    {
        public static IHtmlContent ImageLink(this IHtmlHelper<CategoryViewModel> helper, int id)
        {
            return ImageLinkHelper.Build($"/images/{id}");
        }
    }
}