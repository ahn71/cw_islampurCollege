using DS.DAL;
using DS.PropertyEntities.Model.TeacherEvaluation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DS.BLL.TeacherEvaluation
{
    public class SubCategoryEntry : IDisposable
    {
        private SubCategoryEntities _Entities;
        string sql = string.Empty;
        bool result = false;
        DataTable dt;
        public SubCategoryEntities AddEntities
        {
            set
            {
                _Entities = value;
            }
        }
        public bool Insert()
        {
            sql = string.Format("INSERT INTO [dbo].[TE_SubCategory] " +
                 "([CategoryID],[SubCategory],[Ordering],[Status])"
                 + " VALUES ( " + _Entities.Category.CategoryID + ",'" + _Entities.SubCategory + "'," + _Entities.Ordering + ",'" + _Entities.Status+ "')");
            return result = CRUD.ExecuteQuery(sql);
        }
        public bool Update()
        {
            sql = string.Format("UPDATE [dbo].[TE_SubCategory] " +
                 " SET [CategoryID]=" + _Entities.Category.CategoryID + ",[SubCategory]='" + _Entities.SubCategory + "',[Ordering]=" + _Entities.Ordering + ",[Status]='" + _Entities.Status + "'"
                 + " WHERE [SubCategoryID]=" + _Entities.SubCategoryID + "");
            return result = CRUD.ExecuteQuery(sql);
        }
        public List<SubCategoryEntities> GetEntitiesData()
        {
            List<SubCategoryEntities> ListEntities = new List<SubCategoryEntities>();
            sql = string.Format("SELECT [SubCategoryID],[SubCategory],c.[CategoryID],[Category],sc.[Ordering],[Status] FROM [dbo].[TE_SubCategory] sc inner join [dbo].[TE_Category] c on sc.CategoryID=c.CategoryID");
            DataTable dt = new DataTable();
            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows
                                    select new SubCategoryEntities
                                    {
                                        Category=new CategoryEntities{
                                            CategoryID = int.Parse(row["CategoryID"].ToString()),
                                            Category = row["Category"].ToString()
                                        },

                                        SubCategoryID = int.Parse(row["SubCategoryID"].ToString()),
                                        SubCategory = row["SubCategory"].ToString(),
                                        
                                        Ordering = int.Parse(row["Ordering"].ToString()),
                                        Status =bool.Parse(row["Status"].ToString())

                                    }).ToList();
                    return ListEntities;
                }

            }
            return ListEntities = null;
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