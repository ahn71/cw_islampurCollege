using DS.DAL.AdviitDAL;
using ComplexScriptingSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DS.BLL;

namespace DS.Forms.leave
{
    public partial class application : System.Web.UI.Page
    {
        DataTable dt;
        ArrayList NewAllLeaveDate;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["__UserId__"] == null)
            {
                Response.Redirect("~/UserLogin.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    loadLeaveName();
                    loadLeaveApplication();
                    ViewState["yesAlater"] = "False";
                }
            }
            
        }

        private void loadLeaveApplication()
        {
            try
            {
                sqlDB.fillDataTable("select LACode,ShortName,LeaveId,convert(varchar(11),FromDate,105) as FromDate,convert(varchar(11),ToDate,105) as ToDate,WeekHolydayNo,TotalDays,EID,ECardNo from v_Leave_Application where IsApproved ='true' and IsProcessessed='true'  order by ECardNo,FromDate desc", dt = new DataTable());
                gvLeaveApplicationList.DataSource = dt;
                gvLeaveApplicationList.DataBind();
            }
            catch { }
        }

        private void loadLeaveName()
        {
            try
            {
                sqlDB.fillDataTable("select LeaveName+' '+ShortName as LeaveName,LeaveId from leave_configuration", dt = new DataTable());
                ddlLeaveName.DataTextField = "LeaveName";
                ddlLeaveName.DataValueField = "LeaveId";
                ddlLeaveName.DataSource = dt;
                ddlLeaveName.DataBind();
                ddlLeaveName.Items.Insert(0, new ListItem(" ", "s"));
            }
            catch { }
        }

        private void saveLeaveApplication()
        {
            try
            {
          
                byte process=(long.Parse(txtFromDate.Text.Trim().Replace('-','0'))>=long.Parse(TimeZoneBD.getCurrentTimeBD().ToString("dd-MM-yyyy").Replace('-','0')))?(byte)1:(byte)0;
             
                string[] getColumns = { "LeaveId","EID","FromDate", "ToDate", "WeekHolydayNo", "TotalDays","ApprovedDate","IsApproved","IsProcessessed" };
                string[] getValues = { ddlLeaveName.SelectedValue.ToString(),ViewState["__EId__"].ToString(),convertDateTime.getCertainCulture(txtFromDate.Text.Trim()).ToString(), convertDateTime.getCertainCulture(txtToDate.Text.Trim()).ToString(), txtTotalHolydays.Text.Trim(), txtNoOfDays.Text.Trim(), TimeZoneBD.getCurrentTimeBD("yyyy-MM-dd"), process.ToString(), process .ToString()};
                if (SQLOperation.forSaveValue("Leave_Application", getColumns, getValues,sqlDB.connection) == true)
                {
                    PeocessLeaveDetails();
                    lblMessage.InnerText = "success->Successfully Leave Application Saved";
                    loadLeaveApplication();
                    allClear();
                }
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->"+ex.Message;
            }
        }

      
        protected void btnCalculation_Click(object sender, EventArgs e)
        {
            try
            {
                if (!checkIsCurrectCardNo()) return;
                else
                {
                    string FromDate = txtFromDate.Text.Substring(6, 4) + "-" + txtFromDate.Text.Trim().Substring(3, 2) + "-" + txtFromDate.Text.Trim().Substring(0,2);
                    string ToDate = txtToDate.Text.Substring(6, 4) + "-" + txtToDate.Text.Trim().Substring(3, 2) + "-" + txtToDate.Text.Trim().Substring(0, 2);
                    sqlDB.fillDataTable("select * from OffdaySettings where OffDate>='" + FromDate + "' AND OffDate <='" + ToDate + "'",dt=new DataTable ());
                    //if (dt.Rows.Count > 0)
                    //{
                        txtTotalHolydays.Text = dt.Rows.Count.ToString();
                        sqlDB.fillDataTable("select DateDiff(Day,'" + FromDate + "','" + ToDate + "') as TotalDays", dt = new DataTable());
                        txtNoOfDays.Text = (int.Parse(dt.Rows[0]["TotalDays"].ToString())+ 1).ToString();
                    //}

                   // string aa = Request.ServerVariables["SERVER_SOFTWARE"].ToString();
                
                }
            }
            catch { }
        }

        private bool checkIsCurrectCardNo()
        {
            try
            {
                sqlDB.fillDataTable("select EId from EmployeeInfo where ECardNo=" + txtECardNo.Text.Trim()+ "",dt=new DataTable ());
                if (dt.Rows.Count > 0)
                {
                    ViewState["__EId__"] = dt.Rows[0]["EId"].ToString();
                    return true;
                }
                else return false;
            }
            catch { lblMessage.InnerText = "warning->Sorry This E-CardNo Is Not Valid"; return false; }
        }

        string[] fromDates;
        string[] toDates;
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (ViewState["yesAlater"].Equals("True"))
            {
                

                DataTable dtLeaveDetails=new DataTable ();

                 fromDates = txtFromDate.Text.Trim().Split('-');
                 toDates = txtToDate.Text.Trim().Split('-');

                //sqlDB.fillDataTable("Select lad.SL,lad.LACode,lad.EID,Format(lad.LeaveDate,'dd-MM-yyyy') as LeaveDate,lad.Used,la.FromDate,la.ToDate "+
                //"from Leave_Application_Details as lad,Leave_Application as la  where la.LACode=" + ViewState["__LACode__"].ToString() + " "+
                //" and lad.LACode=" + ViewState["__LACode__"].ToString() + " And lad.LeaveDate>='"+fromDates[2]+"-"+fromDates[1]+"-"+fromDates[0]+"' "+
                //" And lad.LeaveDate <='"+toDates[2]+"-"+toDates[1]+"-"+toDates[0]+"'", dtLeaveDetails);

            


                sqlDB.fillDataTable("select StartTime,CloseTime,LateTime from ShiftConfiguration where ShiftName='Morning'", dt = new DataTable());
                string [] startTimes=dt.Rows[0]["StartTime"].ToString().Split(':');
                string [] closeTimes=dt.Rows[0]["CloseTime"].ToString().Split(':');

                   

           
               
                string DailyStartTimeALT_CloseTime = startTimes[0] + ":" + startTimes[1] + ":" + startTimes[2] + ":" + closeTimes[0] + ":" + closeTimes[1] + ":" + dt.Rows[0]["LateTime"].ToString();
                btnCalculation_Click(sender, e);
               
                if (!txtFromDate.Text.Trim().Equals(ViewState["OldFromDate"].ToString()) || !txtToDate.Text.Trim().Equals(ViewState["OldToDate"].ToString()))
                {
                    calculationForAlterOperation();

                    checkNotSameDate();
                    for (int i = 0; i < dtNotSameDate.Rows.Count; i++)
                    {
                        updateDailyAttendance(dtNotSameDate.Rows[i]["LeaveDate"].ToString(), startTimes[0], startTimes[1], startTimes[2], closeTimes[0], closeTimes[1], closeTimes[2], DailyStartTimeALT_CloseTime,dtNotSameDate.Rows[i]["Status"].ToString());
                    }

                    SQLOperation.forDelete("temp_NewLeaveDateForAlter", sqlDB.connection);
                    SQLOperation.forDelete("temp_OldLeaveDateForAlter", sqlDB.connection);
                }
                

                SQLOperation.forDeleteRecordByIdentifier("Leave_Application", "LACode", ViewState["__LACode__"].ToString(), sqlDB.connection);
                SQLOperation.forDeleteRecordByIdentifier("Leave_Application_Details", "LACode", ViewState["__LACode__"].ToString(), sqlDB.connection);
                
            }

            saveLeaveApplication();
            ViewState["yesAlater"] = "False";
        }



        private void calculationForAlterOperation()
        {
            try
            {
                SQLOperation.forDelete("temp_NewLeaveDateForAlter", sqlDB.connection);
                SQLOperation.forDelete("temp_OldLeaveDateForAlter", sqlDB.connection);

                DateTime NewFromDate = new DateTime(int.Parse(txtFromDate.Text.Substring(6, 4)), int.Parse(txtFromDate.Text.Substring(3, 2)), int.Parse(txtFromDate.Text.Substring(0, 2)));
                DateTime NewToDate = new DateTime(int.Parse(txtToDate.Text.Substring(6, 4)), int.Parse(txtToDate.Text.Substring(3, 2)), int.Parse(txtToDate.Text.Substring(0, 2)));


                
                while (NewFromDate <= NewToDate)
                {
                    string[] newFromDates = NewFromDate.ToString().Split('/');
                    string getDay = (newFromDates[1].Length == 0) ? "0" + newFromDates[1] : newFromDates[1];
                    string getMonth = (newFromDates[0].Length == 0) ? "0" + newFromDates[0] : newFromDates[0];

                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("insert into temp_NewLeaveDateForAlter (LeaveDate,Status) values(@LeaveDate,@Status)", sqlDB.connection);
                    cmd.Parameters.AddWithValue("@LeaveDate", convertDateTime.getCertainCulture(getDay + "-" + getMonth + "-" + newFromDates[2].Substring(0, 4)));
                    cmd.Parameters.AddWithValue("@Status","New");
                    cmd.ExecuteNonQuery();

                    NewFromDate = NewFromDate.AddDays(1);
                    // NewAllLeaveDate.Add(getDay + "-" + getMonth + "-" + oldFromDates[2].Substring(0, 4));
                    // OldFromDate = OldFromDate.AddDays(1);

                }


                fromDates = ViewState["OldFromDate"].ToString().Split('-');
                toDates = ViewState["OldToDate"].ToString().Split('-');

                DateTime oldFromDate = new DateTime(int.Parse(fromDates[2]), int.Parse(fromDates[1]), int.Parse(fromDates[0]));
                DateTime oldToDate = new DateTime(int.Parse(toDates[2]), int.Parse(toDates[1]), int.Parse(toDates[0]));

                while (oldFromDate <= oldToDate)
                {
                    fromDates = oldFromDate.ToString().Split('/');
                    string getDay = (fromDates[1].Length == 0) ? "0" + fromDates[1] : fromDates[1];
                    string getMonth = (fromDates[0].Length == 0) ? "0" + fromDates[0] : fromDates[0];

                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("insert into temp_OldLeaveDateForAlter (LeaveDate,Status) values(@LeaveDate,@Status)", sqlDB.connection);
                    cmd.Parameters.AddWithValue("@LeaveDate", convertDateTime.getCertainCulture(getDay + "-" + getMonth + "-" + fromDates[2].Substring(0, 4)));
                    cmd.Parameters.AddWithValue("@Status", "Old");
                    cmd.ExecuteNonQuery();
                    oldFromDate = oldFromDate.AddDays(1);
                }  
            }
            catch { }
        }


        DataTable dtNotSameDate;
        private void checkNotSameDate()
        {
            try
            {
                sqlDB.fillDataTable("select FORMAT(LeaveDate,'yyyy-MM-dd') as LeaveDate,Status from temp_NewLeaveDateForAlter where LeaveDate  not in (select LeaveDate from temp_OldLeaveDateForAlter)", dtNotSameDate = new DataTable());
                sqlDB.fillDataTable("select FORMAT(LeaveDate,'yyyy-MM-dd') as LeaveDate,Status from temp_OldLeaveDateForAlter where LeaveDate  not in (select LeaveDate from temp_NewLeaveDateForAlter)", dtNotSameDate);
            }
            catch { }
        }
        private void updateDailyAttendance(string attDate, string InHour, string InMin, string InSec, string OutHur, string OutMin, string OutSec, string DailyStartTimeALT_CloseTime,string Status)
        {
            try
            {
                sqlDB.fillDataTable("select * from DailyAttendanceRecord where attDate='" + attDate + "'", dt = new DataTable());
               
                if (dt.Rows.Count > 0)
                {
                  
                    
                    string attStatus;
                    if (Status.Equals("Old"))
                    {
                        sqlDB.fillDataTable("select Convert(varchar(11),OffDate,105) as OffDate,Purpose from OffdaySettings where OffDate='" + attDate + "'", dt = new DataTable());
                        if (dt.Rows.Count > 0) attStatus = (dt.Rows[0]["Purpose"].ToString().Trim().Equals("Weekly Holiday")) ? "w" : "h";
                        else attStatus = "p";
                    }
                    else attStatus = "lv";

                    string stateStatus = (Status.Equals("Old")) ? "Present" : ddlLeaveName.SelectedValue.ToString().Substring(0, ddlLeaveName.SelectedItem.ToString().Length - 3);
                    
                  
                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("update DailyAttendanceRecord set InHur='" + InHour + "',InMin='"+InMin+"', "+
                    "InSec='" + InSec + "',OutHur='" + OutHur + "',OutMin='" + OutMin + "',OutSec='" + OutSec + "',AttStatus='"+attStatus+"',StateStatus='"+stateStatus+"', "+
                    "AttManual='Manual Attendance',DailyStartTimeALT_CloseTime='" + DailyStartTimeALT_CloseTime + "' where EID=" + ViewState["__EId__"].ToString() + " and AttDate='" + attDate + "'", sqlDB.connection);
                    cmd.ExecuteNonQuery();
                }
            }
            catch { }
        }

        
        private void PeocessLeaveDetails()
        {
            try
            {
                sqlDB.fillDataTable("select MAX(LACode) as LACode from Leave_Application where EID=" + ViewState["__EId__"].ToString() + "", dt = new DataTable());
                DateTime FromDate=new DateTime (int.Parse(txtFromDate.Text.Trim().Substring(6,4)),int.Parse(txtFromDate.Text.Trim().Substring(3,2)),int.Parse(txtFromDate.Text.Trim().Substring(0,2)));
                DateTime ToDate = new DateTime(int.Parse(txtToDate.Text.Trim().Substring(6, 4)), int.Parse(txtToDate.Text.Trim().Substring(3, 2)), int.Parse(txtToDate.Text.Trim().Substring(0, 2)));
                while (FromDate <= ToDate)
                {
                    string[] getFromDate = FromDate.ToString().Split('/');
                    saveLeaveDetails(dt.Rows[0]["LACode"].ToString(),getFromDate[1]+"-"+getFromDate[0]+"-"+getFromDate[2].Substring(0,4));  
                    FromDate = FromDate.AddDays(1);
                }

            }
            catch { }
        }

        private void saveLeaveDetails(string LACode,string Date)
        {
            try
            {
                string[] getColumns = { "LACode", "EID", "LeaveDate", "Used" };
                string[] getValues = { LACode, ViewState["__EId__"].ToString(),convertDateTime.getCertainCulture(Date).ToString(),"0"};
                SQLOperation.forSaveValue("Leave_Application_Details", getColumns, getValues, sqlDB.connection);
                
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
            }
        }

        protected void gvLeaveApplicationList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                lblMessage.InnerText = "";
                loadLeaveApplication();
                gvLeaveApplicationList.PageIndex = e.NewPageIndex;
                gvLeaveApplicationList.DataBind();
            }
            catch { }
        }

        bool yesAlater = false;
        protected void gvLeaveApplicationList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
              
                //gvLeaveApplicationList.Rows[2].CssClass.Remove(2,1);
                //gvLeaveApplicationList.Rows[2].BackColor = System.Drawing.Color.Yellow;
                lblMessage.InnerText = "";
                if (e.CommandName.Equals("Status"))
                {
                    DataTable dtLeaveInfo = new DataTable();
                    sqlDB.fillDataTable("select Ename,ECardNo,LeaveName,Used,Format(LeaveDate,'dd-MM-yyyy') as LeaveDate from v_Leave_Application_Details where LACode=" + gvLeaveApplicationList.DataKeys[int.Parse(e.CommandArgument.ToString())].Value.ToString() + "", dtLeaveInfo);
                    dt = new DataTable();
                    DataRow[] dr={};
                    try
                    {                        
                      dr= dtLeaveInfo.Select("Used='true'", null);
                    }
                    catch { }
                    msg.InnerText ="Leave Status Of "+ dtLeaveInfo.Rows[0]["LeaveName"].ToString();
                    lblName.Text = dtLeaveInfo.Rows[0]["EName"].ToString();
                    lblCardNo.Text=dtLeaveInfo.Rows[0]["ECardNo"].ToString();
                    lblTotalLeave.Text = dtLeaveInfo.Rows.Count.ToString();
                    lblUsed.Text = dr.Length.ToString();
                    lblUnUsed.Text = (dtLeaveInfo.Rows.Count - dr.Length).ToString();
                    
                    ModalPopupExtender1.Show();
                }
                else if (e.CommandName.Equals("Alter"))
                {
                    int rIndex = int.Parse(e.CommandArgument.ToString());
                    ViewState["__LACode__"] = gvLeaveApplicationList.DataKeys[rIndex].Value.ToString();

                    txtECardNo.Text = gvLeaveApplicationList.Rows[rIndex].Cells[1].Text;
                    ViewState["OldFromDate"]=txtFromDate.Text = gvLeaveApplicationList.Rows[rIndex].Cells[2].Text;
                    ViewState["OldToDate"] =txtToDate.Text = gvLeaveApplicationList.Rows[rIndex].Cells[3].Text;
                    txtNoOfDays.Text = gvLeaveApplicationList.Rows[rIndex].Cells[5].Text;
                    txtTotalHolydays.Text = gvLeaveApplicationList.Rows[rIndex].Cells[4].Text;

                    for (byte b = 1; b < ddlLeaveName.Items.Count; b++)
                    {
                        if (ddlLeaveName.Items[b].Text.Substring(ddlLeaveName.Items[b].Text.Length - 3, 3).Equals(gvLeaveApplicationList.Rows[rIndex].Cells[6].Text)) ddlLeaveName.SelectedIndex = b;
                    }

                    ViewState["yesAlater"] = "True";


                }

                else
                {
                    SQLOperation.forDeleteRecordByIdentifier("Leave_Application", "LACode",e.CommandArgument.ToString(), sqlDB.connection);
                    SQLOperation.forDeleteRecordByIdentifier("Leave_Application_Details", "LACode", e.CommandArgument.ToString(), sqlDB.connection);
                  
                }
            }
            catch { }
        }

        private void allClear()
        {
            try
            {
                ViewState["yesAlater"] = "False";
                txtECardNo.Text = "";
                txtFromDate.Text = "";
                txtToDate.Text = "";
                txtNoOfDays.Text = "";
                txtTotalHolydays.Text = "";
                ddlLeaveName.SelectedIndex = 0;
            }
            catch { }
        }

        protected void tnReset_Click(object sender, EventArgs e)
        {
            allClear();
        }

        protected void gvLeaveApplicationList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            loadLeaveApplication();
        }

      
    }
}