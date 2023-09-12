using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DS.DAL.AdviitDAL;
using DS.DAL.ComplexScripting;
using System.Globalization;
using DS.BLL.GeneralSettings;
using DS.BLL.ManagedBatch;
using DS.BLL.Attendace;
using DS.BLL.ManagedClass;
using DS.DAL;
using DS.BLL.ControlPanel;

namespace DS.UI.Academics.Attendance.Student.Manually
{
    public partial class AbsentDetails : System.Web.UI.Page
    {        
        string sqlCmd = "";
        string ReportTitel = "";
        string ReportType = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                    if (!IsPostBack)
                    {
                        ShiftEntry.GetDropDownList(ddlShiftList);
                        BatchEntry.GetDropdownlist(ddlBatch, true);
                        SheetInfoEntry.loadMonths(ddlMonths);
                        btnPrintPreview.Enabled = false;
                        btnPrintPreview.CssClass = "";

                        if (!PrivilegeOperation.SetPrivilegeControl(Session["__UserTypeId__"].ToString(), "AbsentDetails.aspx", btnTodayAttendanceList, btnTodayPresentList, btnTodayAbsentList, btnPrintPreview, btnSearch)) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                    }
            }
            catch { }
            lblSectionDiv.InnerText = "";
            lblMessage.InnerText = "";
        }

        protected void dlBatchName_SelectedIndexChanged(object sender, EventArgs e)
        {
            //loadSectionClayseWise();
        }

        //private void loadSectionClayseWise()
        //{
        //    try
        //    {

        //        DataTable dt;
        //        SQLOperation.selectBySetCommandInDatatable("Select ClassOrder From Classes where ClassName='" + new String(dlBatchName.Text.Where(Char.IsLetter).ToArray()) + "'", dt = new DataTable(), sqlDB.connection);
        //        if ((dt.Rows[0]["ClassOrder"].ToString().Equals("9") || (dt.Rows[0]["ClassOrder"].ToString().Equals("10"))))
        //        {

        //            ddlSection.Items.Clear();
        //            ddlSection.Items.Add("...Select...");
        //            ddlSection.Items.Add("Science");
        //            ddlSection.Items.Add("Commerce");
        //            ddlSection.Items.Add("Arts");
        //            ddlSection.SelectedIndex = ddlSection.Items.Count - ddlSection.Items.Count;
        //        }
        //        else
        //        {
        //            ddlSection.Items.Clear();
        //            sqlDB.loadDropDownList("Select  SectionName from Sections where SectionName<>'Science' AND SectionName<>'Commerce' AND SectionName<>'Arts' Order by SectionName", ddlSection);
        //            ddlSection.Items.Add("...Select...");
        //            ddlSection.SelectedIndex = ddlSection.Items.Count - 1;
        //        }
        //    }
        //    catch { }
        //}

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //-----------Validation--------------
            if (ddlShiftList.SelectedValue == "0")
            { lblMessage.InnerText = "warning-> Please select a Shift!"; return; }
            if (ddlBatch.SelectedValue == "0") { lblMessage.InnerText = "warning-> Please select a Batch!"; return; }
            if (ddlgroup.Enabled == true && ddlgroup.SelectedValue == "0") { lblMessage.InnerText = "warning-> Please select a Group!"; return; }
            if (ddlSection.SelectedValue == "0") { lblMessage.InnerText = "warning-> Please select a Section!"; return; }
            if (ddlMonths.SelectedValue == "0") { lblMessage.InnerText = "warning-> Please select a Month!"; return; }  
            //--------------------------------------------------
            gvAttList.Visible = false;
            DataTable dt;
            dt = new DataTable();
            dt = SheetInfoEntry.loadAttendanceSheetByMonthYear(ddlMonths.SelectedItem.Value,ddlBatch.SelectedValue,ddlShiftList.SelectedValue,ddlSection.SelectedValue,ddlgroup.SelectedValue,"");
            if (dt==null)
            {
                btnPrintPreview.Enabled = false;
                btnPrintPreview.CssClass = "";
                lblSectionDiv.InnerText = "Any  attendance record are not available in " + ddlMonths.SelectedItem.Text + "";
                return;
            }          
                btnPrintPreview.Enabled = true;
                btnPrintPreview.CssClass = "btn btn-primary litleMargin";
           
            loadAttendanceSheet(dt);
            Session["__AttendanceSheet__"] = dt;
            ViewState["__Report__"] = "MonthlyAttSheet";
            lblSectionDiv.InnerText = "Attendance sheet of "+ddlMonths.SelectedItem.Text+"";
            
        }
        private void LoadDailyAttendanceReportData(string reportType)
        {
            //-----------Validation--------------
            if (ddlShiftList.SelectedValue == "0")
            { lblMessage.InnerText = "warning-> Please select a Shift!"; return; }
            if (ddlBatch.SelectedValue == "0") { lblMessage.InnerText = "warning-> Please select a Batch!"; return; }
            if (ddlgroup.Enabled == true && ddlgroup.SelectedValue == "0") { lblMessage.InnerText = "warning-> Please select a Group!"; return; }
            if (ddlSection.SelectedValue == "0") { lblMessage.InnerText = "warning-> Please select a Section!"; return; }           
            //--------------------------------------------------
            divMonthWiseAttendaceSheet.InnerHtml = "";
            DataTable dt = new DataTable();
            if (reportType=="attendance") // Daily Attendance Status
            {
                sqlCmd = "select FullName,RollNo,AttStatus,StateStatus,inHur,InMin from v_DailyAttendanceRecordForReport where ShiftId='" + ddlShiftList.SelectedValue + "'and BatchId='" + ddlBatch.SelectedValue + "' and ClsGrpId='" + ddlgroup.SelectedValue + "' and ClsSecId='" + ddlSection.SelectedValue + "' and FORMAT(Attdate,'dd-MM-yyyy')='"+DateTime.Now.ToString("dd-MM-yyyy")+"'";
                ReportTitel = "Daily Attendance Status";
                ReportType = "Status";
            }
            else if (reportType=="present") // Daily Present status
            {
                sqlCmd = "select FullName,RollNo,AttStatus from v_DailyAttendanceRecordForReport where AttStatus='p' and  ShiftId='" + ddlShiftList.SelectedValue + "'and BatchId='" + ddlBatch.SelectedValue + "' and ClsGrpId='" + ddlgroup.SelectedValue + "' and ClsSecId='" + ddlSection.SelectedValue + "' and FORMAT(Attdate,'dd-MM-yyyy')='" + DateTime.Now.ToString("dd-MM-yyyy") + "'";
                ReportTitel = "Daily Present Status";
                ReportType = "PresentAbsent";
            }
            else if (reportType == "absent") // Daily Absent Staust
            {
                sqlCmd = "select FullName,RollNo,AttStatus from v_DailyAttendanceRecordForReport where AttStatus='a' and  ShiftId='" + ddlShiftList.SelectedValue + "'and BatchId='" + ddlBatch.SelectedValue + "' and ClsGrpId='" + ddlgroup.SelectedValue + "' and ClsSecId='" + ddlSection.SelectedValue + "' and FORMAT(Attdate,'dd-MM-yyyy')='" + DateTime.Now.ToString("dd-MM-yyyy") + "'";
                ReportTitel = "Daily Absent Status";
                ReportType = "PresentAbsent";
            }
         
            //sqlDB.fillDataTable(sqlCmd, dt = new DataTable());
            dt = CRUD.ReturnTableNull(sqlCmd);
            if (dt.Rows.Count < 1)
            {
                lblSectionDiv.InnerText = "Any "+reportType+" record are not founded";
                btnPrintPreview.Enabled = false;
                btnPrintPreview.CssClass = "";
                gvAttList.DataSource = null;
                gvAttList.DataBind();
                return;
            }
            btnPrintPreview.Enabled = true;
            btnPrintPreview.CssClass = "btn btn-primary litleMargin";
            gvAttList.Visible = true;
            lblSectionDiv.InnerText = "Todays "+reportType+" List";
            gvAttList.DataSource = dt;
            gvAttList.DataBind();
            Session["__DailyAttendance__"] = dt;
            ViewState["__ReportTitle__"] = ReportTitel;
            ViewState["__ReportType__"] = ReportType;
            ViewState["__Report__"] = "";
        }
        private void GenerateAttendanceSheet()
        {
            //-----------Validation--------------
            if (ddlShiftList.SelectedValue == "0")
            { lblMessage.InnerText = "warning-> Please select a Shift!"; return; }
            if (ddlBatch.SelectedValue == "0") { lblMessage.InnerText = "warning-> Please select a Batch!"; return; }
            if (ddlgroup.Enabled == true && ddlgroup.SelectedValue == "0") { lblMessage.InnerText = "warning-> Please select a Group!"; return; }
            if (ddlSection.SelectedValue == "0") { lblMessage.InnerText = "warning-> Please select a Section!"; return; }
            if (ddlMonths.SelectedValue == "0") { lblMessage.InnerText = "warning-> Please select a Month!"; return; }
            //--------------------------------------------------
            DataTable dt = new DataTable();            
            string SqlCmd = "SELECT   FullName, RollNo, SUM(CASE DATEPART(day, AttDate) WHEN 1 THEN code ELSE 0 END) AS [1], SUM(CASE DATEPART(day, AttDate) "+
                        " WHEN 2 THEN code ELSE 0 END) AS [2], SUM(CASE DATEPART(day, AttDate) WHEN 3 THEN code ELSE 0 END) AS [3], SUM(CASE DATEPART(day, AttDate) WHEN 4 THEN code ELSE 0 END) AS [4], "+            
                        " SUM(CASE DATEPART(day, AttDate) WHEN 5 THEN code ELSE 0 END) AS [5], SUM(CASE DATEPART(day, AttDate) WHEN 6 THEN code ELSE 0 END) AS [6], SUM(CASE DATEPART(day, AttDate) "+
                        " WHEN 7 THEN code ELSE 0 END) AS [7], SUM(CASE DATEPART(day, AttDate) WHEN 8 THEN code ELSE 0 END) AS [8], SUM(CASE DATEPART(day, AttDate) WHEN 9 THEN code ELSE 0 END) AS [9], "+
                        " SUM(CASE DATEPART(day, AttDate) WHEN 10 THEN code ELSE 0 END) AS [10], SUM(CASE DATEPART(day, AttDate) WHEN 11 THEN code ELSE 0 END) AS [11], SUM(CASE DATEPART(day, AttDate) "+
                        " WHEN 12 THEN code ELSE 0 END) AS [12], SUM(CASE DATEPART(day, AttDate) WHEN 13 THEN code ELSE 0 END) AS [13], SUM(CASE DATEPART(day, AttDate) WHEN 14 THEN code ELSE 0 END) AS [14], "+
                        " SUM(CASE DATEPART(day, AttDate) WHEN 15 THEN code ELSE 0 END) AS [15], SUM(CASE DATEPART(day, AttDate) WHEN 16 THEN code ELSE 0 END) AS [16], SUM(CASE DATEPART(day, AttDate) "+
                        " WHEN 17 THEN code ELSE 0 END) AS [17], SUM(CASE DATEPART(day, AttDate) WHEN 18 THEN code ELSE 0 END) AS [18], SUM(CASE DATEPART(day, AttDate) WHEN 19 THEN code ELSE 0 END) AS [19], "+
                        " SUM(CASE DATEPART(day, AttDate) WHEN 20 THEN code ELSE 0 END) AS [20], SUM(CASE DATEPART(day, AttDate) WHEN 21 THEN code ELSE 0 END) AS [21], SUM(CASE DATEPART(day, AttDate) "+
                        " WHEN 22 THEN code ELSE 0 END) AS [22], SUM(CASE DATEPART(day, AttDate) WHEN 23 THEN code ELSE 0 END) AS [23], SUM(CASE DATEPART(day, AttDate) WHEN 24 THEN code ELSE 0 END) AS [24], "+
                        " SUM(CASE DATEPART(day, AttDate) WHEN 25 THEN code ELSE 0 END) AS [25], SUM(CASE DATEPART(day, AttDate) WHEN 26 THEN code ELSE 0 END) AS [26], SUM(CASE DATEPART(day, AttDate) "+
                        " WHEN 27 THEN code ELSE 0 END) AS [27], SUM(CASE DATEPART(day, AttDate) WHEN 28 THEN code ELSE 0 END) AS [28], SUM(CASE DATEPART(day, AttDate) WHEN 29 THEN code ELSE 0 END) AS [29], "+
                        " SUM(CASE DATEPART(day, AttDate) WHEN 30 THEN code ELSE 0 END) AS [30], SUM(CASE DATEPART(day, AttDate) WHEN 31 THEN code ELSE 0 END) AS [31], SUM(CASE Code WHEN 112 THEN 1 ELSE 0 END) "+ 
                        " AS P, SUM(CASE Code WHEN 97 THEN 1 ELSE 0 END) AS A, SUM(CASE Code WHEN 104 THEN 1 ELSE 0 END) AS H, SUM(CASE Code WHEN 119 THEN 1 ELSE 0 END) AS W, "+
                        " SUM(CASE Code WHEN 226 THEN 1 ELSE 0 END) AS LV "+
                        " FROM            dbo.v_DailyAttendanceRecordForReport" +
                        " where ShiftId='" + ddlShiftList.SelectedValue + "' and BatchId='" + ddlBatch.SelectedValue + "' and " +
                        " ClsGrpId='" + ddlgroup.SelectedValue + "' and ClsSecId='" + ddlSection.SelectedValue + "' and Format(attdate,'MM-yyyy')='" + ddlMonths.SelectedValue + "'"+
                        " GROUP BY StudentId, FullName, RollNo";
            sqlDB.fillDataTable(SqlCmd, dt);
            if (dt.Rows.Count < 1)
            {
                lblMessage.InnerText = "warning-> Any attendance Record are not founded!";               
                return;
            }            
            string Group = (ddlgroup.SelectedValue == "0") ? "" : "Group : " + ddlgroup.SelectedItem.Text + ",";
            Session["__AttendanceSheet__"] = dt;            
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=AttendanceSheet-" + ddlShiftList.SelectedItem.Text + "-" + ddlBatch.SelectedItem.Text + "-" + Group + "-" + ddlSection.SelectedItem.Text + "-" + ddlMonths.SelectedItem.Text + "');", true);
            //Open New Tab for Sever side code  
        }
        private void loadAttendanceSheet(DataTable dtStudentInf)
        {
            try
            {
             
                DataTable dt=new DataTable();
               
                //lblMessage.InnerText = "";
                //AttendanceSheetTitle.InnerText = "";

                DataView dv = new DataView(dtStudentInf);
                dt = dv.ToTable(false, "StudentId", "FullName", "RollNo", "1_Code", "2_Code", "3_Code", "4_Code", "5_Code", "6_Code", "7_Code", "8_Code", "9_Code", "10_Code",
                    "11_Code", "12_Code", "13_Code", "14_Code", "15_Code", "16_Code", "17_Code", "18_Code", "19_Code", "20_Code",
                    "21_Code", "22_Code", "23_Code", "24_Code", "25_Code", "26_Code", "27_Code", "28_Code", "29_Code", "30_Code", "31_Code");



                //AttendanceSheetTitle.Style["Color"] = "#1fb5ad";
                //AttendanceSheetTitle.InnerText = "Attendance sheet of " + ddlBatch.SelectedItem.Text + "(" + ddlSection.SelectedItem.Text + ") " + ddlMonths.SelectedItem.Text + "";


                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(dt.Rows.Count));
                string tbl = "";
                string tblInputElement = "";
                int DaysInMonth = DateTime.DaysInMonth(int.Parse(ddlMonths.SelectedItem.Value.Substring(3, 4)), int.Parse(ddlMonths.SelectedItem.Value.Substring(0, 2)));

                DataTable dtOffdays = new DataTable();
                sqlDB.fillDataTable("select Format(OffDate,'dd') as OffDate,Purpose from OffdaySettings where Format(OffDate,'MM-yyyy')='" + ddlMonths.SelectedItem.Value + "' order by OffDate ", dtOffdays);

                for (byte b = 3; b < (DaysInMonth + 3); b++)
                {
                    string[] col = dt.Columns[b].ToString().Split('_');
                    string col1 = col[0];
                    col1 = new String(col1.Where(Char.IsNumber).ToArray());
                    tbl += "<th style='width: 76px;text-align:center'>" + col1 + "</th>";
                }

                string tableInfo = "";
                tableInfo = "<table id='tblStudentAttendance'   >";
                tableInfo += " <th style='width: 70px; text-align:center'>Roll</th> <th style='width: 280px'>Name</th>" + tbl + "";

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    tblInputElement = "";
                    int row = i + 1;
                    DataTable dtTemp = dtOffdays.Copy();
                    for (byte b = 3; b < (DaysInMonth + 3); b++)   // this loop generate every student inputbox 
                    {
                        string attStatus = string.Empty;
                        if (dt.Rows[i].ItemArray[b].ToString().Equals("120") || dt.Rows[i].ItemArray[b].ToString().Equals("0"))
                            attStatus = string.Empty;
                        else if (dt.Rows[i].ItemArray[b].ToString().Equals("112")) attStatus = "p";
                        else if (dt.Rows[i].ItemArray[b].ToString().Equals("97")) attStatus = "a";
                        else if (dt.Rows[i].ItemArray[b].ToString().Equals("108")) attStatus = "l";

                        //string attStatus = string.Empty;
                        if (dtTemp.Rows.Count > 0)
                        {
                            bool isStatus = false;
                            for (byte x = 0; x < dtTemp.Rows.Count; x++)
                            {
                                if (int.Parse((b - 2).ToString()) == int.Parse(dtTemp.Rows[x]["OffDate"].ToString()))
                                {
                                    attStatus = (dtTemp.Rows[x]["Purpose"].ToString().Equals("Weekly Holiday")) ? "w" : "h";
                                    tblInputElement += "<td  style='width: 50px'> <input AutosizeMode ='false' autocomplete='off' readonly='false' style='background-color:#980000 ;color:White; text-align:center'  tabindex=" + row + " onchange='saveData(this)' onkeydown='return acceptValidCharacter(event, this)'  MaxLength='1' type='text' id='DailyAttendanceRecord:" + ddlShiftList.SelectedItem.Value + ":" + ddlBatch.SelectedItem.Value + ":" + ddlgroup.SelectedItem.Value + ":" + ddlSection.SelectedItem.Value + ":" + (b - 2).ToString() + "_" + ddlMonths.SelectedValue.ToString().Replace('-', '_') + ":" + dt.Columns[b].ToString() + ":" + dt.Rows[i]["StudentId"] + "' value=" + attStatus + "> </td>";  // this line for hilight weekly liholyday 

                                    isStatus = true;
                                    dtTemp.Rows.RemoveAt(x);
                                    break;
                                }
                            }

                            if (!isStatus)
                            {
                                if (dt.Rows[i].ItemArray[b].ToString().Trim().Length >= 1) tblInputElement += "<td style='width: 50px'> <input readonly='false' style='text-align:center' autocomplete='off' tabindex=" + row + " onchange='saveData(this)' onkeydown='return acceptValidCharacter(event, this)' MaxLength='1' type='text'  id='DailyAttendanceRecord:" + ddlShiftList.SelectedItem.Value + ":" + ddlBatch.SelectedItem.Value + ":" + ddlgroup.SelectedItem.Value + ":" + ddlSection.SelectedItem.Value + ":" + (b - 2).ToString() + "_" + ddlMonths.SelectedValue.ToString().Replace('-', '_') + ":" + dt.Columns[b].ToString() + ":" + dt.Rows[i]["StudentId"] + "' value=" + attStatus + " > </td>";
                                else tblInputElement += "<td style='width: 50px'> <input style='text-align:center' readonly='false' autocomplete='off' tabindex=" + row + " onchange='saveData(this)' onkeydown='return acceptValidCharacter(event, this)' MaxLength='1' type='text' id='DailyAttendanceRecord:" + ddlShiftList.SelectedItem.Value + ":" + ddlBatch.SelectedItem.Value + ":" + ddlgroup.SelectedItem.Value + ":" + ddlSection.SelectedItem.Value + ":" + (b - 2).ToString() + "_" + ddlMonths.SelectedValue.ToString().Replace('-', '_') + ":" + dt.Columns[b].ToString() + ":" + dt.Rows[i]["StudentId"] + "' value=" + attStatus + "> </td>";
                            }
                        }
                        else
                        {
                            if (dt.Rows[i].ItemArray[b].ToString().Trim().Length >= 1) tblInputElement += "<td style='width: 50px'> <input readonly='false'  style='text-align:center'  autocomplete='off' tabindex=" + row + " onchange='saveData(this)' onkeydown='return acceptValidCharacter(event, this)' MaxLength='1' type='text' id='DailyAttendanceRecord:" + ddlShiftList.SelectedItem.Value + ":" + ddlBatch.SelectedItem.Value + ":" + ddlgroup.SelectedItem.Value + ":" + ddlSection.SelectedItem.Value + ":" + (b - 2).ToString() + "_" + ddlMonths.SelectedValue.ToString().Replace('-', '_') + ":" + dt.Columns[b].ToString() + ":" + dt.Rows[i]["StudentId"] + "' value=" + attStatus + "> </td>";
                            else tblInputElement += "<td style='width: 50px'> <input style='text-align:center' readonly='false' autocomplete='off' tabindex=" + row + " onchange='saveData(this)' onkeydown='return acceptValidCharacter(event, this)' MaxLength='1' type='text' id='DailyAttendanceRecord:" + ddlShiftList.SelectedItem.Value + ":" + ddlBatch.SelectedItem.Value + ":" + ddlgroup.SelectedItem.Value + ":" + ddlSection.SelectedItem.Value + ":" + (b - 2).ToString() + "_" + ddlMonths.SelectedValue.ToString().Replace('-', '_') + ":" + dt.Columns[b].ToString() + ":" + dt.Rows[i]["StudentId"] + "' value=" + attStatus + "> </td>";
                        }
                        row += dt.Rows.Count;
                    }
                    tableInfo += "<tr> <td style='width: 80px;text-align:center'> " + dt.Rows[i]["RollNo"].ToString() + "</td>  <td style='width: 60px'>" + dt.Rows[i]["FullName"].ToString() + "</td>" + tblInputElement + "</tr>";
                }
                tableInfo += "</table>";
                divMonthWiseAttendaceSheet.Controls.Add(new LiteralControl(tableInfo));               
                divMonthWiseAttendaceSheet.Visible = true;
            }
            catch
            {
                //AttendanceSheetTitle.Style["Color"] = "Red";
                //AttendanceSheetTitle.InnerText = "Sorry this attendance sheet is not created";
                divMonthWiseAttendaceSheet.Visible = false;
            }
        }
       

        protected void btnTodayAttendanceSheet_Click(object sender, EventArgs e)
        {
           // loadTodayAttendanceReport();
        }

        protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
        {

            ddlSection.Items.Clear();
            BatchEntry.loadGroupByBatchId(ddlgroup, ddlBatch.SelectedValue.ToString());

            if (ddlgroup.Items.Count == 1)
            {
                ddlgroup.Enabled = false;
               // ClassSectionEntry.GetSectionListByBatchId(ddlSection, ddlBatch.SelectedValue.ToString());
                ClassSectionEntry.GetSectionListByBatchId_ClsGrpId(ddlSection, ddlBatch.SelectedValue.ToString(), ddlgroup.SelectedItem.Value);
            }
            else
            {
                ddlgroup.Enabled = true;
            }
        }

        protected void btnTodayAttendanceList_Click(object sender, EventArgs e)
        {           
            LoadDailyAttendanceReportData("attendance");
        }

        protected void btnPrintPreview_Click(object sender, EventArgs e)
        {
            string GroupName = (ddlgroup.SelectedValue == "0") ? "" : " Group : " + ddlgroup.SelectedItem.Text + ", ";
            if (ViewState["__Report__"].ToString() == "")
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=DailyAttendance-" + ddlShiftList.SelectedItem.Text + "-" + ddlBatch.SelectedItem.Text + "-" + GroupName + "-" + ddlSection.SelectedItem.Text + "-" + DateTime.Now.ToString("dd-MM-yyyy") + "-" + ViewState["__ReportTitle__"].ToString() + "-" + ViewState["__ReportType__"].ToString() + "');", true);
            else
                GenerateAttendanceSheet();
          
        }

        protected void btnTodayPresentList_Click(object sender, EventArgs e)
        {
            LoadDailyAttendanceReportData("present");
        }

        protected void btnTodayAbsentList_Click(object sender, EventArgs e)
        {
            LoadDailyAttendanceReportData("absent");
        }

        protected void ddlgroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClassSectionEntry.GetSectionListByBatchId_ClsGrpId(ddlSection, ddlBatch.SelectedValue.ToString(), ddlgroup.SelectedItem.Value);
        }       
    }
}


       