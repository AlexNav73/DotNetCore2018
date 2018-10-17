using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DotNetCore2018.WebApi.Helpers
{
    public static class ImageLinkHelper
    {
        public static IHtmlContent Build(string url)
        {
            var imageTagBuilder = new TagBuilder("img");

            imageTagBuilder.MergeAttribute("src", url);
            imageTagBuilder.AddCssClass("card-img-top mt-3");
            imageTagBuilder.MergeAttribute("style", "height: 13rem;");

            return imageTagBuilder;
        }
    }
}