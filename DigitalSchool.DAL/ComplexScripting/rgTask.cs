using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DS.DAL.ComplexScripting
{
    public class rgTask
    {
        public static void rgWrite(string rgPath, string rgName, string rgValue)
        {
            try
            {
                Registry.LocalMachine.CreateSubKey(rgPath).SetValue(rgName, (object)rgValue);
            }
            catch
            {
            }
        }

        public static string rgRead(string rgPath, string rgName)
        {
            try
            {
                return (string)Registry.LocalMachine.OpenSubKey(rgPath).GetValue(rgName);
            }
            catch
            {
                return " ";
            }
        }

        public static void rgDelete(string rgPath)
        {
            try
            {
                RegistryKey registryKey = Registry.LocalMachine;
                registryKey.OpenSubKey(rgPath);
                registryKey.DeleteSubKey(rgPath);
            }
            catch
            {
            }
        }

        public static void rgRedForAutoStartApps(string appsName)
        {
            Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true).SetValue(appsName, (object)("\"" + ((object)Application.ExecutablePath).ToString() + "\""));
        }
    }
}
