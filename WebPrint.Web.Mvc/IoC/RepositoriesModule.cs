using Autofac;
using WebPrint.Data.Repositories;

namespace WebPrint.Web.Mvc.IoC
{
    public class RepositoriesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterGeneric(typeof (Repository<>))
            //       .As(typeof (IRepository<>))
            //       .InstancePerRequest();


            builder.RegisterGeneric(typeof(RepositoryProvider))
                .As(typeof(IRepositoryProvider))
                .InstancePerRequest();
        }
    }
}
