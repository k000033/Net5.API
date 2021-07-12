using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;

using PX_WEL_BE_Lib.DAO.WELAppInfoDAO;

namespace PX_WEL_BE_Lib.Service.WELAppInfoService
{
    public class AppInfoService : IAppInfoService
    {

        AppInfoDAO _appInfoDAO = new AppInfoDAO();



        public async Task<string> SelectWELVersionAsync(SqlConnectionStringBuilder connectionStringBuilder, string sqlStr)
        {

            return await this._appInfoDAO.SelectWELVersionAsync(connectionStringBuilder, sqlStr);
        }

    }
}
