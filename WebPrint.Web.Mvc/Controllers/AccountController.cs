using System;
using System.Web.Mvc;
using WebPrint.Logging;
using WebPrint.Model;
using WebPrint.Service;
using WebPrint.Web.Core;
using WebPrint.Web.Mvc.Models;
using WebPrint.Web.Mvc.Helper;

namespace WebPrint.Web.Mvc.Controllers
{
    /// <summary>
    /// 使用 MVC 自带的 Authorize 属性 进行身份验证
    /// </summary>
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly Lazy<IService<User>> lazyService;

        private IService<User> Service
        {
            get { return lazyService.Value; }
        }

        private ILogger Logger { get; set; }

        // 使用Lazy方式，按需初始化Service
        // 就不会导致只要有请求（不过是否有使用Service）,就会创建session并开始事务问题
        public AccountController(Lazy<IService<User>> service)
        {
            lazyService = service;
        }

        // GET: /Account/
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            // 判断是否已经验证通过，如果已验证，则无需再次验证（场景：用户登录成功后，再次请求登录url）
            if (FormsAuth.IsAuthenticated) return this.RedirectLocal(returnUrl);

            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            var loginUser = Service.Get(u => u.UserName == model.Username && u.Password == model.Password);

            /*
             * Cannot simultaneously fetch multiple bags
            var loginUser =
                Service.FetchMany(u => u.UserName == model.Username && u.Password == model.Password, u => u.Groups)
                       .ThenFetch(g => g.Permissions)
                       .FirstOrDefault();
            */

            if (!ModelState.IsValid || loginUser == null)
            {
                ModelState.AddModelError("", "The user name or password provided is incorrect.");
                return View(model);
            }

            FormsAuth.SignIn(loginUser, model.RememberMe);

            return this.RedirectLocal(returnUrl);
        }

        public ActionResult Register()
        {
            throw new Exception("Register");
            return RedirectToAction("Login");
        }

        public ActionResult Find()
        {
            Logger.Info("find");
            Logger.Debug("Find");
            Logger.Error("Find");
            Logger.Fatal("Find");

            return RedirectToAction("Login");
        }

        public ActionResult Logout()
        {
            FormsAuth.SignOut();

            return RedirectToAction("Login");
        }
    }
}
