using DS.DAL;
using DS.PropertyEntities.Model.DSWS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DS.BLL.DSWS
{
    public class ContactEntry:IDisposable
    {
        private ContactEntities _Entities;
        string sql = string.Empty;
        DataTable dt;
        public ContactEntry() { }
        public ContactEntities AddEntities
        {
            set
            {
                _Entities = value;
            }
        }      

        public bool Insert()
        {
            sql = string.Format("INSERT INTO [dbo].[WSContact] " +
                "([Name],[Email],[PhoneNumber],[Comments],"
                + "[SendDate]) VALUES (" +
                "N'" + _Entities.Name + "', " +
                "'" + _Entities.Email + "' ," +
                "'" + _Entities.PhoneNumber + "', " +
                "N'" + _Entities.Comments + "', " +                
                "'" + _Entities.SendDate + "')");
               bool result = CRUD.ExecuteQuery(sql);
               return result;
        }
        public List<ContactEntities> getContactList()
        {
            sql = " select * from WSContact";
            dt = new DataTable();
            List<ContactEntities> ListEntities = new List<ContactEntities>();
            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows
                                    select new ContactEntities
                                    {
                                        CID = int.Parse(row["CID"].ToString()),
                                        Name = row["Name"].ToString(),
                                        Email = row["Email"].ToString(),
                                        PhoneNumber = row["PhoneNumber"].ToString(),
                                        Comments = row["Comments"].ToString(),
                                        SendDate = DateTime.Parse(row["SendDate"].ToString())
                                        
                                    }
                                    ).ToList();

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
