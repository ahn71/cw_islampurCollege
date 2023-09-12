using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DS.DAL;
using System.Web.UI.WebControls;

namespace DS.BLL.Timetable
{
 public static class ForClassRoutineReport
    {
        public static DataTable dt;
        static string sqlcmd = "";
     public static DataTable return_dt_for_Days(string Eid,string ShiftId) //For Teacher Class Routine
     {
         try {

             sqlcmd = "select distinct ShortDayName,DayId from v_tbl_Class_Routine where EId='" + Eid + "' and ShiftId='" + ShiftId + "'";
             dt = new DataTable();
             dt = CRUD.ReturnTableNull(sqlcmd);
             return dt;
         }
         catch { return dt = null; }
     }

     public static DataTable return_dt_for_Days(string ShiftId, string BatchId, string ClsGrpId, string ClsSecId) //For  Class Routine
     {
         try
         {

             sqlcmd = "select distinct DayName,DayId from v_tbl_Class_Routine where ShiftId='"+ShiftId+"' and BatchId='"+BatchId+"' and ClsGrpId='"+ClsGrpId+"' and ClsSecId='"+ClsSecId+"' ";
             dt = new DataTable();
             dt = CRUD.ReturnTableNull(sqlcmd);
             return dt;
         }
         catch { return dt = null; }
     }
     public static DataTable return_dt_for_ClassInfo(string Eid, string dayId, string ShiftId) //For Teacher Class Routine
     {
         try
         {

             sqlcmd = "select DayName,SubName,BatchName,CONVERT(varchar(8),CAST(Starttime AS TIME),100) as Starttime,"+
                 "CONVERT(varchar(8),CAST(EndTime AS TIME),100) as EndTime,"+
                 "GroupName,ClsGrpId,SectionName,RoomName,BuildingName from v_Tbl_Class_Routine where EId='" + Eid + "'" +
                 "and DayID='" + dayId + "' and ShiftId='"+ShiftId+"' Order By StartTime ";
             dt = new DataTable();
             dt = CRUD.ReturnTableNull(sqlcmd);
             return dt;
         }
         catch { return dt = null; }
     }

     public static DataTable return_dt_for_TeacherLoadReport(string Eid,string shift) //For Teacher Load Report by Shift
     {
         try
         {
             Eid = (Eid == "00") ? "" : "Eid='"+Eid+"' and";
             sqlcmd = " select Eid,ECardNo,EName,TCodeNo,DesName,DName, count(EId) as TotalClass,ShiftId,ShiftName " +
				 " from v_Tbl_Class_Routine "+
                 " where  "+Eid+" shiftId="+shift+" "+
                 " group by  Eid,ECardNo,EName,TCodeNo,ShiftId,ShiftName,DesName,DName";
             dt = new DataTable();
             dt = CRUD.ReturnTableNull(sqlcmd);
             return dt;
         }
         catch { return dt = null; }
     }
     public static DataTable return_dt_for_TeacherLoadReport(string Eid, string shift,string DId) //For Teacher Load Report by shift and Department
     {
         try
         {
             Eid = (Eid == "00") ? "" : "Eid='" + Eid + "' and";
             shift = (shift == "0") ? "" :" and shiftId = " + shift + "";
             sqlcmd = " select Eid,ECardNo,EName,TCodeNo,DesName,DName, count(EId) as TotalClass,ShiftId,ShiftName " +
                 " from v_Tbl_Class_Routine " +
                 " where  " + Eid + " DId in("+DId+") "+shift+"" +
                 " group by  Eid,ECardNo,EName,TCodeNo,ShiftId,ShiftName,DesName,DName";
             dt = new DataTable();
             dt = CRUD.ReturnTableNull(sqlcmd);
             return dt;
         }
         catch { return dt = null; }
     }
     public static DataTable return_dt_for_ClassInfo(string ShiftId, string BatchId, string ClsGrpId, string ClsSecId,string DayId) //For Teacher Class Routine
     {
         try
         {

             sqlcmd = "Select DayName, SubName, CONVERT(varchar(8),CAST(Starttime AS TIME),100) as Starttime,CONVERT(varchar(8),CAST(EndTime AS TIME),100) as EndTime,"+
                 " TCodeNo,BuildingName,RoomName from v_Tbl_Class_Routine where ShiftId='" + ShiftId + "' and BatchId='" + BatchId + "' " +
                 "and ClsGrpId='"+ClsGrpId+"' and ClsSecId='"+ClsSecId+"' and DayId='"+DayId+"' Order By StartTime ";
             dt = new DataTable();
             dt = CRUD.ReturnTableNull(sqlcmd);
             return dt;
         }
         catch { return dt = null; }
     }
     public static string GetAlllist(DropDownList dl) // For Department And Designation All Id.
     {
         try
         {
             string setPredicate = "";
             for (byte b = 0; b < dl.Items.Count; b++)
             {
                 setPredicate += dl.Items[b].Value.ToString() + ",";
             }

             setPredicate = setPredicate.Remove(setPredicate.LastIndexOf(','));
             return setPredicate;
         }
         catch { return " "; }

     }
     public static void LoadTeacherInfo(DropDownList dl,string DIdList) 
     {
         try
         {
             sqlcmd = "select distinct EId,(EName+' | '+TCodeNo) as Tinfo from v_tbl_Class_Routine  where DId in(" + DIdList + ")";
             dt = new DataTable();
             dt = CRUD.ReturnTableNull(sqlcmd);
             dl.DataValueField = "EId";
             dl.DataTextField = "Tinfo";
             dl.DataSource = dt;
             dl.DataBind();
             dl.Items.Insert(0, new ListItem("Select", "0"));
         }
         catch { }
     }
     public static void LoadTeacherInfoRoutine(DropDownList dl, string SIdList,string Year)
     {
         try
         {
             sqlcmd = "select distinct EId,(EName) as Tinfo from v_Tbl_Class_Routine  where ShiftId='" + SIdList + "' and BatchYear='" + Year + "'";
             dt = new DataTable();
             dt = CRUD.ReturnTableNull(sqlcmd);
             dl.DataValueField = "EId";
             dl.DataTextField = "Tinfo";
             dl.DataSource = dt;
             dl.DataBind();
             dl.Items.Insert(0, new ListItem("Select", "0"));
         }
         catch { }
     }
     public static DataTable dt_For_Shift(string EId)
     {
         try
         {
             sqlcmd = "select Distinct ShiftId,ShiftName from v_Tbl_Class_Routine where EId='"+EId+"'";
             dt = new DataTable();
             dt = CRUD.ReturnTableNull(sqlcmd);
             return dt;
         }
         catch { return dt = null; }
     }
     public static DataTable dt_For_ShiftByDepartment(string DId) // Shiftlist By Department
     {
         try
         {
             sqlcmd = "select Distinct ShiftId,ShiftName from v_Tbl_Class_Routine where DId in("+DId+")";
             dt = new DataTable();
             dt = CRUD.ReturnTableNull(sqlcmd);
             return dt;
         }
         catch { return dt = null; }
     }
     public static DataTable dt_TeacherIfo( string EId) 
     {
         try
         {
             sqlcmd = "select Distinct  ECardNo,DName,DesName from V_EmployeeInfo where EId='" + EId + "'";
             dt = new DataTable();
             dt = CRUD.ReturnTableNull(sqlcmd);
             return dt;
         }
         catch { return dt = null; }
     }
    }
}
