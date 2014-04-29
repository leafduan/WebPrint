using System;

namespace WebPrint.Data
{
    public interface IUnitOfWork : IDisposable
    {
        // 为了按需获取session和开启事务
        // void Start();
        // void Commit();
        // void Rollback();
    }
}