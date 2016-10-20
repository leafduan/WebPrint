using System.Data;

namespace WebPrint.Service
{
    public enum SystemType
    {
        ConvertionSystem,
        ErpSystem,
        AeoSystem,
        TmwSystem,
    }

    public interface IDbService
    {
        IDbConnection GetConnection(SystemType systemType);
    }
}
