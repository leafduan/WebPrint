using NHibernate;

namespace WebPrint.Data
{
    public interface ISessionProvider
    {
        ISession Session { get; }
    }
}
