using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Configuration;
using System.Text;

namespace Duty_Stamp_SendEmail_Clone_Transaction.Utility
{
    public class Helper
    {
        #region Datatable Function
        public DataTable ToDataTable<T>(List<T> list)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in list)
            {
                for (int i = 0; i < values.Length; i++)
                    values[i] = props[i].GetValue(item) ?? DBNull.Value;
                table.Rows.Add(values);
            }
            return table;
        }
        #endregion
        public bool CheckNull(string sNull)
        {
            bool c = true;
            if (sNull == null)
            {
                c = true;
            }
            else
            {
                if (sNull.Trim() == "")
                {
                    c = true;
                }
                else
                {
                    c = false;
                }

            }
            return c;
        }
        public bool IsNullOrEmpty<T>(IList<T> List)
        {
            return (List == null || List.Count < 1);
        }
        public bool CheckHoliday()
        {
            bool chk = false;
            string _date = ConfigurationManager.AppSettings["Remider"];
            if (_date.Length > 0)
            {
                string _dateNow = DateTime.Now.ToString("dd");
                //check have ','
                if (_date.Contains(","))
                {
                    //split ','
                    string[] _splitDate = _date.Split(',');
                    foreach (var i in _splitDate)
                    {
                        int resultDate = Caldate(i);
                        //check today is date
                        if (Convert.ToInt32(_dateNow) == resultDate)
                        {
                            chk = true;
                            break;
                        }
                    }
                }
                else
                {
                    int resultDate = Caldate(_date);
                    //check today is date
                    if (Convert.ToInt32(_dateNow) == resultDate)
                        chk = true;
                }
            }
            return chk;
        }
        public int Caldate(string dateConfig)
        {
            int calDate = 0;
            //check date is not holiday
            DateTime d = new DateTime(DateTime.Now.Year, DateTime.Now.Month, Convert.ToInt32(dateConfig));
            string daysOfConfig = d.DayOfWeek.ToString();
            if (daysOfConfig == "Sunday")
                calDate = Convert.ToInt32(dateConfig) + 1;
            else if (daysOfConfig == "Saturday")
                calDate = Convert.ToInt32(dateConfig) + 2;
            else
                calDate = Convert.ToInt32(dateConfig);
            return calDate;
        }
    }
}
