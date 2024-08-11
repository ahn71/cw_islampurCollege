using System;
using System.Web.UI.WebControls;

namespace DS.DAL.AdviitDAL
{
    public class adviitDateTime
    {
        public static string GetDateLong(DateTime OnDate)
        {
            try
            {
                string str1 = OnDate.Day.ToString();
                string str2 = OnDate.Month.ToString();
                string str3 = OnDate.Year.ToString();
                string str4 = str2.Length != 1 ? str2 : "0" + str2;
                string str5 = str1.Length != 1 ? str4 + str1 : str4 + "0" + str1;
                return str3 + str5;
            }
            catch
            {
                return "";
            }
        }

        public static DateTime GetDateFromString(string stDate, bool IsAmericanStandard)
        {
            string[] strArray = new string[2]
      {
        "",
        ""
      };
            try
            {
                stDate = stDate.Trim();
                char[] chArray = stDate.ToCharArray();
                string str = "";
                for (int index = 0; index < stDate.Length; ++index)
                {
                    if (Convert.ToInt32(chArray[index]) >= 48 && Convert.ToInt32(chArray[index]) <= 57)
                        str = str + (object)chArray[index];
                    else if (str.Substring(str.Length - 1, 1).CompareTo("/") != 0)
                        str = str + "/";
                }
                strArray = str.Split('/');
                return !IsAmericanStandard ? new DateTime(adviitScripting.valueInt(strArray[2]), adviitScripting.valueInt(strArray[1]), adviitScripting.valueInt(strArray[0])) : new DateTime(adviitScripting.valueInt(strArray[2]), adviitScripting.valueInt(strArray[0]), adviitScripting.valueInt(strArray[1]));
            }
            catch
            {
                try
                {
                    return new DateTime(adviitScripting.valueInt(strArray[2]), adviitScripting.valueInt(strArray[1]), adviitScripting.valueInt(strArray[0]));
                }
                catch
                {
                    return DateTime.Now;
                }
            }
        }

        public static DateTime GetDateTimeFromString(string stDate, bool IsAmericanStandard)
        {
            string[] strArray1 = new string[2]
      {
        "",
        ""
      };
            try
            {
                stDate = stDate.Trim();
                char[] chArray = stDate.ToCharArray();
                string str = "";
                for (int index = 0; index < stDate.Length; ++index)
                {
                    if (Convert.ToInt32(chArray[index]) >= 48 && Convert.ToInt32(chArray[index]) <= 57)
                        str = str + (object)chArray[index];
                    else if (str.Substring(str.Length - 1, 1).CompareTo("/") != 0)
                        str = str + "/";
                }
                string[] strArray2 = str.Split('/');
                return !IsAmericanStandard ? new DateTime(adviitScripting.valueInt(strArray2[2]), adviitScripting.valueInt(strArray2[1]), adviitScripting.valueInt(strArray2[0]), adviitScripting.valueInt(strArray2[3]), adviitScripting.valueInt(strArray2[4]), 0, DateTimeKind.Local) : new DateTime(adviitScripting.valueInt(strArray2[2]), adviitScripting.valueInt(strArray2[0]), adviitScripting.valueInt(strArray2[1]), adviitScripting.valueInt(strArray2[3]), adviitScripting.valueInt(strArray2[4]), 0, DateTimeKind.Local);
            }
            catch
            {
                return DateTime.Now;
            }
        }

