using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebPrint.Web.Mvc.Areas.Order.Controllers
{
    public class GraphicOrderController : Controller
    {
        //
        // GET: /Order/GraphicOrder/

        public ActionResult Details(string jno = null)
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }
    }
}
