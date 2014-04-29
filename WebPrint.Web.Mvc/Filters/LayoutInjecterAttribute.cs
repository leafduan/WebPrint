using System.Web.Mvc;

namespace WebPrint.Web.Mvc.Filters
{
    public class LayoutInjecterAttribute : ActionFilterAttribute
    {
        private readonly string masterName;
        public LayoutInjecterAttribute(string masterName)
        {
            this.masterName = masterName;
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            var result = filterContext.Result as ViewResult;
            if (result != null)
            {
                result.MasterName = masterName;
            }
        }
    }
}