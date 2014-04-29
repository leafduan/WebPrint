using System.Web.Mvc;

using WebPrint.Web.Mvc.Helper;

namespace WebPrint.Web.Mvc.Areas.Set
{
    public class SetAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Set";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRouteLowercase(
                "Set_default",
                "Set/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
