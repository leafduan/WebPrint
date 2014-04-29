using System.Net;
using System.Web.Mvc;
using WebPrint.Framework;
using WebPrint.Logging;

namespace WebPrint.Web.Mvc.Controllers
{
    public class WarningController : Controller
    {
        private ILogger Logger { get; set; }

        public ActionResult Unauthorized()
        {
            var msg = "Unauthorized {0} {1}".Formatting(User.Identity.Name, Request.RawUrl);
            Logger.Info(msg);

            return View();
        }

        [AllowAnonymous]
        public ActionResult NotFound()
        {
            Response.StatusCode = (int) HttpStatusCode.NotFound;

            var rawUrl = TempData["rawUrl"] ?? Request.RawUrl;

            var msg = "NotFount {0} {1}".Formatting(UserName, rawUrl);
            Logger.Error(msg);

            return View();
        }

        [AllowAnonymous]
        public ActionResult Error()
        {
            var model = RouteData.Values["model"] as HandleErrorInfo;
            var rawUrl = TempData["rawUrl"] ?? Request.RawUrl;
            if (model != null)
            {
                var msg = "UnhandleError {0} {1}".Formatting(UserName, rawUrl);
                Logger.Error(msg, model.Exception);
            }

            return View();
        }

        private string UserName
        {
            get { return User.Identity.Name ?? "guest"; }
        }
    }
}