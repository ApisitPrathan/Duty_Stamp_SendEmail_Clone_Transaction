using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Duty_Stamp_SendEmail_Clone_Transaction.Model;

namespace Duty_Stamp_SendEmail_Clone_Transaction.Utility
{
    public class ConnectionService
    {
        private static string HostMail = ConfigurationManager.AppSettings["HostMail"];
        private static string HostEMailFromAddress = ConfigurationManager.AppSettings["HostEMailFromAddress"];
        private static string HostEMailTo = ConfigurationManager.AppSettings["HostEMailFromTo"];
        private static string HostEMailCC = ConfigurationManager.AppSettings["HostEMailCC"];
        private static string Port = ConfigurationManager.AppSettings["Port"];
        private static string SentToUser = ConfigurationManager.AppSettings["SentToUser"];
        private static string Link = ConfigurationManager.AppSettings["Link"];
        public ServiceModel ConnectionServices()
        {
            ServiceModel _s = new ServiceModel();
            _s.Email_HostName = HostMail;
            _s.Email_FromAddress = HostEMailFromAddress;
            _s.Email_ToAddress = HostEMailTo;
            _s.Email_CC = HostEMailCC;
            _s.Email_Port = Port;
            _s.Link = Link;
            _s.SentToUsers = Convert.ToBoolean(SentToUser);
            return _s;
        }
    }
}
