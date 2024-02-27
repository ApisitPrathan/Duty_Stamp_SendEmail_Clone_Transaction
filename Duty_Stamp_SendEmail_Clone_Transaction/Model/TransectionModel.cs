using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Duty_Stamp_SendEmail_Clone_Transaction.Model
{
    public class TransectionModel
    {
        public string ID { get; set; }
        public string Month { get; set; }
        public string Customer_ID { get; set; }
        public string Customer_Name { get; set; }
        public string Contract_Number { get; set; }
        public string Contract_Type { get; set; }
        public string Document_Type { get; set; }
        public string SR_Name { get; set; }
        public string Branch { get; set; }
        public string SR_Team { get; set; }
        public DateTime? Contract_Submit_Date { get; set; }
        public DateTime? Contract_Start_Date { get; set; }
        public DateTime? Contract_End_Date { get; set; }
        public string Months { get; set; }
        public DateTime? Contract_Signed_Date { get; set; }
        public DateTime? Duty_Stamp { get; set; }
        public string Duty_Stamp_Period { get; set; }
        public decimal? Deal_Amount { get; set; }
        public decimal? New_TCLR { get; set; }
        public decimal? Total_TCLR { get; set; }
        public decimal? Duty_Stamp_Amount_Original { get; set; }
        public decimal? Penalty_Original { get; set; }
        public decimal? Normal_Duty_Stamp { get; set; }
        public decimal? Duty_Stamp_Amount_Duplicate { get; set; }
        public decimal? Penalty_Duplicate { get; set; }
        public decimal? Fines { get; set; }
        public decimal? Total_Penalty { get; set; }
        public decimal? Total_Amout { get; set; }
        public DateTime? Submit_Date_For_Cheque { get; set; }
        public string Current_Contract_Period { get; set; }
        public decimal? Total_Invoice_Amount { get; set; }
        public decimal? Avg_Per_Month { get; set; }
        public string Additional_Duty_Stamp { get; set; }
        public decimal? Additional_Penalty_Forecast_Amt { get; set; }
        public decimal? Additional_Duty_Stamp_Amount { get; set; }
        public DateTime? Additional_Duty_Stamp_Paid_Date { get; set; }
        public string Fix_Freeze_Month { get; set; }
        public string Required_IDMP { get; set; }
        public string Remark { get; set; }
        public string Flag_Edit { get; set; }
        public DateTime? Created_Date { get; set; }
        public DateTime? Modified_Date { get; set; }
        public string Flag_Email { get; set; }
        public string Running { get; set; }
        public string Flag_Clone { get; set; }
    }

}
