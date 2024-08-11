using DS.DAL;
using DS.PropertyEntities.Model.Examinition;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace DS.BLL.Examinition
{
    public class QuestionPatternEntry:IDisposable
    {
         private QuestionPatternEntities _Entities;
        private static List<QuestionPatternEntities> QPatternList;
        static string sql = string.Empty;
        bool result = true;
        public QuestionPatternEntry() { }
        public QuestionPatternEntities AddEntities
        {
            set
            {
                _Entities = value;
            }
        }

        public bool Insert()
        {
            sql = string.Format("INSERT INTO [dbo].[QuestionPattern] " +
                "([QPName]) VALUES ( '" + _Entities.QPName + "')");
            return result = CRUD.ExecuteQuery(sql);
        }
        public bool Update()
        {
            sql = string.Format("UPDATE [dbo].[QuestionPattern] SET " +
                "[QPName] = '" + _Entities.QPName + "', " +               
                "WHERE [BatchId] = '" + _Entities.QPId+ "'");
            return result = CRUD.ExecuteQuery(sql);
        }
        public List<QuestionPatternEntities> GetEntitiesData()
        {
            List<QuestionPatternEntities> ListEntities = new List<QuestionPatternEntities>();
            sql = string.Format("SELECT [QPId],[QPName] "
            + "FROM [dbo].[QuestionPattern]");
            DataTable dt = new DataTable();

            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows
                                    select new QuestionPatternEntities
                                    {
                                        QPId = int.Parse(row["QPId"].ToString()),
                                        QPName = row["QPName"].ToString()                                       
                                    }).ToList();
                    return ListEntities;
                }

            }
            return ListEntities = null;
        }       
        public static void GetDropdownlist(DropDownList dl)
        {
            sql = string.Format("SELECT [QPId],[QPName] FROM [dbo].[QuestionPattern] where IsActive is null or IsActive=1");
            DataTable dt = new DataTable();

            dt = CRUD.ReturnTableNull(sql);
            dl.DataValueField = "QPId";
            dl.DataTextField = "QPName";
            dl.DataSource = dt;
            dl.DataBind();
            dl.Items.Insert(0, new ListItem("...Select...", "0"));           
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
