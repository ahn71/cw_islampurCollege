using DS.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace DS.BLL.Attendace
{
    public class EmpSheetInfoEntry
    {
        DataTable dt;

        public static bool InsertEmployeeInDailyAttendanceRecordTable_ForCertainDate(DataTable dt, string MonthYear, string Shift)
        {
            try
            {
                bool result = false;
                DataTable dtShiftTime = new DataTable();
                dtShiftTime = CRUD.ReturnTableNull("select StartTime,CloseTime,LateTime from ShiftConfiguration where ConfigId =" + Shift + "");
                string[] ssHMS = dtShiftTime.Rows[0]["StartTime"].ToString().Split(':');
                string[] seHMS = dtShiftTime.Rows[0]["CloseTime"].ToString().Split(':');

                string[] MY = MonthYear.Split('-');
                int DaysInMonth = DateTime.DaysInMonth(int.Parse(MY[1]), int.Parse(MY[0]));




                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string [] DMY=dt.Rows[i]["EJoiningDate"].ToString().Split('-');
                    string Day = (MonthYear == DMY[1] + "-" + DMY[2]) ? DMY[0] : "01";
                    
                    DateTime BeginDate = new DateTime(int.Parse(MY[1]), int.Parse(MY[0]), int.Parse(Day));

                    DateTime EndDate = new DateTime(int.Parse(MY[1]), int.Parse(MY[0]), DaysInMonth);
                    while (BeginDate <= EndDate)
                    {
                        string[] Dates = BeginDate.ToString().Split('/');

                        string dateFormat = "dd-MM-yyyy HH:mm:ss";
                        string Days = (Dates[1].Trim().Length == 1) ? "0" + Dates[1] : Dates[1];
                        string Months = (Dates[0].Trim().Length == 1) ? "0" + Dates[0] : Dates[0];

                        string datTime = Days + "-" + Months + "-" + Dates[2].Substring(0, 4) + " " + DateTime.Now.ToString("hh:mm:ss");
                        DateTime AttDateTime = DateTime.ParseExact(datTime, dateFormat, CultureInfo.InvariantCulture);

                        result = CRUD.ExecuteQuery("insert into DailyAttendanceRecord (EId,DId,DesId,ShiftId,AttDate,AttStatus,AttManual,InHur,InMin,InSec,OutHur,OutMin,OutSec,DailyStartTimeALT_CloseTime) " +
                             " values (" + dt.Rows[i]["EId"].ToString() + "," + dt.Rows[i]["DId"].ToString() + "," + dt.Rows[i]["DesId"].ToString() + "," + Shift + ",'" + AttDateTime + "','','', " +
                             "" + ssHMS[0] + "," + ssHMS[1] + "," + ssHMS[2].Substring(0, 2) + "," + seHMS[0] + "," + seHMS[1] + "," + seHMS[2].Substring(0, 2) + ",'" + ssHMS[0] + ":" + ssHMS[1] + ":" + ssHMS[2].Substring(0, 2) + ":" + seHMS[0] + ":" + seHMS[1] + ":" + seHMS[2].Substring(0, 2) + ":" + dtShiftTime.Rows[0]["LateTime"].ToString() + "')");

                        BeginDate = BeginDate.AddDays(1);
                    }
                }
                return result;

            }
            catch { return false; }
        }

        public static DataTable HasAttendanceMonthDateInDATable(string ShiftId,string DepartmentIdList,string DesgIdList,string MonthYear)
        {
            try
            {
                string[] MY = MonthYear.Split('-');
                int DaysInMonth = DateTime.DaysInMonth(int.Parse(MY[1]), int.Parse(MY[0]));
                DataTable dt; dt = new DataTable();


                dt = CRUD.ReturnTableNull("select EId,Format(EJoiningDate,'dd-MM-yyyy') as EJoiningDate,DId,DesId from EmployeeInfo where IsActive='True'AND EJoiningDate<='"+MY[1]+"-"+MY[0]+"-"+DaysInMonth+"' AND IsActive='true' AND DId in ("+DepartmentIdList+") AND DesId in ("+DesgIdList+") AND ShiftId="+ShiftId+" AND  "+
                    " EId not in (select distinct EId from DailyAttendanceRecord where FORMAT(AttDate,'MM-yyyy')='" + MonthYear + "' AND EId Is not null ) ");
                
                return dt;
            }
            catch { return null; }
        }

        public static DataTable getEmpAttendanceSheet(string DepartmentId,string MonthYear)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ForStaff_ManuallyAttendanceSheet", DbConnection.Connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Month_year", MonthYear);
                cmd.Parameters.AddWithValue("@DId", DepartmentId);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0) return dt; else return null;

            }
            catch { return null; }
        }

        public static DataTable HasAnyStaffRecord(string DepartmentId, string MonthYear, string DIdList,string DesIdList,string ShiftId)
        {
            try
            {
                string[] MY = MonthYear.Split('-');
                int DaysInMonth = DateTime.DaysInMonth(int.Parse(MY[1]), int.Parse(MY[0]));

                DataTable dt = new DataTable();
                dt = CRUD.ReturnTableNull("select EId,Format(EJoiningDate,'dd-MM-yyyy') as EJoiningDate,DId,DesId from EmployeeInfo where IsActive='True'AND EJoiningDate<='" + MY[1] + "-" + MY[0] + "-" + DaysInMonth + "' AND IsActive='true' AND DId in ("+DIdList+") AND DesId in ("+DesIdList+") AND ShiftId="+ShiftId+"");
              
                return dt;
            }
            catch { return null; }
        }

        public static string GetDepartmentIdList(DropDownList ddl)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                for (byte b=0;b<ddl.Items.Count;b++)
                {
                    sb.Append(ddl.Items[b].Value + ",");
                }
                return sb.ToString().Substring(0, sb.ToString().LastIndexOf(',')).ToString();
            }
            catch { return null; }
        }

        public static string GetDesignationIdList(DropDownList ddl)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                for (byte b = 0; b < ddl.Items.Count; b++)
                {
                    sb.Append(ddl.Items[b].Value + ",");
                }
                return sb.ToString().Substring(0, sb.ToString().LastIndexOf(',')).ToString();
            }
            catch { return null; }
        }

        public static DataTable getEmpAttendanceSheet(string ShiftId, string MonthYear, string DptIdList)
        {
            try
            {
                DataTable dt = CRUD.ReturnTableNull(" SELECT  EID,EName,DId,ECardNo,Format(EJoiningDate,'yyyy-MM-dd') as EJoiningDate," +
                    " SUM(CASE DATEPART(day, AttDate) WHEN 1 THEN Code ELSE 0 END) AS [1_Code],SUM(CASE DATEPART(day, AttDate) WHEN 1 THEN InHur ELSE 0 END) AS [1_InH], SUM(CASE DATEPART(day, AttDate) WHEN 1 THEN InMin ELSE 0 END) AS [1_InM],"+
                    " SUM(CASE DATEPART(day, AttDate) WHEN 1 THEN OutHur ELSE 0 END) AS [1_OutH], SUM(CASE DATEPART(day, AttDate) WHEN 1 THEN OutMin ELSE 0 END) AS [1_OutM], "+
                    " SUM(CASE DATEPART(day, AttDate) WHEN 2 THEN Code ELSE 0 END) AS [2_Code],SUM(CASE DATEPART(day, AttDate) WHEN 2 THEN InHur ELSE 0 END) AS [2_InH], SUM(CASE DATEPART(day, AttDate) WHEN 2 THEN InMin ELSE 0 END) AS [2_InM], "+
                    " SUM(CASE DATEPART(day, AttDate) WHEN 2 THEN OutHur ELSE 0 END) AS [2_OutH], SUM(CASE DATEPART(day, AttDate) WHEN 2 THEN OutMin ELSE 0 END) AS [2_OutM], "+
                    " SUM(CASE DATEPART(day, AttDate) WHEN 3 THEN Code ELSE 0 END) AS [3_Code],SUM(CASE DATEPART(day, AttDate) WHEN 3 THEN InHur ELSE 0 END) AS [3_InH], SUM(CASE DATEPART(day, AttDate) WHEN 3 THEN InMin ELSE 0 END) AS [3_InM],"+
                    " SUM(CASE DATEPART(day, AttDate) WHEN 3 THEN OutHur ELSE 0 END) AS [3_OutH], SUM(CASE DATEPART(day, AttDate) WHEN 3 THEN OutMin ELSE 0 END) AS [3_OutM],"+
                    " SUM(CASE DATEPART(day, AttDate) WHEN 4 THEN Code ELSE 0 END) AS [4_Code],SUM(CASE DATEPART(day, AttDate) WHEN 4 THEN InHur ELSE 0 END) AS [4_InH], SUM(CASE DATEPART(day, AttDate) WHEN 4 THEN InMin ELSE 0 END) AS [4_InM],"+
                    " SUM(CASE DATEPART(day, AttDate) WHEN 4 THEN OutHur ELSE 0 END) AS [4_OutH], SUM(CASE DATEPART(day, AttDate) WHEN 4 THEN OutMin ELSE 0 END) AS [4_OutM], "+
                    " SUM(CASE DATEPART(day, AttDate) WHEN 5 THEN Code ELSE 0 END) AS [5_Code],SUM(CASE DATEPART(day, AttDate) WHEN 5 THEN InHur ELSE 0 END) AS [5_InH], SUM(CASE DATEPART(day, AttDate) WHEN 5 THEN InMin ELSE 0 END) AS [5_InM], "+
                    " SUM(CASE DATEPART(day, AttDate) WHEN 5 THEN OutHur ELSE 0 END) AS [5_OutH],SUM(CASE DATEPART(day, AttDate) WHEN 5 THEN OutMin ELSE 0 END) AS [5_OutM],"+
                    " SUM(CASE DATEPART(day, AttDate) WHEN 6 THEN Code ELSE 0 END) AS [6_Code],SUM(CASE DATEPART(day, AttDate) WHEN 6 THEN InHur ELSE 0 END) AS [6_InH],SUM(CASE DATEPART(day, AttDate) WHEN 6 THEN InMin ELSE 0 END) AS [6_InM],"+
                    " SUM(CASE DATEPART(day, AttDate) WHEN 6 THEN OutHur ELSE 0 END) AS [6_OutH],SUM(CASE DATEPART(day,AttDate)  WHEN 6 THEN OutMin ELSE 0 END) AS [6_OutM],"+
                    " SUM(CASE DATEPART(day, AttDate) WHEN 7 THEN Code ELSE 0 END) AS  [7_Code], SUM(CASE DATEPART(day, AttDate) WHEN 7 THEN InHur ELSE 0 END) AS [7_InH], SUM(CASE DATEPART(day, AttDate) WHEN 7 THEN InMin ELSE 0 END) AS [7_InM],"+
                    " SUM(CASE DATEPART(day, AttDate) WHEN 7 THEN OutHur ELSE 0 END) AS [7_OutH], SUM(CASE DATEPART(day, AttDate) WHEN 7 THEN OutMin ELSE 0 END)  AS [7_OutM],"+
                    " SUM(CASE DATEPART(day, AttDate) WHEN 8 THEN Code ELSE 0 END) AS [8_Code],SUM(CASE DATEPART(day, AttDate) WHEN 8 THEN InHur ELSE 0 END) AS [8_InH], SUM(CASE DATEPART(day, AttDate) WHEN 8 THEN InMin ELSE 0 END) AS [8_InM],"+
                    " SUM(CASE DATEPART(day,AttDate) WHEN 8 THEN OutHur ELSE 0 END) AS [8_OutH], SUM(CASE DATEPART(day, AttDate) WHEN 8 THEN OutMin ELSE 0 END) AS [8_OutM],"+
                    " SUM(CASE DATEPART(day, AttDate) WHEN 9 THEN Code ELSE 0 END) AS [9_Code],SUM(CASE DATEPART(day, AttDate) WHEN 9 THEN InHur ELSE 0 END) AS [9_InH], SUM(CASE DATEPART(day, AttDate) WHEN 9 THEN InMin ELSE 0 END) AS [9_InM],"+
                    " SUM(CASE DATEPART(day, AttDate) WHEN 9 THEN OutHur ELSE 0 END) AS [9_OutH], SUM(CASE DATEPART(day, AttDate) WHEN 9 THEN OutMin ELSE 0 END) AS [9_OutM],"+
                    " SUM(CASE DATEPART(day, AttDate) WHEN 10 THEN Code ELSE 0 END) AS [10_Code],SUM(CASE DATEPART(day, AttDate) WHEN 10 THEN InHur ELSE 0 END) AS [10_InH],SUM(CASE DATEPART(day, AttDate) WHEN 10 THEN InMin ELSE 0 END) AS [10_InM],"+
                    " SUM(CASE DATEPART(day, AttDate) WHEN 10 THEN OutHur ELSE 0 END) AS [10_OutH], SUM(CASE DATEPART(day, AttDate)WHEN 10 THEN OutMin ELSE 0 END) AS [10_OutM],"+
                    " SUM(CASE DATEPART(day, AttDate) WHEN 11 THEN Code ELSE 0 END) AS [11_Code],SUM(CASE DATEPART(day, AttDate) WHEN 11 THEN InHur ELSE 0 END) AS [11_InH], SUM(CASE DATEPART(day, AttDate) WHEN 11 THEN InMin ELSE 0 END) AS [11_InM], "+
                    " SUM(CASE DATEPART(day, AttDate) WHEN 11 THEN OutHur ELSE 0 END) AS [11_OutH], SUM(CASE DATEPART(day, AttDate) WHEN 11 THEN OutMin ELSE 0 END) AS [11_OutM], "+
                    " SUM(CASE DATEPART(day, AttDate) WHEN 12 THEN Code ELSE 0 END) AS [12_Code],SUM(CASE DATEPART(day, AttDate) WHEN 12 THEN InHur ELSE 0 END) AS [12_InH], SUM(CASE DATEPART(day, AttDate) WHEN 12 THEN InMin ELSE 0 END) AS [12_InM], "+
                    " SUM(CASE DATEPART(day, AttDate) WHEN 12 THEN OutHur ELSE 0 END) AS [12_OutH], SUM(CASE DATEPART(day, AttDate) WHEN 12 THEN OutMin ELSE 0 END) AS [12_OutM],"+
                    " SUM(CASE DATEPART(day, AttDate) WHEN 13 THEN Code ELSE 0 END) AS [13_Code],SUM(CASE DATEPART(day, AttDate) WHEN 13 THEN InHur ELSE 0 END) AS [13_InH], SUM(CASE DATEPART(day, AttDate) WHEN 13 THEN InMin ELSE 0 END) AS [13_InM], "+
                    " SUM(CASE DATEPART(day, AttDate) WHEN 13 THEN OutHur ELSE 0 END) AS [13_OutH], SUM(CASE DATEPART(day, AttDate) WHEN 13 THEN OutMin ELSE 0 END) AS [13_OutM],"+
                    " SUM(CASE DATEPART(day, AttDate) WHEN 14 THEN Code ELSE 0 END) AS [14_Code],SUM(CASE DATEPART(day, AttDate) WHEN 14 THEN InHur ELSE 0 END) AS [14_InH],SUM(CASE DATEPART(day, AttDate) WHEN 14 THEN InMin ELSE 0 END) AS [14_InM],"+
                    " SUM(CASE DATEPART(day, AttDate) WHEN 14 THEN OutHur ELSE 0 END) AS [14_OutH], SUM(CASE DATEPART(day, AttDate) WHEN 14 THEN OutMin ELSE 0 END) AS [14_OutM], "+
                    " SUM(CASE DATEPART(day, AttDate) WHEN 15 THEN Code ELSE 0 END) AS [15_Code],SUM(CASE DATEPART(day, AttDate) WHEN 15 THEN InHur ELSE 0 END) AS [15_InH], SUM(CASE DATEPART(day, AttDate) WHEN 15 THEN InMin ELSE 0 END) AS [15_InM],"+
                    " SUM(CASE DATEPART(day, AttDate) WHEN 15 THEN OutHur ELSE 0 END) AS [15_OutH], SUM(CASE DATEPART(day, AttDate) WHEN 15 THEN OutMin ELSE 0 END) AS [15_OutM],"+
                    " SUM(CASE DATEPART(day, AttDate) WHEN 16 THEN Code ELSE 0 END) AS [16_Code],SUM(CASE DATEPART(day, AttDate) WHEN 16 THEN InHur ELSE 0 END) AS [16_InH], SUM(CASE DATEPART(day, AttDate) WHEN 16 THEN InMin ELSE 0 END) AS [16_InM], "+
                    " SUM(CASE DATEPART(day, AttDate) WHEN 16 THEN OutHur ELSE 0 END) AS [16_OutH], SUM(CASE DATEPART(day, AttDate) WHEN 16 THEN OutMin ELSE 0 END) AS [16_OutM],"+
                    " SUM(CASE DATEPART(day, AttDate) WHEN 17 THEN Code ELSE 0 END) AS [17_Code],SUM(CASE DATEPART(day, AttDate) WHEN 17 THEN InHur ELSE 0 END) AS [17_InH],SUM(CASE DATEPART(day, AttDate) WHEN 17 THEN InMin ELSE 0 END) AS [17_InM], "+
                    " SUM(CASE DATEPART(day, AttDate) WHEN 17 THEN OutHur ELSE 0 END) AS [17_OutH],SUM(CASE DATEPART(day, AttDate) WHEN 17 THEN OutMin ELSE 0 END) AS [17_OutM],"+
                    " SUM(CASE DATEPART(day, AttDate) WHEN 18 THEN Code ELSE 0 END) AS [18_Code],SUM(CASE DATEPART(day, AttDate) WHEN 18 THEN InHur ELSE 0 END) AS [18_InH],SUM(CASE DATEPART(day, AttDate) WHEN 18 THEN InMin ELSE 0 END) AS [18_InM], "+
                    " SUM(CASE DATEPART(day, AttDate) WHEN 18 THEN OutHur ELSE 0 END) AS [18_OutH], SUM(CASE DATEPART(day, AttDate)WHEN 18 THEN OutMin ELSE 0 END) AS [18_OutM],"+
                    " SUM(CASE DATEPART(day, AttDate) WHEN 19 THEN Code ELSE 0 END) AS [19_Code], SUM(CASE DATEPART(day, AttDate) WHEN 19 THEN InHur ELSE 0 END) AS [19_InH], SUM(CASE DATEPART(day, AttDate) WHEN 19 THEN InMin ELSE 0 END) AS [19_InM], "+
                    " SUM(CASE DATEPART(day, AttDate) WHEN 19 THEN OutHur ELSE 0 END) AS [19_OutH], SUM(CASE DATEPART(day, AttDate) WHEN 19 THEN OutMin ELSE 0 END) AS [19_OutM],"+
                    " SUM(CASE DATEPART(day, AttDate) WHEN 20 THEN Code ELSE 0 END) AS [20_Code],SUM(CASE DATEPART(day, AttDate) WHEN 20 THEN InHur ELSE 0 END) AS [20_InH], SUM(CASE DATEPART(day, AttDate) WHEN 20 THEN InMin ELSE 0 END) AS [20_InM], "+
                    " SUM(CASE DATEPART(day, AttDate) WHEN 20 THEN OutHur ELSE 0 END) AS [20_OutH], SUM(CASE DATEPART(day, AttDate) WHEN 20 THEN OutMin ELSE 0 END) AS [20_OutM],"+
                    " SUM(CASE DATEPART(day, AttDate) WHEN 21 THEN Code ELSE 0 END) AS [21_Code],SUM(CASE DATEPART(day, AttDate) WHEN 21 THEN InHur ELSE 0 END) AS [21_InH], SUM(CASE DATEPART(day, AttDate) WHEN 21 THEN InMin ELSE 0 END) AS [21_InM],"+
                    " SUM(CASE DATEPART(day, AttDate) WHEN 21 THEN OutHur ELSE 0 END) AS [21_OutH], SUM(CASE DATEPART(day, AttDate) WHEN 21 THEN OutMin ELSE 0 END) AS [21_OutM],"+
                    " SUM(CASE DATEPART(day, AttDate) WHEN 22 THEN Code ELSE 0 END) AS [22_Code],SUM(CASE DATEPART(day, AttDate) WHEN 22 THEN InHur ELSE 0 END) AS [22_InH],SUM(CASE DATEPART(day, AttDate) WHEN 22 THEN InMin ELSE 0 END) AS [22_InM],"+
                    " SUM(CASE DATEPART(day, AttDate) WHEN 22 THEN OutHur ELSE 0 END) AS [22_OutH], SUM(CASE DATEPART(day, AttDate) WHEN 22 THEN OutMin ELSE 0 END) AS [22_OutM],"+
                    " SUM(CASE DATEPART(day, AttDate) WHEN 23 THEN Code ELSE 0 END) AS [23_Code], SUM(CASE DATEPART(day, AttDate) WHEN 23 THEN InHur ELSE 0 END) AS [23_InH], SUM(CASE DATEPART(day, AttDate) WHEN 23 THEN InMin ELSE 0 END)  AS [23_InM],"+
                    " SUM(CASE DATEPART(day, AttDate) WHEN 23 THEN OutHur ELSE 0 END) AS [23_OutH], SUM(CASE DATEPART(day, AttDate) WHEN 23 THEN OutMin ELSE 0 END) AS [23_OutM],"+
                    " SUM(CASE DATEPART(day, AttDate) WHEN 24 THEN Code ELSE 0 END) AS [24_Code],SUM(CASE DATEPART(day, AttDate) WHEN 24 THEN InHur ELSE 0 END) AS [24_InH], SUM(CASE DATEPART(day, AttDate) WHEN 24 THEN InMin ELSE 0 END) AS [24_InM], "+
                    " SUM(CASE DATEPART(day, AttDate) WHEN 24 THEN OutHur ELSE 0 END) AS [24_OutH], SUM(CASE DATEPART(day, AttDate) WHEN 24 THEN OutMin ELSE 0 END) AS [24_OutM],"+
                    " SUM(CASE DATEPART(day, AttDate) WHEN 25 THEN Code ELSE 0 END) AS [25_Code],SUM(CASE DATEPART(day, AttDate) WHEN 25 THEN InHur ELSE 0 END) AS [25_InH], SUM(CASE DATEPART(day, AttDate) WHEN 25 THEN InMin ELSE 0 END) AS [25_InM],"+
                    " SUM(CASE DATEPART(day, AttDate) WHEN 25 THEN OutHur ELSE 0 END) AS [25_OutH], SUM(CASE DATEPART(day, AttDate) WHEN 25 THEN OutMin ELSE 0 END) AS [25_OutM],"+
                    " SUM(CASE DATEPART(day, AttDate) WHEN 26 THEN Code ELSE 0 END) AS [26_Code],SUM(CASE DATEPART(day, AttDate) WHEN 26 THEN InHur ELSE 0 END) AS [26_InH],SUM(CASE DATEPART(day, AttDate) WHEN 26 THEN InMin ELSE 0 END) AS [26_InM],"+
                    " SUM(CASE DATEPART(day, AttDate) WHEN 26 THEN OutHur ELSE 0 END) AS [26_OutH], SUM(CASE DATEPART(day, AttDate) WHEN 26 THEN OutMin ELSE 0 END) AS [26_OutM],"+
                    " SUM(CASE DATEPART(day, AttDate) WHEN 27 THEN Code ELSE 0 END) AS [27_Code], SUM(CASE DATEPART(day, AttDate) WHEN 27 THEN InHur ELSE 0 END) AS [27_InH], SUM(CASE DATEPART(day, AttDate) WHEN 27 THEN InMin ELSE 0 END) AS [27_InM], "+
                    " SUM(CASE DATEPART(day, AttDate) WHEN 27 THEN OutHur ELSE 0 END) AS [27_OutH], SUM(CASE DATEPART(day, AttDate) WHEN 27 THEN OutMin ELSE 0 END) AS [27_OutM],"+
                    " SUM(CASE DATEPART(day, AttDate) WHEN 28 THEN Code ELSE 0 END) AS [28_Code],SUM(CASE DATEPART(day, AttDate) WHEN 28 THEN InHur ELSE 0 END) AS [28_InH], SUM(CASE DATEPART(day, AttDate) WHEN 28 THEN InMin ELSE 0 END) AS [28_InM],"+
                    " SUM(CASE DATEPART(day, AttDate) WHEN 28 THEN OutHur ELSE 0 END) AS [28_OutH], SUM(CASE DATEPART(day, AttDate) WHEN 28 THEN OutMin ELSE 0 END) AS [28_OutM],"+
                    " SUM(CASE DATEPART(day, AttDate) WHEN 29 THEN Code ELSE 0 END) AS [29_Code],SUM(CASE DATEPART(day, AttDate) WHEN 29 THEN InHur ELSE 0 END) AS [29_InH], SUM(CASE DATEPART(day, AttDate) WHEN 29 THEN InMin ELSE 0 END) AS [29_InM],"+
                    " SUM(CASE DATEPART(day, AttDate) WHEN 29 THEN OutHur ELSE 0 END) AS [29_OutH], SUM(CASE DATEPART(day, AttDate) WHEN 29 THEN OutMin ELSE 0 END) AS [29_OutM],"+
                    " SUM(CASE DATEPART(day, AttDate) WHEN 30 THEN Code ELSE 0 END) AS [30_Code],SUM(CASE DATEPART(day, AttDate) WHEN 30 THEN InHur ELSE 0 END) AS [30_InH], SUM(CASE DATEPART(day, AttDate) WHEN 30 THEN InMin ELSE 0 END) AS [30_InM], "+
                    " SUM(CASE DATEPART(day, AttDate) WHEN 30 THEN OutHur ELSE 0 END) AS [30_OutH], SUM(CASE DATEPART(day, AttDate)  WHEN 30 THEN OutMin ELSE 0 END) AS [30_OutM],"+
                    " SUM(CASE DATEPART(day, AttDate) WHEN 31 THEN Code ELSE 0 END) AS [31_Code],SUM(CASE DATEPART(day, AttDate) WHEN 31 THEN InHur ELSE 0 END) AS [31_InH], SUM(CASE DATEPART(day, AttDate) WHEN 31 THEN InMin ELSE 0 END)  AS [31_InM],"+
                    " SUM(CASE DATEPART(day, AttDate) WHEN 31 THEN OutHur ELSE 0 END) AS [31_OutH], SUM(CASE DATEPART(day, AttDate) WHEN 31 THEN OutMin ELSE 0 END) AS [31_OutM]"+
                    " FROM dbo.v_DailyEmployeeAttendanceRecord where FORMAT(AttDates,'MM-yyyy')='"+MonthYear+"' AND DId in ("+DptIdList+")"+
                    " GROUP BY  EID,EName,DId,ECardNo,EJoiningDate");

                return dt;

            }
            catch { return null; }
        }
    }
}
