using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using System.Data;
using System.Linq;
using System.Text;
using Duty_Stamp_SendEmail_Clone_Transaction.Utility;
using Duty_Stamp_SendEmail_Clone_Transaction.Model;
using System.Net.Mail;
using System.Net;

namespace Duty_Stamp_SendEmail_Clone_Transaction.Business
{
    public class SendEmail
    {
        private static ConnectionService _cs = new ConnectionService();
        private static Helper _h = new Helper();
        private static ServiceModel _s;
        private static SmtpClient _smtp;
        private static SqlConnection _con;
        private static ConnectionString _c = new ConnectionString();
        private static Log _l = new Log();
        private static List<EmailModel> _template;
        public SendEmail()
        {
            _s = _cs.ConnectionServices();
            _smtp = GetSmtlEmail(_s);
            _con = new SqlConnection(_c.ConnectionStringss());
            _con.Open();
            //_template = GetTemplateEmail();
        }
        public void SendEmailCloneTransaction(List<TransectionModel> _t)
        {
            try
            {
                foreach (var t in _t)
                {
                   
                    MailMessage mail = new MailMessage();
                    string To = GetEmailTo(_s.Email_ToAddress);
                    string CC = GetEmailCC();
                    mail.From = new MailAddress(_s.Email_FromAddress);
                    if (_s.SentToUsers)
                    {
                        /*PRD*/
                        //To = "";
                        if (To.Contains(';'))
                        {
                            string[] userTo = To.Split(';');
                            foreach (string MultiTo in userTo)
                            {
                                if (!string.IsNullOrEmpty(MultiTo))
                                    mail.To.Add(new MailAddress(MultiTo));
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(To))
                                mail.To.Add(To);
                        }
                        if (CC.Contains(';'))
                        {
                            string[] userCC = CC.Split(';');
                            foreach (string MultiCC in userCC)
                            {
                                if (!string.IsNullOrEmpty(MultiCC))
                                    mail.CC.Add(new MailAddress(MultiCC));
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(CC))
                                mail.CC.Add(CC);
                        }
                    }
                    else
                    {
                        /*UAT*/

                        mail.To.Add("apisit.p.pv@fujifilm.com");
                        if(!string.IsNullOrEmpty(_s.Email_CC))
                            mail.CC.Add(_s.Email_CC);
                    }
                    mail.Subject = "RP4 Additional Duty Stamp Notification :  "+ t.Customer_ID + t.Customer_Name + "_" + t.Contract_Number;
                    //StringBuilder sb1 = new StringBuilder(_template[0].Body1);
                    //StringBuilder sb2 = new StringBuilder(_template[0].Body2);
                    //StringBuilder sb3 = new StringBuilder(_template[0].Body3);
                    if (_s.SentToUsers)
                        mail.Body = String.Format("{0}<br />", ReplaceTable(t));
                    else
                        mail.Body = String.Format("{0}<br />TO : {1}<br />CC :{2}", ReplaceTable(t), To, CC);
                    mail.IsBodyHtml = true;
                    
                    _smtp.Send(mail);
                }
                 
            }
            catch (Exception ex)
            {
                _l.LogFile("Job [SendEmailForGroupSale] Error..");
                _l.LogFile("ErrorMessage :: " + ex.Message + ex.StackTrace);
            }
        }
        private static List<EmailModel> GetTemplateEmail()
        {
            List<EmailModel> EmailModel = new List<EmailModel>();
            try
            {
                var p = new DynamicParameters();
                p.Add("@TableName", "QF_LTO_DL_MasterEmail");
                p.Add("@FilterValue1", "Manager Approved");
                var emailTemplate = _con.Query<EmailModel>("sp_LTO_GetTable", p, commandType: CommandType.StoredProcedure).ToList();
                if (emailTemplate.Count > 0)
                {
                    foreach (var _result in emailTemplate)
                    {
                        EmailModel Email = new EmailModel();
                        Email.Module = _result.Module;
                        Email.Subject = _result.Subject;
                        Email.Body1 = _result.Body1;
                        Email.Body2 = _result.Body2;
                        Email.Body3 = _result.Body3;
                        EmailModel.Add(Email);

                    }
                }
            }
            catch (Exception ex)
            {
            }
            return EmailModel;
        }
        private static SmtpClient GetSmtlEmail(ServiceModel servicesModel)
        {
            SmtpClient SmtpServer = new SmtpClient();
            SmtpServer.EnableSsl = false;
            SmtpServer.Credentials = CredentialCache.DefaultNetworkCredentials;
            SmtpServer.Host = servicesModel.Email_HostName;
            SmtpServer.Port = !string.IsNullOrEmpty(servicesModel.Email_Port) ? Convert.ToInt32(servicesModel.Email_Port) : 0;
            SmtpServer.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            return SmtpServer;
        }
        private static string GetEmailTo(string SentEmailTo)
        {
            string strBuild = "";
            int count = 0;
            strBuild = SentEmailTo;
            //strBuild = "salinrat.w.sq@fujifilm.com;parntipaporn.s.pt@fujifilm.com;watchara.b.gi@fujifilm.com";
            //if (saleOrg.Count > 0)
            //{
            //    foreach (var data in saleOrg)
            //    {
            //        if (!string.IsNullOrEmpty(data.Mail))
            //            strBuild += data.Mail;
            //        if (count != (saleOrg.Count - 1))//not last
            //        {
            //            strBuild += ';';
            //        }
            //        count++;
            //    }
            //}
            return strBuild;
        }
        private static string GetEmailCC()
        {
            string strBuild = "";
            //var p = new DynamicParameters();
            //p.Add("@TableName", "QF_LTO_DL_Config_EmailTo");
            //var emailConfigs = _con.Query<EmailConfig>("sp_LTO_GetTable", p, commandType: CommandType.StoredProcedure).ToList();
            //int count = 0;
            //if (emailConfigs.Count > 0)
            //{
            //    foreach (var data in emailConfigs)
            //    {
            //        if (!string.IsNullOrEmpty(data.Email))
            //            strBuild += data.Email;
            //        if (count != (emailConfigs.Count - 1))//not last
            //        {
            //            strBuild += ';';
            //        }
            //        count++;
            //    }
            //}
            return strBuild;
        }
        private StringBuilder ReplaceBody1(StringBuilder sbTable)
        {

            return sbTable;
        }
        private StringBuilder ReplaceBody2(StringBuilder sbTable)
        {
            return sbTable;
        }

