using DS.DAL.AdviitDAL;
using DS.DAL.ComplexScripting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DS.BLL.ControlPanel;
using DS.BLL.Attendace;

namespace DS.UI.Academics.Attendance.Leave
{
    public partial class application : System.Web.UI.Page
    {
        DataTable dt;
        ArrayList NewAllLeaveDate;
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.InnerText = "";
                if (!IsPostBack)
                {
                    loadLeaveName();
                    loadLeaveApplication();
                    ViewState["yesAlater"] = "False";
                    ViewState["__EId__"] = "0";
                    Classes.commonTask.loadShift(ddlShift);

                    PrivilegeOperation.SetPrivilegeControl(Session["__UserTypeId__"].ToString(), "application.aspx", btnSave, gvLeaveApplicationList,gvRejectedList);
                }
        }       
        private void loadLeaveApplication()
        {
            try
            {
                sqlDB.fillDataTable("select LACode,ShortName,LeaveId,convert(varchar(11),FromDate,105) as FromDate,convert(varchar(11),ToDate,105) as ToDate,WeekHolydayNo,TotalDays,EID,ECardNo,ShiftId,ShiftName,Remarks from v_Leave_Application where IsApproved ='true' and IsProcessessed='true'  order by ECardNo,FromDate desc", dt = new DataTable());
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
                if (!ddlLeaveName.SelectedItem.ToString().Contains("m/l"))
                {

                    string[] getColumns = { "LeaveId", "FromDate", "ToDate", "WeekHolydayNo", "TotalDays", "Remarks", "EID", "IsApproved", "IsProcessessed", "EntryDate", "IsMeternity", "ShiftId" };

                    string[] getValues = {ddlLeaveName.SelectedItem.Value.ToString(),convertDateTime.getCertainCulture(txtFromDate.Text.Trim()).ToString(),convertDateTime.getCertainCulture(txtToDate.Text.Trim()).ToString(),
                                         txtTotalHolydays.Text.Trim(),txtNoOfDays.Text.Trim(),txtNotes.Text.Trim(),ViewState["__EId__"].ToString().ToString(),"0","0",
                                         convertDateTime.getCertainCulture(DateTime.Now.ToString("dd-MM-yyyy")).ToString(),"0",ddlShift.SelectedItem.Value.ToString()};

                    if (SQLOperation.forSaveValue("Leave_Application", getColumns, getValues, sqlDB.connection) == true)
                    {
                        PeocessLeaveDetails();
                        lblMessage.InnerText = "success->Successfully Leave Application Saved";
                        loadLeaveApplication();
                        allClear();
                    }
                }
                else
                {
                    if (!checkSex()) return;

                    string[] getColumns = { "LeaveId", "FromDate", "ToDate", "WeekHolydayNo", "TotalDays", "Remarks", "EID", "IsApproved", "IsProcessessed", "EntryDate", "IsMeternity", "ShiftId", "PregnantDate", "PrasaberaDate" };

                    string[] getValues = {ddlLeaveName.SelectedItem.Value.ToString(),convertDateTime.getCertainCulture(txtFromDate.Text.Trim()).ToString(),convertDateTime.getCertainCulture(txtToDate.Text.Trim()).ToString(),
                                         txtTotalHolydays.Text.Trim(),txtNoOfDays.Text.Trim(),txtNotes.Text.Trim(),ViewState["__EId__"].ToString(),"0","0",
                                         convertDateTime.getCertainCulture(DateTime.Now.ToString("dd-MM-yyyy")).ToString(),"1",ddlShift.SelectedItem.Value.ToString(),
                                         convertDateTime.getCertainCulture(txtPregnantDate.Text.Trim()).ToString(),convertDateTime.getCertainCulture(txtPrasaberaDate.Text.Trim()).ToString(),};

                    if (SQLOperation.forSaveValue("Leave_Application", getColumns, getValues, sqlDB.connection) == true)
                    {
                        PeocessLeaveDetails();
                        lblMessage.InnerText = "success->Successfully Leave Application Saved";
                        loadLeaveApplication();
                        allClear();
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
            }
        }
        bool checkSex()
        {
            try
            {
                sqlDB.fillDataTable("select EGender from EmployeeInfo where EId='" + ViewState["__EId__"].ToString() + "'", dt = new DataTable());
                if (dt.Rows.Count == 0)
                {
                    lblMessage.InnerText = "error->Please set gender or gender of this employee !";
                    return false;
                }
                bool sex = (dt.Rows[0]["EGender"].ToString().Equals("Female")) ? true : false;
                if (!sex)
                {
                    lblMessage.InnerText = "error->This Employee Is Male !";
                    return false;
                }
                else return true;
            }
            catch { return false;}
        }
        protected void btnCalculation_Click(object sender, EventArgs e)
        {
            try
            {
                if (!checkIsCurrectCardNo()) return;
                else
                {
                    string FromDate = txtFromDate.Text.Substring(6, 4) + "-" + txtFromDate.Text.Trim().Substring(3, 2) + "-" + txtFromDate.Text.Trim().Substring(0, 2);
                    string ToDate = txtToDate.Text.Substring(6, 4) + "-" + txtToDate.Text.Trim().Substring(3, 2) + "-" + txtToDate.Text.Trim().Substring(0, 2);
                    sqlDB.fillDataTable("select * from OffdaySettings where OffDate>='" + FromDate + "' AND OffDate <='" + ToDate + "'", dt = new DataTable());
                    //if (dt.Rows.Count > 0)
                    //{
                    txtTotalHolydays.Text = dt.Rows.Count.ToString();
                    sqlDB.fillDataTable("select DateDiff(Day,'" + FromDate + "','" + ToDate + "') as TotalDays", dt = new DataTable());
                    txtNoOfDays.Text = (int.Parse(dt.Rows[0]["TotalDays"].ToString()) + 1).ToString();

                    sqlDB.fillDataTable("select EName From EmployeeInfo where ECardNo=" + txtECardNo.Text.Trim() + "", dt = new DataTable());
                    if (ViewState["yesAlater"].Equals("False")) lblMessage.InnerText = "warning-> Teacher nam is " + dt.Rows[0]["EName"].ToString();

                }
            }
            catch { }
        }
        private bool checkIsCurrectCardNo()
        {
            try
            {
                sqlDB.fillDataTable("select EId from EmployeeInfo where ECardNo=" + txtECardNo.Text.Trim() + " AND ShiftId="+ddlShift.SelectedItem.Value.ToString()+"", dt = new DataTable());
                if (dt.Rows.Count > 0)
                {
                    ViewState["__EId__"] = dt.Rows[0]["EId"].ToString();
                    return true;
                }
                else
                {
                    lblMessage.InnerText = "warning->Sorry This E-CardNo Is Not Valid";
                    return false;
                }
            }
            catch { lblMessage.InnerText = "warning->Sorry This E-CardNo Is Not Valid"; return false; }
        }

        string[] fromDates;
        string[] toDates;
        protected void btnSave_Click(object sender, EventArgs e)
        {
            lblMessage.InnerText = "";

            if (!checkLeaveDaysValidation()) return;
            if (ViewState["__EId__"].ToString().Equals("0")) return;
            if (ViewState["yesAlater"].Equals("True"))
            {
                DataTable dtLeaveDetails = new DataTable();
                fromDates = txtFromDate.Text.Trim().Split('-');
                toDates = txtToDate.Text.Trim().Split('-');
                string DailyStartTimeALT_CloseTime = "00:00:00:00:00:00";
                btnCalculation_Click(sender, e);
                if (!txtFromDate.Text.Trim().Equals(ViewState["OldFromDate"].ToString()) || !txtToDate.Text.Trim().Equals(ViewState["OldToDate"].ToString()))
                {
                    calculationForAlterOperation();
                    checkNotSameDate();
                    for (int i = 0; i < dtNotSameDate.Rows.Count; i++)
                    {
                        updateDailyAttendance(dtNotSameDate.Rows[i]["LeaveDate"].ToString(), "00", "00", "00", "00", "00", "00", DailyStartTimeALT_CloseTime, dtNotSameDate.Rows[i]["Status"].ToString());
                    }

                    SQLOperation.forDelete("temp_NewLeaveDateForAlter", sqlDB.connection);
                    SQLOperation.forDelete("temp_OldLeaveDateForAlter", sqlDB.connection);
                }
                SQLOperation.forDeleteRecordByIdentifier("Leave_LeaveApplication", "LACode", ViewState["__LACode__"].ToString(), sqlDB.connection);
                SQLOperation.forDeleteRecordByIdentifier("Leave_LeaveApplicationDetails", "LACode", ViewState["__LACode__"].ToString(), sqlDB.connection);
            }
            saveLeaveApplication();
            ViewState["yesAlater"] = "False";
        }

        private bool checkLeaveDaysValidation()
        {
            try
            {
                sqlDB.fillDataTable("select LeaveDays,ShortName from leave_configuration where LeaveId=" + ddlLeaveName.SelectedValue.ToString() + "", dt = new DataTable());
                byte getLeaveDays = byte.Parse(dt.Rows[0]["LeaveDays"].ToString());                
                string a = "select Used from v_Leave_LeaveApplicationDetails where EId='" + ViewState["__EId__"].ToString() + "' And ShortName=(select ShortName from leave_configuration where LeaveId=" + ddlLeaveName.SelectedValue.ToString() + ") AND FromYear='" + DateTime.Now.Year + "' ";
                sqlDB.fillDataTable("select Used from v_Leave_LeaveApplicationDetails where EId='" + ViewState["__EId__"].ToString() + "' And ShortName=(select ShortName from leave_configuration where LeaveId=" + ddlLeaveName.SelectedValue.ToString() + ") AND FromYear='" + DateTime.Now.Year + "' ", dt = new DataTable());

                if (dt.Rows.Count + int.Parse(txtNoOfDays.Text.Trim()) > getLeaveDays)
                {
                    lblMessage.InnerText = "error->Already you are spanted " + dt.Rows.Count + " days of  " + ddlLeaveName.SelectedItem.ToString().Substring(0, ddlLeaveName.SelectedItem.ToString().Length - 3) + " of this year /r Total allocated days for " + ddlLeaveName.SelectedItem.ToString().Substring(0, ddlLeaveName.SelectedItem.ToString().Length - 3) + " is " + getLeaveDays + " !";
                    return false;
                }
                else return true;
            }
            catch { return false; }
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
                    cmd.Parameters.AddWithValue("@Status", "New");
                    cmd.ExecuteNonQuery();

                    NewFromDate = NewFromDate.AddDays(1);                  
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
        private void updateDailyAttendance(string attDate, string InHour, string InMin, string InSec, string OutHur, string OutMin, string OutSec, string DailyStartTimeALT_CloseTime, string Status)
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


                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("update DailyAttendanceRecord set InHur='" + InHour + "',InMin='" + InMin + "', " +
                    "InSec='" + InSec + "',OutHur='" + OutHur + "',OutMin='" + OutMin + "',OutSec='" + OutSec + "',AttStatus='" + attStatus + "',StateStatus='" + stateStatus + "', " +
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
                DateTime FromDate = new DateTime(int.Parse(txtFromDate.Text.Trim().Substring(6, 4)), int.Parse(txtFromDate.Text.Trim().Substring(3, 2)), int.Parse(txtFromDate.Text.Trim().Substring(0, 2)));
                DateTime ToDate = new DateTime(int.Parse(txtToDate.Text.Trim().Substring(6, 4)), int.Parse(txtToDate.Text.Trim().Substring(3, 2)), int.Parse(txtToDate.Text.Trim().Substring(0, 2)));
                while (FromDate <= ToDate)
                {
                    string[] getFromDate = FromDate.ToString().Split('/');
                    saveLeaveDetails(dt.Rows[0]["LACode"].ToString(), getFromDate[1] + "-" + getFromDate[0] + "-" + getFromDate[2].Substring(0, 4));
                    FromDate = FromDate.AddDays(1);
                }

            }
            catch { }
        }

        private void saveLeaveDetails(string LACode, string Date)
        {
            try
            {
                string[] getColumns = { "LACode", "EID", "LeaveDate", "Used" };
                string[] getValues = { LACode, ViewState["__EId__"].ToString(), convertDateTime.getCertainCulture(Date).ToString(), "0" };
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
              
                lblMessage.InnerText = "";
               // string a = gvLeaveApplicationList.SelectedRow.DataItemIndex.ToString();
                if (e.CommandName.Equals("Status"))
                {
                    DataTable dtLeaveInfo = new DataTable();
                    sqlDB.fillDataTable("select Ename,ECardNo,LeaveName,Used,Format(LeaveDate,'dd-MM-yyyy') as LeaveDate from v_Leave_LeaveApplicationDetails where LACode=" + gvLeaveApplicationList.DataKeys[int.Parse(e.CommandArgument.ToString())].Value.ToString() + "", dtLeaveInfo);
                    dt = new DataTable();
                    DataRow[] dr = { };
                    try
                    {
                        dr = dtLeaveInfo.Select("Used='true'", null);
                    }
                    catch { }
                    //msg.InnerText = "Leave Status Of " + dtLeaveInfo.Rows[0]["LeaveName"].ToString();
                    lblName.Text = dtLeaveInfo.Rows[0]["EName"].ToString();
                    lblCardNo.Text = dtLeaveInfo.Rows[0]["ECardNo"].ToString();
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
                    ViewState["OldFromDate"] = txtFromDate.Text = gvLeaveApplicationList.Rows[rIndex].Cells[2].Text;
                    ViewState["OldToDate"] = txtToDate.Text = gvLeaveApplicationList.Rows[rIndex].Cells[3].Text;
                    txtNoOfDays.Text = gvLeaveApplicationList.Rows[rIndex].Cells[5].Text;
                    txtTotalHolydays.Text = gvLeaveApplicationList.Rows[rIndex].Cells[4].Text;

                    for (byte b = 1; b < ddlLeaveName.Items.Count; b++)
                    {
                        if (ddlLeaveName.Items[b].Text.Substring(ddlLeaveName.Items[b].Text.Length - 3, 3).Equals(gvLeaveApplicationList.Rows[rIndex].Cells[6].Text)) ddlLeaveName.SelectedIndex = b;
                    }

                    ViewState["yesAlater"] = "True";        
                }
                else if (e.CommandName == "View")
                {
                    ForLeaveReport.generateLeaveApplicationReport(e.CommandArgument.ToString());
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=LeaveApplicationReport');", true);

                }
                else
                {
                    SQLOperation.forDeleteRecordByIdentifier("Leave_Application", "LACode", e.CommandArgument.ToString(), sqlDB.connection);
                    SQLOperation.forDeleteRecordByIdentifier("Leave_Application_Details", "LACode", e.CommandArgument.ToString(), sqlDB.connection);

                }
            }
            catch { }
        }
        protected void gvRejectedList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {

                lblMessage.InnerText = "";               
                if (e.CommandName == "View")
                {
                    ForLeaveReport.RejectedLeaveApplicationReport(e.CommandArgument.ToString());
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=LeaveApplicationReport');", true);

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
                ddlShift.SelectedIndex = 0;
                trPrasaberaDate.Visible = false;
                trPregnantDate.Visible = false;
                txtNotes.Text = "";
                trStatus.Visible = false;
                txtPrasaberaDate.Text = "";
                txtPregnantDate.Text = "";
                ViewState["__EId__"] = "0";
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


        protected void ddlLeaveName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlLeaveName.SelectedItem.Text.Trim().Contains("m/l"))
                {
                    trPrasaberaDate.Visible = true;
                    trPregnantDate.Visible = true;
                }
                else
                {
                    trPrasaberaDate.Visible = false;
                    trPregnantDate.Visible = false;
                }
            }
            catch { }
        }

        void loadRejected_LeaveApplication()  // for display all rejected leave application
        {
            DataTable dtLeaveInfo = new DataTable();

            DataTable dt = new DataTable();
            sqlDB.fillDataTable("select GetDate()-7 as dates", dt);

            string[] getDates = dt.Rows[0]["dates"].ToString().Split('/');
            getDates[0] = getDates[2].Substring(0, 4) + "-" + getDates[0] + "-" + getDates[1];

            
            string strSQL;
            if (ddlShift.SelectedIndex == 0)
                strSQL = "select LACode,LeaveId,convert(varchar(11),FromDate,105) as FromDate,convert (varchar(11),ToDate,105) as ToDate,WeekHolydayNo,TotalDays,EId,ShortName,ECardNo,EName from v_Leave_Application_Log where ApprovedRejected='Rejected'  AND ApprovedDate >='" + getDates[0] + "' AND ApprovedDate<='" + DateTime.Now.ToString("yyyy-MM-dd") + "' Order by ECardNo,FromDate,Lacode desc";
            else strSQL = "select LACode,LeaveId,convert(varchar(11),FromDate,105) as FromDate,convert (varchar(11),ToDate,105) as ToDate,WeekHolydayNo,TotalDays,EId,ShortName,ECardNo,EName from v_Leave_Application_Log where ApprovedRejected='Rejected' AND ShiftId='" + ddlShift.SelectedValue.ToString() + "' AND ApprovedDate >='" + getDates[0] + "' AND ApprovedDate<='" + DateTime.Now.ToString("yyyy-MM-dd") + "' Order by ECardNo,FromDate,Lacode desc";
            sqlDB.fillDataTable(strSQL, dtLeaveInfo);

            gvRejectedList.DataSource = dtLeaveInfo;
            gvRejectedList.DataBind();
        }

        protected void TabContainer1_ActiveTabChanged(object sender, EventArgs e)
        {
            if (TabContainer1.ActiveTabIndex == 1) loadRejected_LeaveApplication();
        }
       
    }
}