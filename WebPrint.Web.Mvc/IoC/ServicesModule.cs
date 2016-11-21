using Autofac;
using WebPrint.Service;

namespace WebPrint.Web.Mvc.IoC
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(Service<>))
                .As(typeof(IService<>))
                .InstancePerRequest();
        }
    }
}
