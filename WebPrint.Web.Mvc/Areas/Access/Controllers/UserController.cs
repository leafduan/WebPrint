using System;
using System.Web.Mvc;
using WebPrint.Model;
using WebPrint.Service;

namespace WebPrint.Web.Mvc.Areas.Access.Controllers
{
    public class UserController : Controller
    {
        private readonly Lazy<IService<User>> lazyService;

        private IService<User> Service
        {
            get { return lazyService.Value; }
        }


        public UserController(Lazy<IService<User>> service)
        {
            lazyService = service;
        }

        //
        // GET: /Access/User/

        public ActionResult List()
        {
            return View(Service.Query(u => u.Id < 6));
        }

        public ActionResult Create()
        {
            return View();
        }
    }
}
