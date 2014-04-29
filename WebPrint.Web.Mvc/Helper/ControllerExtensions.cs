using System.Web.Mvc;

namespace WebPrint.Web.Mvc.Helper
{
    public static class ControllerExtensions
    {
        public static ActionResult RedirectLocal(this Controller controller, string redirectUrl)
        {
            return RedirectLocal(controller, redirectUrl, null);
        }

        public static ActionResult RedirectLocal(this Controller controller, string redirectUrl, string defaultUrl)
        {
            if (controller.Url.IsLocalUrl(redirectUrl))
            {
                return new RedirectResult(redirectUrl);
            }

            return new RedirectResult(defaultUrl ?? "~/");
        }

        public static ActionResult RedirectNotFound(this Controller controller)
        {
            controller.TempData["rawUrl"] = controller.Request.RawUrl;
            return controller.Url.NotFound();
        }

        public static ActionResult RedirectUnHandleError(this Controller controller)
        {
            controller.TempData["rawUrl"] = controller.Request.RawUrl;
            return controller.Url.UnHandleError();
        }
    }
}
