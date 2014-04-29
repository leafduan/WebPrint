using System.Web.Mvc;

using WebPrint.Web.Mvc.Helper;

namespace WebPrint.Web.Mvc.Areas.Order
{
    public class OrderAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "Order"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            // 如果规则 Rfid_Order 再前面，则此规则无效，会总是匹配到 Rfid_Order 规则，把参数当做 action 了
            context.MapRouteLowercase(
                "Rfid_Order_Detail",
                "Order/Rfid/{jno}",
                new {controller = "RfidOrder", action = "Details"}
                );

            context.MapRouteLowercase(
                "Rfid_Order",
                "Order/Rfid/{action}",
                new {controller = "RfidOrder", action = "Create"}
                );

            context.MapRouteLowercase(
                "Graphic_Order_Detail",
                "Order/Graphic/{jno}",
                new {controller = "GraphicOrder", action = "Details"}
                );

            context.MapRouteLowercase(
                "Graphic_Order",
                "Order/Graphic/{action}",
                new {controller = "GraphicOrder", action = "Create"}
                );

            context.MapRouteLowercase(
                "Order_default",
                "Order/{action}/{id}",
                new { controller = "Order", action = "Check", id = UrlParameter.Optional }
                );
        }
    }
}
