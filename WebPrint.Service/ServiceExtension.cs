using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WebPrint.Model;
using WebPrint.Model.Helper;

namespace WebPrint.Service
{
    public static class ServiceExtension
    {
        public static decimal Sum<TEntity>(this IService<TEntity> service, Expression<Func<TEntity, decimal?>> selector,
            Expression<Func<TEntity, bool>> predicate = null) where TEntity : EntityBase
        {
            var reslut = service
                .Queryable(predicate)
                .Sum(selector);

            return reslut ?? default(decimal);
        }

        public static IEnumerable<TResult> Distinct<TEntity, TResult>(this IService<TEntity> service,
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> predicate = null) where TEntity : EntityBase
        {
            return service
                .Queryable(predicate)
                .Select(selector).Distinct();
        }

        public static IEnumerable<TEntity> Query<TEntity>(this IService<TEntity> service,
            Expression<Func<TEntity, bool>> predicate = null) where TEntity : EntityBase
        {
            return service.Queryable(predicate);
        }

        public static IEnumerable<TResult> Query<TEntity, TResult>(this IService<TEntity> service,
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> predicate = null) where TEntity : EntityBase
        {
            return service
                .Queryable(predicate)
                .Select(selector);
        }

        public static PagedList<TEntity> Query<TEntity>(this IService<TEntity> service, int pageIndex, int pageSize,
            Expression<Func<TEntity, bool>> predicate = null) where TEntity : EntityBase
        {
            return service
                .Queryable(predicate)
                .OrderBy(t => t.Id)
                .ToPagedList(pageIndex, pageSize);
        }

        public static PagedList<TEntity> Query<TEntity, TKey>(this IService<TEntity> service, int pageIndex,
            int pageSize,
            Expression<Func<TEntity, TKey>> keySelector, Expression<Func<TEntity, bool>> predicate = null)
            where TEntity : EntityBase
        {
            return service
                .Queryable(predicate)
                .OrderBy(keySelector)
                .ToPagedList(pageIndex, pageSize);
        }

        public static PagedList<TEntity> QueryDescending<TEntity>(this IService<TEntity> service, int pageIndex,
            int pageSize, Expression<Func<TEntity, bool>> predicate = null) where TEntity : EntityBase
        {
            return service
                .Queryable(predicate)
                .OrderByDescending(t => t.Id)
                .ToPagedList(pageIndex, pageSize);
        }

        public static PagedList<TEntity> QueryDescending<TEntity, TKey>(this IService<TEntity> service, int pageIndex,
            int pageSize,
            Expression<Func<TEntity, TKey>> keySelector, Expression<Func<TEntity, bool>> predicate = null)
            where TEntity : EntityBase
        {
            return service
                .Queryable(predicate)
                .OrderByDescending(keySelector)
                .ToPagedList(pageIndex, pageSize);
        }
    }
}
