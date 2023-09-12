using System;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace DS.DAL.AdviitDAL
{
    public class adviitScripting
    {
        public static bool IsValidEmailAddress(string sEmail)
        {
            if (sEmail == null)
                return false;
            int startIndex1 = sEmail.IndexOf("@");
            int num1 = sEmail.Length - startIndex1;
            if (startIndex1 < 2 || num1 < 7 || sEmail.IndexOf(".", startIndex1 + 1) == -1)
                return false;
            int num2 = sEmail.IndexOf(".", startIndex1);
            switch (startIndex1 - num2)
            {
                case -1:
                case 1:
                    return false;
                default:
                    int startIndex2 = 0;
                    int num3 = 0;
                    for (; sEmail.IndexOf("@", startIndex2) != -1; startIndex2 = sEmail.IndexOf("@", startIndex2) + 1)
                        ++num3;
                    if (num3 > 1)
                        return false;
                    int num4 = sEmail.LastIndexOf(".") + 1;
                    if (sEmail.Length - num4 < 2)
                        return false;
                    int num5 = sEmail.LastIndexOf("@") + 1;
                    if (sEmail.Length - num5 < 7)
                        return false;
                    else
                        return Regex.IsMatch(sEmail, "(\\w+)@(\\w+)\\.(\\w+)");
            }
        }

        public static double value(string StringValue)
        {
            try
            {
                if (string.IsNullOrEmpty(StringValue))
                    return 0.0;
                try
                {
                    return double.Parse(StringValue);
                }
                catch
                {
                }
                string s = "";
                StringValue = StringValue.Trim();
                foreach (char ch in StringValue)
                {
                    int num = (int)ch;
                    switch (num)
                    {
                        case 46:
                        case 45:
                            s = s + (object)ch;
                            continue;
                        default:
                            if (num <= 47 || num >= 58)
                                goto label_9;
                            else
                                goto case 46;
                    }
                }
            label_9:
                return double.Parse(s);
            }
            catch
            {
                return 0.0;
            }
        }

        public static int valueInt(string StringValue)
        {
            try
            {
                if (string.IsNullOrEmpty(StringValue))
                    return 0;
                try
                {
                    return int.Parse(StringValue);
                }
                catch
                {
                }
                string s = "";
                StringValue = StringValue.Trim();
                foreach (char ch in StringValue)
                {
                    int num = (int)ch;
                    if (num == 46 || num > 47 && num < 58)
                        s = s + (object)ch;
                    else
                        break;
                }
                if (StringValue.Substring(0, 1).CompareTo("-") == 0)
                    return -int.Parse(s);
                else
                    return int.Parse(s);
            }
            catch
            {
                return 0;
            }
        }

        public static double valueDouble(string StringValue)
        {
            try
            {
                if (string.IsNullOrEmpty(StringValue))
                    return 0.0;
                try
                {
                    return double.Parse(StringValue);
                }
                catch
                {
                }
                string s = "";
                StringValue = StringValue.Trim();
                foreach (char ch in StringValue)
                {
                    int num = (int)ch;
                    if (num == 46 || num > 47 && num < 58)
                        s = s + (object)ch;
                    else
                        break;
                }
                if (StringValue.Substring(0, 1).CompareTo("-") == 0)
                    return -double.Parse(s);
                else
                    return double.Parse(s);
            }
            catch
            {
                return 0.0;
            }
        }

        public static byte valueByte(string StringValue)
        {
            try
            {
                if (string.IsNullOrEmpty(StringValue))
                    return (byte)0;
                try
                {
                    return byte.Parse(StringValue);
                }
                catch
                {
                }
                string s = "";
                StringValue = StringValue.Trim();
                foreach (char ch in StringValue)
                {
                    byte num = (byte)ch;
                    if ((int)num == 46 || (int)num > 47 && (int)num < 58)
                        s = s + (object)ch;
                    else
                        break;
                }
                return byte.Parse(s);
            }
            catch
            {
                return (byte)0;
            }
        }

        public static int increaseIt(string StringValue, int IncreaseValue)
        {
            try
            {
                return adviitScripting.valueInt(StringValue) + IncreaseValue;
            }
            catch
            {
                return adviitScripting.valueInt(StringValue);
            }
        }

        public static int decreaseIt(string StringValue, int DecreaseValue)
        {
            try
            {
                return adviitScripting.valueInt(StringValue) - DecreaseValue;
            }
            catch
            {
                return adviitScripting.valueInt(StringValue);
            }
        }

        public static string getDecimalOnly(string StringValue)
        {
            try
            {
                if (string.IsNullOrEmpty(StringValue))
                    return "";
                string str = "";
                StringValue = StringValue.Trim();
                foreach (char ch in StringValue)
                {
                    int num = (int)ch;
                    switch (num)
                    {
                        case 46:
                        case 45:
                            str = str + (object)ch;
                            break;
                        default:
                            if (num <= 47 || num >= 58)
                                break;
                            else
                                goto case 46;
                    }
                }
                if (StringValue.Substring(0, 1).CompareTo("-") == 0)
                    return "-" + str;
                else
                    return str;
            }
            catch
            {
                return "";
            }
        }

        public static string getFirstNumericOnly(string StringValue)
        {
            try
            {
                if (string.IsNullOrEmpty(StringValue))
                    return "";
                string str = "";
                bool flag = false;
                StringValue = StringValue.Trim();
                foreach (char ch in StringValue)
                {
                    int num = (int)ch;
                    switch (num)
                    {
                        case 46:
                        case 45:
                            str = str + (object)ch;
                            flag = true;
                            break;
                        default:
                            if (num <= 47 || num >= 58)
                            {
                                if (flag)
                                    goto label_9;
                                else
                                    break;
                            }
                            else
                                goto case 46;
                    }
                }
            label_9:
                if (StringValue.Substring(0, 1).CompareTo("-") == 0)
                    return "-" + str;
                else
                    return str;
            }
            catch
            {
                return "";
            }
        }

        public static string getNumberberOrderName(int Number)
        {
            try
            {
                string str = "";
                if (Number == 1)
                    str = (string)(object)Number + (object)"st";
                else if (Number == 2)
                    str = (string)(object)Number + (object)"nd";
                else if (Number == 3)
                    str = (string)(object)Number + (object)"rd";
                else if (Number >= 4 && Number < 21)
                    str = (string)(object)Number + (object)"th";
                else if (Number >= 21 && Number % 10 == 1)
                    str = (string)(object)Number + (object)"st";
                else if (Number >= 21 && Number % 10 == 2)
                    str = (string)(object)Number + (object)"nd";
                else if (Number >= 21 && Number % 10 == 3)
                    str = (string)(object)Number + (object)"rd";
                else if (Number >= 21 && Number % 10 >= 4)
                    str = (string)(object)Number + (object)"th";
                return str;
            }
            catch
            {
                return "";
            }
        }

        public static long valueLong(string StringValue)
        {
            try
            {
                if (string.IsNullOrEmpty(StringValue))
                    return 0L;
                try
                {
                    return long.Parse(StringValue);
                }
                catch
                {
                }
                StringValue = StringValue.Trim();
                string s = "";
                foreach (char ch in StringValue)
                {
                    int num = (int)ch;
                    if (num == 46 || num > 47 && num < 58)
                        s = s + (object)ch;
                    else
                        break;
                }
                if (StringValue.Substring(0, 1).CompareTo("-") == 0)
                    return -long.Parse(s);
                else
                    return long.Parse(s);
            }
            catch
            {
                return 0L;
            }
        }

        public static string getValidPath(string SourcePath, string FindExtension)
        {
            try
            {
                int startIndex = SourcePath.IndexOf(":\\") - 1;
                int num = SourcePath.IndexOf(FindExtension) + FindExtension.Length;
                return SourcePath.Substring(startIndex, num - startIndex);
            }
            catch
            {
                return "";
            }
        }

        public static string getValidUrl(string URL)
        {
            try
            {
                URL = URL.Trim();
                if (URL.ToUpper().IndexOf("HTTP://") == -1)
                    URL = "http://" + URL;
                return URL;
            }
            catch
            {
                return "";
            }
        }

        public static int randomNumber(int min, int max)
        {
            try
            {
                return new Random().Next(min, max);
            }
            catch
            {
                return min;
            }
        }

        public static string setNumberSequence(int number)
        {
            try
            {
                if (number >= 11 && number < 14)
                    return number.ToString() + "th";
                if ((number - 1) % 10 == 0)
                    return number.ToString() + "st";
                if ((number - 2) % 10 == 0)
                    return number.ToString() + "nd";
                if ((number - 3) % 10 == 0)
                    return number.ToString() + "rd";
                else
                    return number.ToString() + "th";
            }
            catch
            {
                return "";
            }
        }

        public static string setNumberSequence(string Number)
        {
            try
            {
                int num = adviitScripting.valueInt(Number);
                if (num == 999)
                    return "Normal";
                if (num >= 11 && num < 14)
                    return num.ToString() + "th";
                if ((num - 1) % 10 == 0)
                    return num.ToString() + "st";
                if ((num - 2) % 10 == 0)
                    return num.ToString() + "nd";
                if ((num - 3) % 10 == 0)
                    return num.ToString() + "rd";
                else
                    return num.ToString() + "th";
            }
            catch
            {
                return "";
            }
        }

        public static string stringParser(string SourceString, string tagName)
        {
            try
            {
                string str1 = "<" + tagName + ">";
                string str2 = "</" + tagName + ">";
                int startIndex = SourceString.IndexOf(str1) + str1.Length;
                int num = SourceString.IndexOf(str2);
                return SourceString.Substring(startIndex, num - startIndex);
            }
            catch
            {
                return "";
            }
        }

        public static double divideByHundred(string convertValue)
        {
            try
            {
                return adviitScripting.valueDouble(convertValue) / 100.0;
            }
            catch
            {
                return 0.0;
            }
        }

        public static string getXML(string Source, string Tag)
        {
            try
            {
                int startIndex = Source.IndexOf("<" + Tag + ">") + Tag.Length + 2;
                int num = Source.IndexOf("</" + Tag + ">", startIndex);
                return Source.Substring(startIndex, num - startIndex);
            }
            catch
            {
                return "";
            }
        }

        public static string getAsciiFromString(string Value)
        {
            try
            {
                byte[] bytes = Encoding.ASCII.GetBytes(Value);
                Value = "";
                foreach (int num in bytes)
                    Value = Value + num.ToString();
                return Value;
            }
            catch
            {
                return "";
            }
        }

        public static string getRandomString(int Length)
        {
            try
            {
                Random random = new Random();
                if (Length <= 0)
                    Length = 15;
                string str = "";
                for (int index = 0; index < Length; ++index)
                    str = index % 2 != 0 ? (index % 3 != 0 ? str + (object)(char)random.Next(48, 58) : str + (object)(char)random.Next(65, 90)) : str + (object)(char)random.Next(97, 122);
                return str;
            }
            catch
            {
                return "";
            }
        }

        public static string getHostIPAddress()
        {
            try
            {
                return HttpContext.Current.Request.UserHostAddress;
            }
            catch
            {
                return "";
            }
        }

        public static bool createDirectory(string path)
        {
            try
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(path);
                if (directoryInfo.Exists)
                    return true;
                directoryInfo.Create();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool deleteDirectory(string path)
        {
            try
            {
                ((FileSystemInfo)new DirectoryInfo(path)).Delete();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool deleteFile(string fileLocation)
        {
            try
            {
                new FileInfo(fileLocation).Delete();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool writeFile(ref string SourceString, string FileName)
        {
            try
            {
                StreamWriter streamWriter = new StreamWriter(FileName);
                streamWriter.Write(SourceString);
                streamWriter.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool readFile(ref string SourceString, string FileName)
        {
            try
            {
                StreamReader streamReader = new StreamReader(FileName);
                SourceString = streamReader.ReadToEnd();
                streamReader.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static string getIPAddress()
        {
            Dns.GetHostName();
            return Dns.Resolve(Dns.GetHostName()).AddressList[0].ToString();
        }

        public static string getClientComputerName()
        {
            try
            {
                return Dns.GetHostEntry(adviitScripting.getHostIPAddress()).HostName;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        public static string sqlInput(string SourceValue)
        {
            try
            {
                SourceValue = SourceValue.Replace("'", "''");
                return SourceValue;
            }
            catch
            {
                return SourceValue;
            }
        }

        public static void sendMailMessage(string from, string to, string bcc, string cc, string subject, string body)
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress(from);
            ((Collection<MailAddress>)message.To).Add(new MailAddress(to));
            if (bcc != null && bcc != string.Empty)
                ((Collection<MailAddress>)message.Bcc).Add(new MailAddress(bcc));
            if (cc != null && cc != string.Empty)
                ((Collection<MailAddress>)message.CC).Add(new MailAddress(cc));
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;
            new SmtpClient().Send(message);
        }

        public static string SendMail(string toList, string UserName, string Password, string ccList, string subject, string body)
        {
            MailMessage message = new MailMessage();
            SmtpClient smtpClient = new SmtpClient();
            string str = string.Empty;
            try
            {
                MailAddress mailAddress = new MailAddress(UserName);
                message.From = mailAddress;
                message.To.Add(toList);
                if (ccList != null && ccList != string.Empty)
                    message.CC.Add(ccList);
                message.Subject = subject;
                message.IsBodyHtml = true;
                message.Body = body;
                smtpClient.Host = "smtp.gmail.com";
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = true;
                smtpClient.Credentials = (ICredentialsByHost)new NetworkCredential(UserName, Password);
                smtpClient.Send(message);
                return "Successful<BR>";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string getOnlyAlphabat(string Source)
        {
            try
            {
                string str = "";
                foreach (char ch in Source)
                {
                    int num = (int)ch;
                    if (num >= 65 && num <= 90 || num >= 97 && num <= 122 || num == 32)
                        str = str + (object)ch;
                }
                return str.Trim();
            }
            catch
            {
                return "";
            }
        }

        public static string getOnlyAlphabatAndSpecial(string Source)
        {
            try
            {
                string str = "";
                foreach (char ch in Source)
                {
                    int num = (int)ch;
                    if (num >= 65 && num <= 90 || num >= 97 && num <= 122 || (num == 32 || num == 47))
                        str = str + (object)ch;
                }
                return str.Trim();
            }
            catch
            {
                return "";
            }
        }

        public static bool privilegePassed(string MenuList, string PageRequested)
        {
            try
            {
                return MenuList.IndexOf(PageRequested) > -1;
            }
            catch
            {
                return false;
            }
        }

        public static string getOrderFormat(int number)
        {
            try
            {
                if (number > 9)
                    return number.ToString();
                else
                    return "0" + number.ToString();
            }
            catch
            {
                return "00";
            }
        }

        public static string getOrderFormat(string number)
        {
            try
            {
                if (number.Length > 1)
                    return ((object)number).ToString();
                else
                    return "0" + ((object)number).ToString();
            }
            catch
            {
                return "00";
            }
        }

        public static string getNameForFixedLength(string SourceName, int length)
        {
            try
            {
                if (SourceName.Length <= length)
                    return SourceName;
                SourceName = SourceName.Substring(0, length + 1);
                SourceName = SourceName.Substring(0, SourceName.LastIndexOf(' ') + 1);
                return SourceName;
            }
            catch
            {
                return SourceName;
            }
        }

        public static string getNameForFixedLengthFromLast(string SourceName, int length)
        {
            try
            {
                if (SourceName.Length <= length)
                    return SourceName;
                int startIndex = SourceName.LastIndexOf(' ', SourceName.Length - length);
                SourceName = startIndex <= 0 ? SourceName.Substring(SourceName.Length - length) : SourceName.Substring(startIndex);
                return SourceName;
            }
            catch (Exception ex)
            {
                return SourceName;
            }
        }

        public static string getGivenLengthString(string SourceName, int length)
        {
            try
            {
                if (string.IsNullOrEmpty(SourceName))
                    return "";
                if (SourceName.Length > length)
                    return SourceName.Substring(0, length);
                else
                    return SourceName;
            }
            catch
            {
                return SourceName;
            }
        }

        public static string getJobDuration(string JoiningDate)
        {
            try
            {
                int days = DateTime.Now.Subtract(DateTime.Parse(JoiningDate)).Days;
                if (days <= 365)
                    return (days / 30).ToString() + " mth";
                int num1 = days % 365;
                int num2 = num1 <= 30 ? 1 : (int)Math.Ceiling((double)num1 / 30.0);
                return (days / 365).ToString() + " yr, " + num2.ToString() + " mth";
            }
            catch
            {
                return JoiningDate;
            }
        }

        public static int binarySearch(ref DataTable dt, int columnIndex, int searchItem)
        {
            try
            {
                return 0;
            }
            catch
            {
                return -1;
            }
        }

        private static int BinarySearch(ref DataTable dt, int columnIndex, int searchItem)
        {
            try
            {
                return 0;
            }
            catch
            {
                return -1;
            }
        }

        public static string makePercent(string value)
        {
            try
            {
                double num = Math.Ceiling(adviitScripting.valueDouble(value));
                if (num > 100.0)
                    num = 100.0;
                return num.ToString();
            }
            catch
            {
                return value;
            }
        }

        public static string makePercent(string value, bool usePercentSymbol)
        {
            try
            {
                double num = Math.Ceiling(adviitScripting.valueDouble(value));
                if (num > 100.0)
                    num = 100.0;
                return num.ToString() + "%";
            }
            catch
            {
                return value;
            }
        }

        public static string getPerfectLengthName(string Name, int maxLength)
        {
            try
            {
                if (Name.Length > maxLength)
                    Name = Name.Substring(0, Name.LastIndexOf(' ')).Trim();
                return Name;
            }
            catch
            {
                return Name;
            }
        }
    }
}