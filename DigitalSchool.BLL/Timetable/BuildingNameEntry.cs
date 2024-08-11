using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.DAL;
using DS.PropertyEntities.Model.Timetable;
using System.Data;

namespace DS.BLL.Timetable
{
    public class BuildingNameEntry : IDisposable
    {
        private BuildingNameEntities _Entities;
        string sql = string.Empty;
        bool result = true;
        public BuildingNameEntry()
        {
            
        }

        public BuildingNameEntities AddEntities
        {
            set
            {
                _Entities = value;
            }
        }     

        public bool Insert()
        {            
            sql = string.Format("INSERT INTO [dbo].[Tbl_Bu‎ilding_Name] " +
                "([BuildingName]) VALUES ( '" + _Entities.BuildingName + "')");
            return result = CRUD.ExecuteQuery(sql);             
        }

        public List<BuildingNameEntities> GetEntitiesData()
        {
            List<BuildingNameEntities> ListEntities = new List<BuildingNameEntities>();
            sql = string.Format("SELECT [BuildingId],[BuildingName],[BuildingCode] FROM [dbo].[Tbl_Bu‎ilding_Name]");
            DataTable dt = new DataTable();
 
            dt = CRUD.ReturnTableNull(sql);
            if(dt != null)
            {
                if(dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows

                                    select new BuildingNameEntities
                                     {
                                         BuildingId = int.Parse(row["BuildingId"].ToString()),
                                         BuildingName = row["BuildingName"].ToString(),
                                         BuildingCode = row["BuildingCode"].ToString()

                                     }).ToList();
                    return ListEntities;
                
                }

            }
            return ListEntities = null;            
        }

        public bool Update()
        {
            sql = string.Format("UPDATE [dbo].[Tbl_Bu‎ilding_Name] SET " +
                "[BuildingName] = '" + _Entities.BuildingName + "' WHERE [BuildingId] = '" + _Entities.BuildingId + "'");
            return result = CRUD.ExecuteQuery(sql);
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