        public static string GetShortDateFromString(string stDate, bool IsAmericanStandard)
        {
            string[] strArray = new string[2]
      {
        "",
        ""
      };
            try
            {
                char[] chArray = stDate.ToCharArray();
                string str = "";
                for (int index = 0; index < stDate.Length; ++index)
                {
                    if (Convert.ToInt32(chArray[index]) >= 48 && Convert.ToInt32(chArray[index]) <= 57)
                        str = str + (object)chArray[index];
                    else if (str.Substring(str.Length - 1, 1).CompareTo("/") != 0)
                        str = str + "/";
                }
                strArray = str.Split('/');
                return (!IsAmericanStandard ? new DateTime(adviitScripting.valueInt(strArray[2]), adviitScripting.valueInt(strArray[1]), adviitScripting.valueInt(strArray[0])) : new DateTime(adviitScripting.valueInt(strArray[2]), adviitScripting.valueInt(strArray[0]), adviitScripting.valueInt(strArray[1]))).ToShortDateString();
            }
            catch
            {
                try
                {
                    return new DateTime(adviitScripting.valueInt(strArray[2]), adviitScripting.valueInt(strArray[0]), adviitScripting.valueInt(strArray[1])).ToShortDateString();
                }
                catch
                {
                    return DateTime.Now.ToShortDateString();
                }
            }
        }

        public static DateTime GetFormatedDate(string DateValue)
        {
            DateTime now = DateTime.Now;
            try
            {
                string s1 = "";
                int length = DateValue.IndexOf("/");
                string s2 = DateValue.Substring(0, length);
                int startIndex1 = length + 1;
                int num1 = DateValue.IndexOf("/", startIndex1);
                string s3 = DateValue.Substring(startIndex1, num1 - startIndex1);
                try
                {
                    int startIndex2 = num1 + 1;
                    int num2 = DateValue.IndexOf(" ", startIndex2);
                    s1 = DateValue.Substring(startIndex2, num2 - startIndex2);
                }
                catch
                {
                    try
                    {
                        s1 = DateValue.Substring(DateValue.Length - 4, 4);
                    }
                    catch
                    {
                    }
                }
                return new DateTime(int.Parse(s1), int.Parse(s2), int.Parse(s3));
            }
            catch
            {
                return now;
            }
        }

        public static string GetFormatedDateFromDateLong(string DateLong)
        {
            try
            {
                string str1 = DateLong.Substring(0, 4);
                string str2 = DateLong.Substring(4, 2);
                return DateLong.Substring(6) + "/" + str2 + "/" + str1;
            }
            catch
            {
                return DateLong;
            }
        }

        public static string GetFormatedTimeFromTimeLong(string TimeLong)
        {
            try
            {
                string str1 = "";
                string StringValue = TimeLong.Substring(0, 2);
                string str2 = TimeLong.Substring(2, 2);
                if (TimeLong.Length > 4)
                    str1 = TimeLong.Substring(4);
                int num = adviitScripting.valueInt(StringValue);
                string str3;
                if (num > 11)
                {
                    num -= 12;
                    str3 = " PM";
                }
                else
                    str3 = " AM";
                string str4 = num.ToString().Length != 2 ? "0" + num.ToString() : num.ToString();
                if (TimeLong.Length <= 4)
                    return str4 + ":" + str2 + str3;
                return str4 + ":" + str2 + ":" + str1 + str3;
            }
            catch
            {
                return TimeLong;
            }
        }

        public static int SubtractTime(string Time1, string Time2)
        {
            try
            {
                if (Time1.Length == 3)
                    Time1 = "0" + Time1;
                if (Time2.Length == 3)
                    Time2 = "0" + Time2;
                int num1 = Convert.ToInt32(Time1.Substring(0, 2));
                int num2 = Convert.ToInt32(Time1.Substring(2));
                int num3 = Convert.ToInt32(Time2.Substring(0, 2));
                int num4 = Convert.ToInt32(Time2.Substring(2));
                int num5 = num1 - num3;
                int num6 = num2 - num4;
                if (num6 < 0)
                {
                    num6 += 60;
                    --num5;
                }
                return num5 * 60 + num6;
            }
            catch
            {
                return 0;
            }
        }

        public static string GetDateInString()
        {
            return DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
        }

        public static string GetDateLongInString(DateTime OnDate)
        {
            try
            {
                string str1 = OnDate.Day.ToString();
                string str2 = OnDate.Month.ToString();
                string str3 = OnDate.Year.ToString();
                string str4 = str2.Length != 1 ? str2 : "0" + str2;
                string str5 = str1.Length != 1 ? str4 + str1 : str4 + "0" + str1;
                return str3 + str5;
            }
            catch
            {
                return "";
            }
        }

