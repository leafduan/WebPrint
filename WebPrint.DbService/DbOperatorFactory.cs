using System;

namespace WebPrint.DbService
{
    public enum DbType
    {
        PostgreSql,
        SqlServer,
        MySql
    }

    public class DbOperatorFactory
    {
        private static DbOperatorFactory factory = null;
        private static object s_mutex = new object();

        public static DbOperatorFactory Factory
        {
            get
            {
                if (factory == null)
                {
                    lock (s_mutex)
                    {
                        if (factory == null)
                            factory = new DbOperatorFactory();
                    }
                }
                return factory;
            }
        }

        public DbOperator CreateOperator(string connectionString)
        {
            return CreateOperator(DbType.PostgreSql, connectionString);
        }

        public DbOperator CreateOperator(DbType dbType, string connectionString)
        {
            switch (dbType)
            {
                case DbType.PostgreSql:
                    return new PgsqlDbOperator(connectionString);
                case DbType.SqlServer:
                    throw new NotImplementedException();
                case DbType.MySql:
                    return new MySqlDbOperator(connectionString);
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
