using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PX_WEL_BE_Lib.Service.TimeMagicianService
{
    interface ITimeMagician
    {

        string UnixDatetimeStr { get; set; }

        string FormatYMDhmsInMSSQL { get; set; }

        string GetDateTimeNowStr();
        
        string GetEndOfDayTimeStr();


        string DatetimeObjectToStr(DateTime TimeObject, String TimeFormat);

        DateTime DatetimeStrToObject(String TimeFormat);
    }
}
