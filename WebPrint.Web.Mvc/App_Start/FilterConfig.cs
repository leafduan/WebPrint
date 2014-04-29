using System.Web.Mvc;
using WebPrint.Web.Mvc.Filters;

namespace WebPrint.Web.Mvc
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(DependencyResolver.Current.GetService<UnHandleErrorAttribute>());
            filters.Add(new UserAuthorizeAttribute());
        }
    }
}