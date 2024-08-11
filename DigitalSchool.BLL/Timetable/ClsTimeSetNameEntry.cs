using DS.DAL;
using DS.PropertyEntities.Model.Timetable;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DS.BLL.Timetable
{
    public class ClsTimeSetNameEntry
    {
        private ClsTimeSetNameEntities _Entities;
        string sql = string.Empty;
        bool result = true;

        public ClsTimeSetNameEntry()
        {
            
        }

        public ClsTimeSetNameEntities AddEntities
        {
            set
            {
                _Entities = value;
            }
        }
        

        public bool Insert()
        {
            sql = string.Format("INSERT INTO [dbo].[Tbl_ClassTime_SetName] " +
                "([Name]) VALUES ( '" + _Entities.Name + "')");
            return result = CRUD.ExecuteQuery(sql);             
        }

        public bool Update()
        {
            sql = string.Format("Update [dbo].[Tbl_ClassTime_SetName] Set " +
                        " [Name] = '" + _Entities.Name + "' " +
                        " WHERE [ClsTimeSetNameId] = '" + _Entities.ClsTimeSetNameId + "'");           
            return result = CRUD.ExecuteQuery(sql);
        }

        public List<ClsTimeSetNameEntities> GetEntitiesData()
        {
            List<ClsTimeSetNameEntities> ListEntities = new List<ClsTimeSetNameEntities>();
            sql = string.Format("SELECT [ClsTimeSetNameId],[Name] FROM [dbo].[Tbl_ClassTime_SetName]");
            DataTable dt = new DataTable();
 
            dt = CRUD.ReturnTableNull(sql);
            if(dt != null)
            {
                if(dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows

                                     select new ClsTimeSetNameEntities
                                     {
                                         ClsTimeSetNameId = int.Parse(row["ClsTimeSetNameId"].ToString()),
                                         Name = row["Name"].ToString()
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
