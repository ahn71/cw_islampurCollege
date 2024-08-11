using DS.DAL;
using DS.PropertyEntities.Model.Timetable;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;

namespace DS.BLL.Timetable
{
    public class ClsTimeSpecificationEntry : IDisposable
    {
        private ClassTimeSpecificationEntities _Entities;        
        private string sql = string.Empty;
        bool result = true;
        public ClsTimeSpecificationEntry()
        {

        }

        public ClassTimeSpecificationEntities AddEntities
        {
            set
            {
                _Entities = value;
            }
        }       

        public bool Insert()
        {
            sql = string.Format("INSERT INTO [dbo].[Tbl_ClassTime_Specification] " +
                " ([Name],[StartTime],[EndTime],[OrderBy],[IsBreakTime]) " +
                " VALUES ( '" + _Entities.Name + "', '" + _Entities.StartTime + "', " +
                " '" + _Entities.EndTime + "', '" + _Entities.OrderBy + "', '" + _Entities.IsbreakTime + "'); " +
                " SELECT SCOPE_IDENTITY()");
            int MaxId = CRUD.GetMaxID(sql);
            if(MaxId > 0)
            {
                result = ClsTimeWithSetNameInsert(MaxId, _Entities.ClsTimeSetNameId);
            }
            return result;
        }

        private bool ClsTimeWithSetNameInsert(int ClassTimeId, int ClsTimeSetNameId)
        {
            sql = string.Format("INSERT INTO [dbo].[Tbl_ClassTimeWithSetName] " +
                " ([ClassTimeId],[ClsTimeSetNameId])" +
                " VALUES ( '" + ClassTimeId + "', '" + ClsTimeSetNameId + "')");
            return result = CRUD.ExecuteQuery(sql);
        }

        public bool Update()
        {
            sql = string.Format("UPDATE [dbo].[Tbl_ClassTime_Specification] SET" +
                                " [Name] = '" + _Entities.Name + "'," +
                                " [StartTIme] = '" + _Entities.StartTime + "', " +
                                " [EndTime] = '" + _Entities.EndTime + "', " +
                                " [OrderBy] = '" + _Entities.OrderBy + "', " +
                                " [IsBreakTIme] = '" + _Entities.IsbreakTime + "'" +
                                " WHERE [ClassTimeId] = '" + _Entities.ClassTimeId + "'");
            return result = CRUD.ExecuteQuery(sql); 
        }

        public List<ClassTimeSpecificationEntities> GetEntitiesData(int? ClsTimeSetNameId)
        {
            List<ClassTimeSpecificationEntities> ListEntities = new List<ClassTimeSpecificationEntities>();
            sql = string.Format("SELECT a.[ClassTimeId],a.[Name],a.[StartTime],a.[EndTime], " +
                    " a.[Period],a.[OrderBy],a.[IsBreakTime], b.[ClsTimeSetNameId] FROM [dbo].[Tbl_ClassTime_Specification] a INNER JOIN " +
                    " [dbo].[Tbl_ClassTimeWithSetName] b ON (a.[ClassTimeId] = b.[ClassTimeId])" +
                    " WHERE b.[ClsTimeSetNameId] = '" + ClsTimeSetNameId + "' ORDER BY a.[OrderBy]");
            DataTable dt = new DataTable();
            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows

                                    select new ClassTimeSpecificationEntities
                                    {
                                        ClassTimeId = int.Parse(row["ClassTimeId"].ToString()),
                                        ClsTimeSetNameId = int.Parse(row["ClsTimeSetNameId"].ToString()),
                                        Name = row["Name"].ToString(),
                                        StartTime = DateTime.Parse(row["StartTime"].ToString()),
                                        EndTime = DateTime.Parse(row["EndTime"].ToString()),
                                        period = row["Period"].ToString(),
                                        OrderBy = int.Parse(row["OrderBy"].ToString()),
                                        IsbreakTime = bool.Parse(row["IsBreakTime"].ToString())
                                    }).ToList();
                    return ListEntities;
                }
            }
            return ListEntities = null;
        }

        public bool ValidationChk()
        {
            return result = false;
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
