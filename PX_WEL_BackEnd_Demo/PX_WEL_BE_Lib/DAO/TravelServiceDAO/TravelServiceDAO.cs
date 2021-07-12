using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;
using System.Reflection; // forloop on class
using System.Diagnostics;

using Newtonsoft.Json;

using PX_WEL_BE_Lib.DAO.ParamsHandling;
using PX_WEL_BE_Lib.Service.TimeMagicianService;

namespace PX_WEL_BE_Lib.DAO.TravelServiceDAO
{
    public class TravelServiceDAO : ITravelServiceDAO
    {
        ParamsModelBind TranModelToDict = new ParamsModelBind();
        TimeMagician TimeMagician = new TimeMagician();


        public Dictionary<string, KeyValuePair<string, string>> fileGenreDict { get; } = new Dictionary<string, KeyValuePair<string, string>>
        {
            ["pdf"] = new KeyValuePair<string, string>("pdf_file", "pdf_compress_type"),
            ["img"] = new KeyValuePair<string, string>("img_file", "img_compress_type")
        };

        

        public async Task<string> SelectTravelAllConditionAsync(SqlConnectionStringBuilder connectionStringBuilder, string sqlSelectStr)
        {
            Dictionary<string, string> travelObjDict = new Dictionary<string, string>();
            List<Dictionary<string, string>> resList = new List<Dictionary<string, string>>();

            try
            {



                using (SqlConnection connection = new SqlConnection(connectionStringBuilder.DataSource))
                using (SqlDataAdapter dAdapter = new SqlDataAdapter(sqlSelectStr, connection))
                {
                    //SqlDataAdapter dAdapter = new SqlDataAdapter();
                    //dAdapter.SelectCommand = new SqlCommand(sqlSelectStr, connection);

                    dAdapter.SelectCommand.CommandTimeout = 15;
                    DataSet dSet = new DataSet();
                    dAdapter.Fill(dSet);
                    

                    
                    foreach (DataRow dRow in dSet.Tables[0].Rows)
                    {

                        var ImgFile = string.Empty;
                        try
                        {
                            ImgFile = Convert.ToBase64String((byte[])dRow["img_file"]);
                            //ImgFile = Convert.ToBase64String(dRow.Field<byte[]>("img_file"));
                        }
                        catch (InvalidCastException ex)
                        {
                            ImgFile = "0";
                        }


                        travelObjDict = new Dictionary<string, string>
                        {
                            ["Guid_Travel"] = dRow["guid_travel"].ToString(),
                            ["CategoryCode"] = dRow["category_code"].ToString(),
                            ["RouteSerial"] = dRow["route_serial"].ToString(),
                            ["RouteIntro"] = dRow["route_intro"].ToString(),
                            ["RouteName"] = dRow["route_name"].ToString(),
                            ["ImgFile"] = ImgFile //Unable to cast object of type 'System.String' to type 'System.Byte[]'.
                        };


                        resList.Add(travelObjDict);


                    }


                }


            }

            catch (SqlException ex)
            {
                if (ex.Number == -2 && (Regex.IsMatch(ex.Message, "[tT]ime-{0,1}out")))
                {
                    Console.WriteLine("Command Timeout occurred");

                }
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            await Task.CompletedTask;
            return JsonConvert.SerializeObject(resList);

        }




        public async Task<string> SelectTravelRefCodeAsync(SqlConnectionStringBuilder connectionStringBuilder, string sqlSelectStr, string fileGenre = "pdf")
        {
            Dictionary<string, string> travelObjDict = new Dictionary<string, string>();
            List<Dictionary<string, string>> resList = new List<Dictionary<string, string>>();

            try
            {

                using (SqlConnection connection = new SqlConnection(connectionStringBuilder.DataSource))
                using (SqlDataAdapter dAdapter = new SqlDataAdapter(sqlSelectStr, connection))
                {

                    //SqlDataAdapter dAdapter = new SqlDataAdapter();
                    //dAdapter.SelectCommand = new SqlCommand(sqlSelectStr, connection);

                    dAdapter.SelectCommand.CommandTimeout = 15;
                    DataSet dSet = new DataSet();
                    dAdapter.Fill(dSet);


                    string fileGenreNameToFrontEnd = fileGenre.ToUpper()[0] + fileGenre.Substring(1);

                    foreach (DataRow dRow in dSet.Tables[0].Rows)
                    {
                        travelObjDict = new Dictionary<string, string>
                        {

                            ["RefCode"] = dRow["ref_code"].ToString(),
                            [$"{fileGenreNameToFrontEnd}CompressType"] = dRow[this.fileGenreDict[fileGenre].Value].ToString()

                        };


                        resList.Add(travelObjDict);


                    }

                }


            }

            catch (SqlException ex)
            {
                if (ex.Number == -2 && (Regex.IsMatch(ex.Message, "[tT]ime-{0,1}out")))
                {
                    Console.WriteLine("Command Timeout occurred");

                }
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            await Task.CompletedTask;
            return JsonConvert.SerializeObject(resList);

        }


    }
}
