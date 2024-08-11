using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Text.RegularExpressions;

using DS.DAL.AdviitDAL;
using System.Data;
using DS.DAL;
using DS.BLL.Attendace;
using DS.BLL.GeneralSettings;
using DS.BLL.ControlPanel;

namespace DS.UI.Academics.Attendance.StafforFaculty.Manually
{
    public partial class FacultyStaffAbsentDetails : System.Web.UI.Page
    {
        string sqlCmd = "";
        string ReportTitel = "";
        string ReportType = "";
        string DepartmentList = "";
        string DesignationList = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.InnerText = "";
                if (!IsPostBack)
                {
                    SheetInfoEntry.loadMonths(dlSheetName);
                    ShiftEntry.GetDropDownList(ddlShiftList);
                    Classes.commonTask.LoadDeprtmentAtttedence(dlDepartment);
                    Classes.commonTask.LoadDesignation(dlDesignation);
                    dlName.Items.Add("All");

                    if (!PrivilegeOperation.SetPrivilegeControl(Session["__UserTypeId__"].ToString(), "FacultyStaffAbsentDetails.aspx", btnTodayAttendanceList, btnTodayPresentList, btnTodayAbsentList, btnPrintPreview, btnSearch)) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                }
        }
        private void LoadMonths()
        {
            try
            {
                DataTable dt = new DataTable();
                sqlDB.fillDataTable("Select ASName From FacultyNStaffAttendenceSheetInfo where ASYear='" + DateTime.Now.Year + "'", dt);
                dlSheetName.Items.Add("-Select-");
                for (byte i = 0; i < dt.Rows.Count; i++)
                {
                    string[] ASName = dt.Rows[i]["ASName"].ToString().Split('_');
                    dlSheetName.Items.Add(ASName[3]);
                }
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gvAttList.DataSource = null;
            gvAttList.DataBind();           
            //sqlDB.fillDataTable("Select * From Faculty_Staff_AttendanceSheet_" + dlSheetName.SelectedItem.Text + "", dt);
            //DataTable dt = new DataTable();
            //FilteringByDepartmentDesignationName(dt);
            //reportGenerateForFiltering(); 
            if (ddlShiftList.SelectedValue == "0") { lblMessage.InnerText = "warning->Please select a Shift!"; ddlShiftList.Focus(); return; }
            if (dlSheetName.SelectedValue == "0") { lblMessage.InnerText = "warning->Please select a Month!"; dlSheetName.Focus(); return; }
            dt = new DataTable();
            DepartmentList = (dlDepartment.SelectedItem.Text == "All") ? GetAlllist(dlDepartment) : dlDepartment.SelectedValue;
            DesignationList = (dlDesignation.SelectedItem.Text == "All") ? GetAlllist(dlDesignation) : dlDesignation.SelectedValue;
            dt = ForAttendanceReport.returnDatatableForEmpAttSheet(ddlShiftList.SelectedValue, dlSheetName.SelectedValue,DepartmentList,DesignationList);
            //dt = EmpSheetInfoEntry.getEmpAttendanceSheet(dlDepartment.SelectedItem.Value.ToString(),dlSheetName.SelectedValue);
            if (dt == null || dt.Rows.Count < 1) 
            {
                divHeadMsg.InnerText = "Any Staff/Facult attendanc record are not available in " + dlSheetName.SelectedItem.Text + " ";
                btnPrintPreview.Enabled = false;
                btnPrintPreview.CssClass = "";
                return;
            }
            divHeadMsg.InnerText = "Monthly Staff/Faculty attendance sheet of " + dlSheetName.SelectedItem.Text + "";
            ViewState["__Report__"] = "MonthlyAttSheet";
            //lblMonthName.Text = "Attendance Sheet at " + dlSheetName.SelectedItem.Text;
            //lblDepName.Text = "Department : " + dlDepartment.SelectedItem.Text;
            //lblDesName.Text = "Designation : " + dlDesignation.SelectedItem.Text;
            btnPrintPreview.Enabled = true;
            btnPrintPreview.CssClass = "btn btn-primary litleMargin";
            loadShiftInfo();
            loadAttendanceSheet(dt);
            //..........................For Sheet ..............................                
        }

        private void reportGenerateForFiltering()
        {
            try
            {
                
                DataTable dt = new DataTable();
                dt = (DataTable)Session["__dt__"];
                int totalRows = dt.Rows.Count;
                string divInfo = "";
                string divInfoReport = "";

                if (totalRows == 0)
                {
                    divInfo = "<div class='noData'>No Student available</div>";
                    divInfo += "<div><div class='head'></div></div>";
                    divMonthWiseAttendaceSheet.Controls.Add(new LiteralControl(divInfo));
                    return;
                }


                divInfo = " <table id='tblStudentList' class='display'  style='width:100%;margin:0px auto;' > ";
                divInfoReport = " <table id='tblStudentList' class='display'  style='width:100%;margin:0px auto;' > ";
                divInfo += "<thead>";
                divInfoReport += "<thead>";
                divInfo += "<tr>";
                divInfoReport += "<tr>";
                divInfo += "<th>Teacher Name</th>";
                divInfoReport += "<th>Teacher Name</th>";
                divInfo += "<th>Card No</th>";
                divInfoReport += "<th>Card No</th>";

                for (byte i = 3; i < dt.Columns.Count - 2; i++)
                {

                    string[] columnname = dt.Columns[i].ToString().Split('_');
                    string val = columnname[0];
                    string col = new String(val.Where(Char.IsNumber).ToArray());

                    divInfo += "<th style='text-align:center'>" + col + "</th>";
                    divInfoReport += "<th style='text-align:center'>" + col + "</th>";
                }
                divInfo += "<th style='text-align:center'>TP</th>";
                divInfoReport += "<th style='text-align:center'>TP</th>";
                divInfo += "<th style='text-align:center'>TA</th>";
                divInfoReport += "<th style='text-align:center'>TA</th>";
                divInfo += "<th style='text-align:center'>View</th>";

                divInfo += "</tr>";
                divInfoReport += "</tr>";
                divInfo += "</thead>";
                divInfoReport += "</thead>";
                divInfo += "<tbody>";
                divInfoReport += "<tbody>";
                string id = "";
                int TP = 0;
                int TA = 0;

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    id = dt.Rows[x]["EID"].ToString();
                    divInfo += "<tr id='r_" + id + "'>";
                    divInfoReport += "<tr id='r_" + id + "'>";
                    for (byte k = 0; k < dt.Columns.Count - 2; k++)
                    {
                        if (k == 2)
                        {
                            divInfo += "<td style='width:3%;display:none'  >" + dt.Rows[x][dt.Columns[k].ToString()].ToString() + "</td>";
                            divInfoReport += "<td style='width:3%;display:none'  >" + dt.Rows[x][dt.Columns[k].ToString()].ToString() + "</td>";
                        }
                        else if (k > 0)
                        {
                            if (dt.Rows[x][dt.Columns[k].ToString()].ToString() == "a") TA += 1;
                            else if (dt.Rows[x][dt.Columns[k].ToString()].ToString() == "p") TP += 1;

                            divInfo += "<td style='width: 3%; text-align: center;'  >" + dt.Rows[x][dt.Columns[k].ToString()].ToString().ToLower() + "</td>";
                            divInfoReport += "<td style='width: 3%; text-align: center;'  >" + dt.Rows[x][dt.Columns[k].ToString()].ToString().ToLower() + "</td>";
                        }
                        else
                        {
                            divInfo += "<td style='width: 3%'  >" + dt.Rows[x][dt.Columns[k].ToString()].ToString() + "</td>";
                            divInfoReport += "<td style='width: 3%'  >" + dt.Rows[x][dt.Columns[k].ToString()].ToString() + "</td>";
                        }

                        if (dt.Columns.Count - 3 == k)
                        {
                            divInfo += "<td style='width:15% ; text-align:center ; font-weight:bold ; color:green' >" + TP + "</td>";
                            divInfoReport += "<td style='width:15% ; text-align:center ; font-weight:bold ; color:green' >" + TP + "</td>";
                            divInfo += "<td style='width:15% ; text-align:center ; font-weight:bold ; color:red' >" + TA + "</td>";
                            divInfoReport += "<td style='width:15% ; text-align:center ; font-weight:bold ; color:red' >" + TA + "</td>";
                            divInfo += "<td style='width:3%;' class='numeric control' >" + "<img src='/Images/gridImages/view.png' onclick='viewEmployee(" + id + ");'  />";
                        }
                    }
                    TP = 0;
                    TA = 0;
                }

                divInfo += "</tbody>";
                divInfoReport += "</tbody>";
                divInfo += "<tfoot>";
                divInfoReport += "<tfoot>";

                divInfo += "</table>";
                divInfoReport += "</table>";
                divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                divInfoReport += "<div class='dataTables_wrapper'><div class='head'></div></div>";

                divMonthWiseAttendaceSheet.Controls.Add(new LiteralControl(divInfo));
                Session["__FacultyReport__"] = divInfoReport;
                Session["__ReportType__"] = "Attendance Sheet at " + dlSheetName.SelectedItem.Text;

                Session["__Department__"] = "Department : " + dlDepartment.SelectedItem.Text;
                Session["Designation"] = "Designation : " + dlDesignation.SelectedItem.Text;
                Session["__MonthName__"] = dlSheetName.SelectedItem.Text;
            }
            catch { }
        }

