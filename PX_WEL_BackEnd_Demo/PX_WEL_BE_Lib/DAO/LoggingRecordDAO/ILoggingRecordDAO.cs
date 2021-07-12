
using System.Threading.Tasks;


using System.Data.SqlClient;


namespace PX_WEL_BE_Lib.DAO.LoggingRecordDAO
{
    public interface ILoggingRecordDAO
    {

        

        Task MergePersonnelState<TModel>(SqlConnectionStringBuilder connectionStringBuilder, string sqlStr, TModel modelBinding);

        

        Task UpdatePersonnelClickTime(SqlConnectionStringBuilder connectionStringBuilder, string sqlStr, string UserId);



    }
}
