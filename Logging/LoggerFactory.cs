
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


namespace Origami.Logging
{
    public class LoggerFactory
    {
        public static ILogger CreateConsoleLogger()
        {
            return new ConsoleLogger();
        }

        public static ILogger CreateFileLogger(string fileName)
        {
            return new FileLogger(fileName);
        }

        public static ILogger CreateDbLogger(DataSource dataSource)
        {
            return new DbLogger(dataSource);
        }

        public static ILogger CreateEventLogger(string eventSource)
        {
            return new EventLogger(eventSource);
        }

        public static ILogger CreateTraceLogger()
        {
            return new TraceLogger();
        }

        public static ILogger CreateSmtpLogger(string host, int port, string mail,
            string password, string destination)
        {
            return new SmtpLogger(host,port,mail,password,destination);
        }

    }
}