        DataTable dtView;
        private void FilteringByDepartmentDesignationName(DataTable dt)
        {
            try
            {

                sqlDB.fillDataTable("SELECT EmployeeInfo.EName,EmployeeInfo.ECardNo,Faculty_Staff_AttendanceSheet_" + dlSheetName.SelectedValue + ".*, Departments_HR.DName, "
                + "Designations.DesName FROM Faculty_Staff_AttendanceSheet_" + dlSheetName.SelectedValue + " INNER JOIN EmployeeInfo ON Faculty_Staff_AttendanceSheet_"
                + dlSheetName.SelectedValue + ".EID = EmployeeInfo.EID INNER JOIN Designations ON EmployeeInfo.DesId = dbo.Designations.DesId INNER JOIN Departments_HR ON "
                + "EmployeeInfo.DId = Departments_HR.DId", dtView = new DataTable());

                if ((dlDepartment.SelectedItem.Text.Trim().Equals("All") && (dlDesignation.SelectedItem.Text.Trim().Equals("All")) && (dlName.SelectedItem.Text.Trim().Equals("All"))))
                    dt = dtView.Select("").CopyToDataTable();

                else if ((!dlDepartment.SelectedItem.Text.Trim().Equals("All") && (dlDesignation.SelectedItem.Text.Trim().Equals("All")) && (dlName.SelectedItem.Text.Trim().Equals("All"))))
                    dt = dtView.Select("DName='" + dlDepartment.SelectedItem.Text + "'").CopyToDataTable();

                else if ((!dlDepartment.SelectedItem.Text.Trim().Equals("All") && (dlDesignation.SelectedItem.Text.Trim().Equals("All")) && (!dlName.SelectedItem.Text.Trim().Equals("All"))))
                    dt = dtView.Select("DName='" + dlDepartment.SelectedItem.Text.Trim() + "' AND EName='" + dlName.SelectedItem.Text.Trim() + "'").CopyToDataTable();

                else if ((!dlDepartment.SelectedItem.Text.Trim().Equals("All") && (!dlDesignation.SelectedItem.Text.Trim().Equals("All")) && (dlName.SelectedItem.Text.Trim().Equals("All"))))
                    dt = dtView.Select("DName='" + dlDepartment.SelectedItem.Text.Trim() + "' AND DesName='" + dlDesignation.SelectedItem.Text.Trim() + "'").CopyToDataTable();

                else if ((!dlDepartment.SelectedItem.Text.Trim().Equals("All") && (!dlDesignation.SelectedItem.Text.Trim().Equals("All")) && (!dlName.SelectedItem.Text.Trim().Equals("All"))))
                    dt = dtView.Select("DName='" + dlDepartment.SelectedItem.Text.Trim() + "'AND DesName='" + dlDesignation.SelectedItem.Text.Trim() + "'  AND EName='" + dlName.SelectedItem.Text.Trim() + "'").CopyToDataTable();

                Session["__dt__"] = dt;
            }
            catch { }
        }

