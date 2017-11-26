
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
    public class ParameterClause
    {
        private ParameterDirection direction;
        private DbType dbType;
        private string name;
        private object argument;
        
        public ParameterClause(string name, DbType dbType,object argument)
        {
            this.name = name;
            this.argument = argument;
            this.dbType = dbType;
        }


        public ParameterClause(ParameterDirection direction,DbType dbType,
            string name, object argument)
        {
            this.direction = direction;
            this.dbType = dbType;
            this.name = name;
            this.argument = argument;
        }

        public ParameterDirection Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        public DbType DbType
        {
            get { return dbType; }
            set { dbType = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public object Argument
        {
            get { return argument; }
            set { argument = value; }
        }
    }

    public enum CommandWrapperType
    {
        Text, StoredProcedure
    }

    public class DbCommandWrapper
    {
        private List<ParameterClause> parameters = new List<ParameterClause>();
        private string commandText;
        private CommandWrapperType commandType;
        private DbCommand command;
             
        public List<ParameterClause> Parameters
        {
            get { return parameters; }
        }
        
        public DbCommand Command
        {
            get { return command; }
            set { command = value; }
        }
        
        public CommandWrapperType CommandType
        {
            get { return commandType; }
            set { commandType = value; }
        }

        public string CommandText
        {
            get { return commandText; }
            set { commandText = value; }
        }

        public object this[string parameterName]
        {
            get
            {
                if (command == null)
                {
                    return null;
                }
                else
                {
                    return command.Parameters[parameterName].Value;
                }
            }
        }

                
        public void SetParameter(string param, DbType dbType,object value)
        {
            ParameterClause parameter = new ParameterClause(param, dbType,value);
            parameters.Add(parameter);
        }


        public void SetParameter(ParameterDirection direction,DbType dbType,
            string param,object value)
        {
            ParameterClause parameter = new ParameterClause(direction,dbType,param, value);
            parameters.Add(parameter);       
        }
    }
}
