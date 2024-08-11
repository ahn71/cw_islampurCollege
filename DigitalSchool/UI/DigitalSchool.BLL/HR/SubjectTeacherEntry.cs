using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.DAL;
using DS.PropertyEntities.Model.HR;
using DS.PropertyEntities.Model.ManagedSubject;
using System.Data;

namespace DS.BLL.HR
{
    public class SubjectTeacherEntry :IDisposable
    {
        private SubjectTeacher _Entities;
        string sql = string.Empty;
        bool result = true;
        public SubjectTeacherEntry() { }
        public SubjectTeacher AddEntities
        {
            set
            {
                _Entities = value;
            }
        }

        public bool SelectInsertUpdate()
        {
            sql = string.Format("SELECT [SubTecherId] FROM [dbo].[Tbl_Subject_Teacher] " +
                                "WHERE [SubId] = '" + _Entities.SubjectId + "' " +
                                "AND [EId] = '" + _Entities.TeacherId + "' " +
                                "AND [ClassId] = '" + _Entities.ClassId + "'");
            DataTable dt = new DataTable();
            dt = CRUD.ReturnTableNull(sql);
            if(dt != null)
            {
                if(dt.Rows.Count > 0)
                {
                    return result = Update(int.Parse(dt.Rows[0]["SubTecherId"].ToString()));
                }
                return result = Insert();
            }
            return result = false;
        }
        public bool Insert()
        {
            sql = string.Format("INSERT INTO [dbo].[Tbl_Subject_Teacher] " +
                "([SubId],[EId],[ClassId]) VALUES (" +
                "'" + _Entities.SubjectId + "'," +
                "'" + _Entities.TeacherId + "'," +
                "'" + _Entities.ClassId + "')");
            return result = CRUD.ExecuteQuery(sql);
        }
        public bool Update(int id)
        {
            sql = string.Format("UPDATE [dbo].[Tbl_Subject_Teacher] SET " +                                
                                "[EId] = '" + _Entities.TeacherId + "' " +
                                "WHERE [SubTecherId] = '" + id + "'");          
            return result = CRUD.ExecuteQuery(sql);
        }
        public List<SubjectTeacher> GetEntitiesData()
        {
            List<SubjectTeacher> ListEntities = new List<SubjectTeacher>();
            sql = string.Format("SELECT  * " +
                                "FROM [dbo].[Tbl_Subject_Teacher]");
            DataTable dt = new DataTable();

            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows
                                    select new SubjectTeacher
                                    {
                                        SubTecherId = int.Parse(row["SubTecherId"].ToString()),
                                        TeacherId = int.Parse(row["EId"].ToString()),
                                        SubjectId = int.Parse(row["SubId"].ToString()),
                                        ClassId = int.Parse(row["ClassId"].ToString())                                                              
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
