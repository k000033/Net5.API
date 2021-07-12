using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;


namespace PX_WEL_BE_Lib.Service.WELAppInfoService
{
    public interface IAppInfoService
    {

        Task<string> SelectWELVersionAsync(SqlConnectionStringBuilder connectionStringBuilder, string sqlStr);

    }
}
