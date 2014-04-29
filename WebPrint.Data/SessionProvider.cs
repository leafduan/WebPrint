using System;
using NHibernate;

namespace WebPrint.Data
{
    /// <summary>
    /// wrapping ISesion so that other assembly needn't to reference NHibernate.dll 
    /// </summary>
    public class SessionProvider : ISessionProvider
    {
        private readonly Lazy<ISession> session;

        public ISession Session
        {
            get { return session.Value; }
        }

        public SessionProvider(ISessionFactoryProvider sessionFactoryProvider)
        {
            session = new Lazy<ISession>(() => sessionFactoryProvider.SessionFactory.OpenSession());
        }
    }
}
