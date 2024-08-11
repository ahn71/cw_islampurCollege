using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.DAL;
using DS.PropertyEntities.Model.GeneralSettings;
using System.Data;
using System.Web.UI.WebControls;

namespace DS.BLL.GeneralSettings
{
    public class DistrictEntry : IDisposable
    {
        private DistrictEntities _Entities;
        string sql = string.Empty;
        bool result = true;
        static List<DistrictEntities> grpList;
        public DistrictEntry() { }
        public DistrictEntities AddEntities
        {
            set
            {
                _Entities = value;
            }
        }

        public bool Insert()
        {
            sql = string.Format("INSERT INTO [dbo].[Distritcts] " +
                "([DistrictName]) VALUES ( '" + _Entities.DistrictName + "')");
            return result = CRUD.ExecuteQuery(sql);
        }
        public bool Update()
        {
            sql = string.Format("UPDATE [dbo].[Distritcts] SET " +
                "[DistrictName] = '" + _Entities.DistrictName + "' " +
                "WHERE [DistrictId] = '" + _Entities.DistrictId + "'");
            return result = CRUD.ExecuteQuery(sql);
        }
        public List<DistrictEntities> GetEntitiesData()
        {
            List<DistrictEntities> ListEntities = new List<DistrictEntities>();
            sql = string.Format("SELECT [DistrictId],[DistrictName] FROM [dbo].[Distritcts]");
            DataTable dt = new DataTable();

            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows
                                    select new DistrictEntities
                                    {
                                        DistrictId = int.Parse(row["DistrictId"].ToString()),
                                        DistrictName = row["DistrictName"].ToString()                                       
                                    }).ToList();
                    return ListEntities;
                }

            }
            return ListEntities = null;
        }
        public static void GetDropDownList(DropDownList dl)
        {
            DistrictEntry clsGrp = new DistrictEntry();

            if (grpList == null)
            {
                grpList = clsGrp.GetEntitiesData();
            }
            dl.DataTextField = "DistrictName";
            dl.DataValueField = "DistrictId";
            dl.DataSource = grpList.ToList();
            dl.DataBind();
            dl.Items.Insert(0, new ListItem("...Select...", "0"));
            dl.Enabled = true;
          
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
