using System;
using System.Web;
using System.Web.Mvc;
using WebPrint.Logging;
using WebPrint.Web.Mvc.Helper;

namespace WebPrint.Web.Mvc.Filters
{
    /// <summary>
    /// 只处理500错误
    /// </summary>
    public class UnHandleErrorAttribute : HandleErrorAttribute
    {
        public ILogger Logger { get; set; }

        public override void OnException(ExceptionContext filterContext)
        {
#if DEBUG
            // 直接抛出黄色异常界面 方便调试
            return;
#endif
            if (filterContext == null)
            {
                Logger.Error("ArgumentNullException: filterContext");
                throw new ArgumentNullException("filterContext");
            }

            // 有多个HandleErrorAttribute时 如果之前的标记已经处理完 则不再处理
            if (filterContext.ExceptionHandled)
                return;

            var exception = filterContext.Exception;
            var helper = new UrlHelper(filterContext.RequestContext);
            var controllerName = (string) filterContext.RouteData.Values["controller"];
            var actionName = (string) filterContext.RouteData.Values["action"];
            var msg = string.Format("UnhandleError /{0}/{1} ", controllerName, actionName);
            Logger.Error(msg, exception);

            filterContext.Result = helper.UnHandleError();
            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
            filterContext.HttpContext.Response.StatusCode = 500;
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
        }
    }
}