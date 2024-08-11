using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DS.DAL;
using System.Web;
using System.Web.UI.WebControls;


namespace DS.BLL.Attendace
{
    
   public static  class ForLeaveReport
    {
       static string SqlCmd = "";
       static DataTable dt;
       public static void generateLeaveApplicationReport( string LACode) 
       {
           SqlCmd = " SELECT LACode,Remarks,ShortName,EID,EName,format(EntryDate,'dd-MM-yyyy') as EntryDate,format(FromDate,'dd-MM-yyyy') as FromDate,format(ToDate,'dd-MM-yyyy') as ToDate,TotalDays,ShiftName,DesName, DName" +
           " FROM v_Leave_Application where LACode='" + LACode + "'";
           dt = new DataTable();
           dt = CRUD.ReturnTableNull(SqlCmd);
           HttpContext.Current.Session["__LeaveApplicationReport__"] = dt;
       }
       public static void RejectedLeaveApplicationReport(string LACode)
       {
           SqlCmd = " SELECT LACode,Remarks,ShortName,EID,EName,format(EntryDate,'dd-MM-yyyy') as EntryDate,format(FromDate,'dd-MM-yyyy') as FromDate,format(ToDate,'dd-MM-yyyy') as ToDate,TotalDays,ShiftName,DesName, DName,ApprovedRejected" +
           " FROM v_Leave_Application_Log where LACode='" + LACode + "'";
           dt = new DataTable();
           dt = CRUD.ReturnTableNull(SqlCmd);
           HttpContext.Current.Session["__LeaveApplicationReport__"] = dt;
       }
       public static DataTable GetRequestedDate(string LACode) 
       {
        SqlCmd="select Format(FromDate,'dd-MM-yyyy') as FromDate,Format(ToDate,'dd-MM-yyyy') as ToDate ,TotalDays,ApprovedRejected "
                + " from"
                + " v_Leave_Application_Log"
                + " where LACode='" + LACode + "'";
        dt = new DataTable();
        dt = CRUD.ReturnTableNull(SqlCmd);
        return dt;
       }
       public static DataTable GetLeaveStatus(string EmpId) 
       {
           SqlCmd = " select  vld.ShortName,COUNT(vld.ShortName) as Amount,tbc.LeaveDays,tbc.LeaveDays-COUNT(vld.ShortName) as Remaining "+
               " from v_Leave_LeaveApplicationDetails as vld Inner join leave_configuration as tbc on vld.LeaveId=tbc.LeaveId AND vld.EID='"+EmpId+"'"+
               " AND vld.FromYear='" + System.DateTime.Now.Year.ToString() + "'and vld.IsApproved=1  group by vld.ShortName,tbc.LeaveDays ";
           dt = new DataTable();
           dt = CRUD.ReturnTableNull(SqlCmd);
           return dt;
       }
       public static DataTable LeaveConfig(string shortname) 
       {
           SqlCmd = " select * from leave_configuration where ShortName='" + shortname + "'";
           dt = new DataTable();
           dt = CRUD.ReturnTableNull(SqlCmd);
           return dt;
       }
       public static bool generateLeaveBalanceReport(string ShiftId,string DId,string DesId,string FDate,string TDate,string ReportType,string ECardNo) 
       {
           string[] FD = FDate.Split('-');
           string[] TD = TDate.Split('-');
           FDate = FD[2] + "-" + FD[1] + "-" + FD[0];
           TDate = TD[2] + "-" + TD[1] + "-" + TD[0];
           if(ReportType=="0")
           SqlCmd = " SELECT  EID, EName, ECardNo, SUM(CL) AS CL, SUM(SL) AS SL, SUM(ML) AS ML, SUM(AL) AS AL, SUM(OPL) AS OPL, SUM(OL) AS OL, SUM(CL) + SUM(SL) + SUM(ML) + SUM(AL) + SUM(OPL) + SUM(OL) AS Total," +
                    " LeaveDays, SUM(DISTINCT LeaveDays) - (SUM(CL) + SUM(SL) + SUM(ML) + SUM(AL) + SUM(OPL) + SUM(OL)) AS Remaining, ShiftId, ShiftName, DId, DName, DesId, DesName"+
                    " FROM   dbo.v_v_Leave_LeaveApplicationDetails AS ld " +
                    " Where LeaveDate>='"+FDate+"' and LeaveDate<='"+TDate+"' and ShiftId='" + ShiftId + "' and DId in(" + DId + ") and DesId in(" + DesId + ")" +
                    " GROUP BY EID, EName, ECardNo, ShiftId, ShiftName, LeaveDays, DId, DName, DesId, DesName ";
           else
           SqlCmd = " SELECT  EID, EName, ECardNo, SUM(CL) AS CL, SUM(SL) AS SL, SUM(ML) AS ML, SUM(AL) AS AL, SUM(OPL) AS OPL, SUM(OL) AS OL, SUM(CL) + SUM(SL) + SUM(ML) + SUM(AL) + SUM(OPL) + SUM(OL) AS Total," +
                    " LeaveDays, SUM(DISTINCT LeaveDays) - (SUM(CL) + SUM(SL) + SUM(ML) + SUM(AL) + SUM(OPL) + SUM(OL)) AS Remaining, ShiftId, ShiftName, DId, DName, DesId, DesName" +
                    " FROM   dbo.v_v_Leave_LeaveApplicationDetails AS ld " +
                    " Where LeaveDate>='" + FDate + "' and LeaveDate<='" + TDate + "' and ECardNo="+ECardNo+"" +
                    " GROUP BY EID, EName, ECardNo, ShiftId, ShiftName, LeaveDays, DId, DName, DesId, DesName ";
           dt = new DataTable();
           dt = CRUD.ReturnTableNull(SqlCmd);
           HttpContext.Current.Session["__LeaveBalanceReport__"] = dt;
           if (dt == null || dt.Rows.Count < 1) return false;
           else return true;          
       }
       public static bool generateLeaveBalanceReport(string FDate, string TDate,string EID)
       {
           string[] FD = FDate.Split('-');
           string[] TD = TDate.Split('-');
           FDate = FD[2] + "-" + FD[1] + "-" + FD[0];
           TDate = TD[2] + "-" + TD[1] + "-" + TD[0];          
               SqlCmd = " SELECT  EID, EName, ECardNo, SUM(CL) AS CL, SUM(SL) AS SL, SUM(ML) AS ML, SUM(AL) AS AL, SUM(OPL) AS OPL, SUM(OL) AS OL, SUM(CL) + SUM(SL) + SUM(ML) + SUM(AL) + SUM(OPL) + SUM(OL) AS Total," +
                        " LeaveDays, SUM(DISTINCT LeaveDays) - (SUM(CL) + SUM(SL) + SUM(ML) + SUM(AL) + SUM(OPL) + SUM(OL)) AS Remaining, ShiftId, ShiftName, DId, DName, DesId, DesName" +
                        " FROM   dbo.v_v_Leave_LeaveApplicationDetails AS ld " +
                        " Where LeaveDate>='" + FDate + "' and LeaveDate<='" + TDate + "' and EID=" + EID + "" +
                        " GROUP BY EID, EName, ECardNo, ShiftId, ShiftName, LeaveDays, DId, DName, DesId, DesName ";
           dt = new DataTable();
           dt = CRUD.ReturnTableNull(SqlCmd);
           HttpContext.Current.Session["__LeaveBalanceReport__"] = dt;
           if (dt == null || dt.Rows.Count < 1) return false;
           else return true;
       }

