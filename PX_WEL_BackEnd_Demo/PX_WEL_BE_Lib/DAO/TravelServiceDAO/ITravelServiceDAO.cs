using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Data.SqlClient;


namespace PX_WEL_BE_Lib.DAO.TravelServiceDAO
{
    public interface ITravelServiceDAO
    {

        //string TravelCodeEn2Cn { get; }
        //string TravelCodeCn2En { get; }

        //string RefCodeCn2En { get; }


        Dictionary<string, KeyValuePair<string, string>> fileGenreDict { get; }

        Task<string> SelectTravelAllConditionAsync(SqlConnectionStringBuilder connectionStringBuilder, string sqlSelectStr);



        Task<string> SelectTravelRefCodeAsync(SqlConnectionStringBuilder connectionStringBuilder, string sqlSelectStr, string fileGenre = "pdf");

        

    }
}
