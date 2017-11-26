
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
using System.Data.Common;
using System.Data;

using Origami.Logging;

namespace Origami.Data
{
    public class DataContext : IDataContext,IDisposable
    {
        private DbConnection conn;
        private DataSource dataSource;
        private string sql;
        private string storedProcName;
        private ILogger logger;

        public delegate T DataMapperDelegate<T>(IDataReader rdr);

        #region Constructors

        
        public DataContext(DataSource dataSource)
        {
            logger = LoggerFactory.CreateTraceLogger();
            this.dataSource = dataSource;
        
            if (conn == null)
            {
                conn = ConnectionFactory.CreateConnection(dataSource);
            }
            
            logger.Write(Severity.Debug, "Connection " + conn.GetHashCode() + " opened");
        }
        

        public DbConnection Connection
        {
            get { return conn; }
        }

        public DataSource DataSource
        {
            get { return dataSource; }
            set { dataSource = value; }
        }


        #endregion

        #region Data Access Helper
        
        #region ExecuteNonQuery()

        public int ExecuteNonQuery(string sql)
        {
            DbHelper dbHelper = new DbHelper(conn);
            logger.Write(Severity.Debug, "ExecuteNonQuery() - " + sql);
            
            int result = dbHelper.ExecuteNonQuery(sql, false, null);

            return result;
        }
                

        public int ExecuteNonQuery(string sql, Transaction tx)
        {
            DbHelper dbHelper = null;
            if (tx == null)
            {
                dbHelper = new DbHelper(conn);
            }
            else
            {
                dbHelper = new DbHelper(tx.GetConnection());
            }

            logger.Write(Severity.Debug, "ExecuteNonQuery() with transaction  - " + sql);
            int result = dbHelper.ExecuteNonQuery(sql, true, tx);

            return result;
        }


        public int ExecuteNonQuery(DbCommandWrapper dbCmdWrapper)
        {

            int result = 0;
            DbHelper dbHelper = new DbHelper(conn);

            if (sql.Equals(""))
            {
                result = dbHelper.ExecuteNonQuery(dbCmdWrapper, true, storedProcName, false, null);
                logger.Write(Severity.Debug, "ExecuteNonQuery() with command wrapper - " + storedProcName);
            }
            else
            {
                result = dbHelper.ExecuteNonQuery(dbCmdWrapper, false, sql, false, null);
                logger.Write(Severity.Debug, "ExecuteNonQuery() with command wrapper - " + sql);
            }

            return result;
        }


        public int ExecuteNonQuery(DbCommandWrapper dbCmdWrapper, Transaction tx)
        {
            int result = 0;
            DbHelper dbHelper = null;

            if (tx == null)
            {
                dbHelper = new DbHelper(conn);
            }
            else
            {
                dbHelper = new DbHelper(tx.GetConnection());
            }

            if (sql.Equals(""))
            {
                result = dbHelper.ExecuteNonQuery(dbCmdWrapper, true, storedProcName, true, tx);
                logger.Write(Severity.Debug, "ExecuteNonQuery() with command wrapper and transaction - " + storedProcName);
            }
            else
            {
                result = dbHelper.ExecuteNonQuery(dbCmdWrapper, false, sql, true, tx);
                logger.Write(Severity.Debug, "ExecuteNonQuery() with command wrapper and transaction - " + sql);
        
            }
           
            return result;
        }

        #endregion


        #region ExecuteReader()

        public IDataReader ExecuteReader(string sql)
        {
            DbHelper dbHelper = new DbHelper(conn);
            IDataReader rdr = dbHelper.ExecuteReader(sql);
            logger.Write(Severity.Debug, "ExecuteReader() - " + sql);
        
            return rdr;
        }

        public IDataReader ExecuteReader(DbCommandWrapper dbCmdWrapper)
        {
            IDataReader rdr = null;
            DbHelper dbHelper = new DbHelper(conn);

            if (sql.Equals(""))
            {
                rdr = dbHelper.ExecuteReader(dbCmdWrapper, true, storedProcName);
                logger.Write(Severity.Debug, "ExecuteReader() with command wrapper - " + storedProcName);
            }
            else
            {
                rdr = dbHelper.ExecuteReader(dbCmdWrapper, false, sql);
                logger.Write(Severity.Debug, "ExecuteReader() with command wrapper - " + sql);
            }

            return rdr;
        }

        #endregion


        #region ExecuteDataSet()

        public DataSet ExecuteDataSet(string sql)
        {
            DbHelper dbHelper = new DbHelper(dataSource);
            logger.Write(Severity.Debug, "ExecuteDataSet() - " + sql);
            DataSet dataSet = dbHelper.ExecuteDataSet(sql);
                      
            return dataSet;
        }


