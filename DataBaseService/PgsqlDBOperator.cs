using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.Common;
using Npgsql;

namespace DataBaseService
{
    public class PgsqlDBOperator : DBOperator
    {
        /// <summary>
        /// Default Config connectionStrings "conn"
        /// </summary>
        public PgsqlDBOperator() //: this("conn") { }
        {
            this.Connection = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString);
        }
        /*
        /// <summary>
        /// Config connectionStrings name
        /// </summary>
        /// <param name="connectionStringName">connectionStrings name</param>
        public PgsqlDBOperator(string connectionStringName) 
        {
            this.Connection = new NpgsqlConnection(ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString);
        }
         * */

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString">connectionString</param>
        public PgsqlDBOperator(string connectionString)
        {
            this.Connection = new NpgsqlConnection(connectionString);
        }

        #region Query
        public override DataTable Query(string cmdText)
        {
            return Query(cmdText, CommandType.Text);
        }

        public override DataTable Query(string cmdText, CommandType cmdType)
        {
            return Query(cmdText, cmdType, null);
        }

        public override DataTable Query(string cmdText, CommandType cmdType, DbParameter[] parameters)
        {
            DataTable rtDt = new DataTable();
            try
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand(cmdText, (NpgsqlConnection)this.Connection))
                {
                    cmd.CommandType = cmdType;
                    if (parameters != null) cmd.Parameters.AddRange(parameters);
                    cmd.CommandTimeout = 0;

                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                    this.OpenConnection();
                    da.Fill(rtDt);
                    da.Dispose();
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

            return rtDt;
        }
        #endregion

        #region ExecuteReader
        public override DbDataReader ExecuteReader(string cmdText)
        {
            return ExecuteReader(cmdText, CommandType.Text);
        }

        public override DbDataReader ExecuteReader(string cmdText, CommandType cmdType)
        {
            return ExecuteReader(cmdText, cmdType, null);
        }

        public override DbDataReader ExecuteReader(string cmdText, CommandType cmdType, DbParameter[] parameters)
        {
            NpgsqlDataReader reader = null;
            try
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand(cmdText, (NpgsqlConnection)this.Connection))
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
        #endregion

        #region ExecuteScalar
        public override object ExecuteScalar(string cmdText)
        {
            return ExecuteScalar(cmdText, CommandType.Text);
        }

        public override object ExecuteScalar(string cmdText, CommandType cmdType)
        {
            return ExecuteScalar(cmdText, cmdType, null);
        }

        public override object ExecuteScalar(string cmdText, CommandType cmdType, DbParameter[] parameters)
        {
            //if (parameters == null) throw new ArgumentNullException("Null reference of param: parameters.");
            object rtValue = new object();
            try
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand(cmdText, (NpgsqlConnection)this.Connection))
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
        #endregion

        #region ExecuteNonQuery
        public override int ExecuteNonQuery(string cmdText)
        {
            return ExecuteNonQuery(cmdText, CommandType.Text);
        }

        public override int ExecuteNonQuery(string cmdText, CommandType cmdType)
        {
            return ExecuteNonQuery(cmdText, cmdType, null);
        }

        public override int ExecuteNonQuery(string cmdText, CommandType cmdType, DbParameter[] parameters)
        {
            //if (parameters == null) throw new ArgumentNullException("Null reference of param: parameters.");
            int numberOfAffectedRows = 0;
            try
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand(cmdText, (NpgsqlConnection)this.Connection))
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
        #endregion

        #region Transaction
        public override void BeginTrans()
        {
            //事务并发控制 串行
            if (this.Transaction != null)
            {
                throw new Exception("System is busy, already exists a transantion.");
            }

            this.OpenConnection();
            this.Transaction = this.Connection.BeginTransaction();
            this.TransCommand = new NpgsqlCommand();
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
        #endregion

        #region ExecuteNonQueryTrans
        public override int ExecuteNonQueryTrans(string cmdText)
        {
            return ExecuteNonQueryTrans(cmdText, null);
        }

        public override int ExecuteNonQueryTrans(string cmdText, DbParameter[] parameters)
        {
            TransCommand.CommandText = cmdText;
            TransCommand.CommandType = CommandType.Text;
            if (parameters != null) TransCommand.Parameters.AddRange(parameters);
            return TransCommand.ExecuteNonQuery();
        }
        #endregion
    }
}