        private StringBuilder ReplaceTable(TransectionModel tdata)
        {
            StringBuilder sb = new StringBuilder();
            DateTime now = DateTime.Now;
            sb.Append("<p>Dear Participant,</p>");
            //sb.Append("<br/>");
            sb.Append("<p>There is update of Duty Stamp alert for the following matter</p>");
            sb.Append("<br/>");

            sb.Append("<table>");
            sb.Append("<tr style='border:1px solid black;'>");
                sb.AppendFormat("<td style='border:1px solid black;text-align: left;vertical-align: text-top;background-color: #D5D8DC'>&nbsp; {0} &nbsp;</td>", "Log ID");
                sb.AppendFormat("<td style='border:1px solid black;text-align: left;vertical-align: text-top;'>&nbsp; {0} &nbsp;</td>", !_h.CheckNull(tdata.Running) ? "DS"+ tdata.Running : "");
            sb.Append("</tr>");
            sb.Append("<tr style='border:1px solid black;'>");
                sb.AppendFormat("<td style='border:1px solid black;text-align: left;vertical-align: text-top;background-color: #D5D8DC'>&nbsp; {0} &nbsp;</td>", "Customer ID");
                sb.AppendFormat("<td style='border:1px solid black;text-align: left;vertical-align: text-top;'>&nbsp; {0} &nbsp;</td>", !_h.CheckNull(tdata.Customer_ID) ? tdata.Customer_ID : "");
            sb.Append("</tr>");
            sb.Append("<tr style='border:1px solid black;'>");
                sb.AppendFormat("<td style='border:1px solid black;text-align: left;vertical-align: text-top;background-color: #D5D8DC'>&nbsp; {0} &nbsp;</td>", "Customer Name");
                sb.AppendFormat("<td style='border:1px solid black;text-align: left;vertical-align: text-top;'>&nbsp; {0} &nbsp;</td>", !_h.CheckNull(tdata.Customer_Name) ? tdata.Customer_Name : "");
            sb.Append("</tr>");
            sb.Append("<tr style='border:1px solid black;'>");
                sb.AppendFormat("<td style='border:1px solid black;text-align: left;vertical-align: text-top;background-color: #D5D8DC'>&nbsp; {0} &nbsp;</td>", "Contract No");
                sb.AppendFormat("<td style='border:1px solid black;text-align: left;vertical-align: text-top;'>&nbsp; {0} &nbsp;</td>", !_h.CheckNull(tdata.Contract_Number) ? tdata.Contract_Number : "");
            sb.Append("</tr>");
            sb.Append("<tr style='border:1px solid black;'>");
                sb.AppendFormat("<td style='border:1px solid black;text-align: left;vertical-align: text-top;background-color: #D5D8DC'>&nbsp; {0} &nbsp;</td>", "Type");
                sb.AppendFormat("<td style='border:1px solid black;text-align: left;vertical-align: text-top;'>&nbsp; {0} &nbsp;</td>", !_h.CheckNull(tdata.Contract_Type) ? tdata.Contract_Type : "");
            sb.Append("</tr>");
            sb.Append("<tr style='border:1px solid black;'>");
                sb.AppendFormat("<td style='border:1px solid black;text-align: left;vertical-align: text-top;background-color: #D5D8DC'>&nbsp; {0} &nbsp;</td>", "Document Type");
                sb.AppendFormat("<td style='border:1px solid black;text-align: left;vertical-align: text-top;'>&nbsp; {0} &nbsp;</td>", !_h.CheckNull(tdata.Document_Type) ? tdata.Document_Type : "");
            sb.Append("</tr>");
            sb.Append("<tr style='border:1px solid black;'>");
                sb.AppendFormat("<td style='border:1px solid black;text-align: left;vertical-align: text-top;background-color: #D5D8DC'>&nbsp; {0} &nbsp;</td>", "Current Contract Period (Mths)");
                sb.AppendFormat("<td style='border:1px solid black;text-align: left;vertical-align: text-top;'>&nbsp; {0} &nbsp;</td>", tdata.Current_Contract_Period);
            sb.Append("</tr>");
            sb.Append("<tr style='border:1px solid black;'>");
                sb.AppendFormat("<td style='border:1px solid black;text-align: left;vertical-align: text-top;background-color: #D5D8DC'>&nbsp; {0} &nbsp;</td>", "Additional Forecast Amount");
                sb.AppendFormat("<td style='border:1px solid black;text-align: left;vertical-align: text-top;'>&nbsp; {0} &nbsp;</td>", String.Format("{0:n}", tdata.Additional_Penalty_Forecast_Amt) + " Baht");
            sb.Append("</tr>");
            sb.Append("<tr style='border:1px solid black;'>");
                sb.AppendFormat("<td style='border:1px solid black;text-align: left;vertical-align: text-top;background-color: #D5D8DC'>&nbsp; {0} &nbsp;</td>", "Current Stage");
                sb.AppendFormat("<td style='border:1px solid black;text-align: left;vertical-align: text-top;'>&nbsp; {0} &nbsp;</td>", "CTS was notified.");
            sb.Append("</tr>");
            sb.Append("<tr style='border:1px solid black;'>");
            sb.AppendFormat("<td style='border:1px solid black;text-align: left;vertical-align: text-top;background-color: #D5D8DC'>&nbsp; {0} &nbsp;</td>", "Details in Link ");
            sb.AppendFormat("<td style='border:1px solid black;text-align: left;vertical-align: text-top;'>&nbsp; {0} &nbsp;</td>", "<a href="+ _s.Link + ">View Detail</a>");
            sb.Append("</tr>");
            sb.Append("</table>");
            sb.ToString();
            return sb;
        }
    }
}
