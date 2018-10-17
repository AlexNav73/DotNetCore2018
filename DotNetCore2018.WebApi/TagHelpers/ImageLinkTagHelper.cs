using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DotNetCore2018.WebApi.TagHelpers
{
    [HtmlTargetElement("image")]
    public class ImageLinkTagHelper : TagHelper
    {
        [HtmlAttributeName("id")]
        public string Id { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "img";
            output.Attributes.SetAttribute("src", $"/images/{Id}");
            output.Attributes.SetAttribute("class", "card-img-top mt-3");
            output.Attributes.SetAttribute("style", "height: 13rem;");
        }
    }
}