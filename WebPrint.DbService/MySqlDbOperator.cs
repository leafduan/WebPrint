using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace WebPrint.DbService
{
    public class MySqlDbOperator : DbOperator
    {
        public MySqlDbOperator(string connectionString)
        {
            this.Connection = new MySqlConnection(connectionString);
        }

        public override System.Data.DataTable Query(string cmdText)
        {
            return Query(cmdText, CommandType.Text);
        }

        public override System.Data.DataTable Query(string cmdText, System.Data.CommandType cmdType)
        {
            return Query(cmdText, cmdType, null);
        }

        public override System.Data.DataTable Query(string cmdText, System.Data.CommandType cmdType, System.Data.Common.DbParameter[] parameters)
        {
            DataTable rDt = new DataTable();
            try
            {
                using (MySqlCommand cmd = new MySqlCommand(cmdText, (MySqlConnection)this.Connection))
                {
                    cmd.CommandType = cmdType;
                    if (parameters != null) cmd.Parameters.AddRange(parameters);

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    this.OpenConnection();
                    adapter.Fill(rDt);
                    adapter.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.CloseConnection();
            }

            return rDt;
        }

        public override System.Data.Common.DbDataReader ExecuteReader(string cmdText)
        {
            return ExecuteReader(cmdText, CommandType.Text);
        }

        public override System.Data.Common.DbDataReader ExecuteReader(string cmdText, System.Data.CommandType cmdType)
        {
            return ExecuteReader(cmdText, cmdType, null);
        }

        public override System.Data.Common.DbDataReader ExecuteReader(string cmdText, System.Data.CommandType cmdType, System.Data.Common.DbParameter[] parameters)
        {
            MySqlDataReader reader = null;

            try
            {
                using (MySqlCommand cmd = new MySqlCommand(cmdText, (MySqlConnection)this.Connection))
                {
                    cmd.CommandType = cmdType;
                    if (parameters != null) cmd.Parameters.AddRange(parameters);
                    cmd.CommandTimeout = 0;

                    this.OpenConnection();
                    reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                }
            }
            catch (Exception ex)
            {
                this.CloseConnection();
                throw ex;
            }

            return reader;
        }

        public override object ExecuteScalar(string cmdText)
        {
            return ExecuteScalar(cmdText, CommandType.Text);
        }

        public override object ExecuteScalar(string cmdText, System.Data.CommandType cmdType)
        {
            return ExecuteScalar(cmdText, cmdType, null);
        }

        public override object ExecuteScalar(string cmdText, System.Data.CommandType cmdType, System.Data.Common.DbParameter[] parameters)
        {
            //if (parameters == null) throw new ArgumentNullException("Null reference of param: parameters.");
            object rtValue = new object();
            try
            {
                using (MySqlCommand cmd = new MySqlCommand(cmdText, (MySqlConnection)this.Connection))
                {
                    cmd.CommandType = cmdType;
                    if (parameters != null) cmd.Parameters.AddRange(parameters);
                    cmd.CommandTimeout = 0;

                    this.OpenConnection();
                    rtValue = cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.CloseConnection();
            }

            return rtValue;
        }

        public override int ExecuteNonQuery(string cmdText)
        {
            return ExecuteNonQuery(cmdText, CommandType.Text);
        }

        public override int ExecuteNonQuery(string cmdText, System.Data.CommandType cmdType)
        {
            return ExecuteNonQuery(cmdText, cmdType, null);
        }

        public override int ExecuteNonQuery(string cmdText, System.Data.CommandType cmdType, System.Data.Common.DbParameter[] parameters)
        {
            //if (parameters == null) throw new ArgumentNullException("Null reference of param: parameters.");
            int numberOfAffectedRows = 0;
            try
            {
                using (MySqlCommand cmd = new MySqlCommand(cmdText, (MySqlConnection)this.Connection))
                {
                    cmd.CommandType = cmdType;
                    if (parameters != null) cmd.Parameters.AddRange(parameters);
                    cmd.CommandTimeout = 0;

                    this.OpenConnection();
                    numberOfAffectedRows = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.CloseConnection();
            }

            return numberOfAffectedRows;
        }

        public override void BeginTrans()
        {
            //事务并发控制 串行
            if (this.Transaction != null)
            {
                throw new Exception("System is busy, already exists a transantion.");
            }

            this.OpenConnection();
            this.Transaction = this.Connection.BeginTransaction();
            this.TransCommand = new MySqlCommand();
            this.TransCommand.Connection = this.Connection;
            this.TransCommand.Transaction = this.Transaction;
            this.TransCommand.CommandTimeout = 7200;
        }

        public override void CommitTrans()
        {
            this.Transaction.Commit();
            this.CloseConnection();
            this.CloseTransaction();
            this.CloseTransCommand();
        }

        public override void RollbackTrans()
        {
            this.Transaction.Rollback();
            this.CloseConnection();
            this.CloseTransaction();
            this.CloseTransCommand();
        }

        public override int ExecuteNonQueryTrans(string cmdText)
        {
            return ExecuteNonQueryTrans(cmdText, null);
        }

        public override int ExecuteNonQueryTrans(string cmdText, System.Data.Common.DbParameter[] parameters)
        {
            TransCommand.CommandText = cmdText;
            TransCommand.CommandType = CommandType.Text;
            if (parameters != null) TransCommand.Parameters.AddRange(parameters);
            return TransCommand.ExecuteNonQuery();
        }
    }
}
