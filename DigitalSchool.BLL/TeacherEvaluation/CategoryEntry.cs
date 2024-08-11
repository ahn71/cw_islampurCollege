using DS.DAL;
using DS.PropertyEntities.Model.TeacherEvaluation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace DS.BLL.TeacherEvaluation
{
   public class CategoryEntry : IDisposable
    {
       private CategoryEntities _Entities;       
       string sql = string.Empty;
       bool result = false;
       DataTable dt;
       public CategoryEntities AddEntities 
       {
           set 
           {
               _Entities = value; 
           }
       }
       public bool Insert() 
       {
           sql = string.Format("INSERT INTO [dbo].[TE_Category] " +
                "([Category],[Ordering])"
                + " VALUES ( '" + _Entities.Category + "'," + _Entities.Ordering + ")");
           return result = CRUD.ExecuteQuery(sql);  
       }
       public bool Update()
       {
           sql = string.Format("UPDATE [dbo].[TE_Category] " +
                " SET [Category]='"+_Entities.Category+"',[Ordering]="+_Entities.Ordering+""
                + " WHERE [CategoryID]="+_Entities.CategoryID+"");
           return result = CRUD.ExecuteQuery(sql);
       }
       public List<CategoryEntities> GetEntitiesData()
       {
           List<CategoryEntities>  ListEntities = new List<CategoryEntities>();
           sql = string.Format("SELECT [CategoryID],[Category],[Ordering] FROM [dbo].[TE_Category]");
           DataTable dt = new DataTable();

           dt = CRUD.ReturnTableNull(sql);
           if (dt != null)
           {
               if (dt.Rows.Count > 0)
               {
                   ListEntities = (from DataRow row in dt.Rows
                                   select new CategoryEntities
                                   {
                                       CategoryID = int.Parse(row["CategoryID"].ToString()),
                                       Category = row["Category"].ToString(),
                                       Ordering = int.Parse(row["Ordering"].ToString())
                                       
                                   }).ToList();
                   return ListEntities;
               }

           }
           return ListEntities = null;
       }
       public void GetDropdownlist(DropDownList dl)
       {
         
           sql = string.Format("SELECT [CategoryID],[Category] FROM [dbo].[TE_Category] ORDER BY [Ordering]");
           DataTable dt = new DataTable();

           dt = CRUD.ReturnTableNull(sql);
            dl.DataSource = dt;
           dl.DataValueField = "CategoryID";
           dl.DataTextField = "Category";          
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