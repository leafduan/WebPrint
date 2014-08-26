using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WebPrint.Data;
using WebPrint.Data.Helper;
using WebPrint.Data.Repositories;
using WebPrint.Model;
using WebPrint.Model.Helper;

namespace WebPrint.Service
{
    /// <summary>
    /// wrapping repository for the simple service(logic) object
    /// </summary>
    /// <typeparam name="TEntity">EntityBase</typeparam>
    public class Service<TEntity> : IService<TEntity> where TEntity : EntityBase
    {
        protected readonly IRepository<TEntity> repository;
        private readonly IUnitOfWork unitOfWork;

        public Service(IRepository<TEntity> repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }

        #region IService<TEntity> 成员

        public object Save(TEntity entity)
        {
            return repository.Save(entity);
        }

        public TKey Save<TKey>(TEntity entity)
        {
            return repository.Save<TKey>(entity);
        }

        public void Save(IEnumerable<TEntity> entities)
        {
            repository.Save(entities);
        }

        public bool Exists(Expression<Func<TEntity, bool>> predicate)
        {
            return repository
                       .Query(predicate)
                       .FirstOrDefault() != null;
        }

        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            return repository
                .Query(predicate)
                .SingleOrDefault();
        }

        public IEnumerable<TEntity> Query(Expression<Func<TEntity, bool>> predicate)
        {
            return repository.Query(predicate);
        }

        public PagedList<TEntity> Query(Expression<Func<TEntity, bool>> predicate, int pageIndex, int pageSize)
        {
            return repository
                .Query(predicate)
                .OrderBy(t => t.Id)
                .ToPagedList(pageIndex, pageSize);
        }

        public PagedList<TEntity> QueryDescending(Expression<Func<TEntity, bool>> predicate, int pageIndex, int pageSize)
        {
            return repository
                .Query(predicate)
                .OrderByDescending(t => t.Id)
                .ToPagedList(pageIndex, pageSize);
        }

        public IFetchRequest<TEntity, TRelated> Fetch<TRelated>(Expression<Func<TEntity, bool>> predicate,
                                                                Expression<Func<TEntity, TRelated>>
                                                                    relatedObjectSelector)
        {
            return repository.Query(predicate).EagerFetch(relatedObjectSelector);
        }

        public IFetchRequest<TEntity, TRelated> FetchMany<TRelated>(Expression<Func<TEntity, bool>> predicate,
                                                                    Expression<Func<TEntity, IEnumerable<TRelated>>>
                                                                        relatedObjectSelector)
        {
            return repository.Query(predicate).EagerFetchMany(relatedObjectSelector);
        }

        public TEntity Load(int id)
        {
            return repository.Load(id);
        }

        public void Update(Expression<Func<TEntity, bool>> predicate, Action<TEntity> action)
        {
            repository.Update(predicate, action);
        }

        public void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            repository.Delete(predicate);
        }

        public void Delete(int id)
        {
            var entity = repository.Load(id);
            repository.Delete(entity);
        }

        #endregion
    }
}
