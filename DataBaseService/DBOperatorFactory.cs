using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataBaseService
{
    public enum DBType
    {
        PostgreSQL,
        SQLServer,
        MySQL
    }

    public class DBOperatorFactory
    {
        private static DBOperatorFactory _factory = null;
        private static object s_mutex = new object();
        public static DBOperatorFactory Factory
        {
            get
            {
                if (_factory == null)
                {
                    lock (s_mutex)
                    {
                        if (_factory == null)
                            _factory = new DBOperatorFactory();
                    }
                }
                return _factory;
            }
        }

        public DBOperator CreateOperator()
        {
            return CreateOperator(DBType.PostgreSQL);
        }

        public DBOperator CreateOperator(string connectionString)
        {
            return CreateOperator(DBType.PostgreSQL, connectionString);
        }

        public DBOperator CreateOperator(DBType dbType)
        {
            return CreateOperator(dbType, string.Empty);
        }

        public DBOperator CreateOperator(DBType dbType, string connectionString)
        {
            switch (dbType)
            {
                case DBType.PostgreSQL:
                    return string.IsNullOrEmpty(connectionString) ?
                        new PgsqlDBOperator() : new PgsqlDBOperator(connectionString);
                case DBType.SQLServer:
                    throw new NotImplementedException();
                case DBType.MySQL:
                    return string.IsNullOrEmpty(connectionString) ? 
                        new MySqlDBOperator() : new MySqlDBOperator(connectionString);
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
