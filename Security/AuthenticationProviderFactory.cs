
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

namespace Origami.Security
{
    public class AuthenticationProviderFactory
    {

        public static IAuthenticationProvider CreateDbAuthentication(DataSource dataSource)
        {
            return new DbAuthentication(dataSource);
        }

        public static IAuthenticationProvider CreateXmlAuthentication(string xmlConfig)
        {
            return new XmlAuthentication(xmlConfig);
        }

        public static IAuthenticationProvider CreateLdapAuthentication(string LdapServer)
        {
            return null;
            //return new LdapAuthentication(LdapServer);
        }
    }
}
