using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Net.Http;
using System.Net.Mime;

using PX_WEL_BE_Lib.CURD.Select;
using PX_WEL_BE_Lib.DAO.TableInfo;
using PX_WEL_BE_Lib.Service.DbInfo;
using PX_WEL_BE_Lib.Service.TravelService;
using PX_WEL_BE_Lib.EnvControl;

namespace PX_WEL_BackEnd_Demo.Controllers
{

    [Produces("application/json")]// Produces(.../json), this is necessary. //CORS---(7)
    [Consumes("application/x-www-form-urlencoded")]
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class WELTravelController : ControllerBase
    {

        // Customed Lib ---------------------------------------------------------------------------
        SQL_Select_Travel SqlSelectStr = new SQL_Select_Travel(new TableName());

        // Declare DI Zone---------------------------------------------------------------------------
        //static readonly HttpClient client = new HttpClient();  //Singleton寫法
        //private readonly IHttpClientFactory _clientFactory;
        private readonly ISwitchEnv _switchEnv;
        private readonly IDbConn _dbConn;
        private readonly ITravelService _travelService;


        public WELTravelController( ISwitchEnv switchEnv, IDbConn dbConn, ITravelService travelService)
        {
            //this._clientFactory = clientFactory;

            this._switchEnv = switchEnv;

            this._dbConn = dbConn;

            this._travelService = travelService;



        }


        [Consumes(MediaTypeNames.Application.Json)]
        [HttpGet]
        public async Task<ActionResult<string>> GetTravelAllConditionInfo([FromQuery] bool atHomePageOrNot = true) // Async Suffix won't show on api
        {

            // 欠缺exception handling

            string dbConnStr = _switchEnv.GetDBConnStr(_switchEnv.IfProdEnv(), _switchEnv.IfProdConn());

            //
            var sqlConnectionStringBuilder = _dbConn.GenerateSqlServerConnStringBuilder(dbConnStr);

            string selectStr = SqlSelectStr.GetSEL_TravelAllConditionStr(atHomePageOrNot);

            string resJsonString = await _travelService.SelectTravelAllConditionAsync(sqlConnectionStringBuilder, selectStr);

            return resJsonString;
        }


    
        [Consumes(MediaTypeNames.Application.Json)]
        [HttpGet]
        public async Task<ActionResult<string>> GetTravelRefCodeInfo([FromQuery] string fileGenre = "pdf", [FromQuery] string fileExtension = "pdf") // Async Suffix won't show on api
        {

            // 欠缺exception handling

            string dbConnStr = _switchEnv.GetDBConnStr(_switchEnv.IfProdEnv(), _switchEnv.IfProdConn());

            var sqlConnectionStringBuilder = _dbConn.GenerateSqlServerConnStringBuilder(dbConnStr);

            string selectStr = SqlSelectStr.GetSEL_TravelRefCodeStr(fileGenre);

            string resJsonString = await _travelService.SelectTravelRefCodeAsync(sqlConnectionStringBuilder, selectStr);

            return resJsonString;
        }

    }

}
