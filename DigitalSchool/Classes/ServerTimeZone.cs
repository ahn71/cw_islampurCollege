using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DS.Classes
{
    public class ServerTimeZone
    {
        public static bool IsBDTZone()
        {
            TimeZone TZ = TimeZone.CurrentTimeZone;
            string getZone = TZ.StandardName;
            if (getZone.Equals("Bangladesh Standard Time")) return true;
            else return false;
        }

        public static string GetBangladeshNowDate()
        {
            var now = DateTime.Now; // Current date/time
            var utcNow = now.ToUniversalTime(); // Converted utc time
            var otherTimezone = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time"); // Bangladeshi time zone
            var bdDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, otherTimezone); // now converted time zone

            return bdDateTime.ToString("dd-MM-yyyy");

        }

        public static string GetBangladeshNowDate(string Format)
        {
            var now = DateTime.Now; // Current date/time
            var utcNow = now.ToUniversalTime(); // Converted utc time
            var otherTimezone = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time"); // Bangladeshi time zone
            var bdDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, otherTimezone); // now converted time zone

            return bdDateTime.ToString(Format);

        }
        //---------- For Previous Day--------------
        public static string GetBangladeshPreviousDate()
        {
            var now = DateTime.Now.AddDays(-1); // Current date/time
            var utcNow = now.ToUniversalTime(); // Converted utc time
            var otherTimezone = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time"); // Bangladeshi time zone
            var bdDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, otherTimezone); // now converted time zone

            return bdDateTime.ToString("dd-MM-yyyy");

        }

        public static string GetBangladeshPreviousDate(string Format)
        {
            var now = DateTime.Now.AddDays(-1); // Current date/time
            var utcNow = now.ToUniversalTime(); // Converted utc time
            var otherTimezone = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time"); // Bangladeshi time zone
            var bdDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, otherTimezone); // now converted time zone

            return bdDateTime.ToString(Format);

        }
        //----------------------------
    }
}