using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WebPrint.Model.Helper;

namespace WebPrint.Model.Services
{
    public interface IService<TEntity> where TEntity : EntityBase
    {
        object Save(TEntity entity);
        TKey Save<TKey>(TEntity entity);
        void Save(IEnumerable<TEntity> entities);

        bool Exists(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// SingleOrDefault
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        TEntity Get(Expression<Func<TEntity, bool>> predicate);

        IEnumerable<TEntity> Query(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// default id int, sorted in asc
        /// </summary>
        PagedList<TEntity> Query(Expression<Func<TEntity, bool>> predicate, int pageIndex, int pageSize);

        PagedList<TEntity> QueryDescending(Expression<Func<TEntity, bool>> predicate, int pageIndex, int pageSize);

        IFetchRequest<TEntity, TRelated> Fetch<TRelated>(Expression<Func<TEntity, bool>> predicate,
                                                         Expression<Func<TEntity, TRelated>> relatedObjectSelector);

        IFetchRequest<TEntity, TRelated> FetchMany<TRelated>(Expression<Func<TEntity, bool>> predicate,
                                                                           Expression<Func<TEntity, IEnumerable<TRelated>>>
                                                                               relatedObjectSelector);

        /// <summary>
        /// <para>Load不会返回null,始终返回代理对象</para>
        /// <para>Load与Get不同,不会立即请求数据库,而是在访问对象属性时,再select数据库,延迟加载.因此要保证延迟加载时session未关闭</para>
        /// <para>用处:在插入,更新或删除时,用Load先加载对象,会假定数据库中存在对应id的row,而不会预先查询一次</para>
        /// <para>场景:当确定对应id的对象在数据库中存在时,基本是在save时,有关联对象的情况下使用</para>
        /// <para>注意:如果class的lazy=false,则行为与Get一致</para>
        /// </summary>
        TEntity Load(int id);

        void Update(Expression<Func<TEntity, bool>> predicate, Action<TEntity> action);

        void Delete(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// delete with load 
        /// </summary>
        void Delete(int id);
    }
}
