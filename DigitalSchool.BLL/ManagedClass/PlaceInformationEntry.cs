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
  public  class PlaceInformationEntry: IDisposable
    {
        private PlaceInformationEntities _Entities;
        string sql = string.Empty;
        bool result = true;
        public PlaceInformationEntry()
        {
            
        }

        public PlaceInformationEntities AddEntities
        {
            set
            {
                _Entities = value;
            }
        }     

        public bool Insert()
        {
            sql = string.Format("INSERT INTO [dbo].[Tbl_Place_Information] " +
                "([PlaceName]) VALUES ( '" + _Entities.PlaceName + "')");
            return result = CRUD.ExecuteQuery(sql);             
        }

        public List<PlaceInformationEntities> GetEntitiesData()
        {
            List<PlaceInformationEntities> ListEntities = new List<PlaceInformationEntities>();
            sql = string.Format("SELECT [PlaceID],[PlaceName] FROM [dbo].[Tbl_Place_Information]");
            DataTable dt = new DataTable();
 
            dt = CRUD.ReturnTableNull(sql);
            if(dt != null)
            {
                if(dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows

                                    select new PlaceInformationEntities
                                     {
                                         PlaceID = int.Parse(row["PlaceID"].ToString()),
                                         PlaceName = row["PlaceName"].ToString()

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
                PlaceInformationEntry ClassName = new PlaceInformationEntry();
                List<PlaceInformationEntities> ClassNameList = ClassName.GetEntitiesData();
                dl.DataTextField = "PlaceName";
                dl.DataValueField = "PlaceID";
                dl.DataSource = ClassNameList;
                dl.DataBind();
                dl.Items.Insert(0, new ListItem("...Select...", "0"));
            }
            catch { }
        }


        public bool Update()
        {
            sql = string.Format("UPDATE [dbo].[Tbl_Place_Information] SET " +
                "[PlaceName] = '" + _Entities.PlaceName + "' WHERE [PlaceID] = '" + _Entities.PlaceID + "'");
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