        public static string GetCurrentMonthName(bool ShowYear)
        {
            try
            {
                string str = "";
                switch (int.Parse(DateTime.Now.Month.ToString()))
                {
                    case 1:
                        str = "January";
                        break;
                    case 2:
                        str = "February";
                        break;
                    case 3:
                        str = "March";
                        break;
                    case 4:
                        str = "April";
                        break;
                    case 5:
                        str = "May";
                        break;
                    case 6:
                        str = "June";
                        break;
                    case 7:
                        str = "July";
                        break;
                    case 8:
                        str = "August";
                        break;
                    case 9:
                        str = "September";
                        break;
                    case 10:
                        str = "October";
                        break;
                    case 11:
                        str = "November";
                        break;
                    case 12:
                        str = "December";
                        break;
                }
                if (ShowYear)
                    return str + "," + DateTime.Now.Year.ToString();
                else
                    return str;
            }
            catch
            {
                return "";
            }
        }

        public static string[] getMonthRange(int year)
        {
            string[] strArray = new string[12];
            try
            {
                int num = year % 4;
                strArray[0] = string.Concat(new object[4]
        {
          (object) "January ",
          (object) year,
          (object) " - February ",
          (object) year
        });
                strArray[1] = string.Concat(new object[4]
        {
          (object) "February ",
          (object) year,
          (object) " - March ",
          (object) year
        });
                strArray[2] = string.Concat(new object[4]
        {
          (object) "March ",
          (object) year,
          (object) " - April ",
          (object) year
        });
                strArray[3] = string.Concat(new object[4]
        {
          (object) "April ",
          (object) year,
          (object) " - May ",
          (object) year
        });
                strArray[4] = string.Concat(new object[4]
        {
          (object) "May ",
          (object) year,
          (object) " - June ",
          (object) year
        });
                strArray[5] = string.Concat(new object[4]
        {
          (object) "June ",
          (object) year,
          (object) " - July ",
          (object) year
        });
                strArray[6] = string.Concat(new object[4]
        {
          (object) "July ",
          (object) year,
          (object) " - August ",
          (object) year
        });
                strArray[7] = string.Concat(new object[4]
        {
          (object) "August ",
          (object) year,
          (object) " - Septembar ",
          (object) year
        });
                strArray[8] = string.Concat(new object[4]
        {
          (object) "Septembar ",
          (object) year,
          (object) " - October ",
          (object) year
        });
                strArray[9] = string.Concat(new object[4]
        {
          (object) "October ",
          (object) year,
          (object) " - November ",
          (object) year
        });
                strArray[10] = string.Concat(new object[4]
        {
          (object) "November ",
          (object) year,
          (object) " - December ",
          (object) year
        });
                strArray[11] = string.Concat(new object[4]
        {
          (object) "December ",
          (object) year,
          (object) " - February ",
          (object) year
        });
                return strArray;
            }
            catch
            {
                return strArray;
            }
        }

        public static string[] getYearMonths(DateTime DateFrom, DateTime DateTo)
        {
            string[] strArray = new string[12];
            try
            {
                int index = 0;
                strArray = new string[DateTo.Subtract(DateFrom).Days / 30 + 2];
                while (DateFrom < DateTo)
                {
                    strArray[index] = adviitDateTime.GetMonthName(DateFrom.Month) + (object)" " + (string)(object)DateFrom.Year;
                    DateFrom = DateFrom.AddMonths(1);
                    ++index;
                    if (index > 200)
                        break;
                }
                return strArray;
            }
            catch
            {
                return strArray;
            }
        }

