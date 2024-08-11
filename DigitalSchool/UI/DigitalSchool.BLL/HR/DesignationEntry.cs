using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.DAL;
using DS.PropertyEntities.Model.HR;
using System.Data;
using System.Web.UI.WebControls;

namespace DS.BLL.HR
{
    public class DesignationEntry : IDisposable
    {
        private DesignationEntities _Entities;
        private static List<DesignationEntities> designationList;
        string sql = string.Empty;
        bool result = true;
        public DesignationEntry() { }
        public DesignationEntities AddEntities
        {
            set
            {
                _Entities = value;
            }
        }

        public bool Insert()
        {
            sql = string.Format("INSERT INTO [dbo].[Designations] " +
                "([DesName]) VALUES (" +
                "'" + _Entities.DesignationName + "')");
            return result = CRUD.ExecuteQuery(sql);
        }
        public bool Update()
        {
            sql = string.Format("UPDATE [dbo].[Designations] SET " +
                "[DesName] = '" + _Entities.DesignationName + "' " +
                "WHERE [DesId] = '" + _Entities.DesignationId + "'");
            return result = CRUD.ExecuteQuery(sql);
        }
        public List<DesignationEntities> GetEntitiesData()
        {
            List<DesignationEntities> ListEntities = new List<DesignationEntities>();
            sql = string.Format("SELECT [DesId],[DesName] " +
                                "FROM [dbo].[Designations]");
            DataTable dt = new DataTable();

            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows
                                    select new DesignationEntities
                                    {
                                        DesignationId = int.Parse(row["DesId"].ToString()),
                                        DesignationName = row["DesName"].ToString()
                                    }).ToList();
                    return ListEntities;
                }
            }
            return ListEntities = null;
        }
        public static void GetDropdownlist(DropDownList dl)
        {
            DesignationEntry designation = new DesignationEntry();
            if (designationList == null)
            {
                designationList = designation.GetEntitiesData();
            }
            dl.DataValueField = "DesignationId";
            dl.DataTextField = "DesignationName";
            dl.DataSource = designationList;
            dl.DataBind();
            dl.Items.Insert(0, new ListItem("...Select...", "0"));
            dl.Items.Insert(1, new ListItem("All", "All"));
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
