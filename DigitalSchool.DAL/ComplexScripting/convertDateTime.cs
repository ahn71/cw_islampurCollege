using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace DS.DAL.ComplexScripting
{
    public class convertDateTime
    {
        public static DateTime getDateTime(string setDateTime, string setDateFormat)
        {
            return DateTime.ParseExact(setDateTime, setDateFormat, (IFormatProvider)CultureInfo.CurrentCulture, DateTimeStyles.None);
        }

        public static DateTime getCertainCulture(string setDate)
        {
            return Convert.ToDateTime(setDate, (IFormatProvider)CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat);
        }
        public static DateTime? DMYtoYMD_alowNull(string setDate)
        {
            if (setDate == "")
                return null;
            else
            {
                string[] d = setDate.Split('-');
                return DateTime.ParseExact(d[2] + "-" + d[1] + "-" + d[0], "yyyy-MM-dd HH:mm tt", null);
            }


        }
        public static DateTime DMYtoYMD(string setDate)
        {
           
                string[] d = setDate.Split('-');
                return DateTime.Parse(d[2] + "-" + d[1] + "-" + d[0]);
            
           


        }
        public static string sDMYtoYMD(string setDate)
        {

            string[] d = setDate.Split('-');
            return d[2] + "-" + d[1] + "-" + d[0];




        }
    }
}
