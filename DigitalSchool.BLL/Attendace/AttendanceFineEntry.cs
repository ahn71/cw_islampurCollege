using DS.DAL;
using DS.PropertyEntities.Model.Attendance;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DS.BLL.Attendace
{
    public class AttendanceFineEntry:IDisposable
    {
        private AttendanceFineEntities _Entities;
        string sql = string.Empty;
        bool result = true;
        public AttendanceFineEntry() { }
        public AttendanceFineEntities AddEntities
        {
            set
            {
                _Entities = value;
            }
        }

        public bool Insert()
        {
            UpdateIsActive();
            sql = string.Format("INSERT INTO [dbo].[AbsentFine] " +
                "([AbsentFineAmount],[Date],[IsActive]) VALUES ( " +
                "'" + _Entities.AbsentFineAmount + "', " +
                "'" + _Entities.Date + "','"+_Entities.IsActive+"')");
            return result = CRUD.ExecuteQuery(sql);
        }
        public bool Update()
        {
            UpdateIsActive();
            sql = string.Format("UPDATE [dbo].[AbsentFine] SET " +
                "[AbsentFineAmount] = '" + _Entities.AbsentFineAmount + "', " +
                "[Date] = '" + _Entities.Date + "', " +
                "[IsActive]='" + _Entities.IsActive + "' " +
                "WHERE [AFId] = '" + _Entities.AFId + "'");
            return result = CRUD.ExecuteQuery(sql);
        }
        private bool UpdateIsActive()
        {
            sql = string.Format("Update AbsentFine set IsActive=0");
            return result = CRUD.ExecuteQuery(sql);
        }
        public List<AttendanceFineEntities> GetEntitiesData()
        {
            List<AttendanceFineEntities> ListEntities = new List<AttendanceFineEntities>();
            sql = string.Format("SELECT [AFId],[AbsentFineAmount],[Date],[IsActive] "
            +"FROM [dbo].[AbsentFine] ORDER BY AFId DESC");
            DataTable dt = new DataTable();

            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows
                                    select new AttendanceFineEntities
                                    {
                                        AFId = int.Parse(row["AFId"].ToString()),
                                        AbsentFineAmount = double.Parse(row["AbsentFineAmount"].ToString()),
                                        Date = DateTime.Parse(row["Date"].ToString()),
                                        IsActive = Boolean.Parse(row["IsActive"].ToString())
                                    }).ToList();
                    return ListEntities;
                }

            }
            return ListEntities = null;
        }
        public DataTable LoadAbsentFine(string stdId)
        {
            DataTable dt = CRUD.ReturnTableNull("SELECT SUM(AbsentFine) as AbsentFine  FROM StudentAbsentDetails WHERE IsPaid='0' AND StudentId='"+stdId+"' ");
            return dt;
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