        protected void dlDesignation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dlDepartment.SelectedItem.Text != "All" && dlDesignation.SelectedItem.Text != "All")
            {
                DataTable dt;
                sqlDB.fillDataTable("Select EID,EName From EmployeeInfo where DesId=" + dlDesignation.SelectedValue + " and DId=" + dlDepartment.SelectedValue + "", dt = new DataTable());
                dlName.DataSource = dt;
                dlName.DataTextField = "EName";
                dlName.DataValueField = "EID";
                dlName.DataBind();
                dlName.Items.Add("All");                
                dlName.SelectedIndex = dlName.Items.Count - 1;
            }
        }

        protected void dlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dlDepartment.SelectedItem.Text == "All")
            {
                Classes.commonTask.LoadDesignation(dlDesignation);
            }
            else
            {
                DataTable dt = new DataTable();
                sqlDB.fillDataTable("Select Distinct DesId,DesName From v_EmployeeInfo where DId=" + dlDepartment.SelectedValue + "", dt);
                dlDesignation.DataSource = dt;
                dlDesignation.DataTextField = "DesName";
                dlDesignation.DataValueField = "DesId";
                dlDesignation.DataBind();
                dlDesignation.Items.Insert(dlDesignation.Items.Count, new ListItem("All", "0"));
               // dlDesignation.Items.Insert(0, new ListItem("", "0"));
                dlDesignation.SelectedIndex = dlDesignation.Items.Count - 1;
            }

        }
        /*
                protected void btnTodayAttendanceSheet_Click(object sender, EventArgs e)
                {
                    try
                    {

                        sqlDB.fillDataTable("SELECT EmployeeInfo.EName,EmployeeInfo.ECardNo,Faculty_Staff_AttendanceSheet_" + DateTime.Today.ToString("MMMMyyyy") + ".*, "
                        + "Departments.DName, Designations.DesName FROM Faculty_Staff_AttendanceSheet_" + DateTime.Today.ToString("MMMMyyyy") + " INNER JOIN EmployeeInfo ON "
                        + "Faculty_Staff_AttendanceSheet_" + DateTime.Today.ToString("MMMMyyyy") + ".EID = EmployeeInfo.EID INNER JOIN Designations ON EmployeeInfo.DesId = "
                        + "dbo.Designations.DesId INNER JOIN Departments ON EmployeeInfo.DId = Departments.DId", dtView = new DataTable());
                        DataTable dtMS;
                        dtMS = dtView.DefaultView.ToTable(false, "ECardNo", "EName", "DName", "DesName", "D" + DateTime.Now.Date.ToString("d_M_yyyy") + "");
                        dtMS = dtMS.Select(" D" + DateTime.Now.Date.ToString("d_M_yyyy") + "='p' OR D" + DateTime.Now.Date.ToString("d_M_yyyy") + "='a' ").CopyToDataTable();

                        Session["__dt__"] = dtMS;
                        ViewState["__tr__"] = "TAS"; // TAL=Today Attendance Sheet  tr=today report
                        AllTodaysReportGenerate();
                        lblMonthName.Text = "Today Attendance Sheet (" + DateTime.Now.ToString("d-MMM-yyyy") + ")";
                        lblDepName.Text = "Department : All";
                        lblDesName.Text = "Designation : All";
                        Session["__ReportType__"] = "Today Attendance Sheet (" + DateTime.Now.ToString("d-MMM-yyyy") + ")";
                    }
                    catch { lblMessage.InnerText = "warning->Any Record is not found"; }
                }

                protected void btnTodayAttendanceList_Click(object sender, EventArgs e)
                {
                    try
                    {
                        sqlDB.fillDataTable("SELECT EmployeeInfo.EName,EmployeeInfo.ECardNo,Faculty_Staff_AttendanceSheet_" + DateTime.Today.ToString("MMMMyyyy") + ".*, "
                        + "Departments.DName, Designations.DesName FROM Faculty_Staff_AttendanceSheet_" + DateTime.Today.ToString("MMMMyyyy") + " INNER JOIN EmployeeInfo ON "
                        + "Faculty_Staff_AttendanceSheet_" + DateTime.Today.ToString("MMMMyyyy") + ".EID = EmployeeInfo.EID INNER JOIN Designations ON EmployeeInfo.DesId = "
                        + "dbo.Designations.DesId INNER JOIN Departments ON EmployeeInfo.DId = Departments.DId", dtView = new DataTable());
                        DataTable dtMS;
                        dtMS = dtView.DefaultView.ToTable(false, "ECardNo", "EName", "DName", "DesName", "D" + DateTime.Now.Date.ToString("d_M_yyyy") + "");
                        dtMS = dtMS.Select(" D" + DateTime.Now.Date.ToString("d_M_yyyy") + "='p'").CopyToDataTable();

                        Session["__dt__"] = dtMS;
                        ViewState["__tr__"] = "TPL"; // TAL=Today Present List  tr=today report
                        AllTodaysReportGenerate();
                        Session["__ReportType__"] = "Today Attendance List (" + DateTime.Now.ToString("d-MMM-yyyy") + ")";

                        lblMonthName.Text = "Today Attendance List (" + DateTime.Now.ToString("d-MMM-yyyy") + ")";
                        lblDepName.Text = "Department : All";
                        lblDesName.Text = "Designation : All";
                    }
                    catch { lblMessage.InnerText = "warning->Any Record is not found"; }
                }

                protected void btnTodayAbsentList_Click(object sender, EventArgs e)
                {
                    try
                    {
                        sqlDB.fillDataTable("SELECT EmployeeInfo.EName,EmployeeInfo.ECardNo,Faculty_Staff_AttendanceSheet_" + DateTime.Today.ToString("MMMMyyyy") + ".*, "
                        + "Departments.DName, Designations.DesName FROM Faculty_Staff_AttendanceSheet_" + DateTime.Today.ToString("MMMMyyyy") + " INNER JOIN EmployeeInfo ON "
                        + "Faculty_Staff_AttendanceSheet_" + DateTime.Today.ToString("MMMMyyyy") + ".EID = EmployeeInfo.EID INNER JOIN Designations ON EmployeeInfo.DesId = "
                        + "dbo.Designations.DesId INNER JOIN Departments ON EmployeeInfo.DId = Departments.DId", dtView = new DataTable());
                        DataTable dtMS;
                        dtMS = dtView.DefaultView.ToTable(false, "ECardNo", "EName", "DName", "DesName", "D" + DateTime.Now.Date.ToString("d_M_yyyy") + "");
                        dtMS = dtMS.Select(" D" + DateTime.Now.Date.ToString("d_M_yyyy") + "='a'").CopyToDataTable();
                        Session["__dt__"] = dtMS;
                        ViewState["__tr__"] = "TAL"; // TAL=Today Absent List  tr=today report
                        AllTodaysReportGenerate();
                        Session["__ReportType__"] = "Today Absent List (" + DateTime.Now.ToString("d-MMM-yyyy") + ")";

                        lblMonthName.Text = "Today Absent List (" + DateTime.Now.ToString("d-MMM-yyyy") + ")";
                        lblDepName.Text = "Department : All";
                        lblDesName.Text = "Designation : All";
                    }
                    catch
                    {
                        lblMessage.InnerText = "warning->Any Record is not found";
                    }
                }
                */
        private void AllTodaysReportGenerate()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = (DataTable)Session["__dt__"];
                int totalRows = dt.Rows.Count;
                string divInfo = "";

                if (totalRows == 0)
                {
                    divInfo = "<div class='noData'>No Teacher available</div>";
                    divInfo += "<div><div class='head'></div></div>";
                    divMonthWiseAttendaceSheet.Controls.Add(new LiteralControl(divInfo));
                    return;
                }


                divInfo = " <table id='tblStudentList' class='display'  style='width:100%;margin:0px auto;' > ";
                divInfo += "<thead>";
                divInfo += "<tr>";

                divInfo += "<th>Card No</th>";
                divInfo += "<th>Name</th>";
                divInfo += "<th>Department</th>";
                divInfo += "<th>Designation</th>";
                bool status = true;
                if ((ViewState["__tr__"].ToString().Equals("TPL")) || (ViewState["__tr__"].ToString().Equals("TAL"))) status = false;

                else divInfo += "<th>Staus</th>";

                divInfo += "</tr>";

                divInfo += "</thead>";

                divInfo += "<tbody>";

                string id = "";


                for (int x = 0; x < dt.Rows.Count; x++)
                {

                    divInfo += "<tr id='r_" + id + "'>";
                    if (x == 0) if (!status) dt.Columns.RemoveAt(4);
                    for (byte k = 0; k < dt.Columns.Count; k++)
                    {
                        //if (k == 2) divInfo += "<td style='width:3%;display:none'  >" + dt.Rows[x][dt.Columns[k].ToString()].ToString() + "</td>";
                        //else if (k > 0) divInfo += "<td style='width: 3%; text-align: center;'  >" + dt.Rows[x][dt.Columns[k].ToString()].ToString() + "</td>";
                        if (dt.Rows[x][dt.Columns[k].ToString()].ToString() == "a") divInfo += "<td style='width: 3%'  >Absent</td>";
                        else if (dt.Rows[x][dt.Columns[k].ToString()].ToString() == "p") divInfo += "<td style='width: 3%'  >Present</td>";
                        else divInfo += "<td style='width: 3%'  >" + dt.Rows[x][dt.Columns[k].ToString()].ToString() + "</td>";
                    }

                }
                divInfo += "</tbody>";
                divInfo += "<tfoot>";

                divInfo += "</table>";
                divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";

                divMonthWiseAttendaceSheet.Controls.Add(new LiteralControl(divInfo));

                Session["__FacultyReport__"] = divInfo;

            }
            catch { }
        }
        private void LoadDailyAttendanceReportData(string reportType)
        {
            //-----------Validation--------------           
            if (ddlShiftList.SelectedValue == "0") { lblMessage.InnerText = "warning-> Please select a Shift!"; ddlShiftList.Focus(); return; }
            //if (ddlShiftList.SelectedValue == "0")
            //{ lblMessage.InnerText = "warning-> Please select a Shift!"; return; }
            //if (ddlBatch.SelectedValue == "0") { lblMessage.InnerText = "warning-> Please select a Batch!"; return; }
            //if (ddlgroup.Enabled == true && ddlgroup.SelectedValue == "0") { lblMessage.InnerText = "warning-> Please select a Group!"; return; }
            //if (ddlSection.SelectedValue == "0") { lblMessage.InnerText = "warning-> Please select a Section!"; return; }
            //--------------------------------------------------
            divMonthWiseAttendaceSheet.InnerHtml = "";
            DataTable dt = new DataTable();
           
            //if (dlDepartment.SelectedItem.Text == "All") DepartmentList = GetAlllist(dlDepartment);
            //else DepartmentList = dlDepartment.SelectedValue;
            //if (dlDesignation.SelectedItem.Text == "All") DesignationList = GetAlllist(dlDesignation);
            //else DesignationList = dlDesignation.SelectedValue;
            DepartmentList = (dlDepartment.SelectedItem.Text == "All") ? GetAlllist(dlDepartment) : dlDepartment.SelectedValue;
            DesignationList = (dlDesignation.SelectedItem.Text == "All") ? GetAlllist(dlDesignation) : dlDesignation.SelectedValue;
            if (reportType == "attendance") // Daily Attendance Status
            {
                sqlCmd = "select ECardNo,EName,DName,DesName,AttStatus,Format(AttDates,'dd-MM-yyyy') as AttDates from v_DailyEmployeeAttendanceRecord where DId in(" +DepartmentList+ ") and DesId in("+ DesignationList+") and ShiftId='"+ddlShiftList.SelectedValue+"' and Format (AttDates,'dd-MM-yyyy')='"+DateTime.Now.ToString("dd-MM-yyyy")+"'";
                ReportTitel = "Daily Attendance Status";
                ReportType = "Status";
            }
            else if (reportType == "present") // Daily Present status
            {
                sqlCmd = "select ECardNo,EName,DName,DesName,AttStatus,Format(AttDates,'dd-MM-yyyy') as AttDates from v_DailyEmployeeAttendanceRecord where  AttStatus='p' and DId in(" + DepartmentList + ") and DesId in(" + DesignationList + ") and ShiftId='" + ddlShiftList.SelectedValue + "' and Format (AttDates,'dd-MM-yyyy')='" + DateTime.Now.ToString("dd-MM-yyyy") + "'";
                ReportTitel = "Daily Present Status";
                ReportType = "PresentAbsent";
            }
            else if (reportType == "absent") // Daily Absent Staust
            {
                sqlCmd = "select ECardNo,EName,DName,DesName,AttStatus,Format(AttDates,'dd-MM-yyyy') as AttDates from v_DailyEmployeeAttendanceRecord where  AttStatus='a' and DId in(" + DepartmentList + ") and DesId in(" + DesignationList + ") and ShiftId='" + ddlShiftList.SelectedValue + "' and Format (AttDates,'dd-MM-yyyy')='" + DateTime.Now.ToString("dd-MM-yyyy") + "'";                
                ReportTitel = "Daily Absent Status";
                ReportType = "PresentAbsent";
            }

            //sqlDB.fillDataTable(sqlCmd, dt = new DataTable());
            dt = CRUD.ReturnTableNull(sqlCmd);
            if (dt.Rows.Count < 1)
            {
                divHeadMsg.InnerText = "Any " + reportType + " record are not founded";
                btnPrintPreview.Enabled = false;
                btnPrintPreview.CssClass = "";
                gvAttList.DataSource = null;
                gvAttList.DataBind();
                return;
            }
            btnPrintPreview.Enabled = true;
            btnPrintPreview.CssClass = "btn btn-primary litleMargin";
            gvAttList.Visible = true;
            divHeadMsg.InnerText = "Todays " + reportType + " List";
            gvAttList.DataSource = dt;
            gvAttList.DataBind();
            Session["__DailyEmpAttendance__"] = dt;
            ViewState["__ReportTitle__"] = ReportTitel;
            ViewState["__ReportType__"] = ReportType;
            ViewState["__Report__"] = "";
        }
        protected void btnPrintPreview_Click(object sender, EventArgs e)
        {
            try
            {
                sqlDB.fillDataTable("SELECT EmployeeInfo.EName as Name,EmployeeInfo.ECardNo as CardNo,Departments_HR.DName, "
                   + "Designations.DesName,Faculty_Staff_AttendanceSheet_" + dlSheetName.SelectedValue + ".* FROM Faculty_Staff_AttendanceSheet_" + dlSheetName.SelectedValue + " INNER JOIN EmployeeInfo ON Faculty_Staff_AttendanceSheet_"
                   + dlSheetName.SelectedValue + ".EID = EmployeeInfo.EID INNER JOIN Designations ON EmployeeInfo.DesId = dbo.Designations.DesId INNER JOIN Departments_HR ON "
                   + "EmployeeInfo.DId = Departments_HR.DId", dtView = new DataTable());

                if ((dlDepartment.SelectedItem.Text.Trim().Equals("All") && (dlDesignation.SelectedItem.Text.Trim().Equals("All")) && (dlName.SelectedItem.Text.Trim().Equals("All"))))
                    dt = dtView.Select("").CopyToDataTable();

                else if ((!dlDepartment.SelectedItem.Text.Trim().Equals("All") && (dlDesignation.SelectedItem.Text.Trim().Equals("All")) && (dlName.SelectedItem.Text.Trim().Equals("All"))))
                    dt = dtView.Select("DName='" + dlDepartment.SelectedItem.Text + "'").CopyToDataTable();

                else if ((!dlDepartment.SelectedItem.Text.Trim().Equals("All") && (dlDesignation.SelectedItem.Text.Trim().Equals("All")) && (!dlName.SelectedItem.Text.Trim().Equals("All"))))
                    dt = dtView.Select("DName='" + dlDepartment.SelectedItem.Text.Trim() + "' AND EName='" + dlName.SelectedItem.Text.Trim() + "'").CopyToDataTable();

                else if ((!dlDepartment.SelectedItem.Text.Trim().Equals("All") && (!dlDesignation.SelectedItem.Text.Trim().Equals("All")) && (dlName.SelectedItem.Text.Trim().Equals("All"))))
                    dt = dtView.Select("DName='" + dlDepartment.SelectedItem.Text.Trim() + "' AND DesName='" + dlDesignation.SelectedItem.Text.Trim() + "'").CopyToDataTable();

                else if ((!dlDepartment.SelectedItem.Text.Trim().Equals("All") && (!dlDesignation.SelectedItem.Text.Trim().Equals("All")) && (!dlName.SelectedItem.Text.Trim().Equals("All"))))
                    dt = dtView.Select("DName='" + dlDepartment.SelectedItem.Text.Trim() + "'AND DesName='" + dlDesignation.SelectedItem.Text.Trim() + "'  AND EName='" + dlName.SelectedItem.Text.Trim() + "'").CopyToDataTable();
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (i > 4)
                    {
                        dt.Columns[dt.Columns[i].ColumnName].ColumnName = (i - 4).ToString();
                    }
                }

                Session["__StaffAttSheet__"] = dt;
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=StaffAttSheet-" + dlSheetName.SelectedItem.Text + "-" + dlDepartment.SelectedItem.Text + "-" + dlDesignation.SelectedItem.Text + "');", true);  //Open New Tab for Sever side code   
                //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/Report/FacultyStaffAttendanceReport.aspx');", true);  //Open New Tab for Sever side code
            }
            catch { }
        }

        protected void btnDateRangeSearch_Click(object sender, EventArgs e)
        {           
            reportGenerateForFilteringForDateRange();
        }

        DataTable dtMonth = new DataTable();
        string allReport;
        DataTable dt;
        private void reportGenerateForFilteringForDateRange()
        {
            try
            {
                dtMonth.Columns.Add("MonthName");
                string[] getFromDate = txtFromDate.Text.Trim().Split('-');
                string[] getToDate = txtToDate.Text.Trim().Split('-');
                int getMonth;
                if (getFromDate[1] == getToDate[1]) getMonth = 1;
                else getMonth = int.Parse(getToDate[1]) + 1 - int.Parse(getFromDate[1]);
                DataSet ds = new DataSet();

                for (byte b = 0; b < getMonth; b++)
                {
                    dt = new DataTable();

                    if (dlName.SelectedItem.Text == "All")
                    {
                        string getMonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(b + int.Parse(getFromDate[1]));
                        sqlDB.fillDataTable("SELECT EmployeeInfo.EName,EmployeeInfo.ECardNo,Faculty_Staff_AttendanceSheet_" + getMonthName + getFromDate[2] + ".*, "
                        + "Departments_HR.DName, Designations.DesName FROM Faculty_Staff_AttendanceSheet_" + getMonthName + getFromDate[2] + " INNER JOIN EmployeeInfo ON "
                        + "Faculty_Staff_AttendanceSheet_" + getMonthName + getFromDate[2] + ".EID = EmployeeInfo.EID INNER JOIN Designations ON EmployeeInfo.DesId = "
                        + "dbo.Designations.DesId INNER JOIN Departments_HR ON EmployeeInfo.DId = Departments_HR.DId  ", dt);
                        dtMonth.Rows.Add(getMonthName);
                    }
                    else
                    {
                        string getMonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(b + int.Parse(getFromDate[1]));
                        sqlDB.fillDataTable("SELECT EmployeeInfo.EName,EmployeeInfo.ECardNo,Faculty_Staff_AttendanceSheet_" + getMonthName + getFromDate[2] + ".*, "
                        + "Departments_HR.DName, Designations.DesName FROM Faculty_Staff_AttendanceSheet_" + getMonthName + getFromDate[2] + " INNER JOIN EmployeeInfo ON "
                        + "Faculty_Staff_AttendanceSheet_" + getMonthName + getFromDate[2] + ".EID = EmployeeInfo.EID INNER JOIN Designations ON EmployeeInfo.DesId = "
                        + "dbo.Designations.DesId INNER JOIN Departments_HR ON EmployeeInfo.DId = Departments_HR.DId Where EmployeeInfo.EName='" + dlName.SelectedItem.Text + "' ", dt);
                        dtMonth.Rows.Add(getMonthName);
                    }

                    int getDays = dt.Columns.Count - (3 + byte.Parse(getFromDate[0].ToString()));

                    if (b == 0)
                    {
                        for (byte f = 0; f < int.Parse(getFromDate[0]) - 1; f++)
                        {
                            dt.Columns.RemoveAt(3);
                        }
                    }
                    int tDays = DateTime.DaysInMonth(int.Parse(getFromDate[2]), b + int.Parse(getFromDate[1]));     // b+getToDate[0] for count from select month number  ,getToDate[1] for get year

                    if ((b + int.Parse(getFromDate[1])).ToString() == getToDate[1])
                    {
                        for (int f = int.Parse(getToDate[0]); f < tDays; f++)
                        {
                            if (b == 0) dt.Columns.RemoveAt(int.Parse(getToDate[0]) + 5 - int.Parse(getFromDate[0]));
                            else dt.Columns.RemoveAt(int.Parse(getToDate[0]) + 3);
                        }
                    }
                    ds.Tables.Add(dt);
                }


                for (int j = 0; j < ds.Tables.Count; j++)
                {
                    int totalRows = ds.Tables[j].Rows.Count;
                    string divInfo = "";
                    string divInfoReport = "";

                    if (totalRows == 0)
                    {
                        divInfo = "<div class='noData'>No Employee available</div>";
                        divInfoReport = "<div class='noData'>No Employee available</div>";
                        divInfo += "<div><div class='head'></div></div>";
                        divInfoReport += "<div><div class='head'></div></div>";
                        goto Outer;
                    }

                    divInfo = "<h4> Month Name : " + dtMonth.Rows[j]["MonthName"] + " </h4>";
                    divInfoReport = "<h4> Month Name : " + dtMonth.Rows[j]["MonthName"] + " </h4>";
                    divInfo += " <table id='tblEmployeeList' class='display'  style='width:100%;margin:0px auto;' > ";
                    divInfoReport += " <table id='tblEmployeeList' class='display'  style='width:100%;margin:0px auto;' > ";
                    divInfo += "<thead>";
                    divInfoReport += "<thead>";
                    divInfo += "<tr>";
                    divInfoReport += "<tr>";
                    divInfo += "<th>Teacher Name</th>";
                    divInfoReport += "<th>Teacher Name</th>";
                    divInfo += "<th class='numeric'>Card No</th>";
                    divInfoReport += "<th class='numeric'>Card No</th>";

                    for (byte i = 3; i < ds.Tables[j].Columns.Count - 2; i++)
                    {

                        string[] columnname = ds.Tables[j].Columns[i].ToString().Split('_');
                        string val = columnname[0];
                        string col = new String(val.Where(Char.IsNumber).ToArray());

                        divInfo += "<th style='text-align:center'>" + col + "</th>";
                        divInfoReport += "<th style='text-align:center'>" + col + "</th>";
                    }
                    divInfo += "<th style='text-align:center'>TP</th>";
                    divInfoReport += "<th style='text-align:center'>TP</th>";
                    divInfo += "<th style='text-align:center'>TA</th>";
                    divInfoReport += "<th style='text-align:center'>TA</th>";
                    divInfo += "<th style='text-align:center'>View</th>";

                    divInfo += "</tr>";
                    divInfoReport += "</tr>";
                    divInfo += "</thead>";
                    divInfoReport += "</thead>";

                    divInfo += "<tbody>";
                    divInfoReport += "<tbody>";

                    string id = "";
                    int TP = 0;
                    int TA = 0;

                    for (int x = 0; x < ds.Tables[j].Rows.Count; x++)
                    {
                        id = ds.Tables[j].Rows[x]["EID"].ToString();
                        divInfo += "<tr id='r_" + id + "'>";
                        divInfoReport += "<tr id='r_" + id + "'>";

                        for (byte k = 0; k < ds.Tables[j].Columns.Count - 2; k++)
                        {
                            if (k == 2)
                            {
                                divInfo += "<td style='width:3%;display:none'  >" + ds.Tables[j].Rows[x][ds.Tables[j].Columns[k].ToString()].ToString() + "</td>";
                                divInfoReport += "<td style='width:3%;display:none'  >" + ds.Tables[j].Rows[x][ds.Tables[j].Columns[k].ToString()].ToString() + "</td>";
                            }
                            else if (k > 0)
                            {
                                if (ds.Tables[j].Rows[x][ds.Tables[j].Columns[k].ToString()].ToString() == "a") TA += 1;
                                else if (ds.Tables[j].Rows[x][ds.Tables[j].Columns[k].ToString()].ToString() == "p") TP += 1;

                                divInfo += "<td style='width: 3%; text-align: center;'  >" + ds.Tables[j].Rows[x][ds.Tables[j].Columns[k].ToString()].ToString().ToLower() + "</td>";
                                divInfoReport += "<td style='width: 3%; text-align: center;'  >" + ds.Tables[j].Rows[x][ds.Tables[j].Columns[k].ToString()].ToString().ToLower() + "</td>";
                            }
                            else
                            {
                                divInfo += "<td style='width: 3%'  >" + ds.Tables[j].Rows[x][ds.Tables[j].Columns[k].ToString()].ToString() + "</td>";
                                divInfoReport += "<td style='width: 3%'  >" + ds.Tables[j].Rows[x][ds.Tables[j].Columns[k].ToString()].ToString() + "</td>";
                            }

                            if (ds.Tables[j].Columns.Count - 3 == k)
                            {
                                divInfo += "<td style='width:3% ; text-align:center ; font-weight:bold ; color:green' >" + TP + "</td>";
                                divInfoReport += "<td style='width:3% ; text-align:center ; font-weight:bold ; color:green' >" + TP + "</td>";
                                divInfo += "<td style='width:3% ; text-align:center ; font-weight:bold ; color:red' >" + TA + "</td>";
                                divInfoReport += "<td style='width:3% ; text-align:center ; font-weight:bold ; color:red' >" + TA + "</td>";
                                divInfo += "<td style='width:3%;' class='numeric control' >" + "<img src='/Images/gridImages/view.png' onclick='viewEmployee(" + id + ");'  />";
                            }
                        }
                        TP = 0;
                        TA = 0;

                    }
                    divInfo += "</tbody>";
                    divInfoReport += "</tbody>";
                    divInfo += "<tfoot>";
                    divInfoReport += "<tfoot>";

                    divInfo += "</table>";
                    divInfoReport += "</table>";
                    divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                    divInfoReport += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                    divMonthWiseAttendaceSheet.Controls.Add(new LiteralControl(divInfo));
                    Session["__ReportType__"] = "Today Attendance Sheet at " + dlSheetName.SelectedItem.Text;

                    allReport += divInfoReport;

                Outer:
                    continue;
                }
                Session["__FacultyReport__"] = allReport;
                Session["__ReportType__"] = "Attendance Information";
                Session["__Department__"] = "Department : " + dlDepartment.SelectedItem.Text;
                Session["Designation"] = "Designation : " + dlDesignation.SelectedItem.Text;

                Session["__DateRange__"] = ds;
                Session["__MonthName__"] = dtMonth;
            }
            catch { }
        }

        protected void btnTodayAttendanceList_Click(object sender, EventArgs e)
        {
            LoadDailyAttendanceReportData("attendance");
        }

        protected void btnTodayPresentList_Click(object sender, EventArgs e)
        {
            LoadDailyAttendanceReportData("present");
        }

        protected void btnTodayAbsentList_Click(object sender, EventArgs e)
        {
            LoadDailyAttendanceReportData("absent");
        }

        protected void btnPrintPreview_Click1(object sender, EventArgs e)
        {
            if (ViewState["__Report__"].ToString() == "")
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=DailyEmpAttendance-" + ViewState["__ReportTitle__"].ToString() + "-" + ViewState["__ReportType__"].ToString() + "-"+ddlShiftList.SelectedItem.Text+"-"+"Staff and Fatulty"+"');", true);
            else
            GeneratMonthlyEmpAttendanceSheet();
        }
        private void loadAttendanceSheet(DataTable dtStudentInf)
        {
            try
            {
                lblMessage.InnerText = "";
               // AttendanceSheetTitle.InnerText = "";

                DataView dv = new DataView(dtStudentInf);
                dt = dv.ToTable(false, "EId", "ECardNo", "EName", "DId", "1_Code", "2_Code", "3_Code", "4_Code", "5_Code", "6_Code", "7_Code", "8_Code", "9_Code", "10_Code",
                    "11_Code", "12_Code", "13_Code", "14_Code", "15_Code", "16_Code", "17_Code", "18_Code", "19_Code", "20_Code",
                    "21_Code", "22_Code", "23_Code", "24_Code", "25_Code", "26_Code", "27_Code", "28_Code", "29_Code", "30_Code", "31_Code");



                //AttendanceSheetTitle.Style["Color"] = "#1fb5ad";
                //AttendanceSheetTitle.InnerText = "Attendance sheet of Faculty and staff " + dlMonths.Text + "";


                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(dt.Rows.Count));
                string tbl = "";
                string tblInputElement = "";
                int DaysInMonth = DateTime.DaysInMonth(int.Parse(dlSheetName.SelectedItem.Value.Substring(3, 4)), int.Parse(dlSheetName.SelectedItem.Value.Substring(0, 2)));

                DataTable dtOffdays = new DataTable();
                sqlDB.fillDataTable("select Format(OffDate,'dd') as OffDate,Purpose from OffdaySettings where Format(OffDate,'MM-yyyy')='" + dlSheetName.SelectedItem.Value + "' order by OffDate ", dtOffdays);

                for (byte b = 4; b < (DaysInMonth + 4); b++)
                {
                    string[] col = dt.Columns[b].ToString().Split('_');
                    string col1 = col[0];
                    col1 = new String(col1.Where(Char.IsNumber).ToArray());
                    tbl += "<th style='width: 76px;text-align:center'>" + col1 + "</th>";
                }

                string tableInfo = "";
                tableInfo = "<table id='tblStudentAttendance'   >";
                tableInfo += " <th style='width: 70px; text-align:center'>CardNo</th> <th style='width: 280px'>Name</th>" + tbl + "";

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    tblInputElement = "";
                    int row = i + 1;
                    DataTable dtTemp = dtOffdays.Copy();
                    for (byte b = 4; b < (DaysInMonth + 4); b++)   // this loop generate every student inputbox 
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
                                if (int.Parse((b - 3).ToString()) == int.Parse(dtTemp.Rows[x]["OffDate"].ToString()))
                                {
                                    attStatus = (dtTemp.Rows[x]["Purpose"].ToString().Equals("Weekly Holiday")) ? "w" : "h";
                                    tblInputElement += "<td  style='width: 50px'> <input AutosizeMode ='false' readonly='true' autocomplete='off' readonly='false' style='background-color:#980000 ;color:White; text-align:center'  tabindex=" + row + " onchange='saveData(this)' MaxLength='1' type='text' id='DailyAttendanceRecord:" + (b - 3).ToString() + "_" + dlSheetName.SelectedValue.ToString().Replace('-', '_') + ":" + dt.Columns[b].ToString() + ":" + dt.Rows[i]["EId"] + ":" + dlDepartment.SelectedItem.Value + ":" + ddlShiftList.SelectedItem.Value + ":" + ViewState["__StartTime__"].ToString() + ":" + ViewState["__CloseTime__"].ToString() + ":" + ViewState["__LateTime__"].ToString() + "' value=" + attStatus + "> </td>";  // this line for hilight weekly liholyday 

                                    isStatus = true;
                                    dtTemp.Rows.RemoveAt(x);
                                    break;
                                }
                            }

                            if (!isStatus)
                            {
                                if (dt.Rows[i].ItemArray[b].ToString().Trim().Length >= 1) tblInputElement += "<td style='width: 50px'> <input  style='text-align:center' autocomplete='off' tabindex=" + row + " onchange='saveData(this)' onkeydown='return acceptValidCharacter(event, this)' MaxLength='1' type='text'  id='DailyAttendanceRecord:" + (b - 3).ToString() + "_" + dlSheetName.SelectedValue.ToString().Replace('-', '_') + ":" + dt.Columns[b].ToString() + ":" + dt.Rows[i]["EId"] + ":" + dlDepartment.SelectedItem.Value + ":" + ddlShiftList.SelectedItem.Value + ":" + ViewState["__StartTime__"].ToString() + ":" + ViewState["__CloseTime__"].ToString() + ":" + ViewState["__LateTime__"].ToString() + "' value=" + attStatus + " > </td>";
                                else tblInputElement += "<td style='width: 50px'> <input style='text-align:center' autocomplete='off' tabindex=" + row + " onchange='saveData(this)' onkeydown='return acceptValidCharacter(event, this)' MaxLength='1' type='text' id='DailyAttendanceRecord:" + (b - 3).ToString() + "_" + dlSheetName.SelectedValue.ToString().Replace('-', '_') + ":" + dt.Columns[b].ToString() + ":" + dt.Rows[i]["EId"] + ":" + "' value=" + attStatus + "> </td>";
                            }
                        }
                        else
                        {
                            if (dt.Rows[i].ItemArray[b].ToString().Trim().Length >= 1) tblInputElement += "<td style='width: 50px'> <input  style='text-align:center' autocomplete='off' tabindex=" + row + " onchange='saveData(this)' onkeydown='return acceptValidCharacter(event, this)' MaxLength='1' type='text' id='DailyAttendanceRecord:" + (b - 3).ToString() + "_" + dlSheetName.SelectedValue.ToString().Replace('-', '_') + ":" + dt.Columns[b].ToString() + ":" + dt.Rows[i]["EId"] + "' value=" + attStatus + "> </td>";
                            else tblInputElement += "<td style='width: 50px'> <input style='text-align:center' autocomplete='off' tabindex=" + row + " onchange='saveData(this)' onkeydown='return acceptValidCharacter(event, this)' MaxLength='1' type='text' id='DailyAttendanceRecord:" + (b - 3).ToString() + "_" + dlSheetName.SelectedValue.ToString().Replace('-', '_') + ":" + dt.Columns[b].ToString() + ":" + dt.Rows[i]["EId"] + "' value=" + attStatus + "> </td>";
                        }
                        row += dt.Rows.Count;
                    }
                    tableInfo += "<tr> <td style='width: 80px;text-align:center'> " + dt.Rows[i]["ECardNo"].ToString() + "</td>  <td style='width: 60px'>" + dt.Rows[i]["EName"].ToString() + "</td>" + tblInputElement + "</tr>";
                }
                tableInfo += "</table>";
                divMonthWiseAttendaceSheet.Controls.Add(new LiteralControl(tableInfo));
               // divTable.Visible = true;

            }
            catch
            {
                //AttendanceSheetTitle.Style["Color"] = "Red";
              // AttendanceSheetTitle.InnerText = "Sorry this attendance sheet is not created";
               // divTable.Visible = false;
            }
        }
        private void loadShiftInfo()
        {
            try
            {
                DataTable dtShiftTime = new DataTable();
                dtShiftTime = CRUD.ReturnTableNull("select StartTime,CloseTime,LateTime from ShiftConfiguration where ConfigId =" + ddlShiftList.SelectedItem.Value + "");
                ViewState["__StartTime__"] = dtShiftTime.Rows[0]["StartTime"].ToString();

                ViewState["__CloseTime__"] = dtShiftTime.Rows[0]["CloseTime"].ToString();

                ViewState["__LateTime__"] = dtShiftTime.Rows[0]["LateTime"].ToString();
            }
            catch { }
        }
        private void GeneratMonthlyEmpAttendanceSheet() 
        {
            //----------validation-------------------------------
            if (ddlShiftList.SelectedValue == "0") { lblMessage.InnerText = "warning-> Please select a Shift!"; ddlShiftList.Focus(); return; }
            if (dlSheetName.SelectedValue == "0") { lblMessage.InnerText = "warning-> Please select a Month!"; dlSheetName.Focus(); return; }
            //-----------------------------------------------------

            if (dlDepartment.SelectedItem.Text == "All") DepartmentList = GetAlllist(dlDepartment);
            else DepartmentList = dlDepartment.SelectedValue;
            if (dlDesignation.SelectedItem.Text == "All") DesignationList = GetAlllist(dlDesignation);
            else DepartmentList = dlDesignation.SelectedValue;
            sqlCmd = " SELECT  EName as FullName, ECardNo as RollNo,  SUM(CASE DATEPART(day, AttDates) WHEN 1 THEN code ELSE 0 END) AS [1], SUM(CASE DATEPART(day, AttDates) " +
                        " WHEN 2 THEN code ELSE 0 END) AS [2], SUM(CASE DATEPART(day, AttDates) WHEN 3 THEN code ELSE 0 END) AS [3], SUM(CASE DATEPART(day, AttDates) WHEN 4 THEN code ELSE 0 END) AS [4], "+
                        " SUM(CASE DATEPART(day, AttDates) WHEN 5 THEN code ELSE 0 END) AS [5], SUM(CASE DATEPART(day, AttDates) WHEN 6 THEN code ELSE 0 END) AS [6], SUM(CASE DATEPART(day, AttDates) "+
                        " WHEN 7 THEN code ELSE 0 END) AS [7], SUM(CASE DATEPART(day, AttDates) WHEN 8 THEN code ELSE 0 END) AS [8], SUM(CASE DATEPART(day, AttDates) WHEN 9 THEN code ELSE 0 END) AS [9],"+ 
                        " SUM(CASE DATEPART(day, AttDates) WHEN 10 THEN code ELSE 0 END) AS [10], SUM(CASE DATEPART(day, AttDates) WHEN 11 THEN code ELSE 0 END) AS [11], SUM(CASE DATEPART(day, AttDates) "+
                        " WHEN 12 THEN code ELSE 0 END) AS [12], SUM(CASE DATEPART(day, AttDates) WHEN 13 THEN code ELSE 0 END) AS [13], SUM(CASE DATEPART(day, AttDates) WHEN 14 THEN code ELSE 0 END) AS [14], "+
                        " SUM(CASE DATEPART(day, AttDates) WHEN 15 THEN code ELSE 0 END) AS [15], SUM(CASE DATEPART(day, AttDates) WHEN 16 THEN code ELSE 0 END) AS [16], SUM(CASE DATEPART(day, AttDates) "+
                        " WHEN 17 THEN code ELSE 0 END) AS [17], SUM(CASE DATEPART(day, AttDates) WHEN 18 THEN code ELSE 0 END) AS [18], SUM(CASE DATEPART(day, AttDates) WHEN 19 THEN code ELSE 0 END) AS [19], "+
                        " SUM(CASE DATEPART(day, AttDates) WHEN 20 THEN code ELSE 0 END) AS [20], SUM(CASE DATEPART(day, AttDates) WHEN 21 THEN code ELSE 0 END) AS [21], SUM(CASE DATEPART(day, AttDates) "+
                        " WHEN 22 THEN code ELSE 0 END) AS [22], SUM(CASE DATEPART(day, AttDates) WHEN 23 THEN code ELSE 0 END) AS [23], SUM(CASE DATEPART(day, AttDates) WHEN 24 THEN code ELSE 0 END) AS [24],"+ 
                        " SUM(CASE DATEPART(day, AttDates) WHEN 25 THEN code ELSE 0 END) AS [25], SUM(CASE DATEPART(day, AttDates) WHEN 26 THEN code ELSE 0 END) AS [26], SUM(CASE DATEPART(day, AttDates) "+
                        " WHEN 27 THEN code ELSE 0 END) AS [27], SUM(CASE DATEPART(day, AttDates) WHEN 28 THEN code ELSE 0 END) AS [28], SUM(CASE DATEPART(day, AttDates) WHEN 29 THEN code ELSE 0 END) AS [29],"+ 
                        " SUM(CASE DATEPART(day, AttDates) WHEN 30 THEN code ELSE 0 END) AS [30], SUM(CASE DATEPART(day, AttDates) WHEN 31 THEN code ELSE 0 END) AS [31], SUM(CASE Code WHEN 112 THEN 1 ELSE 0 END) "+ 
                        " AS P, SUM(CASE Code WHEN 97 THEN 1 ELSE 0 END) AS A, SUM(CASE Code WHEN 104 THEN 1 ELSE 0 END) AS H, SUM(CASE Code WHEN 119 THEN 1 ELSE 0 END) AS W, "+
                        " SUM(CASE Code WHEN 226 THEN 1 ELSE 0 END) AS LV, DName as BatchId " +
                        " FROM dbo.v_DailyEmployeeAttendanceRecord "+
                        " where DId in("+DepartmentList+") and DesId in("+DesignationList+")and ShiftId='"+ddlShiftList.SelectedValue+"' and FORMAT(AttDates,'MM-yyyy')='"+dlSheetName.SelectedValue+"'"+
                        " GROUP BY EName,ECardNo,DName";
           dt=CRUD.ReturnTableNull(sqlCmd);
         if (dt==null|| dt.Rows.Count < 1)
         {
             lblMessage.InnerText = "warning-> Any attendance Record are not founded!";
             return;
         }         
         Session["__EmpAttendanceSheet__"] = dt;
         ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=EmpAttendanceSheet-" + dlSheetName.SelectedItem.Text+ "-"+ddlShiftList.SelectedItem.Text+"-"+" "+"');", true);
            //Open New Tab for Sever side code  
        }
        private  string GetAlllist(DropDownList ddlSftList)
        {
            try
            {
                string setPredicate = "";
                for (byte b = 0; b < ddlSftList.Items.Count; b++)
                {
                    setPredicate += ddlSftList.Items[b].Value.ToString() + ",";
                }

                setPredicate = setPredicate.Remove(setPredicate.LastIndexOf(','));
                return setPredicate;
            }
            catch { return " "; }

        }
    }
}