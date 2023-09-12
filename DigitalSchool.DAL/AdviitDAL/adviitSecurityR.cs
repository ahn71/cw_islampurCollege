using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.DAL.AdviitDAL
{
    public static class adviitSecurity
    {
        public static string errors = "";
        private static string cryptoPassword = "";
        private static string Salt = "";
        private static string HashAlgorithm = "SHA1";
        private static int PasswordIterations = 5;
        private static string InitialVector = "";
        private static int KeySize = 256;
        private static bool securityStatus = false;
        private static string blockedList = "-- ;-- ; /* */ @@@ char nchar varchar nvarchar alter  begin cast create cursor declare delete  drop end exec execute fetch insert  kill open select sys sysobjects syscolumns  table update ";
        private static string siteBlackList = "";
        private static string skipElements = "";
        private static string[] bList;
        private static string[] sqlKeyList;

        public static string crypto(string SourceString, bool Encrypt)
        {
            try
            {
                if (adviitSecurity.cryptoPassword.Length == 0)
                    adviitSecurity.cryptoPassword = "\x0013m\x0016\x00881aK|f\x000F?)ZD8R\x0083m\x0016\x00870aK|e\x000E?)YCt\x001D\x008F7!R\x0083l\x0015\x00870aJ{e\x000E>(YCt\x001C\x008E7!Q\x0082l\x0015\x0087/`J{d\x0095>(YBs\x001C\x008E6 \x0014\x0086/`Izd\x0095>'XBs\x001B\x008D6 Q\x0081k\x0014\x0086.";
                if (adviitSecurity.Salt.Length == 0)
                {
                    for (int index = 0; index < 50; ++index)
                        adviitSecurity.Salt = adviitSecurity.Salt + (object)(char)index;
                        adviitSecurity.Salt = adviitSecurity.Salt + adviitSecurity.cryptoPassword.Substring(2, 10);
                }
                if (adviitSecurity.InitialVector.Length == 0)
                {
                    for (int index = 195; index < 211; ++index)
                        adviitSecurity.InitialVector = adviitSecurity.InitialVector + (object)(char)index;
                }
                if (Encrypt)
                    return adviitSecurity.encrypt(SourceString);
                else
                    return adviitSecurity.decrypt(SourceString);
            }
            catch (Exception ex)
            {
                adviitSecurity.errors = ex.Message;
                return "";
            }
        }

        private static string encrypt(string SourceString)
        {
            try
            {
                if (string.IsNullOrEmpty(SourceString))
                    return "";
                byte[] bytes1 = Encoding.ASCII.GetBytes(adviitSecurity.InitialVector);
                byte[] bytes2 = Encoding.ASCII.GetBytes(adviitSecurity.Salt);
                byte[] bytes3 = Encoding.UTF8.GetBytes(SourceString);
                byte[] bytes4 = new PasswordDeriveBytes(adviitSecurity.cryptoPassword, bytes2, adviitSecurity.HashAlgorithm, adviitSecurity.PasswordIterations).GetBytes(adviitSecurity.KeySize / 8);
                RijndaelManaged rijndaelManaged = new RijndaelManaged();
                rijndaelManaged.Mode = CipherMode.CBC;
                byte[] inArray = (byte[])null;
                using (ICryptoTransform encryptor = rijndaelManaged.CreateEncryptor(bytes4, bytes1))
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                        {
                            cryptoStream.Write(bytes3, 0, bytes3.Length);
                            cryptoStream.FlushFinalBlock();
                            inArray = memoryStream.ToArray();
                            memoryStream.Close();
                            cryptoStream.Close();
                        }
                    }
                }
                rijndaelManaged.Clear();
                return Convert.ToBase64String(inArray);
            }
            catch (Exception ex)
            {
                adviitSecurity.errors = ex.Message;
                return "";
            }
        }

        private static string decrypt(string guponSourceString)
        {
            try
            {
                if (string.IsNullOrEmpty(guponSourceString))
                    return "";
                byte[] bytes1 = Encoding.ASCII.GetBytes(adviitSecurity.InitialVector);
                byte[] bytes2 = Encoding.ASCII.GetBytes(adviitSecurity.Salt);
                byte[] buffer = Convert.FromBase64String(guponSourceString);
                byte[] bytes3 = new PasswordDeriveBytes(adviitSecurity.cryptoPassword, bytes2, adviitSecurity.HashAlgorithm, adviitSecurity.PasswordIterations).GetBytes(adviitSecurity.KeySize / 8);
                RijndaelManaged rijndaelManaged = new RijndaelManaged();
                rijndaelManaged.Mode = CipherMode.CBC;
                byte[] numArray = new byte[guponSourceString.Length];
                int count = 0;
                using (ICryptoTransform decryptor = rijndaelManaged.CreateDecryptor(bytes3, bytes1))
                {
                    using (MemoryStream memoryStream = new MemoryStream(buffer))
                    {
                        using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                        {
                            count = cryptoStream.Read(numArray, 0, numArray.Length);
                            memoryStream.Close();
                            cryptoStream.Close();
                        }
                    }
                }
                rijndaelManaged.Clear();
                return Encoding.UTF8.GetString(numArray, 0, count);
            }
            catch (Exception ex)
            {
                adviitSecurity.errors = ex.Message;
                return "";
            }
        }

        public static string getMD5Hash(string SourceString)
        {
            byte[] hash = new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(SourceString));
            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte num in hash)
                stringBuilder.Append(num.ToString("x2").ToLower());
            return ((object)stringBuilder).ToString();
        }

        public static bool hasAnyInvalidInput(Page targetPage)
        {
            try
            {
                ControlCollection controls = targetPage.Controls;
                adviitSecurity.securityStatus = false;
                adviitSecurity.initiateSqlKeyList();
                return adviitSecurity.checkAllInputs(controls);
            }
            catch
            {
                return true;
            }
        }

        public static bool hasAnyInvalidInput(string inputString)
        {
            try
            {
                adviitSecurity.securityStatus = false;
                adviitSecurity.initiateSqlKeyList();
                return adviitSecurity.checkInput(inputString);
            }
            catch
            {
                return true;
            }
        }

        public static bool hasAnyInvalidInput(ControlCollection page, string BlockedListItems)
        {
            try
            {
                adviitSecurity.securityStatus = false;
                if (BlockedListItems.Length > 0)
                    adviitSecurity.blockedList = adviitSecurity.blockedList + BlockedListItems.ToLower();
                adviitSecurity.initiateSqlKeyList();
                return adviitSecurity.checkAllInputs(page);
            }
            catch
            {
                return true;
            }
        }

        private static bool checkAllInputs(ControlCollection page)
        {
            try
            {
                if (adviitSecurity.securityStatus)
                    return true;
                foreach (Control control in page)
                {
                    if (adviitSecurity.securityStatus)
                        return true;
                    if (control.ID != null)
                    {
                        if (control is TextBox)
                        {
                            TextBox textBox = (TextBox)control;
                            adviitSecurity.securityStatus = adviitSecurity.checkInput(textBox.Text);
                            if (adviitSecurity.securityStatus)
                            {
                                textBox.Attributes["style"] = "border:1px solid red";
                                textBox.Focus();
                            }
                        }
                        else if (control is DropDownList)
                        {
                            DropDownList dropDownList = (DropDownList)control;
                            adviitSecurity.securityStatus = adviitSecurity.checkInput(((object)dropDownList.SelectedItem.Text).ToString());
                            if (adviitSecurity.securityStatus)
                            {
                                dropDownList.Attributes["style"] = "border:1px solid red";
                                dropDownList.Focus();
                            }
                        }
                        else if (control is ListBox)
                        {
                            ListBox listBox = (ListBox)control;
                            adviitSecurity.securityStatus = adviitSecurity.checkInput(((object)listBox.SelectedItem.Text).ToString());
                            if (adviitSecurity.securityStatus)
                            {
                                listBox.Attributes["style"] = "border:1px solid red";
                                listBox.Focus();
                            }
                        }
                        if (adviitSecurity.securityStatus)
                            return true;
                    }
                    if (control.HasControls() && !adviitSecurity.securityStatus)
                        adviitSecurity.checkAllInputs(control.Controls);
                }
                return adviitSecurity.securityStatus;
            }
            catch
            {
                return false;
            }
        }

        private static bool checkInput(string input)
        {
            try
            {
                if (input == null || input.Length == 0)
                    return false;
                input = input.ToLower();
                for (int index = 0; index < adviitSecurity.sqlKeyList.Length; ++index)
                {
                    if (adviitSecurity.sqlKeyList[index].Trim().Length > 0 && input.IndexOf(adviitSecurity.sqlKeyList[index]) > -1)
                        return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        private static void initiateSqlKeyList()
        {
            try
            {
                adviitSecurity.sqlKeyList = adviitSecurity.siteBlackList.Split(' ');
            }
            catch
            {
            }
        }

        public static bool findBlackListItems(string inputString, string blackListItems)
        {
            try
            {
                adviitSecurity.securityStatus = false;
                if (blackListItems.Length < 1)
                    return false;
                adviitSecurity.siteBlackList = blackListItems;
                adviitSecurity.initiateBlackList();
                return adviitSecurity.checkBlackListItem(inputString);
            }
            catch
            {
                return true;
            }
        }

        public static bool findBlackListItems(Page targetPage, string blackListItems, string skipThese)
        {
            try
            {
                if (blackListItems.Length < 1)
                    return false;
                adviitSecurity.siteBlackList = blackListItems;
                adviitSecurity.skipElements = skipThese.ToLower();
                ControlCollection controls = targetPage.Controls;
                adviitSecurity.securityStatus = false;
                adviitSecurity.initiateBlackList();
                return adviitSecurity.findBlackListItems(controls);
            }
            catch
            {
                return true;
            }
        }

        private static bool findBlackListItems(ControlCollection page)
        {
            try
            {
                if (adviitSecurity.securityStatus)
                    return true;
                foreach (Control control in page)
                {
                    if (adviitSecurity.securityStatus)
                        return true;
                    if (control.ID != null && adviitSecurity.skipElements.IndexOf(control.ID.ToLower()) == -1)
                    {
                        if (control is TextBox)
                        {
                            TextBox textBox = (TextBox)control;
                            adviitSecurity.securityStatus = adviitSecurity.checkBlackListItem(textBox.Text);
                            if (adviitSecurity.securityStatus)
                            {
                                textBox.Attributes["style"] = "border:1px solid red";
                                textBox.Focus();
                            }
                        }
                        else if (control is DropDownList)
                        {
                            DropDownList dropDownList = (DropDownList)control;
                            adviitSecurity.securityStatus = adviitSecurity.checkBlackListItem(((object)dropDownList.SelectedItem.Text).ToString());
                            if (adviitSecurity.securityStatus)
                            {
                                dropDownList.Attributes["style"] = "border:1px solid red";
                                dropDownList.Focus();
                            }
                        }
                        else if (control is ListBox)
                        {
                            ListBox listBox = (ListBox)control;
                            adviitSecurity.securityStatus = adviitSecurity.checkBlackListItem(((object)listBox.SelectedItem.Text).ToString());
                            if (adviitSecurity.securityStatus)
                            {
                                listBox.Attributes["style"] = "border:1px solid red";
                                listBox.Focus();
                            }
                        }
                        if (adviitSecurity.securityStatus)
                            return true;
                    }
                    if (control.HasControls() && !adviitSecurity.securityStatus)
                        adviitSecurity.findBlackListItems(control.Controls);
                }
                return adviitSecurity.securityStatus;
            }
            catch
            {
                return false;
            }
        }

        private static bool checkBlackListItem(string input)
        {
            try
            {
                if (input == null || input.Length == 0)
                    return false;
                input = input.ToLower();
                input = input.Replace(" ", "");
                for (int index = 0; index < adviitSecurity.bList.Length; ++index)
                {
                    if (adviitSecurity.bList[index].Trim().Length > 0 && input.IndexOf(adviitSecurity.bList[index]) > -1)
                        return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        private static void initiateBlackList()
        {
            try
            {
                adviitSecurity.siteBlackList = adviitSecurity.siteBlackList.Replace(" ", "");
                adviitSecurity.bList = adviitSecurity.siteBlackList.Split(',');
            }
            catch
            {
            }
        }
    }
}
