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
    public class DepartmentEntry : IDisposable
    {
        private DepartmentEntities _Entities;
        private static List<DepartmentEntities> deptList; 
        string sql = string.Empty;
        bool result = true;
        public DepartmentEntry() { }
        public DepartmentEntities AddEntities
        {
            set
            {
                _Entities = value;
            }
        }

        public bool Insert()
        {
            sql = string.Format("INSERT INTO [dbo].[Departments] " +
                "([DName],[DStatus],[IsTeacher]) VALUES ( " +
                "'" + _Entities.DepartmentName + "'," +
                "'" + _Entities.Status + "'," +
                "'" + _Entities.IsTeacher + "')");
            return result = CRUD.ExecuteQuery(sql);
        }
        public bool Update()
        {
            sql = string.Format("UPDATE [dbo].[Departments] SET " +
                "[DName] = '" + _Entities.DepartmentName + "', " +
                "[DStatus] = '" + _Entities.Status + "', " +
                "[IsTeacher] = '" + _Entities.IsTeacher + "', " +
                "WHERE [DId] = '" + _Entities.DepartmentId + "'");
            return result = CRUD.ExecuteQuery(sql);
        }
        public List<DepartmentEntities> GetEntitiesData()
        {
            List<DepartmentEntities> ListEntities = new List<DepartmentEntities>();
            sql = string.Format("SELECT [DId],[DName],[DStatus],[IsTeacher] FROM [dbo].[Departments]");
            DataTable dt = new DataTable();

            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows
                                    select new DepartmentEntities
                                    {
                                        DepartmentId = int.Parse(row["DId"].ToString()),
                                        DepartmentName = row["DName"].ToString(),
                                        IsTeacher = bool.Parse(row["DStatus"].ToString()),
                                        Status = bool.Parse(row["IsTeacher"].ToString())
                                    }).ToList();
                    return ListEntities;
                }

            }
            return ListEntities = null;
        }
        public static void GetDropdownlist(DropDownList dl)
        {
            DepartmentEntry dept = new DepartmentEntry();
            if(deptList == null)
            {
                deptList = dept.GetEntitiesData();
            }
            dl.DataValueField = "DepartmentId";
            dl.DataTextField = "DepartmentName";
            dl.DataSource = deptList;
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
