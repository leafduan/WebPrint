using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebPrint.Web.Mvc.Areas.Order.Controllers
{
    [RouteArea("order")]
    [RoutePrefix("graphic")]
    public class GraphicOrderController : Controller
    {
        //
        // GET: /Order/GraphicOrder/
        [Route("{jno}")]
        public ActionResult Details(string jno = null)
        {
            return View();
        }

        [Route]
        public ActionResult Create()
        {
            return View();
        }
    }
}
