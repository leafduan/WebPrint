using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebPrint.Web.Mvc.Helper
{
    public static class RouteCollectionExtensions
    {
        public static Route MapRouteLowercase(this RouteCollection routes, string name, string url, object defaults)
        {
            return routes.MapRouteLowercase(name, url, defaults, null, null);
        }

        public static Route MapRouteLowercase(this RouteCollection routes, string name, string url, object defaults,
                                             string[] namespaces)
        {
            return routes.MapRouteLowercase(name, url, defaults, null, namespaces);
        }

        public static Route MapRouteLowercase(this RouteCollection routes, string name, string url, object defaults,
                                             object constraints, string[] namespaces)
        {
            if (routes == null)
                throw new ArgumentNullException("routes");

            if (url == null)
                throw new ArgumentNullException("url");

            var item = new LowercaseRoute(url, new MvcRouteHandler())
                {
                    Defaults = new RouteValueDictionary(defaults),
                    Constraints = new RouteValueDictionary(constraints),
                    DataTokens = new RouteValueDictionary()
                };

            if ((namespaces != null) && (namespaces.Length > 0))
            {
                item.DataTokens["Namespaces"] = namespaces;
            }

            routes.Add(name, item);

            return item;
        }
    }
}