        public static string GetMonthName(int Month)
        {
            try
            {
                string str = "";
                switch (Month)
                {
                    case 1:
                        str = "January";
                        break;
                    case 2:
                        str = "February";
                        break;
                    case 3:
                        str = "March";
                        break;
                    case 4:
                        str = "April";
                        break;
                    case 5:
                        str = "May";
                        break;
                    case 6:
                        str = "June";
                        break;
                    case 7:
                        str = "July";
                        break;
                    case 8:
                        str = "August";
                        break;
                    case 9:
                        str = "September";
                        break;
                    case 10:
                        str = "October";
                        break;
                    case 11:
                        str = "November";
                        break;
                    case 12:
                        str = "December";
                        break;
                }
                return str;
            }
            catch
            {
                return "";
            }
        }

        public static string[] getWeeklyRange(DateTime From, DateTime To, string WeekBeginningDay, bool useAmericanStandard)
        {
            string[] strArray = new string[12];
            try
            {
                int index1 = 0;
                strArray = new string[To.Subtract(From).Days / 7 + 1];
                for (int index2 = 1; index2 <= 7 && WeekBeginningDay.ToLower().CompareTo(((object)From.DayOfWeek).ToString().ToLower()) != 0; ++index2)
                    From = From.AddDays(1.0);
                while (From <= To)
                {
                    strArray[index1] = !useAmericanStandard ? From.ToString("dd/MM/yyyy") + " - " + From.AddDays(6.0).ToString("dd/MM/yyyy") : From.ToShortDateString() + " - " + From.AddDays(6.0).ToShortDateString();
                    From = From.AddDays(7.0);
                    ++index1;
                    if (index1 > 200)
                        break;
                }
                return strArray;
            }
            catch
            {
                return strArray;
            }
        }

        public static string getLastWeek(string WeekBeginningDay, bool useAmericanStandard)
        {
            string str = "";
            try
            {
                DateTime dateTime = DateTime.Now.AddDays(-14.0);
                DateTime.Now.AddDays(-6.0);
                for (int index = 1; index <= 7; ++index)
                {
                    if (WeekBeginningDay.ToLower().CompareTo(((object)dateTime.DayOfWeek).ToString().ToLower()) == 0)
                    {
                        if (useAmericanStandard)
                            return dateTime.ToShortDateString() + " - " + dateTime.AddDays(6.0).ToShortDateString();
                        else
                            return dateTime.ToString("dd/MM/yyyy") + " - " + dateTime.AddDays(6.0).ToString("dd/MM/yyyy");
                    }
                    else
                        dateTime = dateTime.AddDays(1.0);
                }
                return str;
            }
            catch
            {
                return str;
            }
        }

        public static string getCurrentWeek(string WeekBeginningDay, bool useAmericanStandard)
        {
            string str = "";
            try
            {
                DateTime dateTime = DateTime.Now.AddDays(-7.0);
                DateTime.Now.AddDays(8.0);
                for (int index = 1; index <= 7; ++index)
                {
                    if (WeekBeginningDay.ToLower().CompareTo(((object)dateTime.DayOfWeek).ToString().ToLower()) == 0)
                    {
                        if (useAmericanStandard)
                            return dateTime.ToShortDateString() + " - " + dateTime.AddDays(6.0).ToShortDateString();
                        else
                            return dateTime.ToString("dd/MM/yyyy") + " - " + dateTime.AddDays(6.0).ToString("dd/MM/yyyy");
                    }
                    else
                        dateTime = dateTime.AddDays(1.0);
                }
                return str;
            }
            catch
            {
                return str;
            }
        }

        public static int GetDateDiff(string FromDate, string ToDate)
        {
            try
            {
                DateTime dateTime = new DateTime();
                DateTime formatedDate = adviitDateTime.GetFormatedDate(FromDate);
                return adviitDateTime.GetFormatedDate(ToDate).Subtract(formatedDate).Days;
            }
            catch
            {
                return 0;
            }
        }

        public static int GetDateDiffFromCurrentDate(DateTime ddt)
        {
            try
            {
                int days = DateTime.Now.Subtract(ddt).Days;
                if (days == 0 && ddt.Day > DateTime.Now.Day)
                    return -1;
                else
                    return days;
            }
            catch
            {
                return 0;
            }
        }

