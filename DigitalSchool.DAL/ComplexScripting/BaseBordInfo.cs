using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;

namespace DS.DAL.ComplexScripting
{
    public class BaseBordInfo
    {
        public static string BaseBoardId()
        {
            string str = string.Empty;
            foreach (ManagementObject managementObject in new ManagementObjectSearcher("select SerialNumber from win32_Baseboard").Get())
                str = str + managementObject["SerialNumber"].ToString();
            return str;
        }

        public static string ProcessorId()
        {
            string str = string.Empty;
            foreach (ManagementObject managementObject in new ManagementObjectSearcher("select SerialNumber from win32_Processor").Get())
                str = str + managementObject["ProcessorId"].ToString();
            return str;
        }
    }
}
