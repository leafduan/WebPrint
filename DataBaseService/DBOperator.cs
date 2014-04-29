using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;

namespace DataBaseService
{
    public abstract class DBOperator
    {
        protected DbConnection Connection { get; set; }
        protected DbTransaction Transaction { get; set; }
        protected DbCommand TransCommand { get; set; }

        protected DBOperator() { }

        protected void OpenConnection()
        {
            if ((Connection != null) && (Connection.State == ConnectionState.Closed))
            {
                Connection.Open();
            }
        }

        protected void CloseConnection()
        {
            if ((Connection != null) && (Connection.State != ConnectionState.Closed))
            {
                Connection.Close();
            }
        }

        protected void CloseTransaction()
        {
            if (Transaction != null)
            {
                Transaction.Dispose();
                Transaction = null; 
            }
        }

        protected void CloseTransCommand()
        {
            if (TransCommand != null)
            {
                TransCommand.Dispose();
                TransCommand = null;
            }
        }

        public abstract DataTable Query(string cmdText);
        public abstract DataTable Query(string cmdText, CommandType cmdType);
        public abstract DataTable Query(string cmdText, CommandType cmdType, DbParameter[] parameters);

        public abstract DbDataReader ExecuteReader(string cmdText);
        public abstract DbDataReader ExecuteReader(string cmdText, CommandType cmdType);
        public abstract DbDataReader ExecuteReader(string cmdText, CommandType cmdType, DbParameter[] parameters);

        public abstract object ExecuteScalar(string cmdText);
        public abstract object ExecuteScalar(string cmdText, CommandType cmdType);
        public abstract object ExecuteScalar(string cmdText, CommandType cmdType, DbParameter[] parameters);

        public abstract int ExecuteNonQuery(string cmdText);
        public abstract int ExecuteNonQuery(string cmdText, CommandType cmdType);
        public abstract int ExecuteNonQuery(string cmdText, CommandType cmdType, DbParameter[] parameters);

        public abstract void BeginTrans();
        public abstract void CommitTrans();
        public abstract void RollbackTrans();

        public abstract int ExecuteNonQueryTrans(string cmdText);
        public abstract int ExecuteNonQueryTrans(string cmdText, DbParameter[] parameters);
    }
}
