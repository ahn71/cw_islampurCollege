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
        string sql = string.Empty;
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
            sql = string.Format("SELECT [ClassID],[ClassName],[ClassOrder] FROM [dbo].[Classes] ORDER BY [ClassOrder]");
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
                ClassEntry ClassName = new ClassEntry();
                List<ClassEntities> ClassNameList = ClassName.GetEntitiesData();
                dl.DataTextField = "ClassName";
                dl.DataValueField = "ClassID";
                dl.DataSource = ClassNameList;
                dl.DataBind();
                dl.Items.Insert(0, new ListItem("...Select...", "0"));
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
