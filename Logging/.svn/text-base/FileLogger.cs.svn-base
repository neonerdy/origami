
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
using System.IO;

namespace Origami.Logging
{
    public class FileLogger : ILogger
    {
        private StreamWriter file;
        private string fileName;

        public FileLogger()
        {
        }

        public FileLogger(string fileName)
        {
            this.fileName = fileName;
        }

        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }
        
        private void WriteLog(string severity, string message)
        {
            file = new StreamWriter(fileName, true);
            file.WriteLine(DateTime.Now + " [" + severity.Substring(0,1) + "] " + message);
            file.Close();
        }

        private void WriteLog(Severity severity, string message)
        {
            file = new StreamWriter(fileName, true);
            file.WriteLine(DateTime.Now + " [" + severity.ToString().Substring(0,1) + "] " + message);
            file.Close();
        }


        public void Write(string severity, string message)
        {
            WriteLog(severity, message);
        }
       
        public void Write(Severity severity, string message)
        {
            WriteLog(severity, message);
        }

        public void Warn(string message)
        {
            WriteLog("W", message);
        }

        public void Info(string message)
        {
            WriteLog("I", message);
        }

        public void Error(string message)
        {
            WriteLog("E", message);
        }

        public void Fatal(string message)
        {
            WriteLog("F", message);
        }

        public void Debug(string message)
        {
            WriteLog("D", message);
        }
    }
}
