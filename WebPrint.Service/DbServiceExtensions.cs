using System.Data;

namespace WebPrint.Service
{
    public static class DbServiceExtensions
    {
        public static IDbConnection GetConvertionSystemConnection(this IDbService dbService)
        {
            return dbService.GetConnection(SystemType.ConvertionSystem);
        }

        public static IDbConnection GetErpSystemConnection(this IDbService dbService)
        {
            return dbService.GetConnection(SystemType.ErpSystem);
        }

        public static IDbConnection GetAeoSystemConnection(this IDbService dbService)
        {
            return dbService.GetConnection(SystemType.AeoSystem);
        }

        public static IDbConnection GetTwmSystemConnection(this IDbService dbService)
        {
            return dbService.GetConnection(SystemType.TmwSystem);
        }
    }
}
