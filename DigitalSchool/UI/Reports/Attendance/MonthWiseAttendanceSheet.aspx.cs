using DS.BLL;
using DS.BLL.Admission;
using DS.BLL.Attendace;
using DS.BLL.ControlPanel;
using DS.BLL.GeneralSettings;
using DS.BLL.ManagedBatch;
using DS.BLL.ManagedClass;
using DS.DAL;
using DS.DAL.AdviitDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.Reports.Attendance
{
    public partial class MonthWiseAttendanceSheet : System.Web.UI.Page
    {
        CurrentStdEntry currentstdEntry;
        DataTable dt;
        string Group = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                    if (!IsPostBack)
                    {
                        if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "MonthWiseAttendanceSheet.aspx", "")) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");                    
                        ShiftEntry.GetDropDownList(ddlShiftList);
                        ddlShiftList.Items.Insert(1, new ListItem("All", "All"));
                        ddlShiftList.SelectedValue = "All";

                        BatchEntry.GetDropdownlist(ddlBatch, true);
                        ddlBatch.Items.Insert(1, new ListItem("All", "All"));
                        ddlBatch.SelectedValue = "All";
                        SheetInfoEntry.loadMonths(ddlMonths);
                        txtToDate.Text = txtFromDate.Text = TimeZoneBD.getCurrentTimeBD("dd-MM-yyyy");
                        ShiftEntry.GetDropDownList(ddlShit_D);
                        ddlShit_D.Items.Insert(1, new ListItem("All", "All"));
                        BatchEntry.GetDropdownlist(ddlBatch_D, true);
                        ddlBatch_D.Items.Insert(1, new ListItem("All", "All"));
                       // Classes.commonTask.loadAttendanceSheet(dlSheetName);
                        ddlGroup_D.Items.Insert(0,new ListItem("All","All"));
                        ddlSection_D.Items.Insert(0,new ListItem("All","All"));
                        ddlRollNo_D.Items.Insert(0,new ListItem("All","All"));

                        ddlgroup.Items.Insert(0, new ListItem("All", "All"));
                        ddlSection.Items.Insert(0, new ListItem("All", "All"));
                        ddlRollNo.Items.Insert(0, new ListItem("All", "All"));
                    }
            }
            catch { }
            lblMessage.InnerText = "";
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

            loadAttendanceSheet();
        }

        private void loadAttendanceSheet()
        {
            try
            {
                Session["__AttendanceSheet__"] = dlSheetName.Text;
                DataTable dt = new DataTable();
                sqlDB.fillDataTable("Select CurrentStudentInfo.FullName,CurrentStudentInfo.RollNo, " + Session["__AttendanceSheet__"] 
                + ".* from " + Session["__AttendanceSheet__"] + " ,CurrentStudentInfo where " + Session["__AttendanceSheet__"]
                + ".StudentId=CurrentStudentInfo.StudentId  Order By CurrentStudentInfo.RollNo ASC ", dt);
                
                dt.Columns.Add("T.P");           //Add New Columns Total Present
                dt.Columns.Add("T.A");           //Add New Columns Total Absent
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int totalabsent = 0, totalpresent = 0;
                    for (int x = 0; x < dt.Columns.Count; x++)
                    {
                        if (dt.Rows[i].ItemArray[x].ToString().ToUpper() == "a".ToUpper())
                        {
                            totalabsent++;
                        }
                        else if (dt.Rows[i].ItemArray[x].ToString().ToUpper() == "p".ToUpper())
                        {
                            totalpresent++;
                        }
                    }
                    dt.Rows[i]["T.P"] = totalpresent;     //Add New Row Total Present
                    dt.Rows[i]["T.A"] = totalabsent;      //Add New Row Total Absent
                }
                dt.Columns.Remove("StudentId");
                Session["__AttendanceSheetReport__"] = dt;
                gvAttendanceSheet.DataSource = dt;
                gvAttendanceSheet.DataBind();
            }
            catch { }
        }

        private void loadMonthWiseAttendanceSheet()
        {
            try
            {
                string sqlCmd = "select * from " + dlSheetName.Text + "  ";
                DataTable dt = new DataTable();
                sqlDB.fillDataTable(sqlCmd, dt);

                int totalRows = dt.Rows.Count;
                string divInfo = "";

                if (totalRows == 0)
                {
                    divInfo = "<div class='noData'>No Student Attendance available</div>";
                    divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                    divMonthWiseAttendaceSheet.Controls.Add(new LiteralControl(divInfo));
                    btnPrintPreview.Visible = false;
                    return;
                }
                divInfo = " <table id='tblStudentInfo' class='display'  style='width:100%;margin:0px auto;' > ";
                divInfo += "<thead>";
                divInfo += "<tr>";
                divInfo += "<th class='numeric' style='width:50px;'>SL</th>";
                divInfo += "<th style='width:260px'>Full Name</th>";
                divInfo += "<th class='numeric' style='width:120px'>Roll</th>";
                divInfo += "<th style='width:260px'>Guardian Name</th>";
                divInfo += "<th>Guardian Relation</th>";
                divInfo += "<th class='numeric'>Guardian Mobile</th>";
                divInfo += "<th>Guardian Address</th>";
                divInfo += "</tr>";
                divInfo += "</thead>";
                divInfo += "<tbody>";
                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    int sl = x + 1;
                    divInfo += "<tr >";
                    divInfo += "<td class='numeric'>" + sl + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["FullName"].ToString() + "</td>";
                    divInfo += "<td class='numeric'>" + dt.Rows[x]["RollNo"].ToString() + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["GuardianName"].ToString() + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["GuardianRelation"].ToString() + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["GuardianMobileNo"].ToString() + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["GuardianAddress"].ToString() + "</td>";
                }
                divInfo += "</tbody>";
                divInfo += "<tfoot>";
                divInfo += "</table>";
                divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                divMonthWiseAttendaceSheet.Controls.Add(new LiteralControl(divInfo));
            }
            catch { }
        }

        protected void btnPrintPreview_Click(object sender, EventArgs e)
        {
            //-----------Validation--------------
            if (ddlShiftList.SelectedValue == "0")
            { lblMessage.InnerText = "warning-> Please select a Shift!"; return; }
            if (ddlBatch.SelectedValue == "0") { lblMessage.InnerText = "warning-> Please select a Batch!"; return; }
            if (tdGrpMName.Visible == true && ddlgroup.SelectedValue == "0") { lblMessage.InnerText = "warning-> Please select a Group!"; return; }
            if (ddlSection.SelectedValue == "0") { lblMessage.InnerText = "warning-> Please select a Section!"; return; }            
            //--------------------------------------------------  
            if (rblRepotMontly.SelectedValue == "0") // For Monthly Attendance Sheet Report
            {
                
                if (ddlMonths.SelectedValue == "0") { lblMessage.InnerText = "warning-> Please select a Month!"; return; }               
                dt = new DataTable();
                dt = ForAttendanceReport.returntDataTableForAttSheet(ddlShiftList.SelectedValue, ddlBatch.SelectedValue, ddlgroup.SelectedValue, ddlSection.SelectedValue, ddlMonths.SelectedValue, ddlRollNo.SelectedValue);
                if (dt == null || dt.Rows.Count < 1)
                {
                    lblMessage.InnerText = "warning-> Any attendance Record are not founded!";
                    return;
                }
                Group = (ddlgroup.SelectedValue == "0") ? "" : "Group : " + ddlgroup.SelectedItem.Text + ",";
                Session["__AttendanceSheet__"] = dt;               
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=AttendanceSheet-" + ddlShiftList.SelectedItem.Text + "-" + ddlBatch.SelectedItem.Text + "-" + Group + "-" + ddlSection.SelectedItem.Text + "-" + ddlMonths.SelectedItem.Text + "');", true);
                //Open New Tab for Sever side code 
            }
            else if (rblRepotMontly.SelectedValue == "1")
            {
                if (txtFdate.Text.Length == 0) { lblMessage.InnerText = "warning->Please select from date"; txtFdate.Focus(); return; }
                if (txtTdate.Text.Length == 0) { lblMessage.InnerText = "warning->Please select to date"; txtTdate.Focus(); return; }
                dt = new DataTable();               
                dt = ForAttendanceReport.return_dt_for_AttSummary(txtFdate.Text.Trim(),txtTdate.Text.Trim(),ddlBatch.SelectedValue,ddlShiftList.SelectedValue,ddlgroup.SelectedValue,ddlSection.SelectedValue,ddlRollNo.SelectedValue);
                if (dt == null || dt.Rows.Count < 1) 
                {
                    lblMessage.InnerText = "warning-> Any attendance Record are not founded!";
                    return;
                }
                Group = (ddlgroup.SelectedValue == "0") ? "" : "Group : " + ddlgroup.SelectedItem.Text + ",";
                Session["__AttendanceSummaryReport__"] = dt;
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=AttendanceSummaryReport-" + ddlShiftList.SelectedItem.Text + "-" + ddlBatch.SelectedItem.Text + "-" + Group + "-"+ ddlSection.SelectedItem.Text + "-" + txtFdate.Text.Trim() + "-" + txtFdate.Text.Trim() + "');", true);
            }
            else             
            {
                if (txtFdate.Text.Length == 0) { lblMessage.InnerText = "warning->Please select from date"; txtFdate.Focus(); return; }
                if (txtTdate.Text.Length == 0) { lblMessage.InnerText = "warning->Please select to date"; txtTdate.Focus(); return; }              
                
                dt = new DataTable();
                dt = ForAttendanceReport.return_dt_AbsentDetails(txtFdate.Text,txtTdate.Text,ddlShiftList.SelectedValue,ddlgroup.SelectedValue,ddlSection.SelectedValue,ddlRollNo.SelectedValue);
                if (dt == null || dt.Rows.Count < 1) 
                {
                    lblMessage.InnerText = "warning->  Absent records are not available";
                    return;
                }                
                Session["__AbsentDetails__"] = dt;
                //...............
                Group = (ddlgroup.SelectedValue == "0") ? "No Group" : ddlgroup.SelectedItem.Text;
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=IndivisualAbsentDetails-" + ddlBatch.SelectedItem.Text + "-" + ddlSection.SelectedItem.Text + "-" + Group + "-" + txtFdate.Text.Trim() + "-"+txtTdate.Text.Trim()+"-"+ddlShiftList.SelectedItem.Text+"');", true);  //Open New Tab for Sever side code
            }
        }
       
        protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load();", true);
            ddlSection.Items.Clear();
            if (ddlBatch.SelectedValue == "All")
            {
                ddlgroup.Items.Clear();
                ddlgroup.Items.Insert(0, new ListItem("All", "All"));
                ddlSection.Items.Clear();
                ddlSection.Items.Insert(0, new ListItem("All", "All"));
                ddlRollNo.Items.Clear();
                ddlRollNo.Items.Insert(0, new ListItem("All", "All"));
                return;
            }
            BatchEntry.loadGroupByBatchId(ddlgroup, ddlBatch.SelectedValue.ToString());

            if (ddlgroup.Items.Count == 1)
            {
                tdGrpMName.Visible = false;
                //tdGrpMtitle.Visible = false;
               // ClassSectionEntry.GetSectionListByBatchId(ddlSection, ddlBatch.SelectedValue.ToString());
                ClassSectionEntry.GetSectionListByBatchId_ClsGrpId(ddlSection, ddlBatch.SelectedValue.ToString(), ddlgroup.SelectedItem.Value);
                ddlSection.Items.RemoveAt(0);
                ddlSection.Items.Insert(0,new ListItem("All","All"));
            }
            else
            {
                tdGrpMName.Visible = true;
                //tdGrpMtitle.Visible = true;
                ddlgroup.Items.Insert(1, new ListItem("All", "All"));
                ddlSection.Items.Insert(0, new ListItem("All", "All"));
            }
        }
        protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSection.SelectedValue == "0")
            {
                ddlRollNo.Items.Clear();
                ddlRollNo.Items.Insert(0, new ListItem("All", "All"));
            }
            else loadrollno();
        }
        protected void ddlgroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load();", true);
            ddlSection.Items.Clear();
            ClassSectionEntry.GetSectionListByBatchId_ClsGrpId(ddlSection, ddlBatch.SelectedValue.ToString(), ddlgroup.SelectedItem.Value);
            ddlSection.Items.RemoveAt(0);
            ddlSection.Items.Insert(0, new ListItem("All", "All"));
        }
        private void loadrollno()
        {
            if (ddlShiftList.SelectedValue == "0")
            { lblMessage.InnerText = "warning-> Please select a Shift!"; ddlShiftList.Focus(); return; }
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load();", true);
            if (currentstdEntry == null)
            {
                currentstdEntry = new CurrentStdEntry();
            }
            string condition = "";
            condition = currentstdEntry.GetSearchCondition(ddlShiftList.SelectedValue, ddlBatch.SelectedValue, ddlgroup.SelectedValue, ddlSection.SelectedValue);
            currentstdEntry.GetRollNoCondition(ddlRollNo, condition);
            ddlRollNo.Items.RemoveAt(0);
            if (ddlRollNo.Items.Count < 1)
            {
                lblMessage.InnerText = "warning->Students are not available!";
                return;
            }
            if (ddlRollNo.Items.Count > 1)
                ddlRollNo.Items.Insert(0, new ListItem("All", "All"));
        }
        private void LoadRollNo()
        {
            if (ddlShiftList.SelectedValue == "0")
            { lblMessage.InnerText = "warning-> Please select a Shift!"; return; }
            if (ddlBatch.SelectedValue == "0") { lblMessage.InnerText = "warning-> Please select a Batch!"; return; }
            if (tdGrpMName.Visible== true && ddlgroup.SelectedValue == "0") { lblMessage.InnerText = "warning-> Please select a Group!"; return; }
            if (ddlSection.SelectedValue == "0") { lblMessage.InnerText = "warning-> Please select a Section!"; return; }
            ddlMonths.Items.Clear();
            AbsentStudents abS = new AbsentStudents();
            abS.LoadBatchWaysRollNo(ddlMonths, ddlBatch.SelectedValue, ddlgroup.SelectedValue, ddlSection.SelectedValue, ddlShiftList.SelectedValue);
            ddlMonths.Items.RemoveAt(0);
            if (ddlMonths.Items.Count < 1) 
            {
                lblMessage.InnerText = "warning->Students are not available!";                
                return;
            }
            if (ddlMonths.Items.Count > 1)
            ddlMonths.Items.Insert(0, new ListItem("All", "0"));
        }

        protected void rblRepotMontly_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load();", true);
            if (rblRepotMontly.SelectedValue == "0")
            {
                tblDateRange.Visible = false;
                tdMonth.Visible = true;
                tdMonth.InnerText = "Month";
                SheetInfoEntry.loadMonths(ddlMonths);
                ddlMonths.Visible = true;
            }
            else if (rblRepotMontly.SelectedValue == "1")
            {
                tblDateRange.Visible = true;
                tdMonth.Visible = false;
                ddlMonths.Visible = false;
            }
            else
            {
                ddlMonths.Items.Clear();
                tblDateRange.Visible = true;
                tdMonth.Visible = false;
                //tdMonth.InnerText = " Roll No";
                //if (ddlSection.SelectedIndex!=-1)
                //{ if(ddlSection.SelectedValue != "0")
                //  LoadRollNo();
                //}
                ddlMonths.Visible = false;
            }
        }

        protected void ddlBatch_D_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load();", true);
            ddlSection_D.Items.Clear();
            if(ddlBatch_D.SelectedValue=="All")
            {
                ddlGroup_D.Items.Clear();
                ddlGroup_D.Items.Insert(0,new ListItem("All","All"));
                ddlSection_D.Items.Clear();
                ddlSection_D.Items.Insert(0, new ListItem("All","All"));
                ddlRollNo_D.Items.Clear();
                ddlRollNo_D.Items.Insert(0,new ListItem("All","All"));
                return;
            }
            BatchEntry.loadGroupByBatchId(ddlGroup_D, ddlBatch_D.SelectedValue.ToString());

            if (ddlGroup_D.Items.Count == 1)
            {
                tdGrpName.Visible = false;
                //tdGrpTitle.Visible = false;
                ClassSectionEntry.GetSectionListByBatchId_ClsGrpId(ddlSection_D, ddlBatch_D.SelectedValue.ToString(), ddlGroup_D.SelectedItem.Value);
                //ddlSection_D.Items.Insert(1,new ListItem("All","All"));
                //ClassSectionEntry.GetSectionListByBatchId(ddlSection_D, ddlBatch_D.SelectedValue.ToString());
                ddlSection_D.Items.RemoveAt(0);
                ddlSection_D.Items.Insert(0, new ListItem("All", "All"));
            }
            else
            {
                tdGrpName.Visible = true;
                ddlGroup_D.Items.Insert(1,new ListItem("All","All"));
                ddlSection_D.Items.Insert(0, new ListItem("All", "All"));
                //tdGrpTitle.Visible = true;
            }
        }

        protected void ddlGroup_D_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load();", true);
            ddlSection_D.Items.Clear();
            ClassSectionEntry.GetSectionListByBatchId_ClsGrpId(ddlSection_D, ddlBatch_D.SelectedValue.ToString(), ddlGroup_D.SelectedItem.Value);
            ddlSection_D.Items.RemoveAt(0);
            ddlSection_D.Items.Insert(0,new ListItem("All","All"));
        }
        private void LoadDailyAttendanceSummaryReport() 
        {
            string sqlCmd = "";
            string ReportTitel = "";
            string ReportType = "";
            DataTable dt = new DataTable();
            string shift = (ddlShit_D.SelectedValue == "All") ? "" : " and ShiftId=" + ddlShit_D.SelectedValue + "";
            sqlCmd = "with  tbl as("+
"select ShiftId,ClassID,ClsGrpId,ClsSecId,attstatus, RollNo from  v_DailyStudentAttendanceRecord where AttStatus in('a','l') AND AttDate='"+txtFromDate.Text+"' "+shift+" ),"+
"al as(Select Main.ShiftID, Main.attstatus,Main.ClassID, Main.ClsGrpId,Main.ClsSecId,Left(Main.AbsentList,Len(Main.AbsentList)-1) As AbsentList,"+
           " Left(Main.LateList,Len(Main.LateList)-1) As LateList"+
" From(Select distinct ST2.attstatus, ST2.shiftId,ST2.ClassID,ST2.ClsGrpId,ST2.ClsSecId,"+
            " (Select convert(varchar, ST1.RollNo) + ',' AS [text()] From tbl ST1"+
               "  Where ST1.attstatus = 'a' and ST1.ShiftId=ST2.ShiftId and ST1.ClassID = ST2.ClassID and ST1.ClsGrpId=ST2.ClsGrpId and ST1.ClsSecId=ST2.ClsSecId "+
               "  ORDER BY ST1.attstatus"+
               "      For XML PATH ('')"+
            " ) [AbsentList],"+
			" ( Select convert(varchar, ST1.RollNo) + ',' AS [text()] From tbl ST1"+
             "    Where ST1.attstatus ='l' and ST1.ShiftId=ST2.ShiftId and ST1.ClassID = ST2.ClassID and ST1.ClsGrpId=ST2.ClsGrpId and ST1.ClsSecId=ST2.ClsSecId "+
             "    ORDER BY ST1.attstatus For XML PATH ('')"+
            " ) [LateList] From tbl ST2 ) [Main]),"+
            " ar as(select ShiftId,ClassID,ClsGrpId,ClsSecId,shift,ClassName,GroupName,SectionName, count(studentid) as TTL, sum( case when AttStatus='a'"+
            " then 1 else 0 end ) as AB,sum( case when AttStatus='p' then 1 else 0 end) as PR, sum(case when AttStatus='l' then 1 else 0 end) as LT,"+
            " sum(case when AttStatus='lv' then 1 else 0 end) as LV   from v_DailyStudentAttendanceRecord where AttStatus<>'' AND AttDate='" + txtFromDate.Text + "' " + shift + "  " +
           "  group by ShiftId,ClassID,ClsGrpId,ClsSecId,shift,ClassName,GroupName,SectionName)"+
" select * from ar inner join al on ar.ShiftId=al.ShiftId and ar.ClassID=al.ClassID and ar.ClsGrpId=al.ClsGrpId and ar.ClsSecId=al.ClsSecId";
            
            dt = CRUD.ReturnTableNull(sqlCmd);
            if (dt==null || dt.Rows.Count < 1)
            {
                lblMessage.InnerText = "warning-> Any " + rblReportType.SelectedItem.Text + " record are not founded"; return;
            }
            Session["__DailyAttendanceSummary__"] = dt;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=StudentDailyAttendanceSummary-"+ txtToDate.Text.Replace('-','/') + "');", true);
           
        }
        private void LoadDailyAttendanceReportData()
        {
            string sqlCmd = "";
            string ReportTitel = "";
            string ReportType = "";
            DataTable dt = new DataTable();
            string condition = "";
            if (currentstdEntry == null)
            {
                currentstdEntry = new CurrentStdEntry();
            }
            condition = currentstdEntry.GetSearchCondition2(ddlShit_D.SelectedValue, ddlBatch_D.SelectedValue, ddlGroup_D.SelectedValue, ddlSection_D.SelectedValue);
            if (rblReportType.SelectedIndex == 0) // Daily Attendance Status
            {
                
                
                if(ddlRollNo_D.Text=="All")
                {
                    if(condition=="")
                    {
                        condition = "Where Convert(datetime,AttDate,105) between Convert(datetime,'" + txtFromDate.Text + "',105) AND Convert(datetime,'" + txtToDate.Text + "',105)";
                    }
                    else
                    {
                        condition += " and Convert(datetime,AttDate,105) between Convert(datetime,'" + txtFromDate.Text + "',105) AND Convert(datetime,'" + txtToDate.Text + "',105)";
                    }
                    sqlCmd = "select FullName,RollNo,AttStatus,format(Attdate,'dd-MM-yyyy') as StateStatus,inHur,InMin,FathersMobile,MothersMoible from v_DailyAttendanceRecordForReport  " + condition + " order by convert(int,rollNo)";
                }
                    
                else
                {
                    if (condition == "")
                    {
                        condition = "Where Convert(datetime,AttDate,105) between Convert(datetime,'" + txtFromDate.Text + "',105) AND Convert(datetime,'" + txtToDate.Text + "',105) and StudentId='" + ddlRollNo_D.SelectedValue + "'";
                    }
                    else
                    {
                        condition += " and Convert(datetime,AttDate,105) between Convert(datetime,'" + txtFromDate.Text + "',105) AND Convert(datetime,'" + txtToDate.Text + "',105) and StudentId='" + ddlRollNo_D.SelectedValue + "'";
                    }
                    sqlCmd = "select StudentId, FullName,RollNo,AttStatus,StateStatus,inHur,InMin,ShiftId,ShiftName,ClsSecId,SectionName,ClsGrpId,GroupName,BatchId,BatchName,FORMAT(AttDate,'MM-yyyy') as Month ,FORMAT(AttDate,'dd-MM-yyyy') as AttDate from v_DailyAttendanceRecordForReport " + condition + " order by convert(int,rollNo)";
                }
                    
                ReportTitel = "Daily Attendance Status";
                ReportType = "Status";
            }
            else if (rblReportType.SelectedIndex == 1) // Daily Present status
            {
                if (ddlRollNo_D.Text == "All")
                {
                     if(condition=="")
                    {
                        condition = "Where Convert(datetime,AttDate,105) between Convert(datetime,'" + txtFromDate.Text + "',105) AND Convert(datetime,'" + txtToDate.Text + "',105) and AttStatus='p'";
                    }
                    else
                    {
                        condition += " and Convert(datetime,AttDate,105) between Convert(datetime,'" + txtFromDate.Text + "',105) AND Convert(datetime,'" + txtToDate.Text + "',105) and AttStatus='p'";
                    }
                     sqlCmd = "select FullName,RollNo,AttStatus, format(Attdate,'dd-MM-yyyy') as StateStatus,inHur,InMin,FathersMobile,MothersMoible from v_DailyAttendanceRecordForReport " + condition + " order by convert(int,rollNo)";
                }             
                    
                else
                {
                    if (condition == "")
                    {
                        condition = "Where Convert(datetime,AttDate,105) between Convert(datetime,'" + txtFromDate.Text + "',105) AND Convert(datetime,'" + txtToDate.Text + "',105) and StudentId='" + ddlRollNo_D.SelectedValue + "' and AttStatus='p'";
                    }
                    else
                    {
                        condition += " and Convert(datetime,AttDate,105) between Convert(datetime,'" + txtFromDate.Text + "',105) AND Convert(datetime,'" + txtToDate.Text + "',105) and StudentId='" + ddlRollNo_D.SelectedValue + "' and AttStatus='p'";
                    }
                    sqlCmd = "select StudentId, FullName,RollNo,AttStatus,StateStatus,inHur,InMin,ShiftId,ShiftName,ClsSecId,SectionName,ClsGrpId,GroupName,BatchId,BatchName,FORMAT(AttDate,'MM-yyyy') as Month ,FORMAT(AttDate,'dd-MM-yyyy') as AttDate,inHur,InMin from v_DailyAttendanceRecordForReport " + condition + " order by convert(int,rollNo)";
                }
                   
                ReportTitel = "Daily Attendance Status";
                ReportType = "Status";
            }
            else if (rblReportType.SelectedIndex == 2) // Daily Absent Staust
            {
                if (ddlRollNo_D.Text == "All")
                {
                    if (condition == "")
                    {
                        condition = "Where Convert(datetime,AttDate,105) between Convert(datetime,'" + txtFromDate.Text + "',105) AND Convert(datetime,'" + txtToDate.Text + "',105) and AttStatus='a'";
                    }
                    else
                    {
                        condition += " and Convert(datetime,AttDate,105) between Convert(datetime,'" + txtFromDate.Text + "',105) AND Convert(datetime,'" + txtToDate.Text + "',105) and AttStatus='a'";
                    }
                    sqlCmd = "select FullName,RollNo,AttStatus,format(Attdate,'dd-MM-yyyy') as StateStatus,inHur,InMin,FathersMobile,MothersMoible from v_DailyAttendanceRecordForReport " + condition + " order by convert(int,rollNo)";
                }
                else
                {
                    if (condition == "")
                    {
                        condition = "Where Convert(datetime,AttDate,105) between Convert(datetime,'" + txtFromDate.Text + "',105) AND Convert(datetime,'" + txtToDate.Text + "',105) and StudentId='" + ddlRollNo_D.SelectedValue + "' and AttStatus='a'";
                    }
                    else
                    {
                        condition += " and Convert(datetime,AttDate,105) between Convert(datetime,'" + txtFromDate.Text + "',105) AND Convert(datetime,'" + txtToDate.Text + "',105) and StudentId='" + ddlRollNo_D.SelectedValue + "' and AttStatus='a'";
                    }
                    sqlCmd = "select StudentId, FullName,RollNo,AttStatus,StateStatus,inHur,InMin,ShiftId,ShiftName,ClsSecId,SectionName,ClsGrpId,GroupName,BatchId,BatchName,FORMAT(AttDate,'MM-yyyy') as Month ,FORMAT(AttDate,'dd-MM-yyyy') as AttDate,inHur,InMin from v_DailyAttendanceRecordForReport " + condition + " order by convert(int,rollNo)";
                }
                ReportTitel = "Daily Attendance Status";
                ReportType = "Status";
            }
            else
            {
                if (ddlRollNo_D.Text == "All")
                {
                    if (condition == "")
                    {
                        condition = "Where Convert(datetime,AttDate,105) between Convert(datetime,'" + txtFromDate.Text + "',105) AND Convert(datetime,'" + txtToDate.Text + "',105)";
                    }
                    else
                    {
                        condition += " and Convert(datetime,AttDate,105) between Convert(datetime,'" + txtFromDate.Text + "',105) AND Convert(datetime,'" + txtToDate.Text + "',105)";
                    }
                    sqlCmd = "select FullName,RollNo,inHur,InMin ,OutHur,OutMin,format(Attdate,'dd-MM-yyyy') as StateStatus from v_DailyAttendanceRecordForReport " + condition + " order by convert(int,rollNo)";
                }
                   
                else
                {
                    if (condition == "")
                    {
                        condition = "Where Convert(datetime,AttDate,105) between Convert(datetime,'" + txtFromDate.Text + "',105) AND Convert(datetime,'" + txtToDate.Text + "',105) and StudentId='" + ddlRollNo_D.SelectedValue + "'";
                    }
                    else
                    {
                        condition += " and Convert(datetime,AttDate,105) between Convert(datetime,'" + txtFromDate.Text + "',105) AND Convert(datetime,'" + txtToDate.Text + "',105) and StudentId='" + ddlRollNo_D.SelectedValue + "'";
                    }
                    sqlCmd = "select StudentId, FullName,RollNo,AttStatus,StateStatus,inHur,InMin,OutHur,OutMin,ShiftId,ShiftName,ClsSecId,SectionName,ClsGrpId,GroupName,BatchId,BatchName,FORMAT(AttDate,'MM-yyyy') as Month,FORMAT(AttDate,'dd-MM-yyyy') as AttDate from v_DailyAttendanceRecordForReport  " + condition + " order by convert(int,rollNo)";
                }
                    
                ReportTitel = "Daily Log In Out Time";
                ReportType = "InOut";
            }
            //sqlDB.fillDataTable(sqlCmd, dt = new DataTable());
            dt = CRUD.ReturnTableNull(sqlCmd);
            if (dt.Rows.Count < 1)
            {
                lblMessage.InnerText = "warning-> Any "+rblReportType.SelectedItem.Text+" record are not founded"; return;
            }
            Session["__DailyAttendance__"] = dt;
            if (ddlRollNo_D.SelectedItem.Text == "All")
            {
                string GroupName = (ddlGroup_D.SelectedValue == "0") ? "" : " Group : " + ddlGroup_D.SelectedItem.Text + ", ";
                string DateRange = (txtFromDate.Text == txtToDate.Text) ? txtFromDate.Text : txtFromDate.Text + " To " + txtToDate.Text;
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=DailyAttendance-" + ddlShit_D.SelectedItem.Text + "-" + ddlBatch_D.SelectedItem.Text + "-" + GroupName + "-" + ddlSection_D.SelectedItem.Text + "-" + DateRange.Replace('-', '/') + "-" + ReportTitel + "-" + ReportType + "');", true);
            }
            else 
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=StudentDailyAttendance-" +ddlShit_D.SelectedItem.Text + "-" +ddlBatch_D.SelectedItem.Text + "-" +ddlGroup_D.SelectedItem.Text + "-" + ddlSection_D.SelectedItem.Text + "-" + txtFromDate.Text + "-" + ReportTitel + "-" + ReportType + "-" + txtToDate.Text + "');", true);
            }
        }

        protected void btnPrint_D_Click(object sender, EventArgs e)
        {            
            if (ddlShit_D.SelectedValue == "0")
            { lblMessage.InnerText = "warning-> Please select a Shift!"; ddlShit_D.Focus(); return; }
            if (ddlBatch_D.SelectedValue == "0") { lblMessage.InnerText = "warning-> Please select a Batch!"; ddlBatch_D.Focus(); return; }
            //if (tdGrpName.Visible == true && ddlGroup_D.SelectedValue == "0") { lblMessage.InnerText = "warning-> Please select a Group!"; ddlGroup_D.Focus(); return; }
            //if (ddlSection_D.SelectedValue == "0") { lblMessage.InnerText = "warning-> Please select a Section!"; ddlSection_D.Focus(); return; }
            //if (ddlSection_D.SelectedValue == "0") { lblMessage.InnerText = "warning-> Please select a Section!"; ddlSection_D.Focus(); return; }   
            if (rblReportType.SelectedIndex == 3)
                LoadDailyAttendanceSummaryReport();
            else
            LoadDailyAttendanceReportData();
        }

        protected void ddlSection_D_SelectedIndexChanged(object sender, EventArgs e)
        {
           // ForAttendanceReport.GetRollNoBySection(ddlRollNo_D, ddlShit_D.SelectedValue, ddlBatch_D.SelectedValue, ddlGroup_D.SelectedValue, ddlSection_D.SelectedValue);
            if(ddlSection_D.SelectedValue=="0")
            {
                ddlRollNo_D.Items.Clear();
                ddlRollNo_D.Items.Insert(0,new ListItem("All","All"));
            }
            else loadrollno_D();
        }
        private void loadrollno_D() 
        {
            if (ddlShit_D.SelectedValue == "0")
            { lblMessage.InnerText = "warning-> Please select a Shift!"; ddlShit_D.Focus(); return; }
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load();", true);
            if (currentstdEntry == null)
            {
                currentstdEntry = new CurrentStdEntry();
            }
            string condition = "";
            condition = currentstdEntry.GetSearchCondition(ddlShit_D.SelectedValue, ddlBatch_D.SelectedValue, ddlGroup_D.SelectedValue, ddlSection_D.SelectedValue);
            currentstdEntry.GetRollNoCondition(ddlRollNo_D,condition);
            ddlRollNo_D.Items.RemoveAt(0);
            if (ddlRollNo_D.Items.Count < 1)
            {
                lblMessage.InnerText = "warning->Students are not available!";
                return;
            }
            if (ddlRollNo_D.Items.Count > 1)
                ddlRollNo_D.Items.Insert(0, new ListItem("All", "All"));
        }

        protected void ddlShiftList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSection.SelectedIndex > 0)
                loadrollno();
       
        }

        protected void ddlShit_D_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSection_D.SelectedIndex>0)
                loadrollno_D();
               // ForAttendanceReport.GetRollNoBySection(ddlRollNo_D, ddlShit_D.SelectedValue, ddlBatch_D.SelectedValue, ddlGroup_D.SelectedValue, ddlSection_D.SelectedValue);
         
            
        }
    }
}