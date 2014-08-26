using Autofac;
using Autofac.Integration.Mvc;
using WebPrint.Service;

namespace WebPrint.Web.Mvc.IoC
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof (Service<>))
                   .As(typeof (IService<>))
                   .InstancePerHttpRequest();

            /*
            builder.Register(c => new Service<User>(c.Resolve<IRepository<User>>(), c.Resolve<IUnitOfWork>()))
                   .As(typeof (IService<User>))
                   .InstancePerHttpRequest();
             * */
        }
    }
}
