using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PX_WEL_BE_Lib.DAO.TableInfo;
using PX_WEL_BE_Lib.EnvControl;


namespace PX_WEL_BE_Lib.CURD.Select
{
    public class SQL_Select_Travel
    {


        SwitchEnv _switchEnv = new SwitchEnv();

        private TableName _tableName;

        public SQL_Select_Travel(TableName T)
        {

            this._tableName = T;
        }



        public string GetSEL_TravelAllConditionStr(bool atHomePageOrNot = true)
        {
            string dbConnType = _switchEnv.GetDBConnType(_switchEnv.IfProdEnv(), _switchEnv.IfProdConn());

            string resString = string.Empty;
            switch (dbConnType)
            {
                
                case "SqlServer_TestConn_Test_Env":

                    int limitNum = atHomePageOrNot ? 10 : 50;

                    resString = $@"select 
                        guid_travel
                        , category_code
                        , route_serial
                        , route_intro
                        , route_name 
                        , lean_img_file img_file
                        from {this._tableName.Travel}
                        where datetime('now', 'localtime') <= effect_date_to
                        --and category_code = case when '2021-08-31 23:59:59' >= effect_date_to then  'Travel_Spring' else 'Travel_Award' end
                        and category_code = 'Travel_Spring'
                        order by route_serial asc
                        limit {limitNum};";
                    break;

                case "SqlServer_TestConn_Prod_Env":
                case "SqlServer_ProdConn_Test_Env":
                case "SqlServer_ProdConn_Prod_Env":

                    int topNum = atHomePageOrNot ? 10 : 50;
                    resString = $@"select TOP {topNum}
                        guid_travel
                        , category_code
                        , route_serial
                        , route_intro
                        , route_name 
                        , lean_img_file img_file
                        from {this._tableName.Travel}
                        where getdate() <= effect_date_to
                        --and category_code = case when '2021-08-31 23:59:59' >= effect_date_to then  'Travel_Spring' else 'Travel_Award' end
                        and category_code = 'Travel_Spring'
                        order by route_serial asc;";
                    break;
                default:
                    break;
            }

            return resString;
        }

        


        public string GetSEL_TravelRefCodeStr(string fileGenre = "pdf")
        {
            string dbConnType = _switchEnv.GetDBConnType(_switchEnv.IfProdEnv(), _switchEnv.IfProdConn());

            string resString = string.Empty;
            switch (dbConnType)
            {
                case "SqlServer_TestConn_Test_Env":
                    resString = $@"select 
                        ref_code
                        --, {fileGenre}_file
                        , {fileGenre}_compress_type
                    from {this._tableName.TravelRef}   
                    where  ref_genre = 'Travel_Spring';";
                    // ref_genre = case when '2021-08-31 23:59:59' >= datetime('now', 'localtime') then  'Travel_Spring' else 'Travel_Award' end;
                    break;

                case "SqlServer_TestConn_Prod_Env":
                case "SqlServer_ProdConn_Test_Env":
                case "SqlServer_ProdConn_Prod_Env":
                    resString = $@"select 
                        ref_code
                        --, {fileGenre}_file
                        , {fileGenre}_compress_type
                    from {this._tableName.TravelRef}   
                    where  ref_genre = 'Travel_Spring';";
                    break;
                default:
                    break;
            }


            return resString;
        }


        

    }
}
