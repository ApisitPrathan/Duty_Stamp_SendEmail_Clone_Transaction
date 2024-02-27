using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Duty_Stamp_SendEmail_Clone_Transaction.Utility;
using Duty_Stamp_SendEmail_Clone_Transaction.Model;
using System.Data.SqlClient;
using Dapper;
using System.Data;
using System.Linq;

namespace Duty_Stamp_SendEmail_Clone_Transaction.Business
{
    public class GetCloneTransetion
    {
        private static SqlConnection _con;
        private static ConnectionString _c = new ConnectionString();
        private static Log _l = new Log();
        public GetCloneTransetion()
        {
            _con = new SqlConnection(_c.ConnectionStringss());
            _con.Open();
        }
        public void ExecCloneTransaction()
        {
            try
            {
                var p = new DynamicParameters();
                _con.Query<object>("[sp_Duty_Stamp_Clone_Transaction]", p, commandType: CommandType.StoredProcedure).ToList();
                
            }
            catch (Exception ex)
            {
                _l.LogFile("Job [ExecCloneTransaction] Error..");
                _l.LogFile("ErrorMessage :: " + ex.Message + ex.StackTrace);
            }
        }
        public List<TransectionModel> GetTranection()
        {
            List<TransectionModel> result = new List<TransectionModel>();
            try
            {
                var p = new DynamicParameters();
                result = _con.Query<TransectionModel>("[sp_Duty_Stamp_Get_Report]", p, commandType: CommandType.StoredProcedure).ToList();
                if (result.Count > 0)
                {
                    result = result.Where(c => c.Flag_Email != "Y" 
                                                && c.Additional_Duty_Stamp == "YES").ToList();
                    _l.LogFile("Clone Trasaction is " + result.Count + "rows");

                }
                else
                {
                    _l.LogFile("Clone Trasaction is null");
                }
            }
            catch (Exception ex)
            {
                _l.LogFile("Job [GetTranection] Error..");
                _l.LogFile("ErrorMessage :: " + ex.Message + ex.StackTrace);
            }
            return result;
        }
    }
}
