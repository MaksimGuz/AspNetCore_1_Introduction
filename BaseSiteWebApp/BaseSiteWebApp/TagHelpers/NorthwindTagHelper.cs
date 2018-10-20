using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Razor.TagHelpers;

namespace BaseSiteWebApp.TagHelpers
{
    [HtmlTargetElement("a")]
    public class NorthwindTagHelper : TagHelper
    {
        public int NorthwindId { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (NorthwindId > 0)
            {
                output.Attributes.SetAttribute("href", $"/Images/{NorthwindId}");
                output.Attributes.SetAttribute("target", "_blank");
            }
        }
    }
}
