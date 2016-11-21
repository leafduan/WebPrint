using Autofac;
using WebPrint.Data;
using WebPrint.Data.Repositories;

namespace WebPrint.Test
{
    public class RepositoriesModuleTest : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(Repository<>))
                .As(typeof(IRepository<>))
                .InstancePerLifetimeScope();

            builder.Register(c => new UpcRepository(c.Resolve<IUnitOfWork>()))
                .As<IUpcRepository>()
                .InstancePerLifetimeScope();

            builder.Register(c => new RepositoryProvider(c.Resolve<IUnitOfWork>()))
                .As<IRepositoryProvider>()
                .InstancePerLifetimeScope();
        }
    }
}
