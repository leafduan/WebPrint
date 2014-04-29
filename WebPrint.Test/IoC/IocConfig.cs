using Autofac;

namespace WebPrint.Test
{
    public static class IocConfig
    {
        public static IContainer Container { get; set; }

        static IocConfig()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule(new NHibernateModuleTest());
            builder.RegisterModule(new RepositoriesModuleTest());
            builder.RegisterModule(new ServicesModule());

            Container = builder.Build();
        }

        public static T Resolve<T>()
        {
            return Container.Resolve<T>();
        }

        public static T Resolve<T>(string name)
        {
            return Container.ResolveNamed<T>(name);
        }
    }
}
