using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using Dapper;
using System.Data;
using System.Linq;
using Duty_Stamp_SendEmail_Clone_Transaction.Utility;
using Duty_Stamp_SendEmail_Clone_Transaction.Model;

namespace Duty_Stamp_SendEmail_Clone_Transaction.Business
{
    public class UpdateFlag
    {
        private static SqlConnection _con;
        private static ConnectionString _c = new ConnectionString();
        private static Helper _h = new Helper();
        private static Log _l = new Log();
        public UpdateFlag()
        {
            _con = new SqlConnection(_c.ConnectionStringss());
            _con.Open();
        }
        public void UpdateFlagToTransection(List<TransectionModel> _t)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@Duty_Stamp_Clone_Transection", _h.ToDataTable(_t).AsTableValuedParameter());
                p.Add("@StatusChange", dbType: DbType.Int32, direction: ParameterDirection.Output);
                p.Add("@StatusText", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                _con.Query<object>("[sp_Duty_Stamp_Update_Clone_Transaction]", p, commandType: CommandType.StoredProcedure).ToList();
                var return_Status = p.Get<int>("@StatusChange");
                if (return_Status == 0)
                {
                    _l.LogFile("Success Update Clone transaction<br/>");
                }
                else
                {
                    var return_Error = p.Get<string>("@StatusText");
                    _l.LogFile("Error Update Clone transaction <br/>" + " Message : " + return_Error);
                }
            }
            catch (Exception ex)
            {
                _l.LogFile("Error UpdateFlagToTransection Message : " + ex.Message + "<br/>" + "StackTrace : " + ex.StackTrace);
            }
        }
    }
}
