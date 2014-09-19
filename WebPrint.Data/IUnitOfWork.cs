using System;
using WebPrint.Data.Repositories;
using WebPrint.Model;

namespace WebPrint.Data
{
    public interface IUnitOfWork : IDisposable
    {
        // 为了按需获取session和开启事务
        // void Start();
        // void Commit();
        // void Rollback();

        IRepository<TEntity> Repository<TEntity>() where TEntity : EntityBase;
    }
}