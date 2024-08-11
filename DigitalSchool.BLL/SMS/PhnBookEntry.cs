using DS.DAL;
using DS.PropertyEntities.Model.SMS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DS.BLL.SMS
{
    public class PhnBookEntry : IDisposable
    {
        private PhoneBook _Entities;
        private static List<PhoneBook> PhnBooktList;
        string sql = string.Empty;
        bool result = true;
        public PhnBookEntry()
        { }
        public PhoneBook AddEntities
        {
            set
            {
                _Entities = value;
            }
        }
        public bool Insert()
        {
            sql = string.Format("INSERT INTO [dbo].[Tbl_Phone_book] " +
                "([Number],[GrpID],[Name],[Details]) VALUES ( " +
                "'" + _Entities.Number + "'," +
                "'" + _Entities.Group.GrpID + "'," +
                "N'" + _Entities.Name + "'," +
                "N'" + _Entities.Details + "')");
            return result = CRUD.ExecuteQuery(sql);
        }
        public List<PhoneBook> GetAllPhnBookNum(int? GrpId)
        {
            List<PhoneBook> ListEntities = new List<PhoneBook>();
            if(GrpId == null)
            {
                sql = string.Format("SELECT [NumID],[Number],[GrpID],[Name],[Details] " +
                                "FROM [dbo].[Tbl_Phone_book] " +
                                "ORDER BY [NumID] ASC");
            }
            else
            {
                sql = string.Format("SELECT [NumID],[Number],[GrpID],[Name],[Details] " +
                                "FROM [dbo].[Tbl_Phone_book] WHERE [GrpID] = '" + GrpId + "' " +
                                "ORDER BY [NumID] ASC");
            }
            
            DataTable dt = new DataTable();

            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows
                                    select new PhoneBook
                                    {
                                        NumID = int.Parse(row["NumID"].ToString()),
                                        Number = row["Number"].ToString(),
                                        Group = new PhoneGroup(){
                                            GrpID = int.Parse(row["GrpID"].ToString())
                                        },
                                        Name = row["Name"].ToString(),
                                        Details = row["Details"].ToString()
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
