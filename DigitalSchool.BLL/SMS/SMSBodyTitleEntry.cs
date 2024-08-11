using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.PropertyEntities.Model.SMS;
using DS.DAL;
using System.Data;

namespace DS.BLL.SMS
{
    public class SMSBodyTitleEntry : IDisposable
    {
        private SMSBodyTitleEntities _Entities;
        string sql = string.Empty;
        bool result = true;
        public SMSBodyTitleEntry() { }
        public SMSBodyTitleEntities AddEntities
        {
            set
            {
                _Entities = value;
            }
        }

        public bool Insert()
        {
            sql = string.Format("INSERT INTO [dbo].[Tbl_SMS_Body] " +
                "([Title],[Body]) VALUES ( " +
                "N'" + _Entities.Title + "'," +
                "N'" + _Entities.Body + "')");
            return result = CRUD.ExecuteQuery(sql);
        }
        public bool Update()
        {
            sql = string.Format("UPDATE [dbo].[Tbl_SMS_Body] SET " +
                "[Title] = N'" + _Entities.Title + "', " +
                "[Body] = N'" + _Entities.Body + "' " +
                "WHERE [TitleID] = '" + _Entities.TitleID + "'");
            return result = CRUD.ExecuteQuery(sql);
        }
        public List<SMSBodyTitleEntities> GetEntitiesData()
        {
            List<SMSBodyTitleEntities> ListEntities = new List<SMSBodyTitleEntities>();
            sql = string.Format("SELECT [TitleID],[Title],[Body] FROM [dbo].[Tbl_SMS_Body]");
            DataTable dt = new DataTable();

            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows
                                    select new SMSBodyTitleEntities
                                    {
                                        TitleID = int.Parse(row["TitleID"].ToString()),
                                        Title = row["Title"].ToString(),
                                        Body = row["Body"].ToString()                                      
                                    }).ToList();
                    return ListEntities;
                }

            }
            return ListEntities = null;
        }

        public List<SMSBodyTitleEntities> GetEntitiesData(int titleId)
        {
            List<SMSBodyTitleEntities> ListEntities = new List<SMSBodyTitleEntities>();
            sql = string.Format("SELECT [TitleID],[Title],[Body] FROM [dbo].[Tbl_SMS_Body] WHERE [TitleID] = '" + titleId + "'");
            DataTable dt = new DataTable();

            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows
                                    select new SMSBodyTitleEntities
                                    {
                                        TitleID = int.Parse(row["TitleID"].ToString()),
                                        Title = row["Title"].ToString(),
                                        Body = row["Body"].ToString()
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
