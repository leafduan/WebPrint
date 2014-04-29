using Autofac;

namespace WebPrint.Web.Forms
{
    public static class IocConfig
    {
        public static IContainer Container { get; private set; }
        //public static IContainerProvider ContainerProvider { get; private set; }

        static IocConfig()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule(new NHibernateModule());
            builder.RegisterModule(new RepositoriesModule());
            builder.RegisterModule(new ServicesModule());

            Container = builder.Build();
           //ContainerProvider = new ContainerProvider(Container);
        }
    }
}
