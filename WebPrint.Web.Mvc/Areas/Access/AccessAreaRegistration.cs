using System.Web.Mvc;

using WebPrint.Web.Mvc.Helper;

namespace WebPrint.Web.Mvc.Areas.Access
{
    public class AccessAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "Access"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRouteLowercase(
                "Access_default",
                "Access/{controller}/{action}/{id}",
                new {controller = "User", action = "List", id = UrlParameter.Optional}
                );
        }
    }
}
