using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;


using PX_WEL_BE_Lib.DAO.TravelServiceDAO;

namespace PX_WEL_BE_Lib.Service.TravelService
{
    public class TravelService : ITravelService
    {
        public string TravelCodeEn2Cn { get; } = @"{'Travel_Spring':'春季旅遊'
                                    ,'Travel_Winter':'冬季旅遊'
                                    ,'Travel_Dep':'部門旅遊'
                                    ,'Travel_Award':'獎勵旅遊'}";
        public string TravelCodeCn2En { get; } = @"{'春季旅遊':'Travel_Spring'
                                    ,'冬季旅遊':'Travel_Winter'
                                    ,'部門旅遊':'Travel_Dep'
                                    ,'獎勵旅遊':'Travel_Award'}";

        public string RefCodeCn2En { get; } = @"{'各單位窗口':'Contacts'
                                    ,'操作手冊_電腦版&手機版':'ManualForDevice'
                                    ,'旅遊出發日期及上車地點':'DepartureDataAndLocation'
                                    ,'福委會員工旅遊管理規範_修訂版':'TravelRequirement'
                                    ,'補貼明細':'Subsidy'}";

        public string RefCodeEn2Cn { get; } = @"{'Contacts':'各單位窗口'
                                    ,'ManualForDevice':'操作手冊_電腦版&手機版'
                                    ,'DepartureDataAndLocation':'旅遊出發日期及上車地點'
                                    ,'TravelRequirement':'福委會員工旅遊管理規範_修訂版'
                                    ,'Subsidy':'補貼明細'}";




        private TravelServiceDAO _travelDAO = new TravelServiceDAO();




        public async Task<string> SelectTravelAllConditionAsync(SqlConnectionStringBuilder connectionStringBuilder, string sqlSelectStr)
        {
            return await this._travelDAO.SelectTravelAllConditionAsync(connectionStringBuilder, sqlSelectStr);

        }





        public async Task<string> SelectTravelRefCodeAsync(SqlConnectionStringBuilder connectionStringBuilder, string sqlSelectStr, string fileGenre = "pdf")
        {
            return await this._travelDAO.SelectTravelRefCodeAsync(connectionStringBuilder, sqlSelectStr, fileGenre);
        }


    }
}
