using Autofac;
using Autofac.Integration.Web;
using WebPrint.Data;

namespace WebPrint.Web.Forms
{
    public class NHibernateModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            /* one application have only one ISessionFactory */
            builder.Register(c => new SessionFactoryProvider())
                   .As<ISessionFactoryProvider>()
                   .SingleInstance();

            /* should be instance per http request to ensure one session per request model */
            builder.Register(c => new SessionProvider(c.Resolve<ISessionFactoryProvider>()))
                   .As<ISessionProvider>()
                   .InstancePerHttpRequest();

            /* should be instance per http request to ensure one session per request model */
            builder.Register(c => new UnitOfWork(c.Resolve<ISessionProvider>()))
                   .As<IUnitOfWork>()
                   /* must be, to commit transaction, to close session, or will lost data and occupied db connection */
                   .OnRelease(uow => uow.Close())
                   .InstancePerHttpRequest();
        }
    }
}
