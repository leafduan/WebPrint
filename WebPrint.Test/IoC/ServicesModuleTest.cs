using Autofac;
using WebPrint.Service;
using WebPrint.Model.Services;

namespace WebPrint.Test
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof (Service<>))
                   .As(typeof (IService<>))
                   .InstancePerLifetimeScope();
        }
    }
}