        public static long GetDecimalFromDate(string fromDate)
        {
            try
            {
                char[] chArray = fromDate.ToCharArray();
                string StringValue = "";
                foreach (char ch in chArray)
                {
                    int num = Convert.ToInt32(ch);
                    if (num > 47 && num < 58)
                        StringValue = StringValue + ch.ToString();
                }
                if (fromDate.ToLower().IndexOf("pm") > -1)
                    return adviitScripting.valueLong(StringValue) + 1L;
                else
                    return adviitScripting.valueLong(StringValue);
            }
            catch
            {
                return 0L;
            }
        }

        public static int getMonthDays(int month)
        {
            try
            {
                if (month == 1 || month == 3 || (month == 5 || month == 7) || (month == 8 || month == 10 || month == 12))
                    return 31;
                if (month == 4 || month == 6 || (month == 9 || month == 11) || month != 2)
                    return 30;
                return DateTime.Now.Year % 4 == 0 ? 29 : 28;
            }
            catch
            {
                return 30;
            }
        }

        public static int getMonthDays(string monthName, int year)
        {
            try
            {
                monthName = monthName.ToLower();
                switch (monthName)
                {
                    case "january":
                    case "march":
                    case "may":
                    case "july":
                    case "august":
                    case "october":
                    case "december":
                        return 31;
                    case "february":
                        return DateTime.Now.Year % 4 == 0 ? 29 : 28;
                    case "april":
                    case "june":
                    case "september":
                    case "november":
                        return 30;
                    default:
                        return 30;
                }
            }
            catch
            {
                return 0;
            }
        }

        public static int getMonthNumber(string monthName)
        {
            try
            {
                monthName = monthName.ToLower();
                switch (monthName)
                {
                    case "january":
                        return 1;
                    case "february":
                        return 2;
                    case "march":
                        return 3;
                    case "april":
                        return 4;
                    case "may":
                        return 5;
                    case "june":
                        return 6;
                    case "july":
                        return 7;
                    case "august":
                        return 8;
                    case "september":
                        return 9;
                    case "october":
                        return 10;
                    case "november":
                        return 11;
                    case "december":
                        return 12;
                    default:
                        return 0;
                }
            }
            catch
            {
                return 0;
            }
        }

        public static DateTime getMonthFirstDay(DateTime stDate)
        {
            DateTime dateTime = new DateTime();
            try
            {
                return new DateTime(stDate.Year, stDate.Month, 1);
            }
            catch (Exception ex)
            {
                return stDate;
            }
        }

        public static DateTime getMonthFirstDay(string FromMonthYear)
        {
            DateTime dateTime = new DateTime();
            try
            {
                string monthName = FromMonthYear.Substring(0, FromMonthYear.IndexOf(" ")).Trim();
                int year = int.Parse(adviitScripting.getDecimalOnly(FromMonthYear));
                int monthNumber = adviitDateTime.getMonthNumber(monthName);
                adviitDateTime.getMonthDays(monthNumber);
                return new DateTime(year, monthNumber, 1);
            }
            catch (Exception ex)
            {
                return dateTime;
            }
        }

        public static DateTime getMonthEndDay(string FromMonthYear)
        {
            DateTime dateTime = new DateTime();
            try
            {
                string monthName = FromMonthYear.Substring(0, FromMonthYear.IndexOf(" ")).Trim();
                int year = int.Parse(adviitScripting.getDecimalOnly(FromMonthYear));
                int monthNumber = adviitDateTime.getMonthNumber(monthName);
                int monthDays = adviitDateTime.getMonthDays(monthNumber);
                return new DateTime(year, monthNumber, monthDays);
            }
            catch (Exception ex)
            {
                return dateTime;
            }
        }

