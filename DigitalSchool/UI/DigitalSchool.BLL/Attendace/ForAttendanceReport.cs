using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DS.DAL;

namespace DS.BLL.Attendace
{   
 public static   class ForAttendanceReport
    {
     public static DataTable dt;
     static string sqlcmd="";
     public static DataTable returnDatatableForEmpAttSheet(string Shift,string Month,string DIdList,string DesIdList)  // For Monthly Employees Attendance Sheet
     {
         try
         {
             sqlcmd = "SELECT EID,EName,DId,ECardNo," +
        "SUM(CASE DATEPART(day, AttDate) WHEN 1 THEN Code ELSE 0 END) AS [1_Code],SUM(CASE DATEPART(day, AttDate) WHEN 1 THEN InHur ELSE 0 END) AS [1_InH], SUM(CASE DATEPART(day, AttDate) WHEN 1 THEN InMin ELSE 0 END) AS [1_InM]," +
        "SUM(CASE DATEPART(day, AttDate) WHEN 1 THEN OutHur ELSE 0 END) AS [1_OutH], SUM(CASE DATEPART(day, AttDate) WHEN 1 THEN OutMin ELSE 0 END) AS [1_OutM]," +
        "SUM(CASE DATEPART(day, AttDate) WHEN 2 THEN Code ELSE 0 END) AS [2_Code],SUM(CASE DATEPART(day, AttDate) WHEN 2 THEN InHur ELSE 0 END) AS [2_InH], SUM(CASE DATEPART(day, AttDate) WHEN 2 THEN InMin ELSE 0 END) AS [2_InM], " +
        "SUM(CASE DATEPART(day, AttDate) WHEN 2 THEN OutHur ELSE 0 END) AS [2_OutH], SUM(CASE DATEPART(day, AttDate) WHEN 2 THEN OutMin ELSE 0 END) AS [2_OutM], " +
        "SUM(CASE DATEPART(day, AttDate) WHEN 3 THEN Code ELSE 0 END) AS [3_Code],SUM(CASE DATEPART(day, AttDate) WHEN 3 THEN InHur ELSE 0 END) AS [3_InH], SUM(CASE DATEPART(day, AttDate) WHEN 3 THEN InMin ELSE 0 END) AS [3_InM]," +
        "SUM(CASE DATEPART(day, AttDate) WHEN 3 THEN OutHur ELSE 0 END) AS [3_OutH], SUM(CASE DATEPART(day, AttDate) WHEN 3 THEN OutMin ELSE 0 END) AS [3_OutM], " +
        "SUM(CASE DATEPART(day, AttDate) WHEN 4 THEN Code ELSE 0 END) AS [4_Code],SUM(CASE DATEPART(day, AttDate) WHEN 4 THEN InHur ELSE 0 END) AS [4_InH], SUM(CASE DATEPART(day, AttDate) WHEN 4 THEN InMin ELSE 0 END) AS [4_InM]," +
        "SUM(CASE DATEPART(day, AttDate) WHEN 4 THEN OutHur ELSE 0 END) AS [4_OutH], SUM(CASE DATEPART(day, AttDate) WHEN 4 THEN OutMin ELSE 0 END) AS [4_OutM], " +
        "SUM(CASE DATEPART(day, AttDate) WHEN 5 THEN Code ELSE 0 END) AS [5_Code],SUM(CASE DATEPART(day, AttDate) WHEN 5 THEN InHur ELSE 0 END) AS [5_InH], SUM(CASE DATEPART(day, AttDate) WHEN 5 THEN InMin ELSE 0 END) AS [5_InM], " +
        "SUM(CASE DATEPART(day, AttDate) WHEN 5 THEN OutHur ELSE 0 END) AS [5_OutH],SUM(CASE DATEPART(day, AttDate) WHEN 5 THEN OutMin ELSE 0 END) AS [5_OutM]," +
        "SUM(CASE DATEPART(day, AttDate) WHEN 6 THEN Code ELSE 0 END) AS [6_Code],SUM(CASE DATEPART(day, AttDate) WHEN 6 THEN InHur ELSE 0 END) AS [6_InH],SUM(CASE DATEPART(day, AttDate) WHEN 6 THEN InMin ELSE 0 END) AS [6_InM]," +
        "SUM(CASE DATEPART(day, AttDate) WHEN 6 THEN OutHur ELSE 0 END) AS [6_OutH],SUM(CASE DATEPART(day,AttDate)  WHEN 6 THEN OutMin ELSE 0 END) AS [6_OutM]," +
        "SUM(CASE DATEPART(day, AttDate) WHEN 7 THEN Code ELSE 0 END) AS  [7_Code], SUM(CASE DATEPART(day, AttDate) WHEN 7 THEN InHur ELSE 0 END) AS [7_InH], SUM(CASE DATEPART(day, AttDate) WHEN 7 THEN InMin ELSE 0 END) AS [7_InM]," +
        "SUM(CASE DATEPART(day, AttDate) WHEN 7 THEN OutHur ELSE 0 END) AS [7_OutH], SUM(CASE DATEPART(day, AttDate) WHEN 7 THEN OutMin ELSE 0 END)  AS [7_OutM]," +
        "SUM(CASE DATEPART(day, AttDate) WHEN 8 THEN Code ELSE 0 END) AS [8_Code],SUM(CASE DATEPART(day, AttDate) WHEN 8 THEN InHur ELSE 0 END) AS [8_InH], SUM(CASE DATEPART(day, AttDate) WHEN 8 THEN InMin ELSE 0 END) AS [8_InM]," +
        "SUM(CASE DATEPART(day,AttDate) WHEN 8 THEN OutHur ELSE 0 END) AS [8_OutH], SUM(CASE DATEPART(day, AttDate) WHEN 8 THEN OutMin ELSE 0 END) AS [8_OutM]," +
        "SUM(CASE DATEPART(day, AttDate) WHEN 9 THEN Code ELSE 0 END) AS [9_Code],SUM(CASE DATEPART(day, AttDate) WHEN 9 THEN InHur ELSE 0 END) AS [9_InH], SUM(CASE DATEPART(day, AttDate) WHEN 9 THEN InMin ELSE 0 END) AS [9_InM]," +
        "SUM(CASE DATEPART(day, AttDate) WHEN 9 THEN OutHur ELSE 0 END) AS [9_OutH], SUM(CASE DATEPART(day, AttDate) WHEN 9 THEN OutMin ELSE 0 END) AS [9_OutM]," +
        "SUM(CASE DATEPART(day, AttDate) WHEN 10 THEN Code ELSE 0 END) AS [10_Code],SUM(CASE DATEPART(day, AttDate) WHEN 10 THEN InHur ELSE 0 END) AS [10_InH],SUM(CASE DATEPART(day, AttDate) WHEN 10 THEN InMin ELSE 0 END) AS [10_InM]," +
        "SUM(CASE DATEPART(day, AttDate) WHEN 10 THEN OutHur ELSE 0 END) AS [10_OutH], SUM(CASE DATEPART(day, AttDate)WHEN 10 THEN OutMin ELSE 0 END) AS [10_OutM]," +
        "SUM(CASE DATEPART(day, AttDate) WHEN 11 THEN Code ELSE 0 END) AS [11_Code],SUM(CASE DATEPART(day, AttDate) WHEN 11 THEN InHur ELSE 0 END) AS [11_InH], SUM(CASE DATEPART(day, AttDate) WHEN 11 THEN InMin ELSE 0 END) AS [11_InM]," +
        "SUM(CASE DATEPART(day, AttDate) WHEN 11 THEN OutHur ELSE 0 END) AS [11_OutH], SUM(CASE DATEPART(day, AttDate) WHEN 11 THEN OutMin ELSE 0 END) AS [11_OutM]," +
        "SUM(CASE DATEPART(day, AttDate) WHEN 12 THEN Code ELSE 0 END) AS [12_Code],SUM(CASE DATEPART(day, AttDate) WHEN 12 THEN InHur ELSE 0 END) AS [12_InH], SUM(CASE DATEPART(day, AttDate) WHEN 12 THEN InMin ELSE 0 END) AS [12_InM], " +
        "SUM(CASE DATEPART(day, AttDate) WHEN 12 THEN OutHur ELSE 0 END) AS [12_OutH], SUM(CASE DATEPART(day, AttDate) WHEN 12 THEN OutMin ELSE 0 END) AS [12_OutM]," +
        "SUM(CASE DATEPART(day, AttDate) WHEN 13 THEN Code ELSE 0 END) AS [13_Code],SUM(CASE DATEPART(day, AttDate) WHEN 13 THEN InHur ELSE 0 END) AS [13_InH], SUM(CASE DATEPART(day, AttDate) WHEN 13 THEN InMin ELSE 0 END) AS [13_InM]," +
        "SUM(CASE DATEPART(day, AttDate) WHEN 13 THEN OutHur ELSE 0 END) AS [13_OutH], SUM(CASE DATEPART(day, AttDate) WHEN 13 THEN OutMin ELSE 0 END) AS [13_OutM]," +
        "SUM(CASE DATEPART(day, AttDate) WHEN 14 THEN Code ELSE 0 END) AS [14_Code],SUM(CASE DATEPART(day, AttDate) WHEN 14 THEN InHur ELSE 0 END) AS [14_InH],SUM(CASE DATEPART(day, AttDate) WHEN 14 THEN InMin ELSE 0 END) AS [14_InM]," +
        "SUM(CASE DATEPART(day, AttDate) WHEN 14 THEN OutHur ELSE 0 END) AS [14_OutH], SUM(CASE DATEPART(day, AttDate) WHEN 14 THEN OutMin ELSE 0 END) AS [14_OutM], " +
        "SUM(CASE DATEPART(day, AttDate) WHEN 15 THEN Code ELSE 0 END) AS [15_Code],SUM(CASE DATEPART(day, AttDate) WHEN 15 THEN InHur ELSE 0 END) AS [15_InH], SUM(CASE DATEPART(day, AttDate) WHEN 15 THEN InMin ELSE 0 END) AS [15_InM]," +
        "SUM(CASE DATEPART(day, AttDate) WHEN 15 THEN OutHur ELSE 0 END) AS [15_OutH], SUM(CASE DATEPART(day, AttDate) WHEN 15 THEN OutMin ELSE 0 END) AS [15_OutM], " +
        "SUM(CASE DATEPART(day, AttDate) WHEN 16 THEN Code ELSE 0 END) AS [16_Code],SUM(CASE DATEPART(day, AttDate) WHEN 16 THEN InHur ELSE 0 END) AS [16_InH], SUM(CASE DATEPART(day, AttDate) WHEN 16 THEN InMin ELSE 0 END) AS [16_InM]," +
        "SUM(CASE DATEPART(day, AttDate) WHEN 16 THEN OutHur ELSE 0 END) AS [16_OutH], SUM(CASE DATEPART(day, AttDate) WHEN 16 THEN OutMin ELSE 0 END) AS [16_OutM]," +
        "SUM(CASE DATEPART(day, AttDate) WHEN 17 THEN Code ELSE 0 END) AS [17_Code],SUM(CASE DATEPART(day, AttDate) WHEN 17 THEN InHur ELSE 0 END) AS [17_InH],SUM(CASE DATEPART(day, AttDate) WHEN 17 THEN InMin ELSE 0 END) AS [17_InM], " +
        "SUM(CASE DATEPART(day, AttDate) WHEN 17 THEN OutHur ELSE 0 END) AS [17_OutH],SUM(CASE DATEPART(day, AttDate) WHEN 17 THEN OutMin ELSE 0 END) AS [17_OutM]," +
        "SUM(CASE DATEPART(day, AttDate) WHEN 18 THEN Code ELSE 0 END) AS [18_Code],SUM(CASE DATEPART(day, AttDate) WHEN 18 THEN InHur ELSE 0 END) AS [18_InH],SUM(CASE DATEPART(day, AttDate) WHEN 18 THEN InMin ELSE 0 END) AS [18_InM]," +
        "SUM(CASE DATEPART(day, AttDate) WHEN 18 THEN OutHur ELSE 0 END) AS [18_OutH], SUM(CASE DATEPART(day, AttDate)WHEN 18 THEN OutMin ELSE 0 END) AS [18_OutM]," +
        "SUM(CASE DATEPART(day, AttDate) WHEN 19 THEN Code ELSE 0 END) AS [19_Code], SUM(CASE DATEPART(day, AttDate) WHEN 19 THEN InHur ELSE 0 END) AS [19_InH], SUM(CASE DATEPART(day, AttDate) WHEN 19 THEN InMin ELSE 0 END) AS [19_InM]," +
        "SUM(CASE DATEPART(day, AttDate) WHEN 19 THEN OutHur ELSE 0 END) AS [19_OutH], SUM(CASE DATEPART(day, AttDate) WHEN 19 THEN OutMin ELSE 0 END) AS [19_OutM], " +
        "SUM(CASE DATEPART(day, AttDate) WHEN 20 THEN Code ELSE 0 END) AS [20_Code],SUM(CASE DATEPART(day, AttDate) WHEN 20 THEN InHur ELSE 0 END) AS [20_InH], SUM(CASE DATEPART(day, AttDate) WHEN 20 THEN InMin ELSE 0 END) AS [20_InM]," +
        "SUM(CASE DATEPART(day, AttDate) WHEN 20 THEN OutHur ELSE 0 END) AS [20_OutH], SUM(CASE DATEPART(day, AttDate) WHEN 20 THEN OutMin ELSE 0 END) AS [20_OutM]," +
        "SUM(CASE DATEPART(day, AttDate) WHEN 21 THEN Code ELSE 0 END) AS [21_Code],SUM(CASE DATEPART(day, AttDate) WHEN 21 THEN InHur ELSE 0 END) AS [21_InH], SUM(CASE DATEPART(day, AttDate) WHEN 21 THEN InMin ELSE 0 END) AS [21_InM]," +
        "SUM(CASE DATEPART(day, AttDate) WHEN 21 THEN OutHur ELSE 0 END) AS [21_OutH], SUM(CASE DATEPART(day, AttDate) WHEN 21 THEN OutMin ELSE 0 END) AS [21_OutM]," +
        "SUM(CASE DATEPART(day, AttDate) WHEN 22 THEN Code ELSE 0 END) AS [22_Code],SUM(CASE DATEPART(day, AttDate) WHEN 22 THEN InHur ELSE 0 END) AS [22_InH],SUM(CASE DATEPART(day, AttDate) WHEN 22 THEN InMin ELSE 0 END) AS [22_InM]," +
        "SUM(CASE DATEPART(day, AttDate) WHEN 22 THEN OutHur ELSE 0 END) AS [22_OutH], SUM(CASE DATEPART(day, AttDate) WHEN 22 THEN OutMin ELSE 0 END) AS [22_OutM]," +
        "SUM(CASE DATEPART(day, AttDate) WHEN 23 THEN Code ELSE 0 END) AS [23_Code], SUM(CASE DATEPART(day, AttDate) WHEN 23 THEN InHur ELSE 0 END) AS [23_InH], SUM(CASE DATEPART(day, AttDate) WHEN 23 THEN InMin ELSE 0 END)  AS [23_InM]," +
        "SUM(CASE DATEPART(day, AttDate) WHEN 23 THEN OutHur ELSE 0 END) AS [23_OutH], SUM(CASE DATEPART(day, AttDate) WHEN 23 THEN OutMin ELSE 0 END) AS [23_OutM]," +
        "SUM(CASE DATEPART(day, AttDate) WHEN 24 THEN Code ELSE 0 END) AS [24_Code],SUM(CASE DATEPART(day, AttDate) WHEN 24 THEN InHur ELSE 0 END) AS [24_InH], SUM(CASE DATEPART(day, AttDate) WHEN 24 THEN InMin ELSE 0 END) AS [24_InM]," +
        "SUM(CASE DATEPART(day, AttDate) WHEN 24 THEN OutHur ELSE 0 END) AS [24_OutH], SUM(CASE DATEPART(day, AttDate) WHEN 24 THEN OutMin ELSE 0 END) AS [24_OutM]," +
        "SUM(CASE DATEPART(day, AttDate) WHEN 25 THEN Code ELSE 0 END) AS [25_Code],SUM(CASE DATEPART(day, AttDate) WHEN 25 THEN InHur ELSE 0 END) AS [25_InH], SUM(CASE DATEPART(day, AttDate) WHEN 25 THEN InMin ELSE 0 END) AS [25_InM]," +
        "SUM(CASE DATEPART(day, AttDate) WHEN 25 THEN OutHur ELSE 0 END) AS [25_OutH], SUM(CASE DATEPART(day, AttDate) WHEN 25 THEN OutMin ELSE 0 END) AS [25_OutM]," +
        "SUM(CASE DATEPART(day, AttDate) WHEN 26 THEN Code ELSE 0 END) AS [26_Code],SUM(CASE DATEPART(day, AttDate) WHEN 26 THEN InHur ELSE 0 END) AS [26_InH],SUM(CASE DATEPART(day, AttDate) WHEN 26 THEN InMin ELSE 0 END) AS [26_InM]," +
        "SUM(CASE DATEPART(day, AttDate) WHEN 26 THEN OutHur ELSE 0 END) AS [26_OutH], SUM(CASE DATEPART(day, AttDate) WHEN 26 THEN OutMin ELSE 0 END) AS [26_OutM]," +
        "SUM(CASE DATEPART(day, AttDate) WHEN 27 THEN Code ELSE 0 END) AS [27_Code], SUM(CASE DATEPART(day, AttDate) WHEN 27 THEN InHur ELSE 0 END) AS [27_InH], SUM(CASE DATEPART(day, AttDate) WHEN 27 THEN InMin ELSE 0 END) AS [27_InM]," +
        "SUM(CASE DATEPART(day, AttDate) WHEN 27 THEN OutHur ELSE 0 END) AS [27_OutH], SUM(CASE DATEPART(day, AttDate) WHEN 27 THEN OutMin ELSE 0 END) AS [27_OutM], " +
        "SUM(CASE DATEPART(day, AttDate) WHEN 28 THEN Code ELSE 0 END) AS [28_Code],SUM(CASE DATEPART(day, AttDate) WHEN 28 THEN InHur ELSE 0 END) AS [28_InH], SUM(CASE DATEPART(day, AttDate) WHEN 28 THEN InMin ELSE 0 END) AS [28_InM]," +
        "SUM(CASE DATEPART(day, AttDate) WHEN 28 THEN OutHur ELSE 0 END) AS [28_OutH], SUM(CASE DATEPART(day, AttDate) WHEN 28 THEN OutMin ELSE 0 END) AS [28_OutM], " +
        "SUM(CASE DATEPART(day, AttDate) WHEN 29 THEN Code ELSE 0 END) AS [29_Code],SUM(CASE DATEPART(day, AttDate) WHEN 29 THEN InHur ELSE 0 END) AS [29_InH], SUM(CASE DATEPART(day, AttDate) WHEN 29 THEN InMin ELSE 0 END) AS [29_InM]," +
        "SUM(CASE DATEPART(day, AttDate) WHEN 29 THEN OutHur ELSE 0 END) AS [29_OutH], SUM(CASE DATEPART(day, AttDate) WHEN 29 THEN OutMin ELSE 0 END) AS [29_OutM]," +
        "SUM(CASE DATEPART(day, AttDate) WHEN 30 THEN Code ELSE 0 END) AS [30_Code],SUM(CASE DATEPART(day, AttDate) WHEN 30 THEN InHur ELSE 0 END) AS [30_InH], SUM(CASE DATEPART(day, AttDate) WHEN 30 THEN InMin ELSE 0 END) AS [30_InM]," +
        "SUM(CASE DATEPART(day, AttDate) WHEN 30 THEN OutHur ELSE 0 END) AS [30_OutH], SUM(CASE DATEPART(day, AttDate)  WHEN 30 THEN OutMin ELSE 0 END) AS [30_OutM]," +
        "SUM(CASE DATEPART(day, AttDate) WHEN 31 THEN Code ELSE 0 END) AS [31_Code],SUM(CASE DATEPART(day, AttDate) WHEN 31 THEN InHur ELSE 0 END) AS [31_InH], SUM(CASE DATEPART(day, AttDate) WHEN 31 THEN InMin ELSE 0 END)  AS [31_InM]," +
        "SUM(CASE DATEPART(day, AttDate) WHEN 31 THEN OutHur ELSE 0 END) AS [31_OutH], SUM(CASE DATEPART(day, AttDate) WHEN 31 THEN OutMin ELSE 0 END) AS [31_OutM]" +

        " FROM            dbo.v_DailyEmployeeAttendanceRecord" +
        " where ShiftId='"+Shift+"' and format(attDate,'MM-yyyy')='"+Month+"' and DId in("+DIdList+") and DesId in("+DesIdList+") " +
        " group by EID,EName,DId,ECardNo";
             dt = new DataTable();
             dt = CRUD.ReturnTableNull(sqlcmd);
             return dt;
         }
         catch { return dt = null; }
     }
     public static DataTable returntDataTableForAttSheet(string ShifId, string BatchId, string ClsGrpId, string ClsSecId, string MonthId, string StudentId) // For Monthly Student Attendance Sheet
     {
         try
         {
             StudentId = (StudentId == "All") ? "" : " and StudentId='" + StudentId + "' ";
             sqlcmd = "SELECT   FullName, RollNo, SUM(CASE DATEPART(day, AttDate) WHEN 1 THEN code ELSE 0 END) AS [1], SUM(CASE DATEPART(day, AttDate)"+ 
                      "WHEN 2 THEN code ELSE 0 END) AS [2], SUM(CASE DATEPART(day, AttDate) WHEN 3 THEN code ELSE 0 END) AS [3], SUM(CASE DATEPART(day, AttDate) WHEN 4 THEN code ELSE 0 END) AS [4],"+  
                      "SUM(CASE DATEPART(day, AttDate) WHEN 5 THEN code ELSE 0 END) AS [5], SUM(CASE DATEPART(day, AttDate) WHEN 6 THEN code ELSE 0 END) AS [6], SUM(CASE DATEPART(day, AttDate)"+  
                      "WHEN 7 THEN code ELSE 0 END) AS [7], SUM(CASE DATEPART(day, AttDate) WHEN 8 THEN code ELSE 0 END) AS [8], SUM(CASE DATEPART(day, AttDate) WHEN 9 THEN code ELSE 0 END) AS [9],"+  
                      "SUM(CASE DATEPART(day, AttDate) WHEN 10 THEN code ELSE 0 END) AS [10], SUM(CASE DATEPART(day, AttDate) WHEN 11 THEN code ELSE 0 END) AS [11], SUM(CASE DATEPART(day, AttDate)"+  
                      "WHEN 12 THEN code ELSE 0 END) AS [12], SUM(CASE DATEPART(day, AttDate) WHEN 13 THEN code ELSE 0 END) AS [13], SUM(CASE DATEPART(day, AttDate) WHEN 14 THEN code ELSE 0 END) AS [14],"+  
                      "SUM(CASE DATEPART(day, AttDate) WHEN 15 THEN code ELSE 0 END) AS [15], SUM(CASE DATEPART(day, AttDate) WHEN 16 THEN code ELSE 0 END) AS [16], SUM(CASE DATEPART(day, AttDate) "+ 
                      "WHEN 17 THEN code ELSE 0 END) AS [17], SUM(CASE DATEPART(day, AttDate) WHEN 18 THEN code ELSE 0 END) AS [18], SUM(CASE DATEPART(day, AttDate) WHEN 19 THEN code ELSE 0 END) AS [19],"+  
                      "SUM(CASE DATEPART(day, AttDate) WHEN 20 THEN code ELSE 0 END) AS [20], SUM(CASE DATEPART(day, AttDate) WHEN 21 THEN code ELSE 0 END) AS [21], SUM(CASE DATEPART(day, AttDate) "+ 
                      "WHEN 22 THEN code ELSE 0 END) AS [22], SUM(CASE DATEPART(day, AttDate) WHEN 23 THEN code ELSE 0 END) AS [23], SUM(CASE DATEPART(day, AttDate) WHEN 24 THEN code ELSE 0 END) AS [24],"+  
                      "SUM(CASE DATEPART(day, AttDate) WHEN 25 THEN code ELSE 0 END) AS [25], SUM(CASE DATEPART(day, AttDate) WHEN 26 THEN code ELSE 0 END) AS [26], SUM(CASE DATEPART(day, AttDate) "+ 
                      "WHEN 27 THEN code ELSE 0 END) AS [27], SUM(CASE DATEPART(day, AttDate) WHEN 28 THEN code ELSE 0 END) AS [28], SUM(CASE DATEPART(day, AttDate) WHEN 29 THEN code ELSE 0 END) AS [29],"+
                      "SUM(CASE DATEPART(day, AttDate) WHEN 30 THEN code ELSE 0 END) AS [30], SUM(CASE DATEPART(day, AttDate) WHEN 31 THEN code ELSE 0 END) AS [31], SUM(CASE Code WHEN 112 THEN 1 ELSE 0 END) " + 
                      "AS P, SUM(CASE Code WHEN 97 THEN 1 ELSE 0 END) AS A, SUM(CASE Code WHEN 104 THEN 1 ELSE 0 END) AS H, SUM(CASE Code WHEN 119 THEN 1 ELSE 0 END) AS W  " +
                      " FROM  dbo.v_DailyAttendanceRecordForReport " +
                      " where ShiftId='" + ShifId + "' and BatchId='" + BatchId + "' "+StudentId+" and " +
                      " ClsGrpId='" + ClsGrpId + "' and ClsSecId='" + ClsSecId + "' and format(attdate,'MM-yyyy')='" + MonthId + "'" +
                      " GROUP BY  FullName, RollNo Order by RollNo";
             dt = new DataTable();
             dt = CRUD.ReturnTableNull(sqlcmd);
             return dt;
         }
         catch { return dt = null; }
     }
     public static DataTable returntDataTableForAttSheet(string stdID, string MonthId) // For Monthly Student Attendance Sheet
     {
         try
         {
             sqlcmd = "SELECT  StudentId, FullName, RollNo,BatchName,ShiftName,GroupName,SectionName,ClsGrpId,ShiftId,BatchId,ClsSecId, SUM(CASE DATEPART(day, AttDate) WHEN 1 THEN code ELSE 0 END) AS [1], SUM(CASE DATEPART(day, AttDate)" +
                      "WHEN 2 THEN code ELSE 0 END) AS [2], SUM(CASE DATEPART(day, AttDate) WHEN 3 THEN code ELSE 0 END) AS [3], SUM(CASE DATEPART(day, AttDate) WHEN 4 THEN code ELSE 0 END) AS [4]," +
                      "SUM(CASE DATEPART(day, AttDate) WHEN 5 THEN code ELSE 0 END) AS [5], SUM(CASE DATEPART(day, AttDate) WHEN 6 THEN code ELSE 0 END) AS [6], SUM(CASE DATEPART(day, AttDate)" +
                      "WHEN 7 THEN code ELSE 0 END) AS [7], SUM(CASE DATEPART(day, AttDate) WHEN 8 THEN code ELSE 0 END) AS [8], SUM(CASE DATEPART(day, AttDate) WHEN 9 THEN code ELSE 0 END) AS [9]," +
                      "SUM(CASE DATEPART(day, AttDate) WHEN 10 THEN code ELSE 0 END) AS [10], SUM(CASE DATEPART(day, AttDate) WHEN 11 THEN code ELSE 0 END) AS [11], SUM(CASE DATEPART(day, AttDate)" +
                      "WHEN 12 THEN code ELSE 0 END) AS [12], SUM(CASE DATEPART(day, AttDate) WHEN 13 THEN code ELSE 0 END) AS [13], SUM(CASE DATEPART(day, AttDate) WHEN 14 THEN code ELSE 0 END) AS [14]," +
                      "SUM(CASE DATEPART(day, AttDate) WHEN 15 THEN code ELSE 0 END) AS [15], SUM(CASE DATEPART(day, AttDate) WHEN 16 THEN code ELSE 0 END) AS [16], SUM(CASE DATEPART(day, AttDate) " +
                      "WHEN 17 THEN code ELSE 0 END) AS [17], SUM(CASE DATEPART(day, AttDate) WHEN 18 THEN code ELSE 0 END) AS [18], SUM(CASE DATEPART(day, AttDate) WHEN 19 THEN code ELSE 0 END) AS [19]," +
                      "SUM(CASE DATEPART(day, AttDate) WHEN 20 THEN code ELSE 0 END) AS [20], SUM(CASE DATEPART(day, AttDate) WHEN 21 THEN code ELSE 0 END) AS [21], SUM(CASE DATEPART(day, AttDate) " +
                      "WHEN 22 THEN code ELSE 0 END) AS [22], SUM(CASE DATEPART(day, AttDate) WHEN 23 THEN code ELSE 0 END) AS [23], SUM(CASE DATEPART(day, AttDate) WHEN 24 THEN code ELSE 0 END) AS [24]," +
                      "SUM(CASE DATEPART(day, AttDate) WHEN 25 THEN code ELSE 0 END) AS [25], SUM(CASE DATEPART(day, AttDate) WHEN 26 THEN code ELSE 0 END) AS [26], SUM(CASE DATEPART(day, AttDate) " +
                      "WHEN 27 THEN code ELSE 0 END) AS [27], SUM(CASE DATEPART(day, AttDate) WHEN 28 THEN code ELSE 0 END) AS [28], SUM(CASE DATEPART(day, AttDate) WHEN 29 THEN code ELSE 0 END) AS [29]," +
                      "SUM(CASE DATEPART(day, AttDate) WHEN 30 THEN code ELSE 0 END) AS [30], SUM(CASE DATEPART(day, AttDate) WHEN 31 THEN code ELSE 0 END) AS [31], SUM(CASE Code WHEN 112 THEN 1 ELSE 0 END) " +
                      "AS P, SUM(CASE Code WHEN 97 THEN 1 ELSE 0 END) AS A, SUM(CASE Code WHEN 104 THEN 1 ELSE 0 END) AS H, SUM(CASE Code WHEN 119 THEN 1 ELSE 0 END) AS W  " +
                      " FROM  dbo.v_DailyAttendanceRecordForReport " +
                      " where StudentId='"+stdID+"' AND format(attdate,'MMMM-yyyy')='" + MonthId + "'" +
                      " GROUP BY StudentId,FullName, RollNo,BatchName,ShiftName,GroupName,SectionName,ClsGrpId,ShiftId,BatchId,ClsSecId";
             dt = new DataTable();
             dt = CRUD.ReturnTableNull(sqlcmd);
             return dt;
         }
         catch { return dt = null; }
     }
     public static DataTable returntDataTableForAttSheetAdviser(string EID, string MonthId) // For Monthly Student Attendance Sheet
     {
         try
         {
             sqlcmd = "SELECT  StudentId, FullName, RollNo,BatchName,ShiftName,GroupName,SectionName,ClsGrpId,ShiftId,BatchId,ClsSecId, SUM(CASE DATEPART(day, AttDate) WHEN 1 THEN code ELSE 0 END) AS [1], SUM(CASE DATEPART(day, AttDate)" +
                      "WHEN 2 THEN code ELSE 0 END) AS [2], SUM(CASE DATEPART(day, AttDate) WHEN 3 THEN code ELSE 0 END) AS [3], SUM(CASE DATEPART(day, AttDate) WHEN 4 THEN code ELSE 0 END) AS [4]," +
                      "SUM(CASE DATEPART(day, AttDate) WHEN 5 THEN code ELSE 0 END) AS [5], SUM(CASE DATEPART(day, AttDate) WHEN 6 THEN code ELSE 0 END) AS [6], SUM(CASE DATEPART(day, AttDate)" +
                      "WHEN 7 THEN code ELSE 0 END) AS [7], SUM(CASE DATEPART(day, AttDate) WHEN 8 THEN code ELSE 0 END) AS [8], SUM(CASE DATEPART(day, AttDate) WHEN 9 THEN code ELSE 0 END) AS [9]," +
                      "SUM(CASE DATEPART(day, AttDate) WHEN 10 THEN code ELSE 0 END) AS [10], SUM(CASE DATEPART(day, AttDate) WHEN 11 THEN code ELSE 0 END) AS [11], SUM(CASE DATEPART(day, AttDate)" +
                      "WHEN 12 THEN code ELSE 0 END) AS [12], SUM(CASE DATEPART(day, AttDate) WHEN 13 THEN code ELSE 0 END) AS [13], SUM(CASE DATEPART(day, AttDate) WHEN 14 THEN code ELSE 0 END) AS [14]," +
                      "SUM(CASE DATEPART(day, AttDate) WHEN 15 THEN code ELSE 0 END) AS [15], SUM(CASE DATEPART(day, AttDate) WHEN 16 THEN code ELSE 0 END) AS [16], SUM(CASE DATEPART(day, AttDate) " +
                      "WHEN 17 THEN code ELSE 0 END) AS [17], SUM(CASE DATEPART(day, AttDate) WHEN 18 THEN code ELSE 0 END) AS [18], SUM(CASE DATEPART(day, AttDate) WHEN 19 THEN code ELSE 0 END) AS [19]," +
                      "SUM(CASE DATEPART(day, AttDate) WHEN 20 THEN code ELSE 0 END) AS [20], SUM(CASE DATEPART(day, AttDate) WHEN 21 THEN code ELSE 0 END) AS [21], SUM(CASE DATEPART(day, AttDate) " +
                      "WHEN 22 THEN code ELSE 0 END) AS [22], SUM(CASE DATEPART(day, AttDate) WHEN 23 THEN code ELSE 0 END) AS [23], SUM(CASE DATEPART(day, AttDate) WHEN 24 THEN code ELSE 0 END) AS [24]," +
                      "SUM(CASE DATEPART(day, AttDate) WHEN 25 THEN code ELSE 0 END) AS [25], SUM(CASE DATEPART(day, AttDate) WHEN 26 THEN code ELSE 0 END) AS [26], SUM(CASE DATEPART(day, AttDate) " +
                      "WHEN 27 THEN code ELSE 0 END) AS [27], SUM(CASE DATEPART(day, AttDate) WHEN 28 THEN code ELSE 0 END) AS [28], SUM(CASE DATEPART(day, AttDate) WHEN 29 THEN code ELSE 0 END) AS [29]," +
                      "SUM(CASE DATEPART(day, AttDate) WHEN 30 THEN code ELSE 0 END) AS [30], SUM(CASE DATEPART(day, AttDate) WHEN 31 THEN code ELSE 0 END) AS [31], SUM(CASE Code WHEN 112 THEN 1 ELSE 0 END) " +
                      "AS P, SUM(CASE Code WHEN 97 THEN 1 ELSE 0 END) AS A, SUM(CASE Code WHEN 104 THEN 1 ELSE 0 END) AS H, SUM(CASE Code WHEN 119 THEN 1 ELSE 0 END) AS W  " +
                      " FROM  dbo.v_DailyAttendanceRecordForReport " +
                      " where StudentId in (SELECT StudentId FROM tbl_Guide_Teacher WHERE EID='" +EID+ "') AND format(attdate,'MMMM-yyyy')='" + MonthId + "'" +
                      " GROUP BY StudentId,FullName, RollNo,BatchName,ShiftName,GroupName,SectionName,ClsGrpId,ShiftId,BatchId,ClsSecId";
             dt = new DataTable();
             dt = CRUD.ReturnTableNull(sqlcmd);
             return dt;
         }
         catch { return dt = null; }
     }
     public static DataTable return_dt_for_AttSummary(string FDate, string TDate, string BatchId, string ShiftId, string ClsGrpId, string ClsSecId, string StudentId) // For Student's Attendance Summary 
     {
         try{
             StudentId = (StudentId == "All") ? "" : " and DailyAttendanceRecord.StudentId='" + StudentId + "' ";
         sqlcmd = "SELECT DISTINCT dbo.DailyAttendanceRecord.StudentId, dbo.CurrentStudentInfo.FullName , dbo.DailyAttendanceRecord.RollNo," +
                        " SUM(CAST(CASE WHEN ATTStatus = 'p' THEN 1 ELSE 0 END AS int)) AS [Total Present]," +
                        " SUM(CAST(CASE WHEN ATTStatus = 'a' THEN 1 ELSE 0 END AS int)) AS [Total Absent]" +
                        " FROM dbo.DailyAttendanceRecord INNER JOIN dbo.CurrentStudentInfo ON" +
                        " dbo.DailyAttendanceRecord.StudentId = dbo.CurrentStudentInfo.StudentId " +
                        " where  Convert(datetime,AttDate,105) between Convert(datetime,'" + FDate + "',105) AND Convert(datetime,'" + TDate + "',105) " +
                        "and DailyAttendanceRecord.ShiftId='" + ShiftId + "'and DailyAttendanceRecord.BatchId='" + BatchId + "'"+
                        " and DailyAttendanceRecord.ClsGrpId='" + ClsGrpId + "' and DailyAttendanceRecord.ClsSecId='" + ClsSecId + "' "+StudentId+" " +
                        " GROUP BY dbo.DailyAttendanceRecord.RollNo, dbo.DailyAttendanceRecord.StudentId, dbo.CurrentStudentInfo.FullName Order By dbo.DailyAttendanceRecord.RollNo";
           dt = new DataTable();
           dt = CRUD.ReturnTableNull(sqlcmd);
           return dt;
         }
         catch { return dt = null; }
     }
     public static DataTable return_dt_for_AttSummary(string FDate, string TDate, string stdID) // For Student's Attendance Summary 
     {
         try
         {
             sqlcmd = "SELECT DISTINCT StudentId, FullName ,RollNo,ShiftId,ShiftName,BatchId,BatchName,ClsGrpId,GroupName,ClsSecId,SectionName," +
                            " SUM(CAST(CASE WHEN ATTStatus = 'p' THEN 1 ELSE 0 END AS int)) AS [Total Present]," +
                            " SUM(CAST(CASE WHEN ATTStatus = 'a' THEN 1 ELSE 0 END AS int)) AS [Total Absent]" +
                            " FROM v_DailyAttendanceRecordForReport " +
                            " where  Convert(datetime,AttDate,105) between Convert(datetime,'" + FDate + "',105) AND Convert(datetime,'" + TDate + "',105) " +
                            "and StudentId='"+stdID+"'" +
                            " GROUP BY RollNo, StudentId, FullName,ShiftId,ShiftName,BatchId,BatchName,ClsGrpId,GroupName,ClsSecId,SectionName";
             dt = new DataTable();
             dt = CRUD.ReturnTableNull(sqlcmd);
             return dt;
         }
         catch { return dt = null; }
     }
     public static DataTable return_dt_for_AttSummaryAdviserWise(string FDate, string TDate, string EID) // For Student's Attendance Summary 
     {
         try
         {
             sqlcmd = "SELECT DISTINCT StudentId, FullName ,RollNo,ShiftId,ShiftName,BatchId,BatchName,ClsGrpId,GroupName,ClsSecId,SectionName," +
                            " SUM(CAST(CASE WHEN ATTStatus = 'p' THEN 1 ELSE 0 END AS int)) AS [Total Present]," +
                            " SUM(CAST(CASE WHEN ATTStatus = 'a' THEN 1 ELSE 0 END AS int)) AS [Total Absent]" +
                            " FROM v_DailyAttendanceRecordForReport " +
                            " where  Convert(datetime,AttDate,105) between Convert(datetime,'" + FDate + "',105) AND Convert(datetime,'" + TDate + "',105) " +
                            "and StudentId in (SELECT StudentId FROM tbl_Guide_Teacher WHERE EID='" + EID + "') " +
                            " GROUP BY RollNo, StudentId, FullName,ShiftId,ShiftName,BatchId,BatchName,ClsGrpId,GroupName,ClsSecId,SectionName";
             dt = new DataTable();
             dt = CRUD.ReturnTableNull(sqlcmd);
             return dt;
         }
         catch { return dt = null; }
     }
     public static DataTable return_dt_for_EmpAttSummary(string Shift,string DId,string DesId,string FDate,string TDate,string empType, string EId) //For Staff & Faculty Att Summary 
     {
         try {
             empType = (empType == "2") ? "" : "and dbo.EmployeeInfo.IsFaculty=" + empType + ""; 
             Shift=(Shift=="0")?"":" dbo.DailyAttendanceRecord.ShiftId='"+Shift+"'and ";
             if(EId=="0")
             sqlcmd="SELECT DISTINCT "+
                " dbo.DailyAttendanceRecord.EID,DailyAttendanceRecord.ShiftId,ShiftConfiguration.ShiftName, SUM(CAST(CASE WHEN ATTStatus = 'p' THEN 1 ELSE 0 END AS int)) AS [Total Present], SUM(CAST(CASE WHEN ATTStatus = 'a' THEN 1 ELSE 0 END AS int)) AS [Total Absent], " +
                " SUM(CAST(CASE WHEN ATTStatus = 'lv' THEN 1 ELSE 0 END AS int)) AS [Total Leave], dbo.EmployeeInfo.ECardNo as RollNo, dbo.EmployeeInfo.EName as FullName, dbo.Departments.DId, dbo.Designations.DesId, dbo.Departments.DName, dbo.Designations.DesName " +
                " FROM   dbo.DailyAttendanceRecord INNER JOIN "+
                " dbo.EmployeeInfo ON dbo.DailyAttendanceRecord.EID = dbo.EmployeeInfo.EID INNER JOIN "+
                " dbo.Departments ON dbo.DailyAttendanceRecord.DId = dbo.Departments.DId INNER JOIN "+
                " dbo.Designations ON dbo.DailyAttendanceRecord.DesId = dbo.Designations.DesId INNER JOIN " +
                " dbo.ShiftConfiguration ON dbo.DailyAttendanceRecord.ShiftId = dbo.ShiftConfiguration.ConfigId " +
                " where "+Shift+" dbo.DailyAttendanceRecord.DId in("+DId+")"+
                " and dbo.DailyAttendanceRecord.DesId in("+DesId+") and  Convert(datetime,AttDate,105) between Convert(datetime,'"+FDate+"',105) AND Convert(datetime,'"+TDate+"',105)"+empType+"" +
                " GROUP BY dbo.DailyAttendanceRecord.EID,DailyAttendanceRecord.ShiftId,ShiftConfiguration.ShiftName, dbo.EmployeeInfo.ECardNo, dbo.EmployeeInfo.EName, dbo.Departments.DId, dbo.Designations.DesId, dbo.Departments.DName, dbo.Designations.DesName ";
             else
             sqlcmd = "SELECT DISTINCT " +
               " dbo.DailyAttendanceRecord.EID,DailyAttendanceRecord.ShiftId,ShiftConfiguration.ShiftName, SUM(CAST(CASE WHEN ATTStatus = 'p' THEN 1 ELSE 0 END AS int)) AS [Total Present], SUM(CAST(CASE WHEN ATTStatus = 'a' THEN 1 ELSE 0 END AS int)) AS [Total Absent], " +
               " SUM(CAST(CASE WHEN ATTStatus = 'lv' THEN 1 ELSE 0 END AS int)) AS [Total Leave], dbo.EmployeeInfo.ECardNo as RollNo, dbo.EmployeeInfo.EName as FullName, dbo.Departments.DId, dbo.Designations.DesId, dbo.Departments.DName, dbo.Designations.DesName " +
               " FROM   dbo.DailyAttendanceRecord INNER JOIN " +
               " dbo.EmployeeInfo ON dbo.DailyAttendanceRecord.EID = dbo.EmployeeInfo.EID INNER JOIN " +
               " dbo.Departments ON dbo.DailyAttendanceRecord.DId = dbo.Departments.DId INNER JOIN " +
               " dbo.Designations ON dbo.DailyAttendanceRecord.DesId = dbo.Designations.DesId INNER JOIN " +
               " dbo.ShiftConfiguration ON dbo.DailyAttendanceRecord.ShiftId = dbo.ShiftConfiguration.ConfigId " +
               " where " + Shift + " and dbo.DailyAttendanceRecord.Eid='" + EId + "' " +
               " and  Convert(datetime,AttDate,105) between Convert(datetime,'" + FDate + "',105) AND Convert(datetime,'" + TDate + "',105)" + empType + "" +
               " GROUP BY dbo.DailyAttendanceRecord.EID,DailyAttendanceRecord.ShiftId,ShiftConfiguration.ShiftName, dbo.EmployeeInfo.ECardNo, dbo.EmployeeInfo.EName, dbo.Departments.DId, dbo.Designations.DesId, dbo.Departments.DName, dbo.Designations.DesName ";
            
             dt = new DataTable();
             dt = CRUD.ReturnTableNull(sqlcmd);
             return dt;
         }
         catch { return dt = null; }
     }
     public static DataTable return_dt_for_EmpAttSummary(string FDate, string TDate, string EId) //For Staff & Faculty Att Summary 
     {
         try
         {
                 sqlcmd = "SELECT DISTINCT " +
                   " dbo.DailyAttendanceRecord.EID,DailyAttendanceRecord.ShiftId,ShiftConfiguration.ShiftName, SUM(CAST(CASE WHEN ATTStatus = 'p' THEN 1 ELSE 0 END AS int)) AS [Total Present], SUM(CAST(CASE WHEN ATTStatus = 'a' THEN 1 ELSE 0 END AS int)) AS [Total Absent], " +
                   " SUM(CAST(CASE WHEN ATTStatus = 'lv' THEN 1 ELSE 0 END AS int)) AS [Total Leave], dbo.EmployeeInfo.ECardNo as RollNo, dbo.EmployeeInfo.EName as FullName, dbo.Departments.DId, dbo.Designations.DesId, dbo.Departments.DName, dbo.Designations.DesName " +
                   " FROM   dbo.DailyAttendanceRecord INNER JOIN " +
                   " dbo.EmployeeInfo ON dbo.DailyAttendanceRecord.EID = dbo.EmployeeInfo.EID INNER JOIN " +
                   " dbo.Departments ON dbo.DailyAttendanceRecord.DId = dbo.Departments.DId INNER JOIN " +
                   " dbo.Designations ON dbo.DailyAttendanceRecord.DesId = dbo.Designations.DesId INNER JOIN " +
                   " dbo.ShiftConfiguration ON dbo.DailyAttendanceRecord.ShiftId = dbo.ShiftConfiguration.ConfigId " +
                   " where and dbo.DailyAttendanceRecord.Eid='" + EId + "' " +
                   " and  Convert(datetime,AttDate,105) between Convert(datetime,'" + FDate + "',105) AND Convert(datetime,'" + TDate + "',105) " +
                   " GROUP BY dbo.DailyAttendanceRecord.EID,DailyAttendanceRecord.ShiftId,ShiftConfiguration.ShiftName, dbo.EmployeeInfo.ECardNo, dbo.EmployeeInfo.EName, dbo.Departments.DId, dbo.Designations.DesId, dbo.Departments.DName, dbo.Designations.DesName ";

             dt = new DataTable();
             dt = CRUD.ReturnTableNull(sqlcmd);
             return dt;
         }
         catch { return dt = null; }
     }
     public static DataTable return_dt_All_IndividualAbsent(string FDate,string TDate,string StudentId) // For Student's Individual Absent All Records
     {
         try
         {
             if (StudentId == "0")
             {
                 sqlcmd = "SELECT StudentId, RollNo,AttDate as AbsentDate " +
                                  " FROM  dbo.DailyAttendanceRecord " +
                                  " WHERE AttStatus = 'a' and Convert(datetime,AttDate,105) between Convert(datetime,'" + FDate + "',105) AND Convert(datetime,'" + TDate + "',105) and StudentId is not null"+
                                  " Group by StudentId, RollNo,AttDate";
             }
             else
             {
                 sqlcmd = "SELECT StudentId, RollNo,AttDate as AbsentDate " +
                                  " FROM  dbo.DailyAttendanceRecord " +
                                  " WHERE AttStatus = 'a' and Convert(datetime,AttDate,105) between Convert(datetime,'" + FDate + "',105) AND Convert(datetime,'" + TDate + "',105) and StudentId='" + StudentId + "'";
             }
             dt = new DataTable();
             dt = CRUD.ReturnTableNull(sqlcmd);
             return dt;
         }
         catch { return dt = null; }

     }
     public static DataTable return_dt_Dis_IndividualAbsent( string StudentId) // For  Individual Absent Distinct Records
     {
         try
         {
             if (StudentId == "0")
             {
                 sqlcmd = "SELECT   distinct     dbo.DailyAttendanceRecord.StudentId, dbo.DailyAttendanceRecord.RollNo, dbo.CurrentStudentInfo.FullName," +
                               "dbo.CurrentStudentInfo.ClassName,dbo.CurrentStudentInfo.SectionName" +
                               " FROM  dbo.DailyAttendanceRecord INNER JOIN dbo.CurrentStudentInfo ON dbo.DailyAttendanceRecord.StudentId = dbo.CurrentStudentInfo.StudentId";
             }
             else
             {
                 sqlcmd = "SELECT   distinct     dbo.DailyAttendanceRecord.StudentId, dbo.DailyAttendanceRecord.RollNo, dbo.CurrentStudentInfo.FullName," +
                                  "dbo.CurrentStudentInfo.ClassName,dbo.CurrentStudentInfo.SectionName" +
                                  " FROM  dbo.DailyAttendanceRecord INNER JOIN dbo.CurrentStudentInfo ON dbo.DailyAttendanceRecord.StudentId = dbo.CurrentStudentInfo.StudentId" +
                                  " Where dbo.DailyAttendanceRecord.StudentId='" + StudentId + "'";
             }
             dt = new DataTable();
             dt = CRUD.ReturnTableNull(sqlcmd);
             return dt;
         }
         catch { return dt = null; }

     }
     public static DataTable return_dt_AbsentDetails(string FDate, string TDate,string ShiftId,string ClsGrpId,string ClsSecId, string StudentId) // For Student's Absent Details Records
     {
         try
         {
             if (StudentId == "All")
             {
                 sqlcmd = "Select StudentId,FullName,ClassName,RollNo,sum(case when AttStatus='A' then 1 else 0 end) as [Total Days],"+
                     "(Select STUFF ("+
                            "(Select ',' + CONVERT(VARCHAR(50),Format(AttDate,'dd-MM'))"+
                             " from V_dailyAbsentRecord "+
                             " where StudentId = A.StudentId AND A.AttStatus='A' and Convert(datetime,AttDate,105) between Convert(datetime,'" + FDate + "',105) AND Convert(datetime,'" + TDate + "',105)" +
                             " FOR XML PATH('')"+
                             ")"+
                           ",1,1,'') AS cols"+
                 ") as Days from V_dailyAbsentRecord as A "+
               " where AttStatus='A'  and Convert(datetime,AttDate,105) between Convert(datetime,'" + FDate + "',105) AND Convert(datetime,'" + TDate + "',105)"+
               " and ShiftId='"+ShiftId+"' and ClsGrpId='"+ClsGrpId+"' and ClsSecId='"+ClsSecId+"'  " +
              "group by StudentId,FullName,AttStatus,ClassName,RollNo";
             }
             else
             {
                 sqlcmd = "Select StudentId,FullName,ClassName,RollNo,ShiftId,ShiftName,BatchId,BatchName,ClsGrpId,GroupName,ClsSecId,SectionName,sum(case when AttStatus='A' then 1 else 0 end) as [Total Days]," +
                     "(Select STUFF (" +
                            "(Select ',' + CONVERT(VARCHAR(50),Format(AttDate,'dd-MM'))" +
                             " from V_dailyAbsentRecord " +
                             " where StudentId = A.StudentId AND A.AttStatus='A' and Convert(datetime,AttDate,105) between Convert(datetime,'" + FDate + "',105) AND Convert(datetime,'" + TDate + "',105)" +
                             " FOR XML PATH('')" +
                             ")" +
                           ",1,1,'') AS cols" +
                 ") as Days from V_dailyAbsentRecord as A " +
               " where AttStatus='A'  and Convert(datetime,AttDate,105) between Convert(datetime,'" + FDate + "',105) AND Convert(datetime,'" + TDate + "',105) " +
              " and StudentId='" + StudentId + "' group by StudentId,FullName,AttStatus,ClassName,RollNo,ShiftId,ShiftName,BatchId,BatchName,ClsGrpId,GroupName,ClsSecId,SectionName";
             }
             dt = new DataTable();
             dt = CRUD.ReturnTableNull(sqlcmd);
             return dt;
         }
         catch { return dt = null; }

     }
     public static DataTable return_dt_AbsentDetailsAdviserWise(string FDate, string TDate, string ShiftId, string ClsGrpId, string ClsSecId,string EID) // For Student's Absent Details Records
     {
         try
         {
           
                 sqlcmd = "Select StudentId,FullName,ClassName,RollNo,ShiftId,ShiftName,BatchId,BatchName,ClsGrpId,GroupName,ClsSecId,SectionName,sum(case when AttStatus='A' then 1 else 0 end) as [Total Days]," +
                     "(Select STUFF (" +
                            "(Select ',' + CONVERT(VARCHAR(50),Format(AttDate,'dd-MM'))" +
                             " from V_dailyAbsentRecord " +
                             " where StudentId = A.StudentId AND A.AttStatus='A' and Convert(datetime,AttDate,105) between Convert(datetime,'" + FDate + "',105) AND Convert(datetime,'" + TDate + "',105)" +
                             " FOR XML PATH('')" +
                             ")" +
                           ",1,1,'') AS cols" +
                 ") as Days from V_dailyAbsentRecord as A " +
               " where AttStatus='A'  and Convert(datetime,AttDate,105) between Convert(datetime,'" + FDate + "',105) AND Convert(datetime,'" + TDate + "',105) " +
              " and StudentId in (SELECT StudentId FROM tbl_Guide_Teacher WHERE EID='" + EID + "') group by StudentId,FullName,AttStatus,ClassName,RollNo,ShiftId,ShiftName,BatchId,BatchName,ClsGrpId,GroupName,ClsSecId,SectionName";
       
             dt = new DataTable();
             dt = CRUD.ReturnTableNull(sqlcmd);
             return dt;
         }
         catch { return dt = null; }

     }
     public static DataTable return_dt_EmpAbsentDetails(string Shift, string FDate, string TDate, string EID) // For Staff/Faculty Absent Details Records
     {
         try
         {
             Shift = (Shift == "0") ? "" : "and ShiftId='"+Shift+"'";
             sqlcmd = "Select ShiftId,ShiftName,Eid,ECardNo as RollNo,EName as FullName,DName,DesName,sum(case when AttStatus='A' then 1 else 0 end) as [Total Days]," +
                    " (Select STUFF ( "+
                          "  (Select ',' +CONVERT(VARCHAR(50),Format(AttDate,'dd-MM')) "+
                            "  from v_EmpDailyAbsentRecord "+
                            "  where Eid = A.Eid AND A.AttStatus='a' and Convert(datetime,AttDate,105) between Convert(datetime,'"+FDate+"',105) AND Convert(datetime,'"+TDate+"',105) "+
                             " FOR XML PATH('') "+
                            " ) " +
                          " ,1,1,'') AS cols "+
                " ) as Days from v_EmpDailyAbsentRecord as A "+
               " where AttStatus='A'  and Convert(datetime,AttDate,105) between Convert(datetime,'" + FDate + "',105) AND Convert(datetime,'" + TDate + "',105) and EID in("+EID+") "+Shift+" " +
             " group by ShiftId,ShiftName,Eid,ECardNo,EName,DName,DesName,AttStatus";         
             dt = new DataTable();
             dt = CRUD.ReturnTableNull(sqlcmd);
             return dt;
         }
         catch { return dt = null; }
     }
     public static DataTable return_dt_EmpAbsentDetails(string FDate, string TDate, string EID) // For Staff/Faculty Absent Details Records
     {
         try
         {            
             sqlcmd = "Select ShiftId,ShiftName,Eid,ECardNo as RollNo,EName as FullName,DName,DesName,sum(case when AttStatus='A' then 1 else 0 end) as [Total Days]," +
                    " (Select STUFF ( " +
                          "  (Select ',' +CONVERT(VARCHAR(50),Format(AttDate,'dd-MM')) " +
                            "  from v_EmpDailyAbsentRecord " +
                            "  where Eid = A.Eid AND A.AttStatus='a' and Convert(datetime,AttDate,105) between Convert(datetime,'" + FDate + "',105) AND Convert(datetime,'" + TDate + "',105) " +
                             " FOR XML PATH('') " +
                            " ) " +
                          " ,1,1,'') AS cols " +
                " ) as Days from v_EmpDailyAbsentRecord as A " +
               " where AttStatus='A'  and Convert(datetime,AttDate,105) between Convert(datetime,'" + FDate + "',105) AND Convert(datetime,'" + TDate + "',105) and EID in(" + EID + ") " +
             " group by ShiftId,ShiftName,Eid,ECardNo,EName,DName,DesName,AttStatus";
             dt = new DataTable();
             dt = CRUD.ReturnTableNull(sqlcmd);
             return dt;
         }
         catch { return dt = null; }
     }
     public static DataTable return_dt_DailyEmpAttReport(string reportType, string DepartmentList, string DesignationList, string shift, string FDate,string TDate,string empType,string Eid) //Daily Attendance of Staff & Faculty 
     {
         try 
         {       empType=(empType=="2")?"":"and IsFaculty="+empType+"";
         shift = (shift == "0") ? "" : "and ShiftId='" + shift + "'";
          
             if (reportType == "0") // Daily Attendance Status
             {
                 if(Eid=="0")
                     sqlcmd = "select ECardNo,EName,DName,DesName,ShiftName,ShiftId,AttStatus,Format(AttDates,'dd-MM-yyyy') as AttDates,Format(AttDates,'MM-yyyy') as AttMonth from v_DailyEmployeeAttendanceRecord where DId in(" + DepartmentList + ") and DesId in(" + DesignationList + ") " + shift + "  and Convert(datetime,AttDate,105) between Convert(datetime,'" + FDate + "',105) AND Convert(datetime,'" + TDate + "',105) " + empType + "";
                 else
                     sqlcmd = "select ECardNo,EName,DName,DesName,ShiftName,AttStatus,ShiftId,Format(AttDates,'dd-MM-yyyy') as AttDates,Format(AttDates,'MM-yyyy') as AttMonth from v_DailyEmployeeAttendanceRecord where EId='" + Eid + "' " + shift + "  and Convert(datetime,AttDate,105) between Convert(datetime,'" + FDate + "',105) AND Convert(datetime,'" + TDate + "',105) ";
             }
             else if (reportType == "1") // Daily Present status
             {
                 if (Eid == "0")
                     sqlcmd = "select ECardNo,EName,DName,DesName,ShiftName,ShiftId,AttStatus,Format(AttDates,'dd-MM-yyyy') as AttDates,Format(AttDates,'MM-yyyy') as AttMonth from v_DailyEmployeeAttendanceRecord where  AttStatus='p' and DId in(" + DepartmentList + ") and DesId in(" + DesignationList + ") " + shift + " and Convert(datetime,AttDate,105) between Convert(datetime,'" + FDate + "',105) AND Convert(datetime,'" + TDate + "',105) " + empType + "";   
                 else
                     sqlcmd = "select ECardNo,EName,DName,DesName,ShiftName,ShiftId,AttStatus,Format(AttDates,'dd-MM-yyyy') as AttDates,Format(AttDates,'MM-yyyy') as AttMonth from v_DailyEmployeeAttendanceRecord where  AttStatus='p' and Eid='" + Eid + "' " + shift + " and Convert(datetime,AttDate,105) between Convert(datetime,'" + FDate + "',105) AND Convert(datetime,'" + TDate + "',105) ";   
             }
             else if (reportType == "2") // Daily Absent Staust
             {
                 if (Eid == "0")
                     sqlcmd = "select ECardNo,EName,DName,DesName,ShiftName,ShiftId,AttStatus,Format(AttDates,'dd-MM-yyyy') as AttDates,Format(AttDates,'MM-yyyy') as AttMonth from v_DailyEmployeeAttendanceRecord where  AttStatus='a' and DId in(" + DepartmentList + ") and DesId in(" + DesignationList + ") " + shift + " and Convert(datetime,AttDate,105) between Convert(datetime,'" + FDate + "',105) AND Convert(datetime,'" + TDate + "',105) " + empType + "";
                 else
                     sqlcmd = "select ECardNo,EName,DName,DesName,ShiftName,ShiftId,AttStatus,Format(AttDates,'dd-MM-yyyy') as AttDates,Format(AttDates,'MM-yyyy') as AttMonth from v_DailyEmployeeAttendanceRecord where  AttStatus='a' and Eid='" + Eid + "' " + shift + " and Convert(datetime,AttDate,105) between Convert(datetime,'" + FDate + "',105) AND Convert(datetime,'" + TDate + "',105) ";
             }
             else // Daily LogIn Out
             {  
                 if(Eid=="0")
                     sqlcmd = " SELECT ECardNo,EName, DName, DesName,ShiftName,ShiftId, InHur, InMin, OutHur,OutMin,Format (AttDates,'dd-MM-yyyy') as AttDates,Format(AttDates,'MM-yyyy') as AttMonth" +
                         " FROM  v_DailyEmployeeAttendanceRecord" +
                         " where DId in(" + DepartmentList + ") and DesId in(" + DesignationList + ") " + shift + " and Convert(datetime,AttDate,105) between Convert(datetime,'" + FDate + "',105) AND Convert(datetime,'" + TDate + "',105) " + empType + "" +
                          "ORDER BY DName";
                 else
                     sqlcmd = " SELECT ECardNo,EName, DName, DesName,ShiftName,ShiftId, InHur, InMin, OutHur,OutMin,Format (AttDates,'dd-MM-yyyy') as AttDates,Format(AttDates,'MM-yyyy') as AttMonth" +
                       " FROM  v_DailyEmployeeAttendanceRecord" +
                       " where EId='"+Eid+"' " + shift + " and Convert(datetime,AttDate,105) between Convert(datetime,'" + FDate + "',105) AND Convert(datetime,'" + TDate + "',105) " + empType + "" +
                        "ORDER BY DName";
             }
             dt = new DataTable();
             dt = CRUD.ReturnTableNull(sqlcmd);
             return dt;
         }
         catch { return dt = null; }
     }
     public static DataTable return_dt_DailyEmpAttReport(string reportType, string FDate, string TDate, string Eid) //Daily Attendance of Staff & Faculty 
     {
         try
         {            

             if (reportType == "0") // Daily Attendance Status
             {                
                     sqlcmd = "select ECardNo,EName,DName,DesName,ShiftName,AttStatus,ShiftId,Format(AttDates,'dd-MM-yyyy') as AttDates,Format(AttDates,'MM-yyyy') as AttMonth from v_DailyEmployeeAttendanceRecord where EId='" + Eid + "' and Convert(datetime,AttDate,105) between Convert(datetime,'" + FDate + "',105) AND Convert(datetime,'" + TDate + "',105) ";
             }
             else if (reportType == "1") // Daily Present status
             {
               
                     sqlcmd = "select ECardNo,EName,DName,DesName,ShiftName,ShiftId,AttStatus,Format(AttDates,'dd-MM-yyyy') as AttDates,Format(AttDates,'MM-yyyy') as AttMonth from v_DailyEmployeeAttendanceRecord where  AttStatus='p' and Eid='" + Eid + "' and Convert(datetime,AttDate,105) between Convert(datetime,'" + FDate + "',105) AND Convert(datetime,'" + TDate + "',105) ";
             }
             else if (reportType == "2") // Daily Absent Staust
             {
               
                     sqlcmd = "select ECardNo,EName,DName,DesName,ShiftName,ShiftId,AttStatus,Format(AttDates,'dd-MM-yyyy') as AttDates,Format(AttDates,'MM-yyyy') as AttMonth from v_DailyEmployeeAttendanceRecord where  AttStatus='a' and Eid='" + Eid + "' and Convert(datetime,AttDate,105) between Convert(datetime,'" + FDate + "',105) AND Convert(datetime,'" + TDate + "',105) ";
             }
             else // Daily LogIn Out
             {               
                     sqlcmd = " SELECT ECardNo,EName, DName, DesName,ShiftName,ShiftId, InHur, InMin, OutHur,OutMin,Format (AttDates,'dd-MM-yyyy') as AttDates,Format(AttDates,'MM-yyyy') as AttMonth" +
                       " FROM  v_DailyEmployeeAttendanceRecord" +
                       " where EId='" + Eid + "'  and Convert(datetime,AttDate,105) between Convert(datetime,'" + FDate + "',105) AND Convert(datetime,'" + TDate + "',105) " +
                        "ORDER BY DName";
             }
             dt = new DataTable();
             dt = CRUD.ReturnTableNull(sqlcmd);
             return dt;
         }
         catch { return dt = null; }
     }

