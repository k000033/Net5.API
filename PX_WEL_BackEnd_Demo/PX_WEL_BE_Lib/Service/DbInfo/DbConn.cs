using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Data.SqlClient;




namespace PX_WEL_BE_Lib.Service.DbInfo
{
    public class DbConn : IDbConn
    {

        string SqlServer_ProdConn_Test_Env = @"Data Source=LAPTOP-4H8OQNVL\SQLEXPRESS;Initial Catalog=WELtest; User ID=___;Password=___";
        string SqlServer_TestConn_Test_Env = @"Data Source=LAPTOP-4H8OQNVL\SQLEXPRESS;Initial Catalog=WELtest; User ID=___;Password=___";
        

        string SqlServer_ProdConn_Prod_Env = @"Data Source=___.___.___.___,1433;Initial Catalog=_____; User ID=_____;Password=_____";
        string SqlServer_TestConn_Prod_Env = @"Server=___.___.___.___,1433;Initial Catalog=_____; User ID=_____;Password=_____";

        public string GetConnectionStr(string serverName)
        {

            switch (serverName)
            {
                case "SqlServer_ProdConn_Prod_Env":
                    //Debug.WriteLine("Connect to SqlServer_ProdConn_Prod_Env...");
                    //Console.WriteLine("Connect to SqlServer_ProdConn_Prod_Env...");
                    //return "C:\\Users\\carter_yang\\source\\repos\\PX_WEL_BackEnd\\PX_WEL_DBManipulate\\bin\\Debug\\net5.0\\WEL_sqlite3.db";
                    return this.SqlServer_ProdConn_Prod_Env;



                case "SqlServer_TestConn_Prod_Env":
                    // test sql server 
                    //Debug.WriteLine("Connect to SqlServer_TestConn_Prod_Env...");
                    //Console.WriteLine("Connect to SqlServer_TestConn_Prod_Env...");
                    return this.SqlServer_TestConn_Prod_Env;



                case "SqlServer_ProdConn_Test_Env":
                    //Debug.WriteLine("Connect to SqlServer_ProdConn_Test_Env...");
                    //Console.WriteLine("Connect to SqlServer_ProdConn_Test_Env...");
                    return this.SqlServer_ProdConn_Test_Env;


                case "SqlServer_TestConn_Test_Env":
                    //Debug.WriteLine("Connect to SqlServer_TestConn_Test_Env...");
                    //Console.WriteLine("Connect to SqlServer_TestConn_Test_Env...");
                    //Debug.WriteLine(Environment.CurrentDirectory); //C:\Users\UncleCarter\source\repos\PX_WEL_BackEnd\PX_WEL_BackEnd
                    //return "C:\\WEL_sqlite3.db";
                    return this.SqlServer_TestConn_Test_Env;
                            

                default:
                    Debug.WriteLine("Error! ServerName is inevitable!");
                    return string.Empty;
            }

        }


        public SqlConnectionStringBuilder GenerateSqlServerConnStringBuilder(string ConnectionStr)
        {

            SqlConnectionStringBuilder ConnectionStringBuilder = new SqlConnectionStringBuilder();
            ConnectionStringBuilder.DataSource = ConnectionStr;
            return ConnectionStringBuilder;
        }




    }
}
