using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.BLL
{
   public static  class TimeZoneBD
    {
       public static string getCurrentTimeBD(string DateFormat) 
       {
           DateTime utcTime = DateTime.UtcNow;
           TimeZoneInfo BdZone = TimeZoneInfo.FindSystemTimeZoneById("Bangladesh Standard Time");
           DateTime localDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, BdZone);
           return localDateTime.ToString(DateFormat); 
       }
        public static DateTime getCurrentTimeBD()
        {
            DateTime utcTime = DateTime.UtcNow;
            TimeZoneInfo BdZone = TimeZoneInfo.FindSystemTimeZoneById("Bangladesh Standard Time");
            DateTime localDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, BdZone);
            return localDateTime;
        }
    }
}
