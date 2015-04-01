using System;
using System.Linq;
using System.Web.Mvc;
using WebPrint.Model;
using WebPrint.Service;
using WebPrint.Service.Helper;
using WebPrint.Web.Mvc.Helper;

namespace WebPrint.Web.Mvc.Areas.Order.Controllers
{
    [RouteArea("order")]
    [RoutePrefix("rfid")]
    public class RfidOrderController : Controller
    {
        private readonly Lazy<IService<Model.Order>> lazyService;
        private IService<Model.Order> Service
        {
            get { return lazyService.Value; }
        }

        public RfidOrderController(Lazy<IService<Model.Order>> service)
        {
            lazyService = service;
        }

        //
        // GET: /Order/RfidOrder/
        [Route("{jno}")]
        public ActionResult Details(string jno = null)
        {
            var orderId = jno.AsOrderId();
            var order = Service
                .Queryable(o => o.Id == orderId && o.Type == OrderType.ServiceBureau)
                .Fetch(o => o.ShipBill)
                .Fetch(o => o.CreatedUser)
                .FetchMany(o => o.Details)
                .ThenFetch(d => d.Format)
                .FirstOrDefault();

            if (order == null)
            {
                ViewBag.Jno = jno;
                return /*Url.NotFound()*/this.RedirectNotFound();
            }

            return View(order);
        }

        //[UserAuthorize(Roles = "CreateOrder")]
        [Route]
        public ActionResult Create()
        {
            return View();
        }
    }
}
