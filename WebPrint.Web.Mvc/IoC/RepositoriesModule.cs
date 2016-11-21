using Autofac;
using WebPrint.Data.Repositories;

namespace WebPrint.Web.Mvc.IoC
{
    public class RepositoriesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof (Repository<>))
                   .As(typeof (IRepository<>))
                   .InstancePerRequest();

            /* better practice than below */
            /*
            builder.Register(c => new UpcRepository(c.Resolve<ISessionProvider>()))
                   .As<IUpcRepository>()
                   .InstancePerHttpRequest();
             * */

            /*
            builder.RegisterType<UpcRepository>()
                   .As<IUpcRepository>()
                   .InstancePerHttpRequest();
             * */
        }
    }
}
