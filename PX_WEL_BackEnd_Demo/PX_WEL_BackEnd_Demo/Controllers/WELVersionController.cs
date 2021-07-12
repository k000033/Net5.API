using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Net.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using PX_WEL_BE_Lib.Service.DbInfo;
using PX_WEL_BE_Lib.Service.WELAppInfoService;
using PX_WEL_BE_Lib.EnvControl;
using PX_WEL_BE_Lib.DAO.TableInfo;
using PX_WEL_BE_Lib.CURD.Select;

using Microsoft.Extensions.Logging;

namespace PX_WEL_BackEnd_Demo.Controllers
{
    [Produces(MediaTypeNames.Application.Json)]// Produces(.../json), this is necessary. //CORS---(7)
    [Consumes("application/x-www-form-urlencoded")]
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class WELVersionController : ControllerBase
    {

        
        // Customed Lib ---------------------------------------------------------------------------
        SQL_Select_WELVersion SqlSelectStr = new SQL_Select_WELVersion(new TableName());


        // Declare DI Zone---------------------------------------------------------------------------
        //static readonly HttpClient client = new HttpClient();  //Singleton寫法
        private readonly ISwitchEnv _switchEnv;
        private readonly IDbConn _dbConn;
        private readonly IAppInfoService _appInfoService;
        private readonly ILogger<WELLoggingController> _logger;





        public WELVersionController(ISwitchEnv switchEnv
            , IDbConn dbConn
            , IAppInfoService appInfoService
            , ILogger<WELLoggingController> logger)
        {

            this._switchEnv = switchEnv;

            this._dbConn = dbConn;

            this._appInfoService = appInfoService;

            this._logger = logger;
        }



        
        [Consumes(MediaTypeNames.Application.Json)] // if params's attribute is changed to [FromForm], this consume decorator should be removed.
        [HttpPost] // 關鍵字 ActionResult httpost postman
        public async Task<ActionResult<string>> PostWELVersionInfo()
        {

            string resJsonString = string.Empty;

            // 欠缺exception handling
            string dbConnStr = _switchEnv.GetDBConnStr(_switchEnv.IfProdEnv(), _switchEnv.IfProdConn());

            var sqlConnectionStringBuilder = _dbConn.GenerateSqlServerConnStringBuilder(dbConnStr);

            string sqlStr = SqlSelectStr.GetSEL_WELVersionIterationStr();

            resJsonString = await _appInfoService.SelectWELVersionAsync(sqlConnectionStringBuilder, sqlStr);

            //return System.Text.Json.JsonSerializer.Deserialize<string>(resJsonString);
            return resJsonString;

        }


    }
}
