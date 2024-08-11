using DS.DAL;
using DS.PropertyEntities.Model.Timetable;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DS.BLL.Timetable
{
    public class WeeklyDaysBLL : IDisposable
    {
        private WeeklyDaysEntities _weeklyDays;
        string sql = string.Empty;
        bool result = true;

        public WeeklyDaysBLL()
        { 
        }
        public WeeklyDaysEntities AddWeeklyDaysEntities
        {
            set
            {
                _weeklyDays = value;
            }
        }
        public bool UpdateDaySeletion(WeeklyDaysEntities weeklyDays)
        {
            string sql = string.Format("UPDATE [dbo].[Tbl_Weekly_days] SET " +
                                       "[Status] = '" + weeklyDays.status + "' " +
                                       "WHERE [ShortDayName] = '" + weeklyDays.DayShortName + "'");
            return result = CRUD.ExecuteQuery(sql); 
        }

        public List<WeeklyDaysEntities> GetWDaysEntities()
        {
            List<WeeklyDaysEntities> ListWDaysEntities = new List<WeeklyDaysEntities>();
            sql = string.Format("SELECT [WDayId],[DayName],[ShortDayName],[Status] FROM [dbo].[Tbl_Weekly_days]");
            DataTable dt = new DataTable();
            dt = CRUD.ReturnTableNull(sql);
            if (dt.Rows.Count > 0)
            {
                ListWDaysEntities = (from DataRow row in dt.Rows
                                select new WeeklyDaysEntities
                                 {
                                     Id = int.Parse(row["WDayId"].ToString()),
                                     DayName = row["DayName"].ToString(),
                                     DayShortName = row["ShortDayName"].ToString(),
                                     status = Convert.ToBoolean(row["Status"].ToString())
                                 }).ToList();
            }
            else
            {
                ListWDaysEntities = null;
            }
            return ListWDaysEntities;
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
