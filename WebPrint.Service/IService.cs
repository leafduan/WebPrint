using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WebPrint.Model;
using WebPrint.Model.Helper;

namespace WebPrint.Service
{
    public interface IService<TEntity> where TEntity : EntityBase
    {
        object Save(TEntity entity);
        TKey Save<TKey>(TEntity entity);
        void Save(IEnumerable<TEntity> entities);

        bool Exists(Expression<Func<TEntity, bool>> predicate);

        IQueryable<TEntity> Queryable(Expression<Func<TEntity, bool>> predicate = null);

        /// <summary>
        /// SingleOrDefault
        /// </summary>
        TEntity Get(Expression<Func<TEntity, bool>> predicate);

        int Count(Expression<Func<TEntity, bool>> predicate = null);

        decimal Sum(Expression<Func<TEntity, decimal?>> selector,
            Expression<Func<TEntity, bool>> predicate = null);

        IEnumerable<TResult> Distinct<TResult>(Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> predicate = null);

        //IQueryable<IGrouping<TResult, TEntity>> GroupBy<TResult>(Expression<Func<TEntity, TResult>> selector,
        //    Expression<Func<TEntity, bool>> predicate = null);

        IEnumerable<TEntity> Query(Expression<Func<TEntity, bool>> predicate = null);

        IEnumerable<TResult> Query<TResult>(Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> predicate = null);

        /// <summary>
        /// default id(int) asc
        /// </summary>
        PagedList<TEntity> Query(int pageIndex, int pageSize, Expression<Func<TEntity, bool>> predicate = null);

        PagedList<TEntity> Query<TKey>(int pageIndex, int pageSize, Expression<Func<TEntity, TKey>> keySelector,
            Expression<Func<TEntity, bool>> predicate = null);

        /// <summary>
        /// default id(int) desc
        /// </summary>
        PagedList<TEntity> QueryDescending(int pageIndex, int pageSize, Expression<Func<TEntity, bool>> predicate = null);

        PagedList<TEntity> QueryDescending<TKey>(int pageIndex, int pageSize, Expression<Func<TEntity, TKey>> keySelector,
            Expression<Func<TEntity, bool>> predicate = null);

        /* using IQueryable instead */
        /*
        IFetchRequest<TEntity, TRelated> Fetch<TRelated>(Expression<Func<TEntity, bool>> predicate,
                                                         Expression<Func<TEntity, TRelated>> relatedObjectSelector);

        IFetchRequest<TEntity, TRelated> FetchMany<TRelated>(Expression<Func<TEntity, bool>> predicate,
                                                                           Expression<Func<TEntity, IEnumerable<TRelated>>>
                                                                               relatedObjectSelector);
        */
        /// <summary>
        /// <para>Load不会返回null,始终返回代理对象</para>
        /// <para>Load与Get不同,不会立即请求数据库,而是在访问对象属性时,再select数据库,延迟加载.因此要保证延迟加载时session未关闭</para>
        /// <para>用处:在插入,更新或删除时,用Load先加载对象,会假定数据库中存在对应id的row,而不会预先查询一次</para>
        /// <para>场景:当确定对应id的对象在数据库中存在时,基本是在save时,有关联对象的情况下使用</para>
        /// <para>注意:如果class的lazy=false,则行为与Get一致</para>
        /// </summary>
        TEntity Load(int id);

        void Update(Action<TEntity> action, Expression<Func<TEntity, bool>> predicate = null);

        void Delete(Expression<Func<TEntity, bool>> predicate = null);

        /// <summary>
        /// delete with load 
        /// </summary>
        void Delete(int id);
    }
}