       public static void loadYear(DropDownList dl) //Load Year For Yearly Leave Status Report
       {
           try
           {              

               SqlCmd = " select distinct Year  from v_v_v_Leave_Yearly_Status order by Year desc";
               dt = CRUD.ReturnTableNull(SqlCmd);
               dl.DataTextField = "Year";
               dl.DataValueField = "Year";
               dl.DataSource = dt;
               dl.DataBind();
           }
           catch { }
       }
       public static bool generatYearlyLeaveStatus(string Year, string shiftId, string DepartmentList,string DesignationList,string ECardNo,string ReportType) 
       {
           if(ReportType=="0")   //All Yearly Leave Status
           SqlCmd = "SELECT Year,EName,ECardNo,DName,ShiftName,DesName,CL_Spend, CASE  WHEN CL_Remaining IS NULL THEN 0 ELSE CL_Remaining END As CL_Remaining," +
               "CASE  WHEN CL_Total IS NULL THEN 0 ELSE CL_Total END AS CL_Total,SL_Spend, CASE  WHEN SL_Remaining IS NULL THEN 0 ELSE SL_Remaining END As SL_Remaining," +
               "CASE  WHEN v_v_v_Leave_Yearly_Status.SL_Total IS NULL THEN 0 ELSE v_v_v_Leave_Yearly_Status.SL_Total END AS SL_Total,AL_Spend," +
               "CASE  WHEN AL_Remaining IS NULL THEN 0 ELSE AL_Remaining END As AL_Remaining,CASE  WHEN AL_Total IS NULL THEN 0 ELSE AL_Total END AS AL_Total,ML_Spend," +
               "CASE  WHEN ML_Remaining IS NULL THEN 0 ELSE ML_Remaining END As ML_Remaining,CASE  WHEN ML_Total IS NULL THEN 0 ELSE ML_Total END AS ML_Total,OPL_Spend," +
               "CASE  WHEN OPL_Remaining IS NULL THEN 0 ELSE OPL_Remaining END As OPL_Remaining,CASE  WHEN OPL_Total IS NULL THEN 0 ELSE OPL_Total END AS OPL_Total,OL_Spend," +
               "CASE  WHEN OL_Remaining IS NULL THEN 0 ELSE OL_Remaining END As OL_Remaining,CASE  WHEN OL_Total IS NULL THEN 0 ELSE OL_Total END AS OL_Total " +
               "FROM v_v_v_Leave_Yearly_Status " +
               "where Year ='" + Year + "'  AND ShiftId='" + shiftId + "' AND DId in(" + DepartmentList + ") and DesId in(" + DesignationList + ") " +
               "ORDER BY  ShiftName,DName";
           else // Individual Yearly Leave Status
               SqlCmd = "SELECT Year,EName,ECardNo,DName,ShiftName,DesName,CL_Spend, CASE  WHEN CL_Remaining IS NULL THEN 0 ELSE CL_Remaining END As CL_Remaining," +
              "CASE  WHEN CL_Total IS NULL THEN 0 ELSE CL_Total END AS CL_Total,SL_Spend, CASE  WHEN SL_Remaining IS NULL THEN 0 ELSE SL_Remaining END As SL_Remaining," +
              "CASE  WHEN v_v_v_Leave_Yearly_Status.SL_Total IS NULL THEN 0 ELSE v_v_v_Leave_Yearly_Status.SL_Total END AS SL_Total,AL_Spend," +
              "CASE  WHEN AL_Remaining IS NULL THEN 0 ELSE AL_Remaining END As AL_Remaining,CASE  WHEN AL_Total IS NULL THEN 0 ELSE AL_Total END AS AL_Total,ML_Spend," +
              "CASE  WHEN ML_Remaining IS NULL THEN 0 ELSE ML_Remaining END As ML_Remaining,CASE  WHEN ML_Total IS NULL THEN 0 ELSE ML_Total END AS ML_Total,OPL_Spend," +
              "CASE  WHEN OPL_Remaining IS NULL THEN 0 ELSE OPL_Remaining END As OPL_Remaining,CASE  WHEN OPL_Total IS NULL THEN 0 ELSE OPL_Total END AS OPL_Total,OL_Spend," +
              "CASE  WHEN OL_Remaining IS NULL THEN 0 ELSE OL_Remaining END As OL_Remaining,CASE  WHEN OL_Total IS NULL THEN 0 ELSE OL_Total END AS OL_Total " +
              "FROM v_v_v_Leave_Yearly_Status " +
              "where Year ='" + Year + "'  AND ECardNo =" + ECardNo + " " +
              "ORDER BY  ShiftName,DName";
           dt = new DataTable();
           dt = CRUD.ReturnTableNull(SqlCmd);
           HttpContext.Current.Session["__YearlyLeaveStatus__"] = dt;
           if (dt == null || dt.Rows.Count < 1) return false;
           else return true;
       }
       public static bool generatYearlyLeaveStatus(string Year, string EID)
       {          
               SqlCmd = "SELECT Year,EName,ECardNo,DName,ShiftName,DesName,CL_Spend, CASE  WHEN CL_Remaining IS NULL THEN 0 ELSE CL_Remaining END As CL_Remaining," +
              "CASE  WHEN CL_Total IS NULL THEN 0 ELSE CL_Total END AS CL_Total,SL_Spend, CASE  WHEN SL_Remaining IS NULL THEN 0 ELSE SL_Remaining END As SL_Remaining," +
              "CASE  WHEN v_v_v_Leave_Yearly_Status.SL_Total IS NULL THEN 0 ELSE v_v_v_Leave_Yearly_Status.SL_Total END AS SL_Total,AL_Spend," +
              "CASE  WHEN AL_Remaining IS NULL THEN 0 ELSE AL_Remaining END As AL_Remaining,CASE  WHEN AL_Total IS NULL THEN 0 ELSE AL_Total END AS AL_Total,ML_Spend," +
              "CASE  WHEN ML_Remaining IS NULL THEN 0 ELSE ML_Remaining END As ML_Remaining,CASE  WHEN ML_Total IS NULL THEN 0 ELSE ML_Total END AS ML_Total,OPL_Spend," +
              "CASE  WHEN OPL_Remaining IS NULL THEN 0 ELSE OPL_Remaining END As OPL_Remaining,CASE  WHEN OPL_Total IS NULL THEN 0 ELSE OPL_Total END AS OPL_Total,OL_Spend," +
              "CASE  WHEN OL_Remaining IS NULL THEN 0 ELSE OL_Remaining END As OL_Remaining,CASE  WHEN OL_Total IS NULL THEN 0 ELSE OL_Total END AS OL_Total " +
              "FROM v_v_v_Leave_Yearly_Status " +
              "where Year ='" + Year + "'  AND EID =" + EID + " " +
              "ORDER BY  ShiftName,DName";
           dt = new DataTable();
           dt = CRUD.ReturnTableNull(SqlCmd);
           HttpContext.Current.Session["__YearlyLeaveStatus__"] = dt;
           if (dt == null || dt.Rows.Count < 1) return false;
           else return true;
       }
       public static bool generateLeaveListReport(string ShiftId, string DId, string DesId, string FDate, string TDate, string ReportType, string ECardNo) // Leave List Report
       {
           string[] FD = FDate.Split('-');
           string[] TD = TDate.Split('-');
           FDate = FD[2] + "-" + FD[1] + "-" + FD[0];
           TDate = TD[2] + "-" + TD[1] + "-" + TD[0];
           if (ReportType == "0")
               SqlCmd = " select LACode,LeaveName,ECardNo,EName,DName,DesName,ShiftName,Format(FromDate,'dd-MM-yyyy') as FromDate,Format(ToDate,'dd-MM-yyyy') as ToDate from v_Leave_LeaveApplicationDetails" +
                   " where IsApproved=1 and ShiftId='"+ShiftId+"' and DId in("+DId+") and DesId in("+DesId+") and ((FromDate>='"+FDate+"' and FromDate<='"+TDate+"')"+
                   " or(ToDate>='" + FDate + "' and ToDate<='" + TDate + "'))" +
                   " group by LACode,LeaveName,ECardNo,EName,DName,DesName,ShiftName ,FromDate,ToDate  order by FromDate";
           else
               SqlCmd = " select LACode,LeaveName,ECardNo,EName,DName,DesName,ShiftName,Format(FromDate,'dd-MM-yyyy') as FromDate,Format(ToDate,'dd-MM-yyyy') as ToDate from v_Leave_LeaveApplicationDetails" +
                   " where IsApproved=1 and  ECardNo='"+ECardNo+"'and ((FromDate>='" + FDate + "' and FromDate<='" + TDate + "')" +
                   " or(ToDate>='" + FDate + "' and ToDate<='" + TDate + "'))" +
                   " group by LACode,LeaveName,ECardNo,EName,DName,DesName,ShiftName ,FromDate,ToDate  order by FromDate";
           dt = new DataTable();
           dt = CRUD.ReturnTableNull(SqlCmd);
           HttpContext.Current.Session["__LeaveListReport__"] = dt;
           if (dt == null || dt.Rows.Count < 1) return false;
           else return true;

       }
       public static bool generateLeaveListReport(string FDate, string TDate,string EID) // Leave List Report
       {
           string[] FD = FDate.Split('-');
           string[] TD = TDate.Split('-');
           FDate = FD[2] + "-" + FD[1] + "-" + FD[0];
           TDate = TD[2] + "-" + TD[1] + "-" + TD[0];          
               SqlCmd = " select LACode,LeaveName,ECardNo,EName,DName,DesName,ShiftName,Format(FromDate,'dd-MM-yyyy') as FromDate,Format(ToDate,'dd-MM-yyyy') as ToDate from v_Leave_LeaveApplicationDetails" +
                   " where IsApproved=1 and  EID='" + EID + "'and ((FromDate>='" + FDate + "' and FromDate<='" + TDate + "')" +
                   " or(ToDate>='" + FDate + "' and ToDate<='" + TDate + "'))" +
                   " group by LACode,LeaveName,ECardNo,EName,DName,DesName,ShiftName ,FromDate,ToDate  order by FromDate";
           dt = new DataTable();
           dt = CRUD.ReturnTableNull(SqlCmd);
           HttpContext.Current.Session["__LeaveListReport__"] = dt;
           if (dt == null || dt.Rows.Count < 1) return false;
           else return true;

       }
       public static bool generateLeaveApprovedRejectedReport(string ShiftId, string DId, string DesId, string FDate, string TDate, string ReportType, string ECardNo,string ApprovedRejected) // Leave Approved and Rejected List Report
       {
           string[] FD = FDate.Split('-');
           string[] TD = TDate.Split('-');
           FDate = FD[2] + "-" + FD[1] + "-" + FD[0];
           TDate = TD[2] + "-" + TD[1] + "-" + TD[0];
           if (ApprovedRejected == "0" || ApprovedRejected == "1")
           {
               if (ReportType == "0")
                   SqlCmd = "select ShiftName,DName,DesName,ECardNo,EName,Format(FromDate,'dd-MM-yyyy') as FromDate,Format(ToDate,'dd-MM-yyyy') as ToDate,TotalDays,ShortName as LeaveName  from v_Leave_Application_Log where ShiftId='" + ShiftId + "' and DId in(" + DId + ") and DesId in(" + DesId + ") and IsApproved=" + ApprovedRejected + "  and ApprovedDate>='" + FDate + "' and ApprovedDate<='" + TDate + "'";
               else
                   SqlCmd = "select ShiftName,DName,DesName,ECardNo,EName,Format(FromDate,'dd-MM-yyyy') as FromDate,Format(ToDate,'dd-MM-yyyy') as ToDate,TotalDays,ShortName as LeaveName from v_Leave_Application_Log where ECardNo='" + ECardNo + "' and IsApproved=" + ApprovedRejected + " and ApprovedDate>='" + FDate + "' and ApprovedDate<='" + TDate + "'";
           }
           else {
               if (ReportType == "0")
                   SqlCmd = "select ShiftName,DName,DesName,ECardNo,EName,Format(FromDate,'dd-MM-yyyy') as FromDate,Format(ToDate,'dd-MM-yyyy') as ToDate,TotalDays,ShortName as LeaveName,ApprovedRejected as LACode from v_Leave_Application_Log where ShiftId='" + ShiftId + "' and DId in(" + DId + ") and DesId in(" + DesId + ") and ApprovedDate>='" + FDate + "' and ApprovedDate<='" + TDate + "'";
               else
                   SqlCmd = "select ShiftName,DName,DesName,ECardNo,EName,Format(FromDate,'dd-MM-yyyy') as FromDate,Format(ToDate,'dd-MM-yyyy') as ToDate,TotalDays,ShortName as LeaveName,ApprovedRejected as LACode from v_Leave_Application_Log where ECardNo='" + ECardNo + "' and  ApprovedDate>='" + FDate + "' and ApprovedDate<='" + TDate + "'";          
           }
           dt = new DataTable();
           dt = CRUD.ReturnTableNull(SqlCmd);
           HttpContext.Current.Session["__LeaveApprovedRejected__"] = dt;
           if (dt == null || dt.Rows.Count < 1) return false;
           else return true;

       }
       public static bool generateLeaveApprovedRejectedReport(string FDate, string TDate, string EID, string ApprovedRejected) // Leave Approved and Rejected List Report
       {
           string[] FD = FDate.Split('-');
           string[] TD = TDate.Split('-');
           FDate = FD[2] + "-" + FD[1] + "-" + FD[0];
           TDate = TD[2] + "-" + TD[1] + "-" + TD[0];
           if (ApprovedRejected == "0" || ApprovedRejected == "1")
           {             
                   SqlCmd = "select ShiftName,DName,DesName,ECardNo,EName,Format(FromDate,'dd-MM-yyyy') as FromDate,Format(ToDate,'dd-MM-yyyy') as ToDate,TotalDays,ShortName as LeaveName from v_Leave_Application_Log where EID='" + EID + "' and IsApproved=" + ApprovedRejected + " and ApprovedDate>='" + FDate + "' and ApprovedDate<='" + TDate + "'";
           }
           else
           {
               SqlCmd = "select ShiftName,DName,DesName,ECardNo,EName,Format(FromDate,'dd-MM-yyyy') as FromDate,Format(ToDate,'dd-MM-yyyy') as ToDate,TotalDays,ShortName as LeaveName,ApprovedRejected as LACode from v_Leave_Application_Log where EID='" + EID + "' and  ApprovedDate>='" + FDate + "' and ApprovedDate<='" + TDate + "'";
           }
           dt = new DataTable();
           dt = CRUD.ReturnTableNull(SqlCmd);
           HttpContext.Current.Session["__LeaveApprovedRejected__"] = dt;
           if (dt == null || dt.Rows.Count < 1) return false;
           else return true;

       }
    }
}
