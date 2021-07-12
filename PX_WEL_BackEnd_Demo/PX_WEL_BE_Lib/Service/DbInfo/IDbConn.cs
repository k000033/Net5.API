using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;


namespace PX_WEL_BE_Lib.Service.DbInfo
{
    public interface IDbConn
    {

        string GetConnectionStr(string serverName);

        SqlConnectionStringBuilder GenerateSqlServerConnStringBuilder(string ConnectionStr);

        
    }
}
