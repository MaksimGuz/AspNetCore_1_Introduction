using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace BaseSiteWebApp.TagHelpers
{
    public class ImageTagHelper : TagHelper
    {
        public string Id { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "img";
            output.Attributes.SetAttribute("src", $"/images/{Id}");
            output.Attributes.SetAttribute("height", "100");
        }
    }
}