     public static DataTable return_dt_EmpAttSheet(string DepartmentList, string DesignationList, string shift, string MonthId,string empType,string EId) // For Staff & Faculty Attendance Sheet.
     {
         try
         {
             empType = (empType == "2") ? "" : "and IsFaculty=" + empType + "";
             shift = (shift == "0") ? "" :" and ShiftId='" + shift + "'";
             if(EId=="0")
                 sqlcmd = " SELECT  EName as FullName, ECardNo as RollNo,ShiftName,ShiftId,  SUM(CASE DATEPART(day, AttDates) WHEN 1 THEN code ELSE 0 END) AS [1], SUM(CASE DATEPART(day, AttDates) " +
                       " WHEN 2 THEN code ELSE 0 END) AS [2], SUM(CASE DATEPART(day, AttDates) WHEN 3 THEN code ELSE 0 END) AS [3], SUM(CASE DATEPART(day, AttDates) WHEN 4 THEN code ELSE 0 END) AS [4], " +
                       " SUM(CASE DATEPART(day, AttDates) WHEN 5 THEN code ELSE 0 END) AS [5], SUM(CASE DATEPART(day, AttDates) WHEN 6 THEN code ELSE 0 END) AS [6], SUM(CASE DATEPART(day, AttDates) " +
                       " WHEN 7 THEN code ELSE 0 END) AS [7], SUM(CASE DATEPART(day, AttDates) WHEN 8 THEN code ELSE 0 END) AS [8], SUM(CASE DATEPART(day, AttDates) WHEN 9 THEN code ELSE 0 END) AS [9]," +
                       " SUM(CASE DATEPART(day, AttDates) WHEN 10 THEN code ELSE 0 END) AS [10], SUM(CASE DATEPART(day, AttDates) WHEN 11 THEN code ELSE 0 END) AS [11], SUM(CASE DATEPART(day, AttDates) " +
                       " WHEN 12 THEN code ELSE 0 END) AS [12], SUM(CASE DATEPART(day, AttDates) WHEN 13 THEN code ELSE 0 END) AS [13], SUM(CASE DATEPART(day, AttDates) WHEN 14 THEN code ELSE 0 END) AS [14], " +
                       " SUM(CASE DATEPART(day, AttDates) WHEN 15 THEN code ELSE 0 END) AS [15], SUM(CASE DATEPART(day, AttDates) WHEN 16 THEN code ELSE 0 END) AS [16], SUM(CASE DATEPART(day, AttDates) " +
                       " WHEN 17 THEN code ELSE 0 END) AS [17], SUM(CASE DATEPART(day, AttDates) WHEN 18 THEN code ELSE 0 END) AS [18], SUM(CASE DATEPART(day, AttDates) WHEN 19 THEN code ELSE 0 END) AS [19], " +
                       " SUM(CASE DATEPART(day, AttDates) WHEN 20 THEN code ELSE 0 END) AS [20], SUM(CASE DATEPART(day, AttDates) WHEN 21 THEN code ELSE 0 END) AS [21], SUM(CASE DATEPART(day, AttDates) " +
                       " WHEN 22 THEN code ELSE 0 END) AS [22], SUM(CASE DATEPART(day, AttDates) WHEN 23 THEN code ELSE 0 END) AS [23], SUM(CASE DATEPART(day, AttDates) WHEN 24 THEN code ELSE 0 END) AS [24]," +
                       " SUM(CASE DATEPART(day, AttDates) WHEN 25 THEN code ELSE 0 END) AS [25], SUM(CASE DATEPART(day, AttDates) WHEN 26 THEN code ELSE 0 END) AS [26], SUM(CASE DATEPART(day, AttDates) " +
                       " WHEN 27 THEN code ELSE 0 END) AS [27], SUM(CASE DATEPART(day, AttDates) WHEN 28 THEN code ELSE 0 END) AS [28], SUM(CASE DATEPART(day, AttDates) WHEN 29 THEN code ELSE 0 END) AS [29]," +
                       " SUM(CASE DATEPART(day, AttDates) WHEN 30 THEN code ELSE 0 END) AS [30], SUM(CASE DATEPART(day, AttDates) WHEN 31 THEN code ELSE 0 END) AS [31], SUM(CASE Code WHEN 112 THEN 1 ELSE 0 END) " +
                       " AS P, SUM(CASE Code WHEN 97 THEN 1 ELSE 0 END) AS A, SUM(CASE Code WHEN 104 THEN 1 ELSE 0 END) AS H, SUM(CASE Code WHEN 119 THEN 1 ELSE 0 END) AS W, " +
                       " SUM(CASE Code WHEN 226 THEN 1 ELSE 0 END) AS LV, DName as BatchId " +
                       " FROM dbo.v_DailyEmployeeAttendanceRecord " +
                       " where DId in(" + DepartmentList + ") and DesId in(" + DesignationList + ") " + shift + " and FORMAT(AttDates,'MM-yyyy')='" + MonthId + "'"+empType+"" +
                       " GROUP BY EName,ECardNo,DName,ShiftName,ShiftId";
             else
                 sqlcmd = " SELECT  EName as FullName, ECardNo as RollNo,ShiftName,ShiftId,  SUM(CASE DATEPART(day, AttDates) WHEN 1 THEN code ELSE 0 END) AS [1], SUM(CASE DATEPART(day, AttDates) " +
                       " WHEN 2 THEN code ELSE 0 END) AS [2], SUM(CASE DATEPART(day, AttDates) WHEN 3 THEN code ELSE 0 END) AS [3], SUM(CASE DATEPART(day, AttDates) WHEN 4 THEN code ELSE 0 END) AS [4], " +
                       " SUM(CASE DATEPART(day, AttDates) WHEN 5 THEN code ELSE 0 END) AS [5], SUM(CASE DATEPART(day, AttDates) WHEN 6 THEN code ELSE 0 END) AS [6], SUM(CASE DATEPART(day, AttDates) " +
                       " WHEN 7 THEN code ELSE 0 END) AS [7], SUM(CASE DATEPART(day, AttDates) WHEN 8 THEN code ELSE 0 END) AS [8], SUM(CASE DATEPART(day, AttDates) WHEN 9 THEN code ELSE 0 END) AS [9]," +
                       " SUM(CASE DATEPART(day, AttDates) WHEN 10 THEN code ELSE 0 END) AS [10], SUM(CASE DATEPART(day, AttDates) WHEN 11 THEN code ELSE 0 END) AS [11], SUM(CASE DATEPART(day, AttDates) " +
                       " WHEN 12 THEN code ELSE 0 END) AS [12], SUM(CASE DATEPART(day, AttDates) WHEN 13 THEN code ELSE 0 END) AS [13], SUM(CASE DATEPART(day, AttDates) WHEN 14 THEN code ELSE 0 END) AS [14], " +
                       " SUM(CASE DATEPART(day, AttDates) WHEN 15 THEN code ELSE 0 END) AS [15], SUM(CASE DATEPART(day, AttDates) WHEN 16 THEN code ELSE 0 END) AS [16], SUM(CASE DATEPART(day, AttDates) " +
                       " WHEN 17 THEN code ELSE 0 END) AS [17], SUM(CASE DATEPART(day, AttDates) WHEN 18 THEN code ELSE 0 END) AS [18], SUM(CASE DATEPART(day, AttDates) WHEN 19 THEN code ELSE 0 END) AS [19], " +
                       " SUM(CASE DATEPART(day, AttDates) WHEN 20 THEN code ELSE 0 END) AS [20], SUM(CASE DATEPART(day, AttDates) WHEN 21 THEN code ELSE 0 END) AS [21], SUM(CASE DATEPART(day, AttDates) " +
                       " WHEN 22 THEN code ELSE 0 END) AS [22], SUM(CASE DATEPART(day, AttDates) WHEN 23 THEN code ELSE 0 END) AS [23], SUM(CASE DATEPART(day, AttDates) WHEN 24 THEN code ELSE 0 END) AS [24]," +
                       " SUM(CASE DATEPART(day, AttDates) WHEN 25 THEN code ELSE 0 END) AS [25], SUM(CASE DATEPART(day, AttDates) WHEN 26 THEN code ELSE 0 END) AS [26], SUM(CASE DATEPART(day, AttDates) " +
                       " WHEN 27 THEN code ELSE 0 END) AS [27], SUM(CASE DATEPART(day, AttDates) WHEN 28 THEN code ELSE 0 END) AS [28], SUM(CASE DATEPART(day, AttDates) WHEN 29 THEN code ELSE 0 END) AS [29]," +
                       " SUM(CASE DATEPART(day, AttDates) WHEN 30 THEN code ELSE 0 END) AS [30], SUM(CASE DATEPART(day, AttDates) WHEN 31 THEN code ELSE 0 END) AS [31], SUM(CASE Code WHEN 112 THEN 1 ELSE 0 END) " +
                       " AS P, SUM(CASE Code WHEN 97 THEN 1 ELSE 0 END) AS A, SUM(CASE Code WHEN 104 THEN 1 ELSE 0 END) AS H, SUM(CASE Code WHEN 119 THEN 1 ELSE 0 END) AS W, " +
                       " SUM(CASE Code WHEN 226 THEN 1 ELSE 0 END) AS LV, DName as BatchId " +
                       " FROM dbo.v_DailyEmployeeAttendanceRecord " +
                       " where EId='" + EId + "' "+shift+" and FORMAT(AttDates,'MM-yyyy')='" + MonthId + "'" + empType + "" +
                       " GROUP BY EName,ECardNo,DName,ShiftName,ShiftId";
             dt = new DataTable();
             dt = CRUD.ReturnTableNull(sqlcmd);
             return dt;
         }
         catch { return dt = null; }
     }
     public static DataTable return_dt_EmpAttSheet(string MonthId, string EId) // For Staff & Faculty Attendance Sheet.
     {
         try
         {
                 sqlcmd = " SELECT  EName as FullName, ECardNo as RollNo,ShiftName,ShiftId,  SUM(CASE DATEPART(day, AttDates) WHEN 1 THEN code ELSE 0 END) AS [1], SUM(CASE DATEPART(day, AttDates) " +
                       " WHEN 2 THEN code ELSE 0 END) AS [2], SUM(CASE DATEPART(day, AttDates) WHEN 3 THEN code ELSE 0 END) AS [3], SUM(CASE DATEPART(day, AttDates) WHEN 4 THEN code ELSE 0 END) AS [4], " +
                       " SUM(CASE DATEPART(day, AttDates) WHEN 5 THEN code ELSE 0 END) AS [5], SUM(CASE DATEPART(day, AttDates) WHEN 6 THEN code ELSE 0 END) AS [6], SUM(CASE DATEPART(day, AttDates) " +
                       " WHEN 7 THEN code ELSE 0 END) AS [7], SUM(CASE DATEPART(day, AttDates) WHEN 8 THEN code ELSE 0 END) AS [8], SUM(CASE DATEPART(day, AttDates) WHEN 9 THEN code ELSE 0 END) AS [9]," +
                       " SUM(CASE DATEPART(day, AttDates) WHEN 10 THEN code ELSE 0 END) AS [10], SUM(CASE DATEPART(day, AttDates) WHEN 11 THEN code ELSE 0 END) AS [11], SUM(CASE DATEPART(day, AttDates) " +
                       " WHEN 12 THEN code ELSE 0 END) AS [12], SUM(CASE DATEPART(day, AttDates) WHEN 13 THEN code ELSE 0 END) AS [13], SUM(CASE DATEPART(day, AttDates) WHEN 14 THEN code ELSE 0 END) AS [14], " +
                       " SUM(CASE DATEPART(day, AttDates) WHEN 15 THEN code ELSE 0 END) AS [15], SUM(CASE DATEPART(day, AttDates) WHEN 16 THEN code ELSE 0 END) AS [16], SUM(CASE DATEPART(day, AttDates) " +
                       " WHEN 17 THEN code ELSE 0 END) AS [17], SUM(CASE DATEPART(day, AttDates) WHEN 18 THEN code ELSE 0 END) AS [18], SUM(CASE DATEPART(day, AttDates) WHEN 19 THEN code ELSE 0 END) AS [19], " +
                       " SUM(CASE DATEPART(day, AttDates) WHEN 20 THEN code ELSE 0 END) AS [20], SUM(CASE DATEPART(day, AttDates) WHEN 21 THEN code ELSE 0 END) AS [21], SUM(CASE DATEPART(day, AttDates) " +
                       " WHEN 22 THEN code ELSE 0 END) AS [22], SUM(CASE DATEPART(day, AttDates) WHEN 23 THEN code ELSE 0 END) AS [23], SUM(CASE DATEPART(day, AttDates) WHEN 24 THEN code ELSE 0 END) AS [24]," +
                       " SUM(CASE DATEPART(day, AttDates) WHEN 25 THEN code ELSE 0 END) AS [25], SUM(CASE DATEPART(day, AttDates) WHEN 26 THEN code ELSE 0 END) AS [26], SUM(CASE DATEPART(day, AttDates) " +
                       " WHEN 27 THEN code ELSE 0 END) AS [27], SUM(CASE DATEPART(day, AttDates) WHEN 28 THEN code ELSE 0 END) AS [28], SUM(CASE DATEPART(day, AttDates) WHEN 29 THEN code ELSE 0 END) AS [29]," +
                       " SUM(CASE DATEPART(day, AttDates) WHEN 30 THEN code ELSE 0 END) AS [30], SUM(CASE DATEPART(day, AttDates) WHEN 31 THEN code ELSE 0 END) AS [31], SUM(CASE Code WHEN 112 THEN 1 ELSE 0 END) " +
                       " AS P, SUM(CASE Code WHEN 97 THEN 1 ELSE 0 END) AS A, SUM(CASE Code WHEN 104 THEN 1 ELSE 0 END) AS H, SUM(CASE Code WHEN 119 THEN 1 ELSE 0 END) AS W, " +
                       " SUM(CASE Code WHEN 226 THEN 1 ELSE 0 END) AS LV, DName as BatchId " +
                       " FROM dbo.v_DailyEmployeeAttendanceRecord " +
                       " where EId='" + EId + "' and FORMAT(AttDates,'MM-yyyy')='" + MonthId + "'" +
                       " GROUP BY EName,ECardNo,DName,ShiftName,ShiftId";
             dt = new DataTable();
             dt = CRUD.ReturnTableNull(sqlcmd);
             return dt;
         }
         catch { return dt = null; }
     }
    }
}
