
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
using Origami.Security.Cryptography;

namespace Origami.Data
{
    public class ConnectionFactory
    {
        private static DbConnection conn;

        public static DbConnection CreateConnection(DataSource dataSource)
        {
            try
            {
                DbProviderFactory factory = DbProviderFactories.GetFactory(dataSource.Provider);
                conn = factory.CreateConnection();
                conn.ConnectionString = dataSource.ConnectionString;
                conn.Open();
            }
           catch(DbException ex)
            {
                throw ex;
            }
            return conn;

        }


    }
}
