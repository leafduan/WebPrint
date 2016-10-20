using System;
using System.Collections.Generic;
using WebPrint.DbService;

namespace WebPrint.Service
{
    public class DbService : IDbService, IDisposable
    {
        private static readonly Dictionary<SystemType, string> connectionStringNames = new Dictionary
            <SystemType, string>
            {
                {SystemType.ConvertionSystem, "OracleConn"},
                {SystemType.ErpSystem, "OracleErpConn"},
                {SystemType.AeoSystem, "AeoSystem"},
                {SystemType.TmwSystem, "TmwSystem"}
            };

        private IDbUnitOfWork unitOfWork;

        public DbService(IDbUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public System.Data.IDbConnection GetConnection(SystemType systemType)
        {
            return unitOfWork.GetConnection(connectionStringNames[systemType]);
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}
