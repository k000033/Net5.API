using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Text.RegularExpressions;

using System.Data.SqlClient;

using System.Reflection; // forloop on class

using System.Diagnostics;

using PX_WEL_BE_Lib.DAO.ParamsHandling;
using PX_WEL_BE_Lib.EnvControl;




namespace PX_WEL_BE_Lib.DAO.LoggingRecordDAO
{
    public class LoggingRecordDAO : ILoggingRecordDAO
    {

        
        ParamsModelBind TranModelToDict = new ParamsModelBind();
        SwitchEnv SwitchBtn = new SwitchEnv();





        public async Task MergePersonnelState<TModel>(SqlConnectionStringBuilder connectionStringBuilder, string sqlStr, TModel modelBinding)
        {
            Dictionary<string, string> dictTrans = TranModelToDict.TranModelToDict<string, string, TModel>(modelBinding);
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
                                string RowsValue = @$"('{dictTrans["UserId"]}', '{dictTrans["UserIdEn"]}', '{dictTrans["LoginTime"]}', '{dictTrans["IpArr"]}', '{dictTrans["LoginTime"]}', 1)";
                                sqlStr = string.Format(sqlStr, RowsValue);

                                dAdapter.InsertCommand = new SqlCommand(sqlStr, connection, sqlTrans);

                                // no use
                                //string RowsValue = @$"('{dictTrans["UserId"]}', '{dictTrans["UserIdEn"]}', '{dictTrans["LoginTime"]}', '{dictTrans["IpArr"]}', '{dictTrans["LoginTime"]}', 1)";
                                //dAdapter.InsertCommand.Parameters.AddWithValue("@RowsValue", RowsValue);
                                

                                await dAdapter.InsertCommand.ExecuteNonQueryAsync();

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


        }





        public async Task UpdatePersonnelClickTime(SqlConnectionStringBuilder connectionStringBuilder, string sqlStr, string UserId)
        {

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
                                
                                dAdapter.UpdateCommand = new SqlCommand(sqlStr, connection, sqlTrans);
                                dAdapter.UpdateCommand.Parameters.AddWithValue("@emp_id", UserId);


                                await dAdapter.UpdateCommand.ExecuteNonQueryAsync();

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
        }





    }
}
