using Autofac;
using WebPrint.DbService;
using WebPrint.Service;

namespace WebPrint.Test
{
    public class DbServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new DbUnitOfWork())
                .As<IDbUnitOfWork>()
                .OnRelease(uow => uow.Dispose())
                .InstancePerLifetimeScope();

            builder.Register(c => new Service.DbService(c.Resolve<IDbUnitOfWork>()))
                .As<IDbService>()
                .InstancePerLifetimeScope();
        }
    }
}
