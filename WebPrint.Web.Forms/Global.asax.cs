using System;
using System.Web;
using Autofac.Integration.Web;
using WebPrint.Web.Core;

namespace WebPrint.Web.Forms
{
    public class Global : HttpApplication, IContainerProviderAccessor
    {
        // Provider that holds the application container.
        static IContainerProvider containerProvider;

        // Instance property that will be used by Autofac HttpModules to resolve and inject dependencies.
        public IContainerProvider ContainerProvider
        {
            get { return containerProvider; }
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            containerProvider = new ContainerProvider(IocConfig.Container);

            //BarTenderHelper.Start();
            //BtApplicationBootStrapper.Start();
#if DEBUG
            NHibernateProfiler.StartProfiler();
#endif
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {
            //BarTenderHelper.ClearPreviewFile(Session.SessionID);
        }

        protected void Application_End(object sender, EventArgs e)
        {
            //BarTenderHelper.Stop();
            //BtApplicationBootStrapper.Stop();
        }
    }
}