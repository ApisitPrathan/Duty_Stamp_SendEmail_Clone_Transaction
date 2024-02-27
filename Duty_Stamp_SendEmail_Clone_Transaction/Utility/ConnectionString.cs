using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Duty_Stamp_SendEmail_Clone_Transaction.Utility
{
    public class ConnectionString
    {
        public static string DataSource = ConfigurationManager.AppSettings["DataSource"];
        public static string DatabaseName = ConfigurationManager.AppSettings["DatabaseName"];
        public static string UserName = ConfigurationManager.AppSettings["UserName"];
        public static string Password = ConfigurationManager.AppSettings["Password"];
        public static string DataConnection = ConfigurationManager.AppSettings["DataConnection"];
        public string ConnectionStringss()
        {

            DataConnection = DataConnection.Replace("_datasource_", DataSource);
            DataConnection = DataConnection.Replace("_dbname_", DatabaseName);
            DataConnection = DataConnection.Replace("_user_", UserName);
            DataConnection = DataConnection.Replace("_password_", Password);
            return DataConnection;
        }
    }
}