        public DataSet ExecuteDataSet(DbCommandWrapper dbCmdWrapper)
        {
            DataSet dataSet = null;
            DbHelper dbHelper = new DbHelper(dataSource);

            if (sql.Equals(""))
            {
                dataSet = dbHelper.ExecuteDataSet(dbCmdWrapper, true, storedProcName);
                logger.Write(Severity.Debug, "ExecuteDataSet() with command wrapper - " + storedProcName);
            }
            else
            {
                dataSet = dbHelper.ExecuteDataSet(dbCmdWrapper, false, sql);
                logger.Write(Severity.Debug, "ExecuteDataSet() with command wrapper - " + sql);
            }

            return dataSet;
        }

        #endregion


        #region ExecuteScalar()

        public object ExecuteScalar(string sql)
        {
            DbHelper dbHelper = new DbHelper(conn);
            logger.Write(Severity.Debug, "ExecuteScalar() - " + sql);
            object result = dbHelper.ExecuteScalar(sql);

            return result;
        }


        public object ExecuteScalar(DbCommandWrapper dbCmdWrapper)
        {
            object result = null;
            DbHelper dbHelper = new DbHelper(conn);

            if (sql.Equals(""))
            {
                result = dbHelper.ExecuteScalar(dbCmdWrapper, true, storedProcName);
                logger.Write(Severity.Debug, "ExecuteScalar() with command wrapper - " + storedProcName);
            }
            else
            {
                result = dbHelper.ExecuteScalar(dbCmdWrapper, false, sql);
                logger.Write(Severity.Debug, "ExecuteScalar() with command wrapper - " + sql);
            }

            return result;
        }

        #endregion


        #region ExecuteObject() with delegate


        public T ExecuteObject<T>(string sql, DataMapperDelegate<T> dataMapper)
        {
            DbHelper dbHelper = new DbHelper(conn);
            logger.Write(Severity.Debug, "ExecuteObject() with delegate - " + sql);

            return dbHelper.ExecuteObject(sql, dataMapper);
        }


      
        public T ExecuteObject<T>(DbCommandWrapper dbCmdWrapper,
          DataMapperDelegate<T> dataMapper)
        {
            T obj = default(T);

            DbHelper dbHelper = new DbHelper(conn);
            if (sql.Equals(""))
            {
                obj = dbHelper.ExecuteObject<T>(dbCmdWrapper, true,
                    storedProcName, dataMapper);

                logger.Write(Severity.Debug, "ExecuteObject() with command wrapper and delegate - " + storedProcName);
            }
            else
            {
                obj = dbHelper.ExecuteObject<T>(dbCmdWrapper, false,
                    sql, dataMapper);

                logger.Write(Severity.Debug, "ExecuteObject() with command wrapper and delegate - " + sql);
            }

            return obj;
        }


        #endregion

        #region ExecuteObject() with DataMapper


        public T ExecuteObject<T>(string sql, IDataMapper<T> dataMapper)
        {
            DbHelper dbHelper = new DbHelper(conn);
            logger.Write(Severity.Debug, "ExecuteObject() - " + sql);

            return dbHelper.ExecuteObject(sql, dataMapper);
        }


        public T ExecuteObject<T>(DbCommandWrapper dbCmdWrapper,
           IDataMapper<T> dataMapper)
        {
            T obj = default(T);

            DbHelper dbHelper = new DbHelper(conn);
            if (sql.Equals(""))
            {
                obj = dbHelper.ExecuteObject<T>(dbCmdWrapper, true,
                    storedProcName, dataMapper);

                logger.Write(Severity.Debug, "ExecuteObject() with command wrapper - " + storedProcName);
            }
            else
            {
                obj = dbHelper.ExecuteObject<T>(dbCmdWrapper, false,
                    sql, dataMapper);

                logger.Write(Severity.Debug, "ExecuteObject() with command wrapper - " + sql);
            }
           
            return obj;
        }      

        
        #endregion         

        

        #region ExecuteList() with DataMapper


        public List<T> ExecuteList<T>(string sql, IDataMapper<T> dataMapper)
        {
            DbHelper dbHelper = new DbHelper(conn);
            logger.Write(Severity.Debug, "ExecuteList() - " + sql);
            List<T> list = dbHelper.ExecuteList<T>(sql, dataMapper);

            return list;
        }

        public List<T> ExecuteList<T>(DbCommandWrapper dbCmdWrapper,
           IDataMapper<T> dataMapper)
        {
            List<T> list = null;

            DbHelper dbHelper = new DbHelper(conn);
            if (sql.Equals(""))
            {
                list = dbHelper.ExecuteList<T>(dbCmdWrapper, true,
                    storedProcName, dataMapper);

                logger.Write(Severity.Debug, "ExecuteList() with command wrapper - " + storedProcName);
            }
            else
            {
                list = dbHelper.ExecuteList<T>(dbCmdWrapper, false,
                    sql, dataMapper);

                logger.Write(Severity.Debug, "ExecuteList() with command wrapper - " + sql);
            }

            return list;
        }

