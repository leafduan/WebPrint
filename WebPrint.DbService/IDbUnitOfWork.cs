using System;
using System.Data;

namespace WebPrint.DbService
{
    public interface IDbUnitOfWork : IDisposable
    {
        IDbConnection GetConnection(string connectionStringName);
        // void Commit();
    }
}
