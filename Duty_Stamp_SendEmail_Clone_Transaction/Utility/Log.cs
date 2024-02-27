using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Duty_Stamp_SendEmail_Clone_Transaction.Utility
{
    public class Log
    {
        private static string log_path;

        public Log()
        {
            log_path = System.Configuration.ConfigurationManager.AppSettings["LogPath"];
        }
        public void LogFile(string msg)
        {
            int yearnow = DateTime.Now.Year;
            string pathfile = log_path + yearnow + "log.txt";
            string now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            try
            {
                if (!System.IO.File.Exists(pathfile))
                {
                    System.IO.File.Create(pathfile).Dispose();
                    using (System.IO.TextWriter tw = new System.IO.StreamWriter(pathfile))
                    {
                        tw.WriteLine(now + " " + "File Created");
                        tw.WriteLine(now + " " + msg);
                    }
                }
                else if (System.IO.File.Exists(pathfile))
                {
                    //using (TextWriter tw = new StreamWriter(pathfile))
                    //{
                    //    tw.WriteLine(msg);
                    //}
                    using (System.IO.StreamWriter filenow = new System.IO.StreamWriter(pathfile, true))
                    {
                        filenow.WriteLine(now + " " + msg);
                    }
                }

            }
            catch (Exception ex)
            {
                //stampErrorList("", form, id.ToString(), "", "createLogFile", "form=" + form + ",id=" + id + ",msg=" + msg, ex.Message, "");
            }
        }
    }
}
