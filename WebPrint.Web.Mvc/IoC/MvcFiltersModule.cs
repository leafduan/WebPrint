using Autofac;
using WebPrint.Web.Mvc.Filters;

namespace WebPrint.Web.Mvc.IoC
{
    /// <summary>
    /// global filters must register it's self, and resolve
    /// </summary>
    public class MvcFiltersModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new UnHandleErrorAttribute());
        }
    }
}