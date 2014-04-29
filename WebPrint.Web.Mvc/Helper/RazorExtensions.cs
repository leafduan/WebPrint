using System;
using System.Collections.Generic;
using System.Web.WebPages;

namespace WebPrint.Web.Mvc.Helper
{
    public static class RazorExtensions
    {
        public static HelperResult /*List*/Repeat<T>(this IEnumerable<T> items,
                                           Func<T, HelperResult> template)
        {
            return new HelperResult(writer =>
                {
                    foreach (var item in items)
                    {
                        template(item).WriteTo(writer);
                    }
                });
        }

        public static HelperResult RenderSection(this WebPageBase webPage,
                                                 string name, Func<dynamic, HelperResult> defaultContents)
        {
            if (webPage.IsSectionDefined(name))
            {
                return webPage.RenderSection(name);
            }
            return defaultContents(null);
        }
    }
}