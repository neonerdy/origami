
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
using System.Linq;
using System.Text;

namespace Origami.Security
{
    public interface IAuthenticationProvider
    {
        bool Authenticate(string userName, string password);
        bool IsUserInRole(string userName,string roleName);
        bool IsUserExist(string userName);
        string[] GetAllUsers();
        string[] GetRolesForUser(string userName);
   }
}
