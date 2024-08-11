using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.DAL;
using DS.PropertyEntities.Model.ManagedClass;
using System.Data;
using System.Web.UI.WebControls;

namespace DS.BLL.ManagedClass
{
    public class ClassEntry : IDisposable
    {
        private ClassEntities _Entities;
        static  string sql = string.Empty;
        bool result = true;
        public ClassEntry() { }
        public ClassEntities AddEntities
        {
            set
            {
                _Entities = value;
            }
        }

        public bool Insert()
        {
            sql = string.Format("INSERT INTO [dbo].[Classes] " +
                "([ClassName], [ClassOrder]) VALUES ( '" + _Entities.ClassName + "', " +
                " '" + _Entities.ClassOrder + "')");
            return result = CRUD.ExecuteQuery(sql);
        }
        public bool Update()
        {
            sql = string.Format("UPDATE [dbo].[Classes] SET " +
                "[ClassName] = '" + _Entities.ClassName + "', " +
                "[ClassOrder] = '" + _Entities.ClassOrder + "' " +
                "WHERE [ClassID] = '" + _Entities.ClassID + "'");
            return result = CRUD.ExecuteQuery(sql);
        }
        public List<ClassEntities> GetEntitiesData()
        {
            List<ClassEntities> ListEntities = new List<ClassEntities>();
            sql = string.Format("SELECT [ClassID],[ClassName],[ClassOrder] FROM [dbo].[Classes]  ORDER BY [ClassOrder]");
            DataTable dt = new DataTable();

            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows
                                    select new ClassEntities
                                    {
                                        ClassID = int.Parse(row["ClassID"].ToString()),
                                        ClassName = row["ClassName"].ToString(),
                                        ClassOrder = int.Parse(row["ClassOrder"].ToString())
                                    }).ToList();
                    return ListEntities;
                }

            }
            return ListEntities = null;
        }

        public List<ClassEntities> GetEntitiesData(string className)
        {
            List<ClassEntities> ListEntities = new List<ClassEntities>();
            sql = string.Format("SELECT [ClassID],[ClassName],[ClassOrder] FROM [dbo].[Classes] WHERE [ClassName] = '" + className + "'");
            DataTable dt = new DataTable();

            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows
                                    select new ClassEntities
                                    {
                                        ClassID = int.Parse(row["ClassID"].ToString()),
                                        ClassName = row["ClassName"].ToString(),
                                        ClassOrder = int.Parse(row["ClassOrder"].ToString())
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
                DataTable dt = new DataTable();
                sql = string.Format("SELECT [ClassID],[ClassName],[ClassOrder] FROM [dbo].[Classes] where (IsActive is null or IsActive=1)  ORDER BY [ClassOrder]");
                dt = CRUD.ReturnTableNull(sql);
                dl.DataTextField = "ClassName";
                dl.DataValueField = "ClassID";
                dl.DataSource = dt;
                dl.DataBind();
                dl.Items.Insert(0, new ListItem("...Select...", "0"));
            }
            catch { }
        }
        public static void GetAdmissionClasses(DropDownList dl)
        {
            try
            {
                DataTable dt = new DataTable();
                sql = string.Format("SELECT convert(varchar, ca.[ClassID]) + '_' + convert(varchar, ca.[AdmissionYear]) as ClassID, c.[ClassName], c.[ClassOrder], ca.[AdmissionYear] FROM[dbo].[ClassesAdmission] ca inner join[dbo].[Classes] c on ca.ClassID = c.ClassID where ca.IsActive = 1 ORDER BY c.[ClassOrder]");
                dt = CRUD.ReturnTableNull(sql);
                dl.DataTextField = "ClassName";
                dl.DataValueField = "ClassID";
                dl.DataSource = dt;
                dl.DataBind();
                dl.Items.Insert(0, new ListItem("...Select...", "0"));
            }
            catch { }
        }
        public static void GetEntitiesDataWithAll(DropDownList dl)
        {
            try
            {
                DataTable dt = new DataTable();
                sql = string.Format("SELECT [ClassID],[ClassName],[ClassOrder] FROM [dbo].[Classes] where (IsActive is null or IsActive=1)  ORDER BY [ClassOrder]");
                dt = CRUD.ReturnTableNull(sql);
                dl.DataTextField = "ClassName";
                dl.DataValueField = "ClassID";
                dl.DataSource = dt;
                dl.DataBind();
                if(dl.Items.Count>1)
                dl.Items.Insert(0, new ListItem("All", "00"));
            }
            catch { }
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
