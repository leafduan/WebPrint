using System.Collections.Generic;
using System.Data;

namespace WebPrint.DbService
{
    public class DbUnitOfWork : IDbUnitOfWork
    {
        private readonly Dictionary<string,IDbConnection> connections = new Dictionary<string, IDbConnection>();
        private bool isDisposed;

        public DbUnitOfWork()
        {
            isDisposed = false;
        }

        public IDbConnection GetConnection(string connectionStringName)
        {
            if (connections.ContainsKey(connectionStringName))
            {
                return connections[connectionStringName];
            }
            else
            {
                var connection = ConnectionManager.GetConnection(connectionStringName);
                connections.Add(connectionStringName,connection);

                return connection;
            }
        }

        private void Close()
        {
            if (isDisposed) return;

            foreach (var item in connections)
            {
                var connection = item.Value;
                if (connection != null && connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                }
            }

            //connections.Clear();

            isDisposed = true;
        }

        public void Dispose()
        {
            Close();
        }
    }
}
