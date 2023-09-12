using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DS.DAL.AdviitDAL;
using System.Data;
using System.Data.SqlClient;
using ComplexScriptingSystem;
using DS.DAL;

namespace DS.UI.Academic.Attendance.StafforFaculty.Manually
{
    public partial class ForUpdate : System.Web.UI.Page
    {
        DataTable dt;
        string getValue;
        string getTblData;
        SqlCommand cmd;
        SqlDataAdapter da;
        string[] getTableName;
        protected void Page_Load(object sender, EventArgs e)
        {           
            // string value format = TableName,ShiftId,BatchId,ClsGroupId,SectionId,Date,ColumnName,StudentId
            getTblData = Request.QueryString["tbldata"];
            getValue = Request.QueryString["val"];
            getTableName = getTblData.Split(',');
            if (getTableName[0].Contains("DailyAttendanceRecord")) FacultyNStaffAttendanceCount();  // faculty and staff attendence count
            
        }



        private void FacultyNStaffAttendanceCount()   // update daily attendance for every student
        {
            try
            {
                string[] getTableData = getTblData.Split(',');
                string[] getdates = getTableData[1].Split('_');
                string AttDate = getdates[2] + "-" + getdates[1] + "-" + ((getdates[0].Length == 1) ? "0" + getdates[0] : getdates[0]);
                if (getValue.ToString().ToLower().Equals("v"))
                {
                    getValue = "lv";

                    if (!FindAnyLeaveAreExists_AndCounting(getTableData[3],AttDate))
                    {
                        Response.Write("Leave Are Not Exists"); return;
                    }

                    cmd = new SqlCommand("update DailyAttendanceRecord set AttStatus ='" + getValue.ToLower() + "',StateStatus='" + ViewState["__LeaveName__"].ToString() + "',AttManual='Manual' where EId=" + getTableData[3] + " AND Format(AttDate,'yyyy-MM-dd')='" + AttDate + "'", DbConnection.Connection);
                    byte b=(byte)cmd.ExecuteNonQuery();
                    if (b==0)AddNewAttrecordAsAttendance(getTableName[3],getTableName[4],getTableName[5],AttDate,getValue,ViewState["__LeaveName__"].ToString(),getTableName[6],getTableName[7],getTableName[8].Substring(0,getTableName[8].LastIndexOf(' ')),getTableName[9],getTableName[10],getTableName[11].Substring(0,getTableName[11].LastIndexOf(' ')),getTableName[12]);
                }

                else
                {

                    if (!FindAnyAbsentRecordAreExists_ThisDate(getTableData[3], AttDate))
                    {
                        Response.Write("Leave Are Exists"); return;
                    }
                    else
                    {
                        
                        string stateStatus =(getValue == "p" || getValue == "l") ? "Present":"Absent";

                        cmd = new SqlCommand("update DailyAttendanceRecord set AttStatus ='" + getValue.ToLower() + "',StateStatus='" + stateStatus + "',AttManual='Manual' where EId=" + getTableData[3] + " AND Format(AttDate,'yyyy-MM-dd')='" + AttDate + "'", DbConnection.Connection);
                        byte b=(byte)cmd.ExecuteNonQuery();
                        if (b == 0)
                        {
                            AddNewAttrecordAsAttendance(getTableName[3], getTableName[4], getTableName[5], AttDate, getValue,stateStatus, getTableName[6], getTableName[7], getTableName[8].Substring(0, getTableName[8].LastIndexOf(' ')), getTableName[9], getTableName[10], getTableName[11].Substring(0, getTableName[11].LastIndexOf(' ')), getTableName[12]);
                        }
                    }
                    
                   
                }
                
  
            }
            catch { }
        }

        private bool FindAnyAbsentRecordAreExists_ThisDate(string EId,string AttDate)
        {
            try
            {
                SQLOperation.selectBySetCommandInDatatable("select LAcode from Leave_Application where LAcode=( select LAcode from Leave_Application_Details where LeaveDate='" + AttDate + "' AND EId=" + EId + ") AND IsApproved='True'", dt = new DataTable(), DbConnection.Connection);
                if (dt.Rows.Count > 0) return false;
                else return true;
                

            }
            catch { return false; }
        }

        private bool FindAnyLeaveAreExists_AndCounting(string EId,string AttDate)
        {
            try
            {
                SQLOperation.selectBySetCommandInDatatable("select LAcode,LeaveName,Format(ToDate,'yyyy-MM-dd') as ToDate from v_Leave_Application where LAcode=( select LAcode from Leave_Application_Details where LeaveDate='" + AttDate + "' AND EId=" + EId + ") AND IsApproved='True'", dt = new DataTable(), DbConnection.Connection);
                if (dt.Rows.Count > 0)
                {
                    ViewState["__LeaveName__"] = dt.Rows[0]["LeaveName"].ToString();
                    SqlCommand cmd = new SqlCommand("update Leave_Application_Details set Used='True' Where LeaveDate='" + AttDate + "' AND EId=" + EId + " AND LACode="+dt.Rows[0]["LACode"].ToString()+"", DbConnection.Connection);
                    cmd.ExecuteNonQuery();

                    if (dt.Rows[0]["ToDate"].ToString() == AttDate)
                    {
                        cmd = new SqlCommand("update Leave_Application set IsProcessessed='False' where LACode=" + dt.Rows[0]["LACode"].ToString() + " AND Format(ToDate,'yyyy-MM-dd')='" + AttDate + "'", DbConnection.Connection);
                        cmd.ExecuteNonQuery();
                    }

                    return true;
                }

               else  return false;
            }
            catch { return false; }
        }

        public void AddNewAttrecordAsAttendance(string EId, string DId,string shiftId, string AttDate, string AttStatus, string StateStatus, string InH, string InM, string Ins, string OutH, string OutM, string OutS, string ALT)
        {
            try
            {
                DataTable dt=CRUD.ReturnTableNull("select DesId From EmployeeInfo where EId="+EId+"");
                cmd = new SqlCommand("insert into DailyAttendanceRecord (EId,DId,DesId,ShiftId,AttDate,AttStatus,StateStatus,AttManual,InHur,InMin,InSec,OutHur,OutMin,OutSec,DailyStartTimeALT_CloseTime) "+
                      "Values ("+EId+","+DId+","+dt.Rows[0]["DesId"].ToString()+","+shiftId+",'"+AttDate+"','"+AttStatus+"','"+StateStatus+"','Manual',"+InH+","+InM+","+Ins+","+OutH+","+OutM+","+OutS+",'"+InH+":"+InM+":"+Ins+":"+OutH+":"+OutM+":"+OutS+":"+ALT+"')",DbConnection.Connection);
                cmd.ExecuteNonQuery();
            }
            catch { }
        
        }
    }
}