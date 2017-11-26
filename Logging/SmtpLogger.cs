
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
using System.Net;
using System.Net.Mail;

namespace Origami.Logging
{
    public class SmtpLogger : ILogger
    {
        private string host;
        private int port;
        private string mail;
        private string password;
        private string destination;
        
        public SmtpLogger()
        {

        }

        public SmtpLogger(string host, int port, string mail,
            string password, string destination)
        {
            this.host = host;
            this.port = port;
            this.mail = mail;
            this.password = password;
            this.destination = destination;

        }

        public string Host
        {
            get { return host; }
            set { host = value; }
        }

        public int Port
        {
            get { return port; }
            set { port = value; }
        }

        public string Mail
        {
            get { return mail; }
            set { mail = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public string Destination
        {
            get { return destination; }
            set { destination = value; }
        }


        private void SendMail(string subject, string body)
        {
            try
            {
                MailMessage msg = new MailMessage(mail, destination, subject, body);

                SmtpClient smtpClient = new SmtpClient(host,port);
                smtpClient.Credentials = new NetworkCredential(mail, password);
                smtpClient.Send(msg);
            }
            catch (SmtpException ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        private void WriteLog(Severity severity, string message)
        {
            string msg = DateTime.Now + " [" + severity.ToString().Substring(0,1) + "] " + message;
            SendMail(msg, msg);
        }


        private void WriteLog(string severity, string message)
        {
            string msg = DateTime.Now + " [" + severity.Substring(0,1) + "] " + message;
            SendMail(msg, msg);
        }


        public void Write(string severity, string message)
        {
            WriteLog(severity, message);
        }


        public void Write(Severity severity, string message)
        {
            WriteLog(severity.ToString(), message);
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
