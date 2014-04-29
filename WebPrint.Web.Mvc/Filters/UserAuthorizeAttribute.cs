using System.Net;
using System.Web.Mvc;
using WebPrint.Web.Mvc.Helper;

namespace WebPrint.Web.Mvc.Filters
{
    public class UserAuthorizeAttribute : AuthorizeAttribute
    {
        public string RedirectUrl { get; set; }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            #region

            /*
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                string[] acceptedTypes = filterContext.HttpContext.Request.AcceptTypes;
                foreach (string type in acceptedTypes)
                {
                    if (type.Contains("html"))
                    {
                        if (filterContext.HttpContext.Request.IsAjaxRequest())
                            filterContext.Result = new ViewResult { ViewName = "AccessDeniedPartial" };
                        else
                            filterContext.Result = new ViewResult { ViewName = "AccessDenied" };
                        break;
                    }
                    else if (type.Contains("javascript"))
                    {
                        filterContext.Result = new JsonResult { Data = new { success = false, message = "Access denied." } };
                        break;
                    }
                    else if (type.Contains("xml"))
                    {
                        filterContext.Result = new HttpUnauthorizedResult(); //this will redirect to login page with forms auth you could instead serialize a custom xml payload and return here.
                    }
                }
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
            * */

            #endregion

            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                var helper = new UrlHelper(filterContext.RequestContext);
                filterContext.Result = helper.Unauthorized(RedirectUrl);

                // 两个有什么区别
                // filterContext.HttpContext.Response.Redirect(redicretUrl);
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}