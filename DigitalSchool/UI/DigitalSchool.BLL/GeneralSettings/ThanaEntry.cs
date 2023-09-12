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
    public class ThanaEntry : IDisposable
    {
        private ThanaEntities _Entities;
        string sql = string.Empty;
        bool result = true;
        static List<ThanaEntities> grpList;
        public ThanaEntry() { }
        public ThanaEntities AddEntities
        {
            set
            {
                _Entities = value;
            }
        }

        public bool Insert()
        {
            sql = string.Format("INSERT INTO [dbo].[Thanas] " +
                "([DistrictId],[ThanaName]) VALUES ( " +
                "'" + _Entities.DistrictId + "', " +
                "'" + _Entities.ThanaName + "')");
            return result = CRUD.ExecuteQuery(sql);
        }
        public bool Update()
        {
            sql = string.Format("UPDATE [dbo].[Thanas] SET " +
                "[DistrictId] = '" + _Entities.DistrictId + "' " +
                "[ThanaName] = '" + _Entities.ThanaName + "' " +
                "WHERE [ThanaId] = '" + _Entities.ThanaId + "'");
            return result = CRUD.ExecuteQuery(sql);
        }
        public List<ThanaEntities> GetEntitiesData()
        {
            List<ThanaEntities> ListEntities = new List<ThanaEntities>();
            sql = string.Format("SELECT [ThanaId],[DistrictId],[ThanaName] FROM [dbo].[Thanas]");
            DataTable dt = new DataTable();

            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows
                                    select new ThanaEntities
                                    {
                                        ThanaId = int.Parse(row["ThanaId"].ToString()),
                                        DistrictId = int.Parse(row["DistrictId"].ToString()),
                                        ThanaName = row["ThanaName"].ToString()                                      
                                    }).ToList();
                    return ListEntities;
                }

            }
            return ListEntities = null;
        }
        public static void GetDropDownList(int districId, DropDownList dl)
        {
            ThanaEntry clsGrp = new ThanaEntry();
            var thana = grpList;
            if (grpList == null)
            {
                grpList = clsGrp.GetEntitiesData();
            }
            if (grpList != null)
            {
                thana = grpList.FindAll(c => c.DistrictId == districId);
            }
            if (thana != null)
            {
                dl.DataTextField = "ThanaName";
                dl.DataValueField = "ThanaId";
                dl.DataSource = thana.ToList();
                dl.DataBind();
                dl.Items.Insert(0, new ListItem("...Select...", "0"));
            }
            else
            {
                dl.DataSource = thana;
            }

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
