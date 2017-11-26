
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
using System.Data.Common;

using Origami.Logging;

namespace Origami.Data
{
    public class Transaction : IDisposable
    {
        private DbConnection conn;
        private DbTransaction tx;
        private ILogger logger;

        public Transaction(DbConnection conn)
        {
            try
            {
                logger = LoggerFactory.CreateTraceLogger();
         
                this.conn = conn;
                tx = conn.BeginTransaction();
            
                logger.Write(Severity.Debug, "Transaction " + tx.GetHashCode() + " created");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DbConnection GetConnection()
        {
            return conn;
        }
       

        public DbTransaction GetTransaction()
        {
            return tx; 
        }

        public void Commit()
        {
            try
            {
                tx.Commit();
                logger.Write(Severity.Debug, "Transaction " + tx.GetHashCode() + " commited");
            }
            catch (Exception ex)
            {
                logger.Write(Severity.Error,ex.Message);
                throw ex;
            }
        }

        public void Rollback()
        {
            try
            {
                tx.Rollback();
                logger.Write(Severity.Debug, "Transaction " + tx.GetHashCode() + " roolback");
            }
            catch (Exception ex)
            {
                logger.Write(Severity.Error, ex.Message);
                throw ex;
            }
        }

        
        #region IDisposable Members

        public void Dispose()
        {
            if (this.tx != null)
                tx.Dispose();
            if (this.conn != null)
                conn.Dispose();
           
            logger.Write(Severity.Debug, "Transaction " + tx.GetHashCode() 
                + " and connection " + conn.GetHashCode() + " disposed");
       }

        #endregion
    }
}
