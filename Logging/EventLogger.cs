
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
    public class EventLogger : ILogger
    {
        private EventLog eventLog;
        private string eventSource;

        public EventLogger()
        {

        }

        public EventLogger(string eventSource)
        {
            this.eventSource = eventSource;
        }

        public string EventSource
        {
            get { return eventSource; }
            set { eventSource = value; }
        }
        
        
        private void WriteLog(string message, EventLogEntryType severity)
        {
            eventLog = new EventLog();
            eventLog.Source = eventSource;
            eventLog.WriteEntry(message, severity);
            eventLog.Close();
        }

        public void Write(string severity, string message)
        {
        
        }

        public void Write(Severity severity, string message)
        {
            EventLogEntryType eventLogEntryType = EventLogEntryType.Information;

            switch (eventLogEntryType)
            {
                case EventLogEntryType.Information :
                    severity = Severity.Info;
                    break;
                case EventLogEntryType.Error:
                    severity = Severity.Error;
                    break;
                case EventLogEntryType.Warning :
                    severity = Severity.Warn;
                    break;
                default :
                    severity = Severity.Info;
                    break;
            }

            WriteLog(message, EventLogEntryType.Information);
        }


        public void Warn(string message)
        {
            WriteLog(message, EventLogEntryType.Warning);
        }

        public void Info(string message)
        {
            WriteLog(message, EventLogEntryType.Information);
        }

        public void Error(string message)
        {
            WriteLog(message, EventLogEntryType.Error);
        }

        public void Fatal(string message)
        {
            WriteLog(message, EventLogEntryType.Error);
        }

        public void Debug(string message)
        {
            WriteLog(message, EventLogEntryType.Information);
        }



    }
}
