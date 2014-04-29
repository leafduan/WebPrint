using Autofac;
using Autofac.Integration.Web;
using WebPrint.Service;
using WebPrint.Model.Services;

namespace WebPrint.Web.Forms
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof (Service<>))
                   .As(typeof (IService<>))
                   .InstancePerHttpRequest();
        }
    }
}
