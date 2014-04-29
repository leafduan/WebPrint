using System.Web.Mvc;
using System.Web.Routing;
using WebPrint.Web.Mvc.Helper;

namespace WebPrint.Web.Mvc
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            var namespaces = new[] {typeof (WebPrint.Web.Mvc.Controllers.HomeController).Namespace};

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // 如果不设置defaults 则表示此值不能为空
            routes.MapRouteLowercase(
                name: "Account",
                url: "{action}",
                defaults: new {controller = "Account"},
                namespaces: namespaces
                );

            routes.MapRouteLowercase(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new {controller = "Home", action = "Index", id = UrlParameter.Optional},
                // 解决Areas中有同名的Controller问题
                namespaces: namespaces
                );
        }
    }
}