        #endregion

        #region ExecuteList() with delegate


        public List<T> ExecuteList<T>(string sql, DataMapperDelegate<T> dataMapper)
        {
            DbHelper dbHelper = new DbHelper(conn);
            logger.Write(Severity.Debug, "ExecuteList() with delegate - " + sql);
            List<T> list = dbHelper.ExecuteList<T>(sql, dataMapper);
           
            return list;
        }

        public List<T> ExecuteList<T>(DbCommandWrapper dbCmdWrapper,
             DataMapperDelegate<T> dataMapper)
        {
            List<T> list = null;

            DbHelper dbHelper = new DbHelper(conn);
            if (sql.Equals(""))
            {
                list = dbHelper.ExecuteList<T>(dbCmdWrapper, true,
                    storedProcName, dataMapper);

                logger.Write(Severity.Debug, "ExecuteList() with command wrapper and delegate - " + storedProcName);
            }
            else
            {
                list = dbHelper.ExecuteList<T>(dbCmdWrapper, false,
                    sql, dataMapper);

                logger.Write(Severity.Debug, "ExecuteList() with command wrapper and delegate - " + sql);
            }

            return list;
        }

        #endregion                          


        #region ExecuteList() with paging

        public List<T> ExecuteList<T>(DbCommandWrapper dbCommandWrapper,
            IDataMapper<T> dataMapper, int index, int size)
        {
            List<T> list = null;

            DbHelper dbHelper = new DbHelper(dataSource);
            if (sql.Equals(""))
            {
                list = dbHelper.ExecuteList<T>(dbCommandWrapper, true,
                    storedProcName, dataMapper, index, size);

                logger.Write(Severity.Debug, "ExecuteList() with command wrapper"
                    + " and paging - " + storedProcName);
            }
            else
            {
                list = dbHelper.ExecuteList<T>(dbCommandWrapper, false,
                    sql, dataMapper, index, size);

                logger.Write(Severity.Debug, "ExecuteList() with command wrapper"
                    + " and paging- " + sql);
            }

            return list;
        }


        public List<T> ExecuteList<T>(string sql, IDataMapper<T> dataMapper,
         int index, int size)
        {
            DbHelper dbHelper = new DbHelper(dataSource);
            logger.Write(Severity.Debug, "ExecuteList() with paging - " + sql);
            List<T> list = dbHelper.ExecuteList<T>(sql, dataMapper, index, size);

            return list;
        }

        # endregion


        public DbCommandWrapper CreateCommand(CommandWrapperType cmdWrapperType, string cmdText)
        {
            DbCommandWrapper dbCmdWrapper = new DbCommandWrapper();
            switch (cmdWrapperType)
            {
                case CommandWrapperType.Text:
                    this.sql = cmdText;
                    this.storedProcName = string.Empty;
                    break;

                case CommandWrapperType.StoredProcedure:
                    this.storedProcName = cmdText;
                    this.sql = "";
                    break;
            }

            return dbCmdWrapper;

        }


        public DbCommandWrapper CreateCommand()
        {
            DbCommandWrapper dbCmdWrapper = new DbCommandWrapper();
            CommandWrapperType cmdWrapperType = dbCmdWrapper.CommandType;

            switch (cmdWrapperType)
            {
                case CommandWrapperType.Text:
                    this.sql = dbCmdWrapper.CommandText;
                    this.storedProcName = string.Empty;
                    break;

                case CommandWrapperType.StoredProcedure:
                    this.storedProcName = dbCmdWrapper.CommandText;
                    this.sql = string.Empty;
                    break;
            }

            return dbCmdWrapper;

        }


        public void Transform(string sql, DocType docType, string fileName)
        {
            DbHelper dbHelper = new DbHelper(conn);
            logger.Write(Severity.Debug, "Transform() - " + sql);
      
            dbHelper.TransformSql(sql, docType, fileName);
        }

        public Transaction BeginTransaction()
        {
            if (conn == null || conn.State == ConnectionState.Closed)
            {
                conn = ConnectionFactory.CreateConnection(dataSource);
            }
     
            logger.Write(Severity.Debug, "Connection for transaction " + conn.GetHashCode() + " opened");
     
            return new Transaction(conn);
        }


        #endregion
        

        #region IDisposable Members

        public void Dispose()
        {
            if (conn != null)
            {
                conn.Dispose();
            }
            logger.Write(Severity.Debug, "Connection " + conn.GetHashCode() + " disposed");
     
       }

        #endregion
    }
}
