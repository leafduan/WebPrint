using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WebPrint.Model;

namespace WebPrint.Data.Repositories
{
    public interface IRepository<TEntity> where TEntity : EntityBase
    {
        object Save(TEntity entity);
        TKey Save<TKey>(TEntity entity);
        void Save(IEnumerable<TEntity> items);

        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate = null);

        /*
        /// <summary>
        /// default id int, sorted in asc
        /// </summary>
        PagedList<TEntity> Query(Expression<Func<TEntity, bool>> predicate, int pageIndex, int pageSize);

        PagedList<TEntity> QueryDescending(Expression<Func<TEntity, bool>> predicate, int pageIndex, int pageSize);
         * */

        TEntity Load(object id);

        void Update(TEntity entity);
        void Update(Action<TEntity> action, Expression<Func<TEntity, bool>> predicate = null);

        void Delete(TEntity entity);
        void Delete(IEnumerable<TEntity> entities);
        void Delete(Expression<Func<TEntity, bool>> predicate = null);
    }
}
