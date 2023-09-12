using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DS.PropertyEntities.Model.Timetable;
using DS.DAL;


namespace DS.BLL.Timetable
{
    public class Tbl_ExamTime_SpecificationEntry
    {
        public bool result = false;
        DataTable dt = new DataTable();
        Tbl_ExamTime_Specification _Entities;
        string sql;

        public Tbl_ExamTime_Specification AddEntities
        {
            set
            {
                _Entities = value;
            }
        }

        public bool Insert()
        {
            sql = string.Format("INSERT INTO [dbo].[Tbl_ExamTime_Specification] " +
                " ([Name],[StartTime],[EndTime],[OrderBy],[IsBreakTime],ExamTimeSetNameId) " +
                " VALUES ( '" + _Entities.Name + "', '" + _Entities.StartTime + "', " +
                " '" + _Entities.EndTime + "', '" + _Entities.OrderBy + "', '" + _Entities.IsBreakTime + "','"+_Entities.ExamTimeSetNameId+"'); " +
                " SELECT SCOPE_IDENTITY()");
            result = CRUD.ExecuteQuery(sql);
           
            return result;
        }

        //private bool ClsTimeWithSetNameInsert(int ClassTimeId, int ExamTimeSetNameId)
        //{
        //    sql = string.Format("INSERT INTO [dbo].[Tbl_ExamTime_Specification] " +
        //        " ([ClassTimeId],[ExamTimeSetNameId])" +
        //        " VALUES ( '" + ClassTimeId + "', '" + ExamTimeSetNameId + "')");
        //    return result = CRUD.ExecuteQuery(sql);
        //}

        public bool Update()
        {
            sql = string.Format("UPDATE [dbo].[Tbl_ExamTime_Specification] SET" +
                                " [Name] = '" + _Entities.Name + "'," +
                                " [StartTIme] = '" + _Entities.StartTime + "', " +
                                " [EndTime] = '" + _Entities.EndTime + "', " +
                                " [OrderBy] = '" + _Entities.OrderBy + "', " +
                                " [IsBreakTIme] = '" + _Entities.IsBreakTime + "'" +
                                " WHERE [ExamTimeId] = '" + _Entities.ExamTimeId + "'");
            return result = CRUD.ExecuteQuery(sql);
        }

        public List<Tbl_ExamTime_Specification> GetEntitiesData(int? ExamTimeSetNameId)
        {
            List<Tbl_ExamTime_Specification> ListEntities = new List<Tbl_ExamTime_Specification>();
            sql = "SELECT ExamTimeId,Name,StartTime,EndTime,Period,OrderBy,IsBreakTime, ExamTimeSetNameId FROM Tbl_ExamTime_Specification  WHERE ExamTimeSetNameId = '" + ExamTimeSetNameId + "' ORDER BY OrderBy";
               
            DataTable dt = new DataTable();
            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows

                                    select new Tbl_ExamTime_Specification
                                    {
                                        ExamTimeId = int.Parse(row["ExamTimeId"].ToString()),
                                        ExamTimeSetNameId = int.Parse(row["ExamTimeSetNameId"].ToString()),
                                        Name = row["Name"].ToString(),
                                        StartTime = DateTime.Parse(row["StartTime"].ToString()),
                                        EndTime = DateTime.Parse(row["EndTime"].ToString()),
                                        Period = row["Period"].ToString(),
                                        OrderBy = int.Parse(row["OrderBy"].ToString()),
                                        IsBreakTime = bool.Parse(row["IsBreakTime"].ToString())
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
