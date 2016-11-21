using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WebPrint.Data.Repositories;
using WebPrint.Model;

namespace WebPrint.Service
{
    /// <summary>
    /// wrapping repository for the simple service(logic) object
    /// </summary>
    /// <typeparam name="TEntity">EntityBase</typeparam>
    public class Service<TEntity> : IService<TEntity> where TEntity : EntityBase
    {
        private IRepository<TEntity> repository;

        public Service(IRepositoryProvider repositoryProvider)
        {
            repository = repositoryProvider.GetRepository<TEntity>();
        }

        #region IService<TEntity> 成员

        public int Save(TEntity entity)
        {
            return (int) (repository.Save(entity) ?? 0);
        }

        public bool Exists(Expression<Func<TEntity, bool>> predicate)
        {
            /*
            return repository
                       .Query(predicate)
                       .FirstOrDefault() != null;
             * */

            return repository
                .Query(predicate)
                .Any();
        }

        public IQueryable<TEntity> Queryable(Expression<Func<TEntity, bool>> predicate = null)
        {
            return repository.Query(predicate);
        }

        public TEntity Get(int id)
        {
            return repository.Get(id);
        }

        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            return repository
                .Query(predicate)
                .SingleOrDefault();
        }

        public int Count(Expression<Func<TEntity, bool>> predicate = null)
        {
            return repository
                .Query(predicate)
                .Count();
        }

        /*
        public decimal Sum(Expression<Func<TEntity, decimal?>> selector,
            Expression<Func<TEntity, bool>> predicate = null)
        {
            var reslut = Repository
                .Query(predicate)
                .Sum(selector);

            return reslut ?? default(decimal);
        }

        public IEnumerable<TResult> Distinct<TResult>(Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> predicate = null)
        {
            return Repository
                .Query(predicate)
                .Select(selector).Distinct();
        }

        //public IQueryable<IGrouping<TResult, TEntity>> GroupBy<TResult>(Expression<Func<TEntity, TResult>> selector,
        //    Expression<Func<TEntity, bool>> predicate = null)
        //{
        //    return Repository
        //        .Query(predicate)
        //        .GroupBy(selector);
        //}

        public IEnumerable<TEntity> Query(Expression<Func<TEntity, bool>> predicate = null)
        {
            return Repository.Query(predicate);
        }

        public IEnumerable<TResult> Query<TResult>(Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> predicate = null)
        {
            return Repository
                .Query(predicate)
                .Select(selector);
        }

        public PagedList<TEntity> Query(int pageIndex, int pageSize, Expression<Func<TEntity, bool>> predicate = null)
        {
            return Repository
                .Query(predicate)
                .OrderBy(t => t.Id)
                .ToPagedList(pageIndex, pageSize);
        }

        public PagedList<TEntity> Query<TKey>(int pageIndex, int pageSize,
            Expression<Func<TEntity, TKey>> keySelector, Expression<Func<TEntity, bool>> predicate = null)
        {
            return Repository
                .Query(predicate)
                .OrderBy(keySelector)
                .ToPagedList(pageIndex, pageSize);
        }

        public PagedList<TEntity> QueryDescending(int pageIndex, int pageSize,
            Expression<Func<TEntity, bool>> predicate = null)
        {
            return Repository
                .Query(predicate)
                .OrderByDescending(t => t.Id)
                .ToPagedList(pageIndex, pageSize);
        }

        public PagedList<TEntity> QueryDescending<TKey>(int pageIndex, int pageSize,
            Expression<Func<TEntity, TKey>> keySelector, Expression<Func<TEntity, bool>> predicate = null)
        {
            return Repository
                .Query(predicate)
                .OrderByDescending(keySelector)
                .ToPagedList(pageIndex, pageSize);
        }
        */

        /*
        public IFetchRequest<TEntity, TRelated> Fetch<TRelated>(Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TRelated>>
                relatedObjectSelector)
        {
            return Repository
                .Query(predicate)
                .EagerFetch(relatedObjectSelector);
        }

        public IFetchRequest<TEntity, TRelated> FetchMany<TRelated>(Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, IEnumerable<TRelated>>>
                relatedObjectSelector)
        {
            return Repository
                .Query(predicate)
                .EagerFetchMany(relatedObjectSelector);
        
         * */

        public TEntity Load(int id)
        {
            return repository.Load(id);
        }

        public void Update(Action<TEntity> action, Expression<Func<TEntity, bool>> predicate = null)
        {
            repository.Update(action, predicate);
        }

        public void Delete(Expression<Func<TEntity, bool>> predicate = null)
        {
            repository.Delete(predicate);
        }

        public void Delete(int id)
        {
            var entity = repository.Load(id);
            repository.Delete(entity);
        }

        public IEnumerable<TResult> SqlQuery<TResult>(string sql)
        {
            return repository.SqlQuery<TResult>(sql);
        }

        public int ExcuteSql(string sql)
        {
            return repository.ExcuteSql(sql);
        }

        #endregion
    }
}
