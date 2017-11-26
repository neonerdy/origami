
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

namespace Origami.Logging
{
    public class ConsoleLogger : ILogger
    {
        public ConsoleLogger()
        {

        }


        public void Write(string severity, string message)
        {
            Console.WriteLine(DateTime.Now + " [" + severity.Substring(0,1) + "] " + message);
        }
        
        public void Write(Severity severity, string message)
        {
            Console.WriteLine(DateTime.Now + " [" + severity.ToString().Substring(0,1) + "] " + message);
        }

        public void Warn(string message)
        {
            Console.WriteLine(DateTime.Now + " [W] " + message);
        }

        public void Info(string message)
        {
            Console.WriteLine(DateTime.Now + " [I] " + message);
        }

        public void Error(string message)
        {
            Console.WriteLine(DateTime.Now + " [E] " + message);
        }

        public void Fatal(string message)
        {
            Console.WriteLine(DateTime.Now + " [F] " + message);
        }

        public void Debug(string message)
        {
            Console.WriteLine(DateTime.Now + " [D] " + message);
        }


    }
}
