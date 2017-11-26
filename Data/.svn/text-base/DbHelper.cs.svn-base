
/*===========================================================
 * 
 * Origami (Object Relational Gateway Microarchitecture)
 * 
 * Lightweight Enterprise Application Framework
 *
 * Version  : 3.0
 * Author   : Ariyanto
 * E-Mail   : neonerdy@yahoo.com
 *  
 * 
 * © 2009, Under Apache Licence 
 * 
 *==========================================================
 */

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

using Origami.Logging;

namespace Origami.Data
{
    public class DbHelper
    {
        private DbConnection conn;
        private DataSource dataSource;
        private ILogger logger;

        public DbHelper(DbConnection conn)
        {
            logger = LoggerFactory.CreateTraceLogger();
            this.conn = conn;
        }

        public DbHelper(DataSource dataSource)
        {
            logger = LoggerFactory.CreateTraceLogger();
            this.dataSource = dataSource;
            if (conn == null)
            {
                conn = ConnectionFactory.CreateConnection(dataSource);
            }
       }

        private void AddParameter(DbCommand cmd, ParameterClause param)
        {
            DbParameter dbParameter = cmd.CreateParameter();
        
            dbParameter.Direction = param.Direction;
            dbParameter.DbType = param.DbType;
            dbParameter.ParameterName = param.Name;
            dbParameter.Value = param.Argument;

            cmd.Parameters.Add(dbParameter);
        }
        

        public DbConnection Connection
        {
            get { return conn; }
        }

        public DataSource DataSource
        {
            get { return dataSource; }
        }

        public Transaction BeginTransaction()
        {
            return new Transaction(conn);
        }

        public int ExecuteNonQuery(string sql, bool isTransaction, Transaction tx)
        {
            int result = 0;
            DbCommand cmd = null;
            try
            {
                cmd = conn.CreateCommand();
                
                cmd.CommandType = CommandType.Text;
                if (isTransaction)
                {
                    cmd.Transaction = tx.GetTransaction();
                }

                cmd.CommandText = sql;
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                tx.Rollback();
                if (cmd != null) cmd.Dispose();
                logger.Write(Severity.Error,ex.ToString());
            
                throw ex;
            }        
            finally
            {
                if (cmd != null) cmd.Dispose();
            }

            return result;
        }


        public int ExecuteNonQuery(DbCommandWrapper cmdWrapper,
            bool isStoredProc, string cmdText, bool isTransaction, Transaction tx)
        {

            int result = 0;
            DbCommand cmd = null;

            try
            {
                cmd = conn.CreateCommand();

                if (isTransaction)
                {
                    cmd.Transaction = tx.GetTransaction();
                }

                if (isStoredProc)
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                }
                else
                {
                    cmd.CommandType = CommandType.Text;
                }

                cmd.CommandText = cmdText;
                foreach (ParameterClause param in cmdWrapper.Parameters)
                {
                    AddParameter(cmd, param);
                }
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                if (cmd != null) cmd.Dispose();
                logger.Write(Severity.Error, ex.ToString());
                
                throw ex;
            }
            finally
            {
                if (cmd != null) cmd.Dispose();
            }
            return result;         
        }

        
        public IDataReader ExecuteReader(string sql)
        {
            DbDataReader reader = null;
            DbCommand cmd = null;
            try
            {
                cmd = conn.CreateCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                reader = cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                if (cmd != null) cmd.Dispose();
                logger.Write(Severity.Error, ex.ToString());
            
                throw ex;
            }
            finally
            {
                if (cmd != null) cmd.Dispose();
            }
            return reader;
        }


        public IDataReader ExecuteReader(DbCommandWrapper cmdWrapper,
           bool isStoredProc, string cmdText)
        {
            DbCommand cmd = null;
            IDataReader rdr = null;
            try
            {
                cmd = conn.CreateCommand();
                cmdWrapper.Command = cmd;

                if (isStoredProc)
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                }
                else
                {
                    cmd.CommandType = CommandType.Text;
                }

                cmd.CommandText = cmdText;

                foreach (ParameterClause param in cmdWrapper.Parameters)
                {
                    AddParameter(cmd, param);
                }
                rdr = cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                if (cmd != null) cmd.Dispose();
                logger.Write(Severity.Error, ex.ToString());
            
                throw ex;
            }
            finally
            {
                if (cmd != null) cmd.Dispose();
            }

