using System.Web.Mvc;
using WebPrint.Framework;

namespace WebPrint.Web.Mvc.Helper
{
    public static class UrlHelperExtensions
    {
        public static ActionResult Unauthorized(this UrlHelper helper)
        {
            return Unauthorized(helper, string.Empty);
        }

        public static ActionResult Unauthorized(this UrlHelper helper, string redirectUrl)
        {
            if (!redirectUrl.IsNullOrEmpty() && helper.IsLocalUrl(redirectUrl))
                return new RedirectResult(redirectUrl);

            return new RedirectResult("/warning/unauthorized");
        }

        public static ActionResult NotFound(this UrlHelper helper)
        {
            return new RedirectResult("/warning/notfound");
        }

        public static ActionResult UnHandleError(this UrlHelper helper)
        {
            return new RedirectResult("/warning/error");
        }
    }
}