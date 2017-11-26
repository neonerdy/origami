
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
using System.Data;
using Origami.Data;
using System.Collections.Generic;

namespace Origami.Security
{
    public class DbAuthentication : IAuthenticationProvider
    {
        private DataSource dataSource;
        private IDataContext dx;
      
        public DbAuthentication()
        {

        }

        public DbAuthentication(DataSource dataSource)
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
        
        public bool Authenticate(string userName, string password)
        {
            bool found = false;
        
            Query q=new Query().Select("UserName,Password").From("Users")
                .Where("UserName").Equal(userName).And("Password").Equal(password);
            
            IDataReader rdr = dx.ExecuteReader(q.GetSql());

            if (rdr.Read())
            {
                found = true;
            }
            else
            {
                found = false;
            }

            return found;
        }
        
        
        public bool IsUserInRole(string roleName,string userName)
        {
            bool found = false;

            Query q = new Query().From("UserInRoles")
                .InnerJoin("Users", "UserId", "UserId")
                .InnerJoin("Roles", "RoleId", "RoleId")
                .Where("RoleName").Equal(roleName)
                .And("UserName").Equal(userName);
                

            IDataReader rdr = dx.ExecuteReader(q.GetSql());
            
            if (rdr.Read())
            {
                found = true;
            }

            return found;
        }       


        public bool IsUserExist(string userName)
        {
            bool found = false;

            Query q = new Query().From("Users").Where("UserName").Equal(userName);
            IDataReader rdr=dx.ExecuteReader(q.GetSql());
            if (rdr.Read())
            {
                found = true;      
            }

            return found;
        }


        public string[] GetRolesForUser(string userName)
        {
            Query q = new Query().From("UserInRoles")
                .InnerJoin("Users", "UserId", "UserId")
                .InnerJoin("Roles", "RoleId", "RoleId")
                .Where("UserName").Equal(userName);

            IDataReader rdr = dx.ExecuteReader(q.GetSql());

            List<string> roles = new List<string>();
            while (rdr.Read())
            {
                roles.Add(rdr["RoleName"].ToString());
            }

            return roles.ToArray();

        }

      

        public string[] GetAllUsers()
        {
            Query q = new Query().From("Users");
            IDataReader rdr=dx.ExecuteReader(q.GetSql());

            List<string> users = new List<string>();

            while (rdr.Read())
            {
               users.Add(rdr["UserName"].ToString());
            }

            return users.ToArray();
        }

   
    }

}
