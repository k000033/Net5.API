using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PX_WEL_BE_Lib.DAO.TableInfo;
using PX_WEL_BE_Lib.EnvControl;

namespace PX_WEL_BE_Lib.CURD.Select
{
    public class SQL_Select_WELVersion
    {


        SwitchEnv _switchEnv = new SwitchEnv();

        private TableName _tableName;

        public SQL_Select_WELVersion(TableName T)
        {

            this._tableName = T;
        }


        public string GetSEL_WELVersionIterationStr(bool atHomePageOrNot = true)
        {
            string dbConnType = _switchEnv.GetDBConnType(_switchEnv.IfProdEnv(), _switchEnv.IfProdConn());

            string resString = string.Empty;
            switch (dbConnType)
            {

                case "SqlServer_TestConn_Test_Env":
                    resString = $@"select VersionIteration from WELVersionIteration order by update_time desc limit 1;";
                    break;

                case "SqlServer_TestConn_Prod_Env":
                case "SqlServer_ProdConn_Test_Env":
                case "SqlServer_ProdConn_Prod_Env":

                    resString = $"select TOP 1 [VersionIteration] from [WELVersionIteration] order by update_time desc;";
                    break;
                default:
                    break;
            }

            return resString;
        }





    }
}
