using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace todo5.Services
{
    public class ConnectionService
    {

        public DataTable DataTable(string ConnectionString, SqlCommand com)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    com.Connection = con;
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    da.Fill(dt);
                    return dt;
                }
            }
        }
    }
}
