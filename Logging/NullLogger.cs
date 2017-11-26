
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

namespace Origami.Logging
{
    public class NullLogger : ILogger
    {

        #region ILogger Members

        public void Write(Severity severity, string message)
        {
        }

        public void Write(string severity, string message)
        {
        }

        public void Warn(string message)
        {
        }

        public void Info(string message)
        {
        }

        public void Error(string message)
        {
        }

        public void Fatal(string message)
        {
        }

        public void Debug(string message)
        {
        }

        #endregion
    }
}
