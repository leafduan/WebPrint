using System;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using WebPrint.Web.Core;
using WebPrint.Web.Mvc.Controllers;
using WebPrint.Web.Mvc.IoC;

namespace WebPrint.Web.Mvc
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected MvcApplication()
        {
            // 用户验证后 赋予权限值
            // 一定要放在构造函数中，放在Application_Start会导致异常
            AuthorizeRequest += MvcApplication_AuthorizeRequest;
        }

        protected void Application_Start()
        {
            // Autofac IoC Register
            IocConfig.Register();

            // log4net
            LogConfig.Register();

            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            // 去掉Header中mvc版本的描述
            MvcHandler.DisableMvcResponseHeader = true;
            var config = GlobalConfiguration.Configuration;

            // web api 一律放回json格式
            // config.Formatters.Remove(config.Formatters.XmlFormatter);
            // 忽略web api序列化对象的Reference Loop(如NHibernate 中对象的互相引用)
            // 要设置结果只返回json格式时 此设置才生效
            //config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

#if DEBUG
            // Start profile
            NHibernateProfiler.StartProfiler();
#endif
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var exception = Server.GetLastError();
            var httpException = exception as HttpException;
            HandleErrorInfo model;
            var routeData = new RouteData();
            routeData.Values["controller"] = "warning";

            if (httpException != null && httpException.GetHttpCode() == 404)
            {
                model = new HandleErrorInfo(exception, "Warning", "NotFound");
                routeData.Values["action"] = "notfound";
            }
            else
            {
                model = new HandleErrorInfo(exception, "Warning", "Error");
                routeData.Values["action"] = "error";
            }

            Response.Clear();
            Server.ClearError();

            routeData.Values["model"] = model;
            IController controller = DependencyResolver.Current.GetService<WarningController>();//new WarningController();
            controller.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));

            Response.TrySkipIisCustomErrors = true;
        }

        private void MvcApplication_AuthorizeRequest(object sender, System.EventArgs e)
        {
            var user = Context.User;
            if (user.Identity.IsAuthenticated && user.Identity.AuthenticationType == "Forms")
            {
                var id = (FormsIdentity) Context.User.Identity;
                /*
                var user2 = DependencyResolver.Current.GetService<WebPrint.Model.Services.IService<WebPrint.Model.User>>();
                var permissions = new List<string>();
                user2.Get(u => u.UserName == id.Name)
                    .Groups
                    .Select(g => g.Permissions.Select(p => p.Name))
                    .ForEach(p => permissions = permissions.Union(p).ToList());
                * */
                var permissions = id.Ticket.UserData.Split(',');
                Context.User = new GenericPrincipal(id, permissions.ToArray()); 
            }
        }
    }
}