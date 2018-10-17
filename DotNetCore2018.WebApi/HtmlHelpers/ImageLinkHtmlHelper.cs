using DotNetCore2018.WebApi.ViewModels;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DotNetCore2018.WebApi.HtmlHelpers
{
    public static class ImageLinkHtmlHelper
    {
        public static IHtmlContent ImageLink(this IHtmlHelper<CategoryViewModel> helper, int id)
        {
            var imageTagBuilder = new TagBuilder("img");

            imageTagBuilder.MergeAttribute("src", $"/images/{id}");
            imageTagBuilder.AddCssClass("card-img-top mt-3");
            imageTagBuilder.MergeAttribute("style", "height: 13rem;");

            return imageTagBuilder;
        }
    }
}