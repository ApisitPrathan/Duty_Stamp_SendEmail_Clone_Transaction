using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Duty_Stamp_SendEmail_Clone_Transaction.Model
{
    public class ServiceModel
    {
        public string Email_HostName { get; set; }
        public string Email_FromAddress { get; set; }
        public string Email_ToAddress { get; set; }
        public string Email_CC { get; set; }
        public string Email_Port { get; set; }
        public string Link { get; set; }
        public bool SentToUsers { get; set; }
    }
}
