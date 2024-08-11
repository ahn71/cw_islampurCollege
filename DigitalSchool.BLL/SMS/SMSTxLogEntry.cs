using DS.DAL;
using DS.PropertyEntities.Model.SMS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DS.BLL.SMS
{
    public class SMSTxLogEntry: IDisposable
    {
        private SMSTransactionLog _Entities;        
        string sql = string.Empty;
        bool result = true;

        public SMSTransactionLog AddEntities
        {
            set
            {
                _Entities = value;
            }
        }
        public bool Insert()
        {
            sql = string.Format("INSERT INTO [dbo].[Tbl_SMS_TransactionLog] " +
                "([insertedSmsIds],[SMStype],[Template],[SendingTime]) VALUES ( " +
                "'" + _Entities.insertedSmsIds + "'," +
                "'" + _Entities.SMStype + "'," +
                "'" + _Entities.Template + "'," +
                "'" + _Entities.SendingTime + "')");
            return result = CRUD.ExecuteQuery(sql);
        }
        public List<SMSTransactionLog> GetSMSTransactionList()
        {
            List<SMSTransactionLog> ListEntities = new List<SMSTransactionLog>();

            sql = string.Format("SELECT [SL],[insertedSmsIds],[SMStype],[Template],[SendingTime] " +
                                "FROM [dbo].[Tbl_SMS_TransactionLog] " +
                                "ORDER BY [SL] ASC");
       
            
            DataTable dt = new DataTable();

            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows
                                    select new SMSTransactionLog
                                    {
                                        SL = int.Parse(row["SL"].ToString()),
                                        SMStype = row["SMStype"].ToString(),
                                        Template = row["Template"].ToString(),
                                        SendingTime = row["SendingTime"].ToString()
                                    }).ToList();
                    return ListEntities;
                }

            }
            return ListEntities = null;
        }        

        bool disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;
            disposed = true;
        }
    }
}