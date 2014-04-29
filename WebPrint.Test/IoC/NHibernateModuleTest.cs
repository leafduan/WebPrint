using Autofac;
using WebPrint.Data;

namespace WebPrint.Test
{
    public class NHibernateModuleTest : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new SessionFactoryProvider())
                   .As<ISessionFactoryProvider>()
                   .SingleInstance();

            builder.Register(c => new SessionProvider(c.Resolve<ISessionFactoryProvider>()))
                   .As<ISessionProvider>()
                   .InstancePerLifetimeScope();

            builder.Register(c => new UnitOfWork(c.Resolve<ISessionProvider>()))
                   .As<IUnitOfWork>()
                /* must be, to commit transaction, to close session, or will lost data and occupied db connection */
                   .OnRelease(uow => uow.Close())
                   .InstancePerLifetimeScope();
        }
    }
}
