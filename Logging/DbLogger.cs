
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
using Origami.Data;

namespace Origami.Logging
{
    public class DbLogger : ILogger
    {
        private string tableName = "Logs";
        private IDataContext dx;
        private DataSource dataSource;
        
        public DbLogger()
        {
        
        }
        public DbLogger(DataSource dataSource)
        {
            dx = DataContextFactory.CreateInstance(dataSource);
        }

        public DataSource DataSource
        {
            get { return dataSource; }
            set
            {
                dataSource = value;
                if (dataSource != null)
                {
                    dx = DataContextFactory.CreateInstance(dataSource);
                }
            }
        }
        

        private void WriteLog(Severity severity, string message)
        {
            string[] fields = { "LogId", "Date", "Severity", "Message" };
            object[] values = { Guid.NewGuid().ToString(), DateTime.Now, severity.ToString().Substring(0,1), message };

            Query q = new Query().Select(fields).From(tableName).Insert(values);

            dx.ExecuteNonQuery(q.GetSql());
        }

        private void WriteLog(string severity, string message)
        {
            string[] fields = { "LogId", "Date", "Severity", "Message" };
            object[] values = { Guid.NewGuid(), DateTime.Now, severity, message };

            Query q = new Query().Select(fields).From(tableName).Insert(values);

            dx.ExecuteNonQuery(q.GetSql());
        }
       

        public void Write(string severity, string message)
        {
            WriteLog(severity, message);
        }

        public void Write(Severity severity, string message)
        {
            WriteLog(severity.ToString().Substring(0,1), message);
        }

        public void Warn(string message)
        {
            WriteLog("W", message);
        }

        public void Info(string message)
        {
            WriteLog("I", message);
        }

        public void Error(string message)
        {
            WriteLog("E", message);
        }

        public void Fatal(string message)
        {
            WriteLog("F", message);
        }

        public void Debug(string message)
        {
            WriteLog("D", message);
        }

    }
}
