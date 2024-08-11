using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace DS.DAL.ComplexScripting
{
    public class DatabaseBackupRestore
    {
        public static SqlCommand cmd;

        public static void DatabaseBackup(string SourceDatabaseName, string DestinationPath_DatabaseName, SqlConnection setConnection)
        {
            File.Delete(DestinationPath_DatabaseName + ".bak");
            DatabaseBackupRestore.cmd = new SqlCommand("BACKUP DATABASE " + SourceDatabaseName + " TO DISK='" + DestinationPath_DatabaseName + ".bak'", setConnection);
            DatabaseBackupRestore.cmd.ExecuteNonQuery();
        }
    }
}
