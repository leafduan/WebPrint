using NHibernate;

namespace WebPrint.Data
{
    public interface ISessionFactoryProvider
    {
        ISessionFactory SessionFactory { get; }
    }
}