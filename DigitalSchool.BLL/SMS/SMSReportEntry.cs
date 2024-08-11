using DS.DAL;
using DS.PropertyEntities.Model.SMS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DS.BLL.SMS
{
    public class SMSReportEntry : IDisposable
    {
        private SMSEntites _Entities;
        string sql = string.Empty;
        bool result = true;
        public SMSReportEntry() { }
        public SMSEntites AddEntities
        {
            set
            {
                _Entities = value;
            }
        }
        public bool Insert()
        {
            sql = string.Format("INSERT INTO [dbo].[Tbl_SMS_Report] " +
                "([SMSID],[MobileNo],[MessageBody],[Purpose],[SentTime],[Status]) VALUES ( " +
                "'" + _Entities.SMSID + "'," +
                "N'" + _Entities.MobileNo + "'," +
                "N'" + _Entities.MessageBody+ "'," +
                "N'" + _Entities.Purpose + "'," +
                "N'" + _Entities.SentTime + "'," +
                "N'" + _Entities.Status + "')");
            return result = CRUD.ExecuteQuery(sql);
        } 
        public bool BulkInsert(List<SMSEntites> smsList)
        {
            DataTable dt = SMSReportEntry.ToDataTable(smsList);
            return result = CRUD.BulkInsert(dt, "Tbl_SMS_Report");
        }
        public List<SMSEntites> GetEntitiesData()
        {
            List<SMSEntites> ListEntities = new List<SMSEntites>();
            sql = string.Format("SELECT [SMSReportID],[SMSID],[MobileNo],[MessageBody]," +
                                "[Purpose],[SentTime],[Status] FROM [dbo].[Tbl_SMS_Report] where format(SentTime ,'yyyy-MM')='"+DateTime.Now.ToString("yyyy-MM") +"' order by SentTime desc");
            DataTable dt = new DataTable();

            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows
                                    select new SMSEntites
                                    {
                                        ID = int.Parse(row["SMSReportID"].ToString()),
                                        SMSID = row["SMSID"].ToString(),
                                        MobileNo = row["MobileNo"].ToString(),
                                        MessageBody = row["MessageBody"].ToString(),
                                        Purpose = row["Purpose"].ToString(),
                                        SentTime = DateTime.Parse(row["SentTime"].ToString()),
                                        Status = row["Status"].ToString()                                      
                                    }).ToList();
                    return ListEntities;
                }

            }
            return ListEntities = null;
        }
        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
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
