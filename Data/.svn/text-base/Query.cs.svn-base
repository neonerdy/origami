
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
using System.Text;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;

using Origami.Logging;

namespace Origami.Data
{
    public enum OrderType
    {
        Ascending,
        Descending
    }

    public class Query
    {
        private StringBuilder q = new StringBuilder();
        private string tableName;
        private string columns;
        private int topRecords;
        private string[] fields;
        private ILogger logger;

        private DataSource dataSource;

        public Query()
        {
            logger = LoggerFactory.CreateTraceLogger();
            q.Append("SELECT");
        }

        public Query(DataSource dataSource)
        {
            logger = LoggerFactory.CreateTraceLogger();
            q.Append("SELECT");
            this.dataSource = dataSource;
        }        


        public Query Select(string columns)
        {
            this.columns = columns;
            q.Append(" " + columns);
            return this;
        }

        public Query Select(string[] fields)
        {
            this.fields = fields;
            return this;
        }

        public Query Select(int topRecords)
        {
            this.topRecords = topRecords;
            q.Append(" TOP(" + topRecords + ")");
            return this;
        }
       
        public Query From(string tableName)
        {
            if (fields == null)
            {
                if (columns != null)
                {
                    q.Append(" FROM " + tableName);
                }
                else
                {
                    q.Append(" * FROM " + tableName);
                }
            }

            this.tableName = tableName;
            return this;
        }

        public Query GroupBy(string columnName)
        {
            q.Append(" GROUP BY " + columnName);
            return this;
        }

        public Query Having(string columnName)
        {
            q.Append(" HAVING " + columnName);
            return this;
        }


        public Query Where(string columnName)
        {
            q.Append(" WHERE " + columnName);
            return this;
        }

        public Query OrderBy(string columnName, OrderType orderType)
        {
            if (orderType == OrderType.Ascending)
            {
                q.Append(" ORDER BY " + columnName + " ASC");
            }
            else if (orderType == OrderType.Descending)
            {
                q.Append(" ORDER BY " + columnName + " DESC");
            }

            return this;
        }

        public Query And(string columnName)
        {
            q.Append(" AND " + columnName);
            return this;
        }

        public Query Or(string columnName)
        {
            q.Append(" OR " + columnName);
            return this;
        }


        public Query Equal(object value)
        {
            if (value.GetType() == typeof(string) || value.GetType() ==typeof(Guid))
            {
                q.Append(" = '" + value + "'");
            }
            else
            {
                q.Append(" = " + value);
            }
            return this;
        }


        public Query NotEqual(object value)
        {
            if (value.GetType() == typeof(string))
            {
                q.Append(" <> '" + value + "'");
            }
            else
            {
                q.Append(" <> " + value);
            }
            return this;
        }

        public Query Like(string value)
        {
            q.Append(" LIKE " + "'" + value + "'");
            return this;
        }

        public Query NotLike(string value)
        {
            q.Append(" NOT LIKE '" + value + "'");
            return this;
        }

        public Query LessThan(object value)
        {
            q.Append(" < " + value);
            return this;
        }

        public Query LessEqualThan(object value)
        {
            q.Append(" <= " + value);
            return this;
        }


        public Query GreaterThan(object value)
        {
            q.Append(" > " + value);
            return this;
        }

        public Query GreaterEqualThan(object value)
        {
            q.Append(" >= " + value);
            return this;
        }

        public Query Between(object from, object to)
        {
            if (from.GetType() == typeof(string))
            {
                q.Append(" BETWEEN '" + from + "' AND '" + to + "'");
            }
            else
            {
                q.Append(" BETWEEN " + from + " AND " + to);
            }
            return this;
        }

        public Query NotBetween(object from, object to)
        {
            if (from.GetType() == typeof(string))
            {
                q.Append(" NOT BETWEEN '" + from + "' AND '" + to + "'");
            }
            else
            {
                q.Append(" NOT BETWEEN " + from + " AND " + to);
            }
            return this;
        }

