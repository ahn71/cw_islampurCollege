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
   public class BusInformationEntry: IDisposable
    {
        private BusInformationEntities _Entities;
        string sql = string.Empty;
        bool result = true;
        public BusInformationEntry()
        {
            
        }

        public BusInformationEntities AddEntities
        {
            set
            {
                _Entities = value;
            }
        }     

        public bool Insert()
        {
            sql = string.Format("INSERT INTO [dbo].[tbl_BusInformation] " +
                "([BusName]) VALUES ( '" + _Entities.BusName + "')");
            return result = CRUD.ExecuteQuery(sql);             
        }

        public List<BusInformationEntities> GetEntitiesData()
        {
            List<BusInformationEntities> ListEntities = new List<BusInformationEntities>();
            sql = string.Format("SELECT [BusID],[BusName] FROM [dbo].[tbl_BusInformation]");
            DataTable dt = new DataTable();
 
            dt = CRUD.ReturnTableNull(sql);
            if(dt != null)
            {
                if(dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows

                                    select new BusInformationEntities
                                     {
                                         BusID = int.Parse(row["BusID"].ToString()),
                                         BusName = row["BusName"].ToString()

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
                BusInformationEntry ClassName = new BusInformationEntry();
                List<BusInformationEntities> ClassNameList = ClassName.GetEntitiesData();
                dl.DataTextField = "BusName";
                dl.DataValueField = "BusID";
                dl.DataSource = ClassNameList;
                dl.DataBind();
                dl.Items.Insert(0, new ListItem("...Select...", "0"));
            }
            catch { }
        }

        public bool Update()
        {
            sql = string.Format("UPDATE [dbo].[tbl_BusInformation] SET " +
                "[BusName] = '" + _Entities.BusName + "' WHERE [BusID] = '" + _Entities.BusID + "'");
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