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
    public class SectionEntry : IDisposable
    {
        private SectionEntities _Entities;
        string sql = string.Empty;
        bool result = true;
        public SectionEntry() { }
        public SectionEntities AddEntities
        {
            set
            {
                _Entities = value;
            }
        }

        public bool Insert()
        {
            sql = string.Format("INSERT INTO [dbo].[Sections] " +
                "([SectionName]) VALUES (" +                 
                "'" + _Entities.SectionName + "')");
            return result = CRUD.ExecuteQuery(sql);
        }
        public bool Update()
        {
            sql = string.Format("UPDATE [dbo].[Sections] SET " +
                "[SectionName] = '" + _Entities.SectionName + "' " +
                "WHERE [SectionID] = '" + _Entities.SectionID + "'");
            return result = CRUD.ExecuteQuery(sql);
        }
        public List<SectionEntities> GetEntitiesData()
        {
            List<SectionEntities> ListEntities = new List<SectionEntities>();
            sql = string.Format("SELECT [SectionID],[SectionName] " +
                                "FROM [dbo].[Sections]");
            DataTable dt = new DataTable();

            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows
                                    select new SectionEntities
                                    {
                                        SectionID = int.Parse(row["SectionID"].ToString()),
                                        SectionName = row["SectionName"].ToString()                                        
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
                SectionEntry SectionName = new SectionEntry();
                List<SectionEntities> ClassNameList = SectionName.GetEntitiesData();
                dl.DataTextField = "SectionName";
                dl.DataValueField = "SectionID";
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
