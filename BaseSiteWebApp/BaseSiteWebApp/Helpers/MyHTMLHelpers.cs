using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace BaseSiteWebApp.Helpers
{
    public static class MyHTMLHelpers
    {
        public static HtmlString NorthwindImageLink(this IHtmlHelper htmlHelper, int id, string linkText)
            => new HtmlString($"<a target=\"_blank\" href=\"/Images/{id}\">{linkText}</a>");
    }
}
