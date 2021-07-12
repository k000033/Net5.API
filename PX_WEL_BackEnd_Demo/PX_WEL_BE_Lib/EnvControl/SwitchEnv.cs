using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using PX_WEL_BE_Lib.EnvControl;
using PX_WEL_BE_Lib.Service.DbInfo;


namespace PX_WEL_BE_Lib.EnvControl
{

    public class SwitchEnv: ISwitchEnv
    {

        // --------------------SYS info--------------------------
        public string ApiVersion = "v1";

        public int TryTimesForDeadLock = 3;



        // --------------------Obj info--------------------------
        
        DbConn DbConnBuilder = new DbConn();


        // --------------------Turn on or off--------------------------
        public bool IfProdEnv() 
        {
            return false;  
        }

        public bool IfProdConn()
        {
            return true;  
        }

        public string WhereIsHost(string position = "PxmartInside")
        {
            /**
             * PxmartInside
             * HiCloud
             * 
             * 
             */
              
            return position;
        }



        // --------------------db connection--------------------------
        public string GetDBConnStr(bool ifProdEnv, bool IfProdConn)
        {
            string dbLink = string.Empty;


            switch (IfProdConn)
            {
                case true:
                    dbLink = ifProdEnv == true ? DbConnBuilder.GetConnectionStr("SqlServer_ProdConn_Prod_Env") : DbConnBuilder.GetConnectionStr("SqlServer_ProdConn_Test_Env");
                    break;
                case false:
                    dbLink = ifProdEnv == true ? DbConnBuilder.GetConnectionStr("SqlServer_TestConn_Prod_Env") : DbConnBuilder.GetConnectionStr("SqlServer_TestConn_Test_Env");
                    break;

            }

            return dbLink;
        }


        public string GetDBConnType(bool ifProdEnv, bool IfProdConn)
        {
            string dbLink = string.Empty;


            switch (IfProdConn)
            {
                //SqlServer_TestConn_Test_Env connects to sqlite3. Remaining string connects to sql server.
                case true:
                    dbLink = ifProdEnv == true ? "SqlServer_ProdConn_Prod_Env" : "SqlServer_ProdConn_Test_Env";
                    break;
                case false:
                    
                    dbLink = ifProdEnv == true ? "SqlServer_TestConn_Prod_Env" : "SqlServer_TestConn_Test_Env";
                    break;

            }

            return dbLink;
        }




    }
}