            return rdr;
        }
        

        public DataSet ExecuteDataSet(string sql)
        {
            DataSet dataSet = null;
            DbCommand cmd = null;
            DbDataAdapter da = null;
            DbProviderFactory factory = null;

            try
            {
                factory = DbProviderFactories.GetFactory(dataSource.Provider);
                da = factory.CreateDataAdapter();
                cmd = conn.CreateCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;

                dataSet = new DataSet();
                da.SelectCommand = cmd;
                da.Fill(dataSet);
            }
            catch (Exception ex)
            {
                if (da != null) da.Dispose();
                if (cmd != null) cmd.Dispose();

                logger.Write(Severity.Error, ex.ToString());
            
                throw ex;
            }
            finally
            {
                if (da != null) da.Dispose();
                if (cmd != null) cmd.Dispose();
            }
            return dataSet;
        }


        public DataSet ExecuteDataSet(DbCommandWrapper cmdWrapper,
           bool isStoredProc, string cmdText)
        {
            DataSet dataSet = null;
            DbCommand cmd = null;
            DbDataAdapter da = null;
            DbProviderFactory factory = null;

            try
            {
                factory = DbProviderFactories.GetFactory(dataSource.Provider);
                da = factory.CreateDataAdapter();
                cmd.Connection = conn;

                cmd = conn.CreateCommand();
                cmdWrapper.Command = cmd;

                if (isStoredProc)
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                }
                else
                {
                    cmd.CommandType = CommandType.Text;
                }
                cmd.CommandText = cmdText;

                dataSet = new DataSet();

                foreach (ParameterClause param in cmdWrapper.Parameters)
                {
                    AddParameter(cmd, param);
                }

                da.SelectCommand = cmd;
                da.Fill(dataSet);
            }
            catch (Exception ex)
            {
                if (da != null) da.Dispose();
                if (cmd != null) cmd.Dispose();

                logger.Write(Severity.Error, ex.ToString());
            
                throw ex;
            }
            finally
            {
                if (cmd != null) cmd.Dispose();
            }

            return dataSet;
        }
        


        public object ExecuteScalar(string sql)
        {
            object result = null;
            DbCommand cmd = null;

            try
            {
                cmd = conn.CreateCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                result = cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                if (cmd != null) cmd.Dispose();

                logger.Write(Severity.Error, ex.ToString());
                throw ex;
            }
            finally
            {
                if (cmd != null) cmd.Dispose();
            }
            return result;
        }

        
        public object ExecuteScalar(DbCommandWrapper cmdWrapper,
           bool isStoredProc, string cmdText)
        {
            object result = null;
            DbCommand cmd = null;
            try
            {
                cmd = conn.CreateCommand();
                if (isStoredProc)
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                }
                else
                {
                    cmd.CommandType = CommandType.Text;
                }

                cmd = conn.CreateCommand();
                cmdWrapper.Command = cmd;
                cmd.Connection = conn;
                cmd.CommandText = cmdText;
                              
                foreach (ParameterClause param in cmdWrapper.Parameters)
                {
                    AddParameter(cmd, param);
                }
                result = cmd.ExecuteScalar();          
            }
            catch (Exception ex)
            {
                if (cmd != null) cmd.Dispose();
                logger.Write(Severity.Error, ex.ToString());

                throw ex;
            }
            finally
            {
                if (cmd != null) cmd.Dispose();
            }

            return result;
        }
        
        

        public T ExecuteObject<T>(string sql, IDataMapper<T> dataMapper)
        {
            T obj = default(T);
            IDataReader rdr = null;
            DbCommand cmd = null;

            try
            {
                cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    obj = dataMapper.Map(rdr);
                }
            }
            catch (Exception ex)
            {
                if (cmd != null) cmd.Dispose();
                if (rdr != null) rdr.Close();

                logger.Write(Severity.Error, ex.ToString());
                throw ex;
            }
            finally
            {
                if (cmd != null) cmd.Dispose();
                if (rdr != null) rdr.Close();
            }

            return obj;
        }



        public T ExecuteObject<T>(string sql, Origami.Data.DataContext.DataMapperDelegate<T> dataMapper)
        {
            T obj = default(T);
            IDataReader rdr = null;
            DbCommand cmd = null;

            try
            {
                cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    obj = dataMapper.Invoke(rdr);
                }
            }
            catch (Exception ex)
            {
                if (cmd != null) cmd.Dispose();
                if (rdr != null) rdr.Close();

                logger.Write(Severity.Error, ex.ToString());
                throw ex;
            }
            finally
            {
                if (cmd != null) cmd.Dispose();
                if (rdr != null) rdr.Close();
            }

            return obj;
        }




        public T ExecuteObject<T>(DbCommandWrapper cmdWrapper, bool isStoredProc,
           string cmdText, IDataMapper<T> dataMapper)
        {
            T obj = default(T);
            DbCommand cmd = null;
            IDataReader rdr = null;

            try
            {
                cmd = conn.CreateCommand();
                cmdWrapper.Command = cmd;

                if (isStoredProc)
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                }
                else
                {
                    cmd.CommandType = CommandType.Text;
                }

                cmd.CommandText = cmdText;

                foreach (ParameterClause param in cmdWrapper.Parameters)
                {
                    AddParameter(cmd, param);
                }

                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    obj = dataMapper.Map(rdr);
                }
            }
            catch (Exception ex)
            {
                if (cmd != null) cmd.Dispose();
                if (rdr != null) rdr.Close();
                logger.Write(Severity.Error, ex.ToString());
          
                throw ex;
            }
            finally
            {
                if (cmd != null) cmd.Dispose();
                if (rdr != null) rdr.Close();
            }
            return obj;
        }


        public T ExecuteObject<T>(DbCommandWrapper cmdWrapper, bool isStoredProc,
          string cmdText, Origami.Data.DataContext.DataMapperDelegate<T> dataMapper)
        {
            T obj = default(T);
            DbCommand cmd = null;
            IDataReader rdr = null;

            try
            {
                cmd = conn.CreateCommand();
                cmdWrapper.Command = cmd;

                if (isStoredProc)
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                }
                else
                {
                    cmd.CommandType = CommandType.Text;
                }

                cmd.CommandText = cmdText;

                foreach (ParameterClause param in cmdWrapper.Parameters)
                {
                    AddParameter(cmd, param);
                }

                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    obj = dataMapper.Invoke(rdr);
                }
            }
            catch (Exception ex)
            {
                if (cmd != null) cmd.Dispose();
                if (rdr != null) rdr.Close();
                logger.Write(Severity.Error, ex.ToString());

                throw ex;
            }
            finally
            {
                if (cmd != null) cmd.Dispose();
                if (rdr != null) rdr.Close();
            }
            return obj;
        }

        
        public List<T> ExecuteList<T>(string sql, IDataMapper<T> dataMapper)
        {
            List<T> list = new List<T>();
            IDataReader rdr = null;
            DbCommand cmd = null;

            try
            {
                cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    list.Add(dataMapper.Map(rdr));
                }
            }
            catch (Exception ex)
            {
                if (cmd != null) cmd.Dispose();
                if (rdr != null) rdr.Close();

                logger.Write(Severity.Error, ex.ToString());
                throw ex;
            }
            finally
            {
                if (cmd != null) cmd.Dispose();
                if (rdr != null) rdr.Close();
            }
                       
            return list;

        }
        

        public List<T> ExecuteList<T>(string sql, IDataMapper<T> rowMapper,
           int index, int size)
        {
            List<T> list = new List<T>();
            IDataReader rdr = null;
            DataSet dataSet = null;
            DbCommand cmd = null;
            DbDataAdapter da = null;
            DbProviderFactory factory = null;

            try
            {
                factory = DbProviderFactories.GetFactory(dataSource.Provider);
                da = factory.CreateDataAdapter();
                cmd = conn.CreateCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;

                dataSet = new DataSet();
                da.SelectCommand = cmd;
                da.Fill(dataSet, index, size, typeof(T).ToString());

                rdr = dataSet.Tables[typeof(T).ToString()].CreateDataReader();

                while (rdr.Read())
                {
                    list.Add(rowMapper.Map(rdr));
                }
            }
            catch (Exception ex)
            {
                if (cmd != null) cmd.Dispose();
                if (da != null) da.Dispose();
                if (dataSet != null) dataSet.Dispose();
                if (rdr != null) rdr.Close();

                logger.Write(Severity.Error, ex.ToString());
                throw ex;
            }
            finally
            {
                if (cmd != null) cmd.Dispose();
                if (da != null) da.Dispose();
                if (dataSet != null) dataSet.Dispose();
                if (rdr != null) rdr.Close();
            }

            return list;
        }

        

        public List<T> ExecuteList<T>(string sql, 
            Origami.Data.DataContext.DataMapperDelegate<T> dataMapper)
        {
            List<T> list = new List<T>();
            IDataReader rdr = null;
            DbCommand cmd = null;

            try
            {
                cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    list.Add(dataMapper.Invoke(rdr));
                }
            }
            catch (Exception ex)
            {
                if (cmd != null) cmd.Dispose();
                if (rdr != null) rdr.Close();

                logger.Write(Severity.Error, ex.ToString());
                throw ex;
            }
            finally
            {
                if (cmd != null) cmd.Dispose();
                if (rdr != null) rdr.Close();
            }

            return list;

        }


        public List<T> ExecuteList<T>(DbCommandWrapper cmdWrapper, bool isStoredProc,
          string cmdText, IDataMapper<T> dataMapper)
        {
            List<T> list = new List<T>();
            DbCommand cmd = null;
            IDataReader rdr = null;

            try
            {
                cmd = conn.CreateCommand();
                cmdWrapper.Command = cmd;

                if (isStoredProc)
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                }
                else
                {
                    cmd.CommandType = CommandType.Text;
                }

                cmd.CommandText = cmdText;

                foreach (ParameterClause param in cmdWrapper.Parameters)
                {
                    AddParameter(cmd, param);
                }

                cmd.CommandText = cmdText;
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    list.Add(dataMapper.Map(rdr));
                }
            }
            catch (Exception ex)
            {
                logger.Write(Severity.Error, ex.ToString());
              
                if (cmd != null) cmd.Dispose();
                if (rdr != null) rdr.Close();

                throw ex;
            }
            finally
            {
                if (cmd != null) cmd.Dispose();
                if (rdr != null) rdr.Close();
            }
            return list;
        }



        public List<T> ExecuteList<T>(DbCommandWrapper cmdWrapper, bool isStoredProc,
            string cmdText, Origami.Data.DataContext.DataMapperDelegate<T> dataMapper)
        {
            List<T> list = new List<T>();
            DbCommand cmd = null;
            IDataReader rdr = null;

            try
            {
                cmd = conn.CreateCommand();
                cmdWrapper.Command = cmd;

                if (isStoredProc)
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                }
                else
                {
                    cmd.CommandType = CommandType.Text;
                }

                cmd.CommandText = cmdText;

                foreach (ParameterClause param in cmdWrapper.Parameters)
                {
                    AddParameter(cmd, param);        
                }

                cmd.CommandText = cmdText;
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    list.Add(dataMapper.Invoke(rdr));
                }
            }
            catch (Exception ex)
            {
                logger.Write(Severity.Error, ex.ToString());

                if (cmd != null) cmd.Dispose();
                if (rdr != null) rdr.Close();

                throw ex;
            }
            finally
            {
                if (cmd != null) cmd.Dispose();
                if (rdr != null) rdr.Close();
            }
            return list;
        }


        public List<T> ExecuteList<T>(DbCommandWrapper cmdWrapper, bool isStoredProc,
            string sql, IDataMapper<T> dataMapper, int index, int size)
        {
            List<T> list = new List<T>();
            IDataReader rdr = null;
            DataSet dataSet = null;
            DbCommand cmd = null;
            DbDataAdapter da = null;
            DbProviderFactory factory = null;

            try
            {
                factory = DbProviderFactories.GetFactory(dataSource.Provider);
                da = factory.CreateDataAdapter();
                cmd = conn.CreateCommand();
                cmdWrapper.Command = cmd;

                cmd.Connection = conn;

                if (isStoredProc)
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                }
                else
                {
                    cmd.CommandType = CommandType.Text;
                }

                cmd.CommandText = sql;

                dataSet = new DataSet();
                da.SelectCommand = cmd;
                da.Fill(dataSet, index, size, typeof(T).ToString());

                foreach (ParameterClause param in cmdWrapper.Parameters)
                {
                    AddParameter(cmd, param);
                }

                rdr = dataSet.Tables[typeof(T).ToString()].CreateDataReader();

                while (rdr.Read())
                {
                    list.Add(dataMapper.Map(rdr));
                }
            }
            catch (Exception ex)
            {
                logger.Write(Severity.Error, ex.ToString());

                if (cmd != null) cmd.Dispose();
                if (da != null) da.Dispose();
                if (dataSet != null) dataSet.Dispose();
                if (rdr != null) rdr.Close();

                throw ex;
            }
            finally
            {
                if (cmd != null) cmd.Dispose();
                if (da != null) da.Dispose();
                if (dataSet != null) dataSet.Dispose();
                if (rdr != null) rdr.Close();
            }
            return list;
        }


        public void TransformSql(string sql, DocType docType, string fileName)
        {
            Transformer t = new Transformer(this);

            switch (docType)
            {
                case DocType.Csv:
                    t.Export2Csv(sql, fileName);
                    break;
                case DocType.Html:
                    t.Export2Html(sql, fileName);
                    break;
                case DocType.Excel:
                    t.Export2Excel(sql, fileName);
                    break;
                case DocType.Xml:
                    t.Export2Xml(sql, fileName);
                    break;
            }
        }
        
    }
}
