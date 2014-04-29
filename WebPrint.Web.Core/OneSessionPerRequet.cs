using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

using NHibernate;
using NHibernate.Cfg;

namespace WebPrint.UI.Common
{
    public sealed class OneSessionPerRequest
    {
        private static readonly string currentSessionKey = "nhibernate.current_session";
        private static readonly ISessionFactory sessionFactory;
        private static readonly Configuration configuration;

        static OneSessionPerRequest()
        {
            configuration = new Configuration().Configure();
            sessionFactory = configuration.BuildSessionFactory();
        }

        public static Configuration Configuration
        {
            get {
                return configuration;
            }
        }

        public static ISession CurrentSession
        {
            get
            {   
                ISession session = HttpRequestState.Get(currentSessionKey) as ISession;

                if (session == null)
                {
                    session = sessionFactory.OpenSession();
                    HttpRequestState.Store(currentSessionKey, session);
                }

                return session;
            }
        }

        public static void CloseSession()
        {
            ISession session = HttpRequestState.Get(currentSessionKey) as ISession;

            if (session == null)
            {
                return;
            }

            session.Close();
            HttpRequestState.Remove(currentSessionKey);
        }
    }
}
