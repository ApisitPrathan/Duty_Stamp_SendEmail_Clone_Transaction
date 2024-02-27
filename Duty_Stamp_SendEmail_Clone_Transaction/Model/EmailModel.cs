using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Duty_Stamp_SendEmail_Clone_Transaction.Model
{
    public class EmailModel
    {
        public string Module { get; set; }
        public string Subject { get; set; }
        public string Body1 { get; set; }
        public string Body2 { get; set; }
        public string Body3 { get; set; }
        public string To { get; set; }
        public string Cc { get; set; }
    }

    public class SaleOrg
    {
        public string Employee { get; set; }
        public string Manager { get; set; }
        public string Mail { get; set; }
        public int Lvl { get; set; }
    }
    public class EmailConfig
    {
        public string Display_Name { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }
    }
}
