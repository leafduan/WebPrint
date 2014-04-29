using Autofac;
using Autofac.Integration.Mvc;
using WebPrint.Data;
using WebPrint.Data.Repositories;
using WebPrint.Model.Repositories;

namespace WebPrint.Web.Mvc.IoC
{
    public class RepositoriesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof (Repository<>))
                   .As(typeof (IRepository<>))
                   .InstancePerHttpRequest();

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
