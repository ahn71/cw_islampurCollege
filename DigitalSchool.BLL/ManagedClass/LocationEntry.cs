using DS.DAL;
using DS.PropertyEntities.Model.ManagedClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace DS.BLL.ManagedClass
{
   public class LocationEntry: IDisposable
    {
        private LocationEntities _Entities;
        string sql = string.Empty;
        bool result = true;
        public LocationEntry()
        {
            
        }

        public LocationEntities AddEntities
        {
            set
            {
                _Entities = value;
            }
        }     

        public bool Insert()
        {
            sql = string.Format("INSERT INTO [dbo].[Tbl_Location_Information] " +
                "([LocationName]) VALUES ( '" + _Entities.LocationName + "')");
            return result = CRUD.ExecuteQuery(sql);             
        }

        public List<LocationEntities> GetEntitiesData()
        {
            List<LocationEntities> ListEntities = new List<LocationEntities>();
            sql = string.Format("SELECT [LocationID],[LocationName] FROM [dbo].[Tbl_Location_Information]");
            DataTable dt = new DataTable();
 
            dt = CRUD.ReturnTableNull(sql);
            if(dt != null)
            {
                if(dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows

                                    select new LocationEntities
                                     {
                                         LocationID = int.Parse(row["LocationID"].ToString()),
                                         LocationName = row["LocationName"].ToString()

                                     }).ToList();
                    return ListEntities;
                
                }

            }
            return ListEntities = null;            
        }
        public static void GetEntitiesData(DropDownList dl)
        {
            try
            {
                LocationEntry ClassName = new LocationEntry();
                List<LocationEntities> ClassNameList = ClassName.GetEntitiesData();
                dl.DataTextField = "LocationName";
                dl.DataValueField = "LocationID";
                dl.DataSource = ClassNameList;
                dl.DataBind();
                dl.Items.Insert(0, new ListItem("...Select...", "0"));
            }
            catch { }
        }

        public bool Update()
        {
            sql = string.Format("UPDATE [dbo].[Tbl_Location_Information] SET " +
                "[LocationName] = '" + _Entities.LocationName + "' WHERE [LocationID] = '" + _Entities.LocationID + "'");
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
