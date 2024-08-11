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
    public class stdtypeEntry: IDisposable
    {
        private stdtypeEntities _Entities;
        string sql = string.Empty;
        bool result = true;
        public stdtypeEntry() { }
        public stdtypeEntities AddEntities
        {
            set
            {
                _Entities = value;
            }
        }

        public bool Insert()
        {
            sql = string.Format("INSERT INTO [dbo].[tbl_StdType] " +
                "([StdTypeName]) VALUES (" +                 
                "'" + _Entities.StdTypeName + "')");
            return result = CRUD.ExecuteQuery(sql);
        }
        public bool Update()
        {
            sql = string.Format("UPDATE [dbo].[tbl_StdType] SET " +
                "[SectionName] = '" + _Entities.StdTypeName + "' " +
                "WHERE [StdTypeId] = '" + _Entities.StdTypeId + "'");
            return result = CRUD.ExecuteQuery(sql);
        }
        public List<stdtypeEntities> GetEntitiesData()
        {
            List<stdtypeEntities> ListEntities = new List<stdtypeEntities>();
            sql = string.Format("SELECT [StdTypeId],[StdTypeName] " +
                                "FROM [dbo].[tbl_StdType]");
            DataTable dt = new DataTable();

            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows
                                    select new stdtypeEntities
                                    {
                                        StdTypeId = int.Parse(row["StdTypeId"].ToString()),
                                        StdTypeName = row["StdTypeName"].ToString()                                        
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
                stdtypeEntry stdtype = new stdtypeEntry();
                List<stdtypeEntities> ClassNameList = stdtype.GetEntitiesData();
                dl.DataTextField = "StdTypeName";
                dl.DataValueField = "StdTypeId";
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
