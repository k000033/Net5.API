using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Net.Http;
using System.Net.Mime;

using PX_WEL_BE_Lib.Service.DbInfo;
using PX_WEL_BE_Lib.DAO.TableInfo;
using PX_WEL_BE_Lib.CURD.Merge;
using PX_WEL_BE_Lib.CURD.Update;
using PX_WEL_BE_Lib.Service.LoggingRecordService;
using PX_WEL_BE_Lib.EnvControl;
using PX_WEL_BE_Model.AccModel;


namespace PX_WEL_BackEnd_Demo.Controllers
{



    [Produces(MediaTypeNames.Application.Json)]// Produces(.../json), this is necessary. //CORS---(7)
    [Consumes("application/x-www-form-urlencoded")]
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class WELLoggingController : ControllerBase
    {

        // Customed Lib ---------------------------------------------------------------------------
        SQL_Update_Log SqlUpdateStr = new SQL_Update_Log(new TableName());
        SQL_Merge_Log SqlMergeStr = new SQL_Merge_Log(new TableName());



        // Declare DI Zone---------------------------------------------------------------------------
        //static readonly HttpClient client = new HttpClient();  //Singleton寫法
        private readonly ILoggingRecordService _loggingRecordService;
        private readonly ISwitchEnv _switchEnv;
        private readonly IDbConn _dbConn;
        private readonly IHttpContextAccessor _httpContextAccessor;




        public WELLoggingController(ISwitchEnv switchEnv
            , IDbConn dbConn
            , IHttpContextAccessor httpContextAccessor
            , ILoggingRecordService loggingRecordService)
        {

            this._switchEnv = switchEnv;

            this._dbConn = dbConn;

            this._httpContextAccessor = httpContextAccessor;

            this._loggingRecordService = loggingRecordService;

        }




        
        [Consumes(MediaTypeNames.Application.Json)] // if params's attribute is changed to [FromForm], this consume decorator should be removed.
        [HttpPost] // 關鍵字 ActionResult httpost postman
        public async Task PostPersonnelStateChangeInfo([FromBody] AccAuthCtl form)
        {
            // 欠缺exception handling
            string dbConnStr = _switchEnv.GetDBConnStr(_switchEnv.IfProdEnv(), _switchEnv.IfProdConn());

            var sqlConnectionStringBuilder = _dbConn.GenerateSqlServerConnStringBuilder(dbConnStr);

            string sqlStr = SqlMergeStr.GetMERG_PersonnelInfoStr();

            // 取得客戶IP的方式
            form.IpArr = await _loggingRecordService.GetPureIP4Address(_httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString());
            await _loggingRecordService.MergePersonnelState(sqlConnectionStringBuilder, sqlStr, form);


        }


        
        [Consumes(MediaTypeNames.Application.Json)] // if params's attribute is changed to [FromForm], this consume decorator should be removed.
        [HttpPut] // 關鍵字 ActionResult httpost postman
        public async Task PutPersonnelClickTimeInfo([FromBody] string UserId)
        {
            // 欠缺exception handling
            string dbConnStr = _switchEnv.GetDBConnStr(_switchEnv.IfProdEnv(), _switchEnv.IfProdConn());

            var sqlConnectionStringBuilder = _dbConn.GenerateSqlServerConnStringBuilder(dbConnStr);

            string sqlStr = SqlUpdateStr.GetUPD_PersonnelClickTimeStr();

            
            await _loggingRecordService.UpdatePersonnelClickTime(sqlConnectionStringBuilder, sqlStr, UserId);


        }

    }
}
