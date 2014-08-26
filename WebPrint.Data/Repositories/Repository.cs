using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NHibernate;
using NHibernate.Linq;
using WebPrint.Model;

namespace WebPrint.Data.Repositories
{
    /// <summary>
    /// respository should only dependency ISession, not care the transaction that services layer(BLL) caring
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : EntityBase
    {
        /*
         * 如何自动将操作打包到一个事物中，如Insert update等
         * linq to sql 中有个SubmitChange方法，一次提交之前修改
         * (Injectiont)
         * 
         * 单条sql(一个方法内)的操作很好打包
         * 
         * 不同的Repository使用一个session(One Session per Request),如何保证在一个事物中一同提交，单独的repository可以保证所有操作在一个事务中
         * End Request 提交事务的时候异常 这个怎么处理呢？
         * 
         * 应该单独出一个UnitOfWork，独立于各repository，然后所有的事务操作在其中完成
         * */

        protected ISession Session {
            get { return sessionProvider.Session; }
        }
        private readonly ISessionProvider sessionProvider;

        public Repository(ISessionProvider sessionProvider)
        {
            this.sessionProvider = sessionProvider;
        }

        public object Save(TEntity entity)
        {
            return Session.Save(entity);
        }

        public TKey Save<TKey>(TEntity entity)
        {
            return (TKey) Session.Save(entity);
        }

        public void Save(IEnumerable<TEntity> items)
        {
            foreach (var entity in items)
            {
                Session.Save(entity);
            }
        }

        public IQueryable<TEntity> Query()
        {
            return Session.Query<TEntity>();
        }

        /*
        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            // SingleOrDefault is translated to 'limit 1' by NHibernate
            return Query()
                .Where(predicate)
                .SingleOrDefault();
        }
        * */

        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate)
        {
            return Query().Where(predicate);
        }

        /*
        public PagedList<TEntity> Query(Expression<Func<TEntity, bool>> predicate, int pageIndex, int pageSize)
        {
            return Query()
                .Where(predicate)
                .OrderBy(entity => entity.Id)
                .ToPagedList(pageIndex, pageSize);
        }

        public PagedList<TEntity> QueryDescending(Expression<Func<TEntity, bool>> predicate, int pageIndex, int pageSize)
        {
            return Query()
                .Where(predicate)
                .OrderByDescending(entity => entity.Id)
                .ToPagedList(pageIndex, pageSize);
        }
        * */

        public TEntity Load(object id)
        {
            return Session.Load<TEntity>(id);
        }

        public void Update(TEntity entity)
        {
            Session.Update(entity);
        }

        public void Update(Expression<Func<TEntity, bool>> predicate, Action<TEntity> action)
        {
            var entities = Query(predicate);

            foreach (var entity in entities)
            {
                action(entity);
                Update(entity);
            }
        }

        public void Delete(TEntity entity)
        {
            Session.Delete(entity);
        }

        public void Delete(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                Session.Delete(entity);
            }
        }

        public void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            var entities = Query(predicate);
            Delete(entities);
        }
    }
}
