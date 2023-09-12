using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.PropertyEntities.Model.Attendance;
using DS.DAL;
using System.Data;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Data.SqlClient;
using DS.DAL.ComplexScripting;

namespace DS.BLL.Attendace
{
    public class SheetInfoEntry : IDisposable
    {
        private SheetInfoEntities _Entities;
        string sql = string.Empty;
        bool result = true;
        public SheetInfoEntry() { }
        public SheetInfoEntities AddEntities
        {
            set
            {
                _Entities = value;
            }
        }

        public static void loadMonths(DropDownList dl)
        {
            dl.Items.Clear();
            dl.Items.Add(new ListItem("Select Month", "0"));
            dl.Items.Insert(1, new ListItem ("January-"+DateTime.Now.Year,"01-"+DateTime.Now.Year.ToString()));
            dl.Items.Insert(2, new ListItem("February-" + DateTime.Now.Year, "02-" + DateTime.Now.Year.ToString()));
            dl.Items.Insert(3, new ListItem("March-" + DateTime.Now.Year, "03-" + DateTime.Now.Year.ToString()));
            dl.Items.Insert(4, new ListItem("April-" + DateTime.Now.Year, "04-" + DateTime.Now.Year.ToString()));
            dl.Items.Insert(5, new ListItem("May-" + DateTime.Now.Year, "05-" + DateTime.Now.Year.ToString()));
            dl.Items.Insert(6, new ListItem("June-" + DateTime.Now.Year, "06-" + DateTime.Now.Year.ToString()));
            dl.Items.Insert(7, new ListItem("July-" + DateTime.Now.Year, "07-" + DateTime.Now.Year.ToString()));
            dl.Items.Insert(8, new ListItem("August-" + DateTime.Now.Year, "08-" + DateTime.Now.Year.ToString()));
            dl.Items.Insert(9, new ListItem("September-" + DateTime.Now.Year, "09-" + DateTime.Now.Year.ToString()));
            dl.Items.Insert(10, new ListItem("October-" + DateTime.Now.Year, "10-" + DateTime.Now.Year.ToString()));
            dl.Items.Insert(11, new ListItem("November-" + DateTime.Now.Year, "11-" + DateTime.Now.Year.ToString()));
            dl.Items.Insert(12, new ListItem("December-" + DateTime.Now.Year, "12-" + DateTime.Now.Year.ToString()));
            
        }

        public static void loadDates(DropDownList dl)
        {
            dl.Items.Insert(0,new ListItem("Date", "0"));

            for (byte b = 1; b <= 31; b++)
            {
                dl.Items.Insert(b, new ListItem((b.ToString().Length == 1) ? "0" + b.ToString() : b.ToString(), (b.ToString().Length == 1) ? "0" + b.ToString() : b.ToString()));

            }
        }

        public static bool InsertSudentInDailyAttendanceRecordTable_ForCertainDate(DataTable dt,string Shift,string Batch,string group,string section,string MonthYear,string Day)
        {
            try
            {
                bool result=false ;
                DataTable dtShiftTime = new DataTable();
                dtShiftTime = CRUD.ReturnTableNull("select StartTime,CloseTime,LateTime from ShiftConfiguration where ConfigId ="+Shift+"");
                string[] ssHMS = dtShiftTime.Rows[0]["StartTime"].ToString().Split(':');
                string[] seHMS = dtShiftTime.Rows[0]["CloseTime"].ToString().Split(':');
                          
                string [] MY=MonthYear.Split('-');
                int DaysInMonth = DateTime.DaysInMonth(int.Parse(MY[1]), int.Parse(MY[0]));
                //DateTime BeginDate = new DateTime(int.Parse(MY[1]), int.Parse(MY[0]), 1);
                //DateTime EndDate = new DateTime(int.Parse(MY[1]), int.Parse(MY[0]), DaysInMonth);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string [] DMY = dt.Rows[i]["AdmissionDate"].ToString().Split('-');
                    string day = (MonthYear == DMY[1] + "-" + DMY[2]) ? DMY[0] : "01";

                    DateTime BeginDate = new DateTime(int.Parse(MY[1]), int.Parse(MY[0]), int.Parse(day));
                    DateTime EndDate = new DateTime(int.Parse(MY[1]), int.Parse(MY[0]), DaysInMonth);

                    while (BeginDate <= EndDate)
                    {
                        string[] Dates = BeginDate.ToString().Split('/');

                        string dateFormat = "dd-MM-yyyy HH:mm:ss";
                        string Days = (Dates[1].Trim().Length == 1) ? "0" + Dates[1] : Dates[1];
                        string Months = (Dates[0].Trim().Length == 1) ? "0" + Dates[0] : Dates[0];

                        string datTime = Days + "-" + Months + "-" + Dates[2].Substring(0, 4) + " " + DateTime.Now.ToString("hh:mm:ss");
                        DateTime AttDateTime = DateTime.ParseExact(datTime, dateFormat, CultureInfo.InvariantCulture);


                        result = CRUD.ExecuteQuery("insert into DailyAttendanceRecord (StudentId,RollNo,BatchId,ShiftId,ClsSecId,ClsGrpId,AttDate,AttStatus,AttManual,InHur,InMin,InSec,OutHur,OutMin,OutSec,DailyStartTimeALT_CloseTime) " +
                             " values (" + dt.Rows[i]["StudentId"].ToString() + "," + dt.Rows[i]["RollNo"].ToString() + "," + Batch + "," + Shift + "," + section + "," + group + ",'" + AttDateTime + "',' ',' ', " +
                             "" + ssHMS[0] + "," + ssHMS[1] + "," + ssHMS[2].Substring(0, 2) + "," + seHMS[0] + "," + seHMS[1] + "," + seHMS[2].Substring(0, 2) + ",'" + ssHMS[0] + ":" + ssHMS[1] + ":" + ssHMS[2].Substring(0, 2) + ":" + seHMS[0] + ":" + seHMS[1] + ":" + seHMS[2].Substring(0, 2) + ":" + dtShiftTime.Rows[0]["LateTime"].ToString() + "')");
                        BeginDate = BeginDate.AddDays(1);
                    }
                }
                return result;
                
            }
            catch { return false; }
        }