        public Query In(object[] values)
        {
            string s = "";
            string str = "";

            for (int i = 0; i <= values.Length - 1; i++)
            {
                if (values.GetType() == typeof(string[]))
                {
                    s = s + "'" + values[i] + "',";
                    str = s.Substring(0, s.Length - 1);
                }
                else
                {
                    s = s + values[i] + ",";
                    str = s.Substring(0, s.Length - 1);
                }
            }

            q.Append(" IN (" + str + ")");

            return this;

        }


        public Query NotIn(object[] values)
        {
            string s = "";
            string str = "";

            for (int i = 0; i <= values.Length - 1; i++)
            {
                if (values.GetType() == typeof(string[]))
                {
                    s = s + "'" + values[i] + "',";
                    str = s.Substring(0, s.Length - 1);
                }
                else
                {
                    s = s + values[i] + ",";
                    str = s.Substring(0, s.Length - 1);
                }
            }

            q.Append(" NOT IN (" + str + ")");

            return this;

        }

        public Query InnerJoin(string joinTable, string foreignKey,
            string joinTableKey)
        {
            q.Append(" INNER JOIN " + joinTable + " ON " + this.tableName + "."
                + foreignKey + " = " + joinTable + "." + joinTableKey);
            return this;
        }

        public Query OuterJoin(string joinTable, string foreignKey,
            string joinTableKey)
        {
            q.Append(" OUTER JOIN " + joinTable + " ON " + this.tableName + "."
                + foreignKey + " = " + joinTable + "." + joinTableKey);
            return this;
        }


        public Query RightJoin(string joinTable, string foreignKey,
            string joinTableKey)
        {
            q.Append(" RIGHT JOIN " + joinTable + " ON " + this.tableName + "."
                + foreignKey + " = " + joinTable + "." + joinTableKey);
            return this;
        }


        public Query LeftJoin(string joinTable, string foreignKey,
            string joinTableKey)
        {
            q.Append(" LEFT JOIN " + joinTable + " ON " + this.tableName + "."
                + foreignKey + " = " + joinTable + "." + joinTableKey);
            return this;
        }


        public Query Insert(object[] values)
        {
            string sql = "INSERT INTO " + this.tableName + " (";

            for (int i = 0; i <= this.fields.Length - 1; i++)
            {
                if (i == this.fields.Length - 1)
                {
                    sql = sql + this.fields[i] + ")";
                }
                else
                {
                    sql = sql + this.fields[i] + ",";
                }
            }

            sql = sql + " VALUES (";

            for (int i = 0; i <= values.Length - 1; i++)
            {
                if (i == values.Length - 1)
                {
                    if (values[i].GetType() == typeof(string) || values[i].GetType() == typeof(char) ||
                     values[i].GetType() == typeof(DateTime) || values[i].GetType() == typeof(Guid) ||
                     values[i].GetType()==typeof(Boolean))
                    {
                        sql = sql + "'" + values[i] + "')";
                    }
                    else
                    {
                        sql = sql + values[i] + ")";
                    }
                }
                else
                {
                    if (values[i].GetType() == typeof(string) || values[i].GetType() == typeof(char) ||
                     values[i].GetType() == typeof(DateTime) || values[i].GetType() == typeof(Guid) ||
                     values[i].GetType()==typeof(Boolean))             
                    {
                        sql = sql + "'" + values[i] + "',";
                    }
                    else
                    {
                        sql = sql + values[i] + ",";
                    }
                }
            }

            q.Remove(0, 6);
            q.Append(sql);

            return this;
        }


