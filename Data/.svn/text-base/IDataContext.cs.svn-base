
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

namespace Origami.Data
{
    public interface IDataContext : IDisposable
    {
        DbConnection Connection { get; }
        DataSource DataSource { get; }

        int ExecuteNonQuery(string sql);
        int ExecuteNonQuery(string sql, Transaction tx);
        int ExecuteNonQuery(DbCommandWrapper dbCmdWrapper);
        int ExecuteNonQuery(DbCommandWrapper dbCmdWrapper, Transaction tx);
        
        IDataReader ExecuteReader(string sql);
        IDataReader ExecuteReader(DbCommandWrapper dbCmdWrapper);        
        
        DataSet ExecuteDataSet(string sql);
        DataSet ExecuteDataSet(DbCommandWrapper dbCmdWrapper);
        
        object ExecuteScalar(string sql);
        object ExecuteScalar(DbCommandWrapper dbCmdWrapper);
        
        T ExecuteObject<T>(string sql, IDataMapper<T> dataMapper);
        T ExecuteObject<T>(DbCommandWrapper dbCmdWrapper, IDataMapper<T> dataMapper);
        T ExecuteObject<T>(string sql, Origami.Data.DataContext.DataMapperDelegate<T> dataMapper);
        
        List<T> ExecuteList<T>(string sql, IDataMapper<T> dataMapper);
        List<T> ExecuteList<T>(DbCommandWrapper dbCmdWrapper, IDataMapper<T> dataMapper);
        List<T> ExecuteList<T>(string sql, Origami.Data.DataContext.DataMapperDelegate<T> dataMapper);
        List<T> ExecuteList<T>(DbCommandWrapper dbCmdWrapper, 
            Origami.Data.DataContext.DataMapperDelegate<T> dataMapper);        
        List<T> ExecuteList<T>(string sql, IDataMapper<T> dataMapper, int index, int size);
        List<T> ExecuteList<T>(DbCommandWrapper dbCommandWrapper,
            IDataMapper<T> dataMapper, int index, int size);

        DbCommandWrapper CreateCommand();
        DbCommandWrapper CreateCommand(CommandWrapperType cmdWrapperType, string cmdText);
        Transaction BeginTransaction();
    }
}