        public bool Insert()
        {
            sql = string.Format("INSERT INTO [dbo].[AttendanceSheetInfo] " +
                "([ASName],[Batch],[Class],[Section],[Month],[Year]) VALUES " +
                "( '" + _Entities.ASName + "', '" + _Entities.Batch + "'," +
                "'" + _Entities.Class + "','" + _Entities.Section + "'," +
                "'" + _Entities.Month + "','" + _Entities.Year + "')");
            return result = CRUD.ExecuteQuery(sql);
        }
        public bool Update()
        {
            sql = string.Format("UPDATE [dbo].[AttendanceSheetInfo] SET " +
                "[ASName] = '" + _Entities.ASName + "'," +
                "[Batch] = '" + _Entities.Batch + "'," +
                "[Class] = '" + _Entities.Class + "'," +
                "[Section] = '" + _Entities.Section + "'," +
                "[Month] = '" + _Entities.Month + "'," +
                "[Year] = '" + _Entities.Year + "' " +
                "WHERE [Id] = '" + _Entities.sheetInfoID + "'");
            return result = CRUD.ExecuteQuery(sql);
        }
        public List<SheetInfoEntities> GetEntitiesData()
        {
            List<SheetInfoEntities> ListEntities = new List<SheetInfoEntities>();
            sql = string.Format("SELECT a.[Id],a.[ASName],a.[Batch],a.[Class],a.[Section],a.[Month],a.[Year] " +
                                "FROM [dbo].[AttendanceSheetInfo] a JOIN [dbo].[Classes] c ON " +
                                "(a.[Class] = c.[ClassName]) ORDER BY c.[ClassOrder] ASC");
            DataTable dt = new DataTable();

            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows
                                    select new SheetInfoEntities
                                    {
                                        sheetInfoID = int.Parse(row["Id"].ToString()),
                                        ASName = row["ASName"].ToString(),
                                        Batch = row["Batch"].ToString(),
                                        Class = row["Class"].ToString(),
                                        Section = row["Section"].ToString(),
                                        Month = row["Month"].ToString(),
                                        Year = int.Parse(row["Year"].ToString())
                                    }).ToList();
                    return ListEntities;
                }

            }
            return ListEntities = null;
        }
        public List<SheetInfoEntities> GetEntitiesData(string month, int year)
        {
            List<SheetInfoEntities> ListEntities = new List<SheetInfoEntities>();            
            sql = string.Format("SELECT a.[Id],a.[ASName],a.[Batch],a.[Class],a.[Section],a.[Month],a.[Year] " +
                                "FROM [dbo].[AttendanceSheetInfo] a JOIN [dbo].[Classes] c ON " +
                                "(a.[Class] = c.[ClassName])  WHERE a.[Month] = '" + month + "' " +
                                "AND a.[Year] = '" + year + "' ORDER BY c.[ClassOrder] ASC");
            DataTable dt = new DataTable();

            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows
                                    select new SheetInfoEntities
                                    {
                                        sheetInfoID = int.Parse(row["Id"].ToString()),
                                        ASName = row["ASName"].ToString(),
                                        Batch = row["Batch"].ToString(),
                                        Class = row["Class"].ToString(),
                                        Section = row["Section"].ToString(),
                                        Month = row["Month"].ToString(),
                                        Year = int.Parse(row["Year"].ToString())
                                    }).ToList();
                    return ListEntities;
                }

            }
            return ListEntities = null;
        }
        public List<SheetInfoEntities> GetEntitiesData(string batch, string month, int year)
        {
            List<SheetInfoEntities> ListEntities = new List<SheetInfoEntities>();
            sql = string.Format("SELECT a.[Id],a.[ASName],a.[Batch],a.[Class],a.[Section],a.[Month],a.[Year] " +
                                "FROM [dbo].[AttendanceSheetInfo] a JOIN [dbo].[Classes] c ON " +
                                "(a.[Class] = c.[ClassName])  WHERE a.[Month] = '" + month + "' " +
                                "AND a.[Year] = '" + year + "' AND a.[Batch] = '" + batch + "' " +
                                "ORDER BY c.[ClassOrder] ASC");
            DataTable dt = new DataTable();

            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows
                                    select new SheetInfoEntities
                                    {
                                        sheetInfoID = int.Parse(row["Id"].ToString()),
                                        ASName = row["ASName"].ToString(),
                                        Batch = row["Batch"].ToString(),
                                        Class = row["Class"].ToString(),
                                        Section = row["Section"].ToString(),
                                        Month = row["Month"].ToString(),
                                        Year = int.Parse(row["Year"].ToString())
                                    }).ToList();
                    return ListEntities;
                }

            }
            return ListEntities = null;
        }

       
        public static DataTable  HasAttendanceMonthDateInDATable(string Shift,string Batch,string group,string section,string MonthYear,string Day)
        {
            try
            {
                string[] MY = MonthYear.Split('-');
                int DaysInMonth = DateTime.DaysInMonth(int.Parse(MY[1]), int.Parse(MY[0]));
                DataTable dt; dt = new DataTable();

                dt = CRUD.ReturnTableNull("select Distinct StudentId,RollNo,FullName,Format(AdmissionDate,'dd-MM-yyyy') as AdmissionDate from v_CurrentStudentInfo_ForAttendance where ConfigId=" + Shift + " AND BatchId=" + Batch + " AND ClsGrpID=" + group + "  AND ClsSecID=" + section + " AND " +
                    " AdmissionDate<='" +MY[1]+"-"+MY[0]+"-" + DaysInMonth + "'  AND IsActive='true' AND StudentId not in " +
                    " (select distinct StudentId from DailyAttendanceRecord where FORMAT(AttDate,'MM-yyyy')='" + MonthYear + "' AND StudentId is not null ) ");


                // dt = CRUD.ReturnTableNull("select AttDate from DailyAttendanceRecord where ShiftId=" + Shift + " AND BatchId=" + Batch + " AND ClsGrpId=" + group + " AND ClsSecId=" + section + " AND  Format(AttDate,'MM-yyyy')= '" + MonthYear + "'");
                 return dt;
   
            }
            catch { return null; }
        }

        public static DataTable  CheckAnyStudentIsExists(string Shift, string Batch, string group, string section, string MonthYear)
        {
            try
            {
                string[] MY = MonthYear.Split('-');
                int DaysInMonth = DateTime.DaysInMonth(int.Parse(MY[1]), int.Parse(MY[0]));

                DataTable dt; dt = new DataTable();
                dt = CRUD.ReturnTableNull("select StudentId,RollNo,FullName from v_CurrentStudentInfo_ForAttendance where ConfigId=" + Shift + " AND BatchId=" + Batch + " AND ClsGrpID=" + group + "  AND ClsSecID=" + section + " AND IsActive='true' AND AdmissionDate<='" + MY[1] + "-" + MY[0] + "-" + DaysInMonth + "' ");
                if (dt.Rows.Count > 0) return dt;
                return null;
            }
            catch { return null; }
        }


        public static DataTable loadAttendanceSheetByMonthYear(string months_year, string BatchId, string ShiftId, string ClsSecId, string ClsGrpId)
        {
            try
            {

                SqlCommand cmd = new SqlCommand("sp_ForManuallyAttendanceSheet", DbConnection.Connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Month_year", months_year);
                cmd.Parameters.AddWithValue("@BatchId",BatchId);
                cmd.Parameters.AddWithValue("@ShiftId", ShiftId);
                cmd.Parameters.AddWithValue("@ClsSecId", ClsSecId);
                cmd.Parameters.AddWithValue("@ClsGrpId", ClsGrpId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0) return dt;
                return null;
                
            }
            catch { return null; }

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
