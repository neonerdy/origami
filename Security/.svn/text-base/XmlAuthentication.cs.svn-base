
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
 * © 2008 XERIS System Interface
 * 
 * Under Apache Licence
 * 
 *==========================================================
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Origami.Security
{
    public class XmlAuthentication : IAuthenticationProvider
    {

        private string userConfig;
        private XDocument userDoc;
     
        public XmlAuthentication()
        {

        }

        public XmlAuthentication(string xmlConfig)
        {
            userDoc = XDocument.Load(xmlConfig);
        }

        public string UserConfig
        {
            get { return userConfig; }
            set 
            {
                userConfig = value;
                userDoc = XDocument.Load(userConfig);
            }
        }

        public bool Authenticate(string userName, string password)
        {
            bool found = false;
            string xmlFile = string.Empty;
            
            try
            {     var users = from u in userDoc.Elements("users").Elements("user")
                        where u.Attribute("name").Value.Equals(userName)
                        && u.Attribute("password").Value.Equals(password)
                        select u;

                if (users.Count() > 0)
                {
                    found = true;
                }
                else
                {
                    found = false;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return found;
        }


        public bool IsUserInRole(string userName,string roleName)
        {
            var user = from u in userDoc.Elements("users").Elements("user")
                        where u.Attribute("name").Value == userName
                        select u;

            var userInRole = from u in user.Elements("roles").Elements("role")
                             where u.Attribute("name").Value == roleName
                             select u;
            bool found=userInRole.Count() > 0;
            
            return found;

        }


        public bool IsUserExist(string userName)
        {
            var user = from u in userDoc.Elements("users").Elements("user")
                       where u.Attribute("name").Value == userName
                       select u;
            
            return user.Count() > 0;
        }


        public string[] GetRolesForUser(string userName)
        {
            var user = from u in userDoc.Elements("users").Elements("user")
                       where u.Attribute("name").Value == userName
                       select u;

            var rolesInUser = from r in user.Elements("role")
                        select r;

            List<string> roleList = new List<string>();
            foreach (var r in rolesInUser)
            {
                roleList.Add(r.Attribute("name").Value);
            }

            return roleList.ToArray();        
        }


        public string[] GetAllUsers()
        {
            var users = from u in userDoc.Elements("users").Elements("user")
                        select u;

            List<string> userList = new List<string>();
            foreach (var u in users)
            {
                userList.Add(u.Attribute("name").Value);
            }

            return userList.ToArray();
        
        }


   
    }
}
