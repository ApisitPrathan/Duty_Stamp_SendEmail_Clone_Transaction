using System;
using System.Collections.Generic;
using Duty_Stamp_SendEmail_Clone_Transaction.Model;
using Duty_Stamp_SendEmail_Clone_Transaction.Business;
using Duty_Stamp_SendEmail_Clone_Transaction.Utility;
using System.Linq;
using System.Configuration;

namespace Duty_Stamp_SendEmail_Clone_Transaction
{
    class Program
    {
        private static Log _l = new Log();
        private static Helper _h = new Helper();
        static void Main(string[] args)
        {
            _l.LogFile("Start Duty Stamp SendEmail Clone Transaction");
            //check 1 not holiday
            //if (_h.CheckHoliday())
            //{

                // exec clone and get transection 


                var transection = GetTransection();
                if (transection.Count > 0)
                {
                    //var groupBySale = GroupBySale(transection);
                    //loop sendemail by sale
                    SendEmail(transection);
                    //update flag sendemail to table Topic_Hold_Transection
                    UpdateFlag(transection);
                }
            //}
            _l.LogFile("End Duty Stamp SendEmail Clone Transaction");
        }
        public static List<TransectionModel> GetTransection()
        {
            GetCloneTransetion _s = new GetCloneTransetion();
            var result = _s.GetTranection();
            return result;
        }

        //public static List<GroupTranectionBySale> GroupBySale(List<TransectionModel> _t)
        //{
        //    List<GroupTranectionBySale> TempSales = new List<GroupTranectionBySale>();
        //    var listSale = _t.Select(c => c.Sale_Ower).Distinct().ToList();
        //    foreach (var i in listSale)
        //    {
        //        try
        //        {
        //            GroupTranectionBySale data = new GroupTranectionBySale();
        //            var groupTransection = _t.Where(x => x.Sale_Ower == i).ToList();
        //            data.Sale = i;
        //            data.Transection = groupTransection;
        //            TempSales.Add(data);
        //        }
        //        catch (Exception ex)
        //        { }
        //    }
        //    return TempSales;
        //}

        public static void SendEmail(List<TransectionModel> _t)
        {
            SendEmail _s = new SendEmail();
            _s.SendEmailCloneTransaction(_t);
        }

        public static void UpdateFlag(List<TransectionModel> _t)
        {
            UpdateFlag _u = new UpdateFlag();
            _u.UpdateFlagToTransection(_t);
        }
    }
}
