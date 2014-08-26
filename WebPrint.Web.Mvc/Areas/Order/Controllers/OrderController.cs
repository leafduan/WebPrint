using System;
using System.Web.Mvc;
using WebPrint.Model;
using WebPrint.Framework;
using WebPrint.Service;
using WebPrint.Web.Mvc.Filters;

namespace WebPrint.Web.Mvc.Areas.Order.Controllers
{
    [UserAuthorizeAttribute(Roles = "CheckOrder")]
    public class OrderController : Controller
    {
        private readonly Lazy<IService<ViewOrder>> lazyService;
        private IService<ViewOrder> Service
        {
            get { return lazyService.Value; }
        }

        public OrderController(Lazy<IService<ViewOrder>> service)
        {
            lazyService = service;
        }

        //
        // GET: /Order/Order/
        //[UserAuthorize(Roles = "CheckOrder")]
        public ActionResult Check(string jno, string po, string type, string status)
        {
            ViewBag.Jno = jno;
            ViewBag.Po = po;
            ViewBag.Type = type;
            ViewBag.Status = status;

            var predicate = Predicate.True<ViewOrder>();

            if (!jno.IsNullOrEmpty())
                predicate = predicate.And(o => o.JobNo == jno);
            if (!po.IsNullOrEmpty())
                predicate = predicate.And(o => o.PoNo == po);
            if (!type.IsNullOrEmpty())
                predicate = predicate.And(o => o.Type == type.AsEnum<OrderType>());
            if (!status.IsNullOrEmpty())
                predicate = predicate.And(o => o.Status == status.AsEnum<OrderStatus>());

            return View(Service.QueryDescending(predicate, 1, 50));
        }
    }
}
