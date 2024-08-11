using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.PropertyEntities.Model.Timetable;
using DS.DAL;
using System.Data;

namespace DS.BLL.Timetable
{
    public class SubTeacherName : IDisposable
    {
        private SubTeacherNameList _Entities;
        string sql = string.Empty;
        bool result = true;
        public SubTeacherName() { }
        public SubTeacherNameList AddEntities
        {
            set
            {
                _Entities = value;
            }
        }

        public bool Insert()
        {            
            return result = CRUD.ExecuteQuery(sql);
        }
        public bool Update()
        {            
            return result = CRUD.ExecuteQuery(sql);
        }
        public List<SubTeacherNameList> GetEntitiesData(int classId)
        {
            List<SubTeacherNameList> ListEntities = new List<SubTeacherNameList>();
            sql = string.Format("SELECT st.[SubTecherId], s.[SubName], e.[EName]  FROM [dbo].[Tbl_Subject_Teacher] st " +
                                "INNER JOIN [dbo].[NewSubject] s ON (st.[SubId] = s.[SubId]) INNER JOIN " +
                                "[dbo].[EmployeeInfo] e ON  (st.[EId] = e.[EID]) WHERE st.[ClassId] = " + classId + "");
            DataTable dt = new DataTable();

            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows
                                    select new SubTeacherNameList
                                    {
                                        SubTecherId = int.Parse(row["SubTecherId"].ToString()),
                                        Subject = row["SubName"].ToString(),
                                        Teacher = row["EName"].ToString()
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
