using System;
using NHibernate;

namespace WebPrint.Data
{
    /// <summary>
    /// instance life per http request(one session per request)
    /// ? 可以由 SessionFactoryProvider 或 UnitOfWorkFactory 提供 ?
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ISession session;
        private readonly ITransaction transaction;
        private bool wasDisposed;
        private bool wasCommitted;

        public UnitOfWork(ISessionProvider sessionProvider)
        {
            SessionProvider = sessionProvider;
            session = sessionProvider.Session;
            transaction = session.BeginTransaction();
            wasDisposed = false;
            wasCommitted = false;
        }

        public ISessionProvider SessionProvider { get; private set; }

        /*
        public void Start()
        {
            session = sessionProvider.Session;
            transaction = session.BeginTransaction();
            isDisposed = false;
            wasCommitted = false;
        }
        * */

        private void Commit()
        {
            /* 防止多次提交事务导致异常（暂不考虑事务嵌套 一个请求一个事务） */
            if (wasCommitted) return;

            if (!transaction.IsActive)
            {
                throw new InvalidOperationException("No active transaction");
            }
            transaction.Commit();
            wasCommitted = true;
        }

        private void Rollback()
        {
            if (transaction.IsActive)
            {
                transaction.Rollback();
            }
        }

        public void Close()
        {
            if (wasDisposed) return;

            try
            {
                Commit();
            }
            catch (Exception)
            {
                Rollback();
                throw;
            }
            finally
            {
                session.Close();
                wasDisposed = true;
            }
        }

        #region IDisposable 成员

        public void Dispose()
        {
            Close();
        }

        #endregion
    }
}
