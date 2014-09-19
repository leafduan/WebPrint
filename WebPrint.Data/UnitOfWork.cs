using System;
using NHibernate;
using WebPrint.Data.Repositories;
using WebPrint.Model;

namespace WebPrint.Data
{
    /// <summary>
    /// instance life per http request(one session per request)
    /// ? 可以由 SessionFactoryProvider 或 UnitOfWorkFactory 提供 ?
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ISessionProvider sessionProvider;
        private readonly ISession session;
        private readonly ITransaction transaction;
        private bool isDisposed;
        private bool wasCommitted;

        public UnitOfWork(ISessionProvider sessionProvider)
        {
            this.session = sessionProvider.Session;
            transaction = session.BeginTransaction();
            isDisposed = false;
            wasCommitted = false;
        }

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
            if (isDisposed) return;

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
                isDisposed = true;
            }
        }

        #region IDisposable 成员

        public void Dispose()
        {
            Close();
        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : EntityBase
        {
            return new Repository<TEntity>(sessionProvider);
        }

        #endregion
    }
}
