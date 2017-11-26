
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
using System.Diagnostics;

namespace Origami.Logging
{
    public class TraceLogger : ILogger
    {
        public void Write(string severity, string message)
        {
            Trace.WriteLine(message, DateTime.Now + " [" + severity.Substring(0, 1) + "] ");
        }
        
        public void Write(Severity severity, string message)
        {
            Trace.WriteLine(message, DateTime.Now + " [" +  severity.ToString().Substring(0, 1) + "] ");
        }

        public void Warn(string message)
        {
            Trace.WriteLine(message, DateTime.Now + " [W] ");
        }

        public void Info(string message)
        {
            Trace.WriteLine(message, DateTime.Now + " [I] ");
        }

        public void Error(string message)
        {
            Trace.WriteLine(message, DateTime.Now + " [E] ");
        }

        public void Fatal(string message)
        {
            Trace.WriteLine(message, DateTime.Now + " [F] ");
        }

        public void Debug(string message)
        {
            Trace.WriteLine(message, DateTime.Now + " [D] ");
        }
    }
}
