using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;


namespace PX_WEL_BE_Lib.DAO.WELAppInfoDAO
{
    public interface IAppInfoDAO
    {

        

        Task<string> SelectWELVersionAsync(SqlConnectionStringBuilder connectionStringBuilder, string sqlStr);

    }
}
