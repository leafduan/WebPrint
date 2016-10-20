using System.Configuration;
using System.Data;
using System.Data.Common;

namespace WebPrint.DbService
{
    public static class ConnectionManager
    {
        public static IDbConnection GetConnection(string connectionStringName)
        {
            var connectionStringSetting = ConfigurationManager.ConnectionStrings[connectionStringName];

            // 需要 config <system.data><DbProviderFactories> 麻烦，又不需要配置的方法吗？
            var connection = DbProviderFactories.GetFactory(connectionStringSetting.ProviderName).CreateConnection();
            connection.ConnectionString = connectionStringSetting.ConnectionString;

            return connection;
        }
    }
}
