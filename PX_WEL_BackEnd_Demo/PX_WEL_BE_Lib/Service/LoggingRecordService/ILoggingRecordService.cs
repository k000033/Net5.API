
using System.Threading.Tasks;

using System.Data.SqlClient;


namespace PX_WEL_BE_Lib.Service.LoggingRecordService
{
    public interface ILoggingRecordService
    {


        Task<string> GetPureIP4Address(string IPArrFromHttpContext);

        


        Task MergePersonnelState<TModel>(SqlConnectionStringBuilder connectionStringBuilder, string sqlStr, TModel modelBinding);

        

        Task UpdatePersonnelClickTime(SqlConnectionStringBuilder connectionStringBuilder, string sqlStr, string UserId);




    }
}
