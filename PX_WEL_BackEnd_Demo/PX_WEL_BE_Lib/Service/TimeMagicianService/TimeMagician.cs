using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;


namespace PX_WEL_BE_Lib.Service.TimeMagicianService
{
    public class TimeMagician : ITimeMagician
    {

        public string UnixDatetimeStr { get; set; } = "1970-01-01 00:00:00";

        public string FormatYMDhmsInMSSQL { get; set; } = "yyyy-MM-dd HH:mm:ss";

        public string GetDateTimeNowStr()
        {
            DateTime Time = DateTime.Now;              // Use current time
            string FormatTime = "yyyy-MM-dd HH:mm:ss";
            String DateTimeStr = Time.ToString(FormatTime);
            return DateTimeStr;
        }




        public string GetEndOfDayTimeStr()
        {
            DateTime Time = DateTime.Now.Date;
            Time = Time + TimeSpan.FromHours(23) + TimeSpan.FromMinutes(59) + TimeSpan.FromSeconds(59);
            string FormatTime = "yyyy-MM-dd HH:mm:ss";
            String DateTimeStr = Time.ToString(FormatTime);
            return DateTimeStr;
        }


        public string DatetimeObjectToStr(DateTime TimeObject, String TimeFormat)
        {

            return TimeObject.ToString(TimeFormat, CultureInfo.InvariantCulture);
        }

        public DateTime DatetimeStrToObject(String TimeFormat)
        {
            //2021 - 12 - 31 00:00:00  
            //12 / 31 / 2021 12:00:00 AM
            return DateTime.Parse(TimeFormat, CultureInfo.InvariantCulture);
        }



    }
}
