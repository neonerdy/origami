
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
    public interface ILogger
    {        
        void Write(Severity severity, string message);
        void Write(string severity, string message);
        void Warn(string message);
        void Info(string message);
        void Error(string message);
        void Fatal(string message);
        void Debug(string message);
    }
}