        public Query Update(object[] values)
        {
            string sql = "UPDATE " + tableName + " SET ";

            for (int i = 0; i <= this.fields.Length - 1; i++)
            {
                if (i == fields.Length - 1)
                {
                    if (values[i].GetType() == typeof(string) || values[i].GetType() == typeof(char) ||
                        values[i].GetType() == typeof(DateTime) || values[i].GetType() == typeof(Guid) ||
                        values[i].GetType() == typeof(Boolean))      
                  
                    {
                        sql = sql + this.fields[i] + "='" + values[i] + "'";
                    }
                    else
                    {
                        sql = sql + this.fields[i] + "=" + values[i] + "";
                    }
                }
                else
                {
                    if (values[i].GetType() == typeof(string) || values[i].GetType() == typeof(char) ||
                       values[i].GetType() == typeof(DateTime) || values[i].GetType() == typeof(Guid) ||
                       values[i].GetType() == typeof(Boolean))  
                    {
                        sql = sql + this.fields[i] + "='" + values[i] + "',";
                    }
                    else
                    {
                        sql = sql + this.fields[i] + "=" + values[i] + ",";
                    }
                }
            }

            q.Remove(0, 6);
            q.Append(sql);

            return this;
        }

        public Query Delete()
        {
            string sql = "DELETE FROM " + this.tableName;
            q.Remove(0, 14+ this.tableName.Length);
            q.Append(sql);

            return this;
        }

 

        public IDataReader ExecuteReader()
        {
            string sql = q.ToString();

            DbHelper dbHelper = new DbHelper(dataSource);
            logger.Write(Severity.Debug, "ExecuteReader() - " + sql);
        
            return dbHelper.ExecuteReader(sql);
        }



        public int ExecuteNonQuery()
        {
            string sql = q.ToString();

            DbHelper dbHelper = new DbHelper(dataSource);
            logger.Write(Severity.Debug, "ExecuteNonQuery() - " + sql);
        
            return dbHelper.ExecuteNonQuery(sql, false, null);
        }



        public DataSet ExecuteDataSet()
        {
            string sql = q.ToString();

            DbHelper dbHelper = new DbHelper(dataSource);
            logger.Write(Severity.Debug, "ExecuteDataSet() - " + sql);
          
            return dbHelper.ExecuteDataSet(q.ToString());
        }


        public T ExecuteObject<T>(IDataMapper<T> dataMapper)
        {
            string sql = q.ToString();

            DbHelper dbHelper = new DbHelper(dataSource);
            logger.Write(Severity.Debug, "ExecuteObject() - " + sql);
            
            return dbHelper.ExecuteObject<T>(q.ToString(), dataMapper);
        }


        public T ExecuteObject<T>(string sql, Origami.Data.DataContext.DataMapperDelegate<T> dataMapper)
        {
            DbHelper dbHelper = new DbHelper(dataSource);
            logger.Write(Severity.Debug, "ExecuteObject() with delegate - " + sql);

            return dbHelper.ExecuteObject(sql, dataMapper);
        }
                

        public List<T> ExecuteList<T>(IDataMapper<T> dataMapper)
        {
            string sql = q.ToString();

            DbHelper dbHelper = new DbHelper(dataSource);
            logger.Write(Severity.Debug, "ExecuteList() - " + sql);
            
            return dbHelper.ExecuteList<T>(q.ToString(), dataMapper);
        }


        public List<T> ExecuteList<T>(string sql, Origami.Data.DataContext.DataMapperDelegate<T> dataMapper)
        {
            DbHelper dbHelper = new DbHelper(dataSource);
            logger.Write(Severity.Debug, "ExecuteList() with delegate - " + sql);
            List<T> list = dbHelper.ExecuteList<T>(sql, dataMapper);

            return list;
        }


        public object ExecuteScalar()
        {
            string sql = q.ToString();

            DbHelper dbHelper = new DbHelper(dataSource);
            logger.Write(Severity.Debug, "ExecuteScalar() - " + sql);
          
            return dbHelper.ExecuteScalar(q.ToString());
        }


        public void Transform(DocType docType, string fileName)
        {
            string sql = q.ToString();
                       
            DbHelper dbHelper = new DbHelper(dataSource);
            logger.Write(Severity.Debug, "Transform() - " + sql);
          
            dbHelper.TransformSql(sql, docType, fileName);
        
        }


        public string GetSql()
        {
            return q.ToString();
        }



    }
}