        public static DateTime getMonthEndDay(DateTime stDate)
        {
            DateTime dateTime = new DateTime();
            try
            {
                int monthDays = adviitDateTime.getMonthDays(stDate.Month);
                return new DateTime(stDate.Year, stDate.Month, monthDays);
            }
            catch (Exception ex)
            {
                return stDate;
            }
        }

        public static string addTwoTime(string FirstTime, string SecondTime)
        {
            try
            {
                int num1 = 0;
                int num2 = 0;
                if (FirstTime.Length > 2)
                {
                    num1 = adviitScripting.valueInt(FirstTime);
                    num2 = adviitScripting.valueInt(FirstTime.Substring(FirstTime.IndexOf(':') + 1));
                }
                int num3 = 0;
                int num4 = 0;
                if (SecondTime.Length > 2)
                {
                    num3 = adviitScripting.valueInt(SecondTime);
                    num4 = adviitScripting.valueInt(SecondTime.Substring(SecondTime.IndexOf(':') + 1));
                }
                return (num1 + num3 + (num2 + num4) / 60).ToString() + ":" + ((num2 + num4) % 60).ToString();
            }
            catch
            {
                return "";
            }
        }

        public static string roundHours(string Time)
        {
            try
            {
                if (Time.Length < 3)
                    return "0";
                string str = Time.Substring(Time.IndexOf(':') + 1);
                Time = Time.Substring(0, Time.IndexOf(':'));
                if (Time.Length <= 0)
                    return "0";
                if (str == "0")
                    return Time;
                else
                    return (int.Parse(Time) + 1).ToString();
            }
            catch
            {
                return "0";
            }
        }

        public static string addHoursMinutes(string Hours1, string Hours2)
        {
            try
            {
                int num1 = adviitScripting.valueInt(Hours1) + adviitScripting.valueInt(Hours2);
                int num2 = adviitScripting.valueInt(Hours1.Substring(Hours1.IndexOf(":") + 1)) + adviitScripting.valueInt(Hours2.Substring(Hours2.IndexOf(":") + 1));
                return (string)(object)(num1 + num2 / 60) + (object)":" + (string)(object)(num2 % 60);
            }
            catch
            {
                return "";
            }
        }

        public static string getHourMin(int Seconds)
        {
            try
            {
                return (Seconds / 3600).ToString() + (object)':' + (Seconds / 60 % 60).ToString();
            }
            catch
            {
                return "0:0";
            }
        }

        public static string convertToHours(double Seconds)
        {
            try
            {
                return Math.Ceiling(Seconds / 3600.0).ToString();
            }
            catch
            {
                return "0";
            }
        }

        public static int convertToHours(long Seconds)
        {
            try
            {
                return (int)Math.Ceiling((double)Seconds / 3600.0);
            }
            catch
            {
                return 0;
            }
        }

        public static string convertToHours(string Seconds)
        {
            try
            {
                return Math.Ceiling(adviitScripting.valueDouble(Seconds) / 3600.0).ToString();
            }
            catch
            {
                return "0";
            }
        }

        public static string getHourMin(string Seconds)
        {
            try
            {
                int num = adviitScripting.valueInt(Seconds);
                return (num / 3600).ToString() + (object)':' + (num / 60 % 60).ToString();
            }
            catch
            {
                return "0:0";
            }
        }

        public static string getHourMinFromMinute(string Minutes)
        {
            try
            {
                int num = adviitScripting.valueInt(Minutes);
                return (num / 60).ToString() + (object)':' + (num % 60).ToString();
            }
            catch
            {
                return "0:0";
            }
        }

        public static DateTime getDateFromDropdown(DropDownList dlDay, DropDownList dlMonth, DropDownList dlYear)
        {
            try
            {
                return new DateTime(int.Parse(((object)dlYear.SelectedValue).ToString()), int.Parse(((object)dlMonth.SelectedValue).ToString()), int.Parse(((object)dlDay.SelectedValue).ToString()));
            }
            catch
            {
                return DateTime.Now;
            }
        }
    }
}