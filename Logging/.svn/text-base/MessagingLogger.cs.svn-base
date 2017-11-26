
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
using System.Messaging;

namespace Origami.Logging
{
    public class MessagingLogger : ILogger
    {
        private string path;

        public MessagingLogger(string path)
        {
            this.path = path;
        }

        public MessagingLogger()
        {

        }

        public string Path
        {
            get { return path; }
            set { path = value; }
        }

        private void WriteLog(string severity, string message)
        {
            MessageQueue mq = null;

            if (MessageQueue.Exists(path))
            {
                mq = new MessageQueue(path);
            }
            else
            {
                mq = MessageQueue.Create(path);
            }

            Message m = new Message(DateTime.Now + " [" + severity.Substring(0, 1) + "] " + message);
            mq.Send(m);
            mq.Close();

        }

        private void WriteLog(Severity severity, string message)
        {
            MessageQueue mq = null;

            if (MessageQueue.Exists(path))
            {
                mq = new MessageQueue(path);
            }
            else
            {
                mq = MessageQueue.Create(path);
            }

            Message m = new Message(DateTime.Now + " [" + severity.ToString().Substring(0, 1) + "] " + message);
            mq.Send(m);
            mq.Close();
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
