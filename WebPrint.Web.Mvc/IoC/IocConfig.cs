using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;

namespace WebPrint.Web.Mvc.IoC
{
    public static class IocConfig
    {
        public static void Register(/*params Assembly[] contorllerAssemblies*/)
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule(new NHibernateModule());
            builder.RegisterModule(new RepositoriesModule());
            builder.RegisterModule(new ServicesModule());
            builder.RegisterModule(new LogModule());
            builder.RegisterModule(new MvcFiltersModule());

            // register controller
            builder.RegisterControllers(typeof (MvcApplication).Assembly);

            // register api controller
            builder.RegisterApiControllers(typeof (MvcApplication).Assembly);

            // register filters
            // global filters is not working
            builder.RegisterFilterProvider();

            var container = builder.Build();
            // Configure contollers with the dependency resolver
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            // Configure Web API with the dependency resolver
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
