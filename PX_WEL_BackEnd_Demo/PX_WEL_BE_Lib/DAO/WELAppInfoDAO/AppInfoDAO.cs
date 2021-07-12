using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Data;
using System.Reflection;
using System.Data.SqlClient;



using Newtonsoft.Json;

using PX_WEL_BE_Lib.EnvControl;

namespace PX_WEL_BE_Lib.DAO.WELAppInfoDAO
{
    public class AppInfoDAO : IAppInfoDAO
    {
        SwitchEnv SwitchBtn = new SwitchEnv();



        public async Task<string> SelectWELVersionAsync(SqlConnectionStringBuilder connectionStringBuilder, string sqlStr)
        {
            string dr = "BadConnectionVersion";
            try
            {

                using (SqlConnection connection = new SqlConnection(connectionStringBuilder.DataSource))
                using (SqlDataAdapter dAdapter = new SqlDataAdapter())
                {
                    connection.Open();
                    using (SqlTransaction sqlTrans = connection.BeginTransaction())
                    {

                        for (int i = 0; i < SwitchBtn.TryTimesForDeadLock; i++)
                        {
                            try
                            {
                                dAdapter.SelectCommand = new SqlCommand(sqlStr, connection, sqlTrans);
                                dAdapter.SelectCommand.CommandTimeout = 15;
                                DataSet dSet = new DataSet();
                                dAdapter.Fill(dSet);

                                foreach (DataRow dRow in dSet.Tables[0].Rows)
                                {
                                    dr = dRow[0].ToString();
                                }

                                sqlTrans.Commit();

                                break;

                            }
                            catch (SqlException ex) when (ex.Number == 1205) // deadlock
                            {
                                sqlTrans.Rollback();
                                Console.WriteLine($"{MethodBase.GetCurrentMethod().Name}  is deadlocking.");

                                await Task.Delay(1000);

                            }
                            catch (SqlException ex) when (ex.Number == -2) // Command Timeout
                            {
                                sqlTrans.Rollback();
                                Console.WriteLine($"{MethodBase.GetCurrentMethod().Name}  is Command Timeout.");

                                await Task.Delay(1000);

                            }
                            catch (SqlException ex)
                            {
                                sqlTrans.Rollback();
                                Console.WriteLine($"{MethodBase.GetCurrentMethod().Name}  is SqlException.");
                                await Task.Delay(1000);

                            }

                        }

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

            return await Task.FromResult(JsonConvert.SerializeObject(dr));
        }




    }
}
