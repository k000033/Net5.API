using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Data.SqlClient;


using PX_WEL_BE_Lib.DAO.LoggingRecordDAO;

namespace PX_WEL_BE_Lib.Service.LoggingRecordService
{
    public class LoggingRecordService : ILoggingRecordService
    {

        private LoggingRecordDAO _loggingRecordDAO = new LoggingRecordDAO();


        // 造成無法注入的原因
        //public LoggingRecordService(LoggingRecordDAO loggingRecordDAO)
        //{
        //    this._loggingRecordDAO = loggingRecordDAO;
        //}


        public async Task<string> GetPureIP4Address(string IPArrFromHttpContext)
        {
            //Console.WriteLine("::::ffff:192.168.100.161");            

            if (Regex.IsMatch(IPArrFromHttpContext, "[:]{1,}"))
            {
                string[] IpAdrressArr = IPArrFromHttpContext.Split(":");

                return await Task.FromResult(IpAdrressArr[IpAdrressArr.Length - 1]);

            }
            else
            {
                return IPArrFromHttpContext;
            }
        }



        public async Task MergePersonnelState<TModel>(SqlConnectionStringBuilder connectionStringBuilder, string sqlStr, TModel modelBinding)
        {
            await this._loggingRecordDAO.MergePersonnelState(connectionStringBuilder, sqlStr, modelBinding);

        }

        public async Task UpdatePersonnelClickTime(SqlConnectionStringBuilder connectionStringBuilder, string sqlStr, string UserId)
        {
            await this._loggingRecordDAO.UpdatePersonnelClickTime(connectionStringBuilder, sqlStr, UserId);

        }







    }
}
