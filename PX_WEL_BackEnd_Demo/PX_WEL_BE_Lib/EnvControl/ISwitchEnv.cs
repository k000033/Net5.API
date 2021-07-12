using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


using PX_WEL_BE_Lib.Service.DbInfo;

namespace PX_WEL_BE_Lib.EnvControl
{
    public interface ISwitchEnv
    {
        //public bool Production = true;
        bool IfProdEnv();

        bool IfProdConn();

        string WhereIsHost(string position);


        string GetDBConnStr(bool ifProdEnv, bool IfProdConn);




        //UrlPxERPApi UrlForRequest { get; set; }

        //DbConn DbConnBuilder { get; set; }




    }
}
