using DS.DAL.AdviitDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Text.RegularExpressions;
using DS.BLL;

namespace DS.Forms
{
    public partial class FacultyStaffAbsentDetails : System.Web.UI.Page
    {
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
                    LoadMonths();
                    Classes.commonTask.LoadDeprtmentAtttedence(dlDepartment);
                    Classes.commonTask.LoadDesignation(dlDesignation);
                    dlName.Items.Add("All");
                }
            }
        }
        private void LoadMonths()
        {
            try
            {
                DataTable dt = new DataTable();
                sqlDB.fillDataTable("Select ASName From FacultyNStaffAttendenceSheetInfo where ASYear='"+ TimeZoneBD.getCurrentTimeBD().Year+"'", dt);
                dlSheetName.Items.Add("-Select-");
                for (byte i = 0; i < dt.Rows.Count; i++)
                {
                    string[] ASName = dt.Rows[i]["ASName"].ToString().Split('_');
                    dlSheetName.Items.Add(ASName[3]);
                }
            }
            catch(Exception ex) 
            {
                lblMessage.InnerText = "error->" + ex.Message;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {                       
            //sqlDB.fillDataTable("Select * From Faculty_Staff_AttendanceSheet_" + dlSheetName.SelectedItem.Text + "", dt);
            DataTable dt = new DataTable();   
            FilteringByDepartmentDesignationName(dt);
            reportGenerateForFiltering();
            lblMonthName.Text = "Attendance Sheet at " + dlSheetName.SelectedItem.Text;
            lblDepName.Text = "Department : " + dlDepartment.SelectedItem.Text;
            lblDesName.Text = "Designation : " + dlDesignation.SelectedItem.Text;
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

                        if (dt.Columns.Count -3 == k)
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
        private  void FilteringByDepartmentDesignationName(DataTable dt)
        {
            try
            {

                sqlDB.fillDataTable("SELECT EmployeeInfo.EName,EmployeeInfo.ECardNo,Faculty_Staff_AttendanceSheet_" + dlSheetName.SelectedValue + ".*, Departments_HR.DName, "
                +"Designations.DesName FROM Faculty_Staff_AttendanceSheet_" + dlSheetName.SelectedValue + " INNER JOIN EmployeeInfo ON Faculty_Staff_AttendanceSheet_"
                + dlSheetName.SelectedValue + ".EID = EmployeeInfo.EID INNER JOIN Designations ON EmployeeInfo.DesId = dbo.Designations.DesId INNER JOIN Departments_HR ON "
                + "EmployeeInfo.DId = Departments_HR.DId", dtView = new DataTable());

                if ((dlDepartment.SelectedItem.Text.Trim().Equals("All") && (dlDesignation.SelectedItem.Text.Trim().Equals("All")) && (dlName.SelectedItem.Text.Trim().Equals("All"))))
                     dt = dtView.Select("").CopyToDataTable();

                else if ((!dlDepartment.SelectedItem.Text.Trim().Equals("All") && (dlDesignation.SelectedItem.Text.Trim().Equals("All")) && (dlName.SelectedItem.Text.Trim().Equals("All"))))
                   dt = dtView.Select("DName='"+dlDepartment.SelectedItem.Text+"'").CopyToDataTable();

                else if ((!dlDepartment.SelectedItem.Text.Trim().Equals("All") && (dlDesignation.SelectedItem.Text.Trim().Equals("All")) && (!dlName.SelectedItem.Text.Trim().Equals("All"))))
                     dt = dtView.Select("DName='" + dlDepartment.SelectedItem.Text.Trim() + "' AND EName='"+dlName.SelectedItem.Text.Trim()+"'").CopyToDataTable();

                else if ((!dlDepartment.SelectedItem.Text.Trim().Equals("All") && (!dlDesignation.SelectedItem.Text.Trim().Equals("All")) && (dlName.SelectedItem.Text.Trim().Equals("All"))))
                     dt = dtView.Select("DName='" + dlDepartment.SelectedItem.Text.Trim() + "' AND DesName='" + dlDesignation.SelectedItem.Text.Trim() + "'").CopyToDataTable();

                else if ((!dlDepartment.SelectedItem.Text.Trim().Equals("All") && (!dlDesignation.SelectedItem.Text.Trim().Equals("All")) && (!dlName.SelectedItem.Text.Trim().Equals("All"))))
                      dt = dtView.Select("DName='" + dlDepartment.SelectedItem.Text.Trim() + "'AND DesName='"+dlDesignation.SelectedItem.Text.Trim()+"'  AND EName='" + dlName.SelectedItem.Text.Trim() + "'").CopyToDataTable();

                Session["__dt__"] = dt;
            }
            catch {  }
        }

        protected void dlDesignation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dlDepartment.SelectedItem.Text != "All"&&dlDesignation.SelectedItem.Text!="All")
            {
                DataTable dt;
                sqlDB.fillDataTable("Select EID,EName From EmployeeInfo where DesId=" + dlDesignation.SelectedValue + " and DId="+dlDepartment.SelectedValue+"", dt = new DataTable());
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
                sqlDB.fillDataTable("Select Distinct DesId,DesName From v_EmployeeInfo where DId="+dlDepartment.SelectedValue+"", dt);
                dlDesignation.DataSource = dt;
                dlDesignation.DataTextField = "DesName";
                dlDesignation.DataValueField = "DesId";
                dlDesignation.DataBind();
                dlDesignation.Items.Add("All");
                dlDesignation.SelectedIndex = dlDesignation.Items.Count - 1;
            }

        }

        protected void btnTodayAttendanceSheet_Click(object sender, EventArgs e)
        {
            try
            {

                sqlDB.fillDataTable("SELECT EmployeeInfo.EName,EmployeeInfo.ECardNo,Faculty_Staff_AttendanceSheet_" + DateTime.Today.ToString("MMMMyyyy") + ".*, "
                + "Departments_HR.DName, Designations.DesName FROM Faculty_Staff_AttendanceSheet_" + DateTime.Today.ToString("MMMMyyyy") + " INNER JOIN EmployeeInfo ON "
                +"Faculty_Staff_AttendanceSheet_" + DateTime.Today.ToString("MMMMyyyy") + ".EID = EmployeeInfo.EID INNER JOIN Designations ON EmployeeInfo.DesId = "
                + "dbo.Designations.DesId INNER JOIN Departments_HR ON EmployeeInfo.DId = Departments_HR.DId", dtView = new DataTable());
                DataTable dtMS;
                dtMS = dtView.DefaultView.ToTable(false, "ECardNo", "EName", "DName", "DesName", "D" + TimeZoneBD.getCurrentTimeBD().Date.ToString("d_M_yyyy") + "");
                dtMS = dtMS.Select(" D" + TimeZoneBD.getCurrentTimeBD().Date.ToString("d_M_yyyy") + "='p' OR D" + TimeZoneBD.getCurrentTimeBD().Date.ToString("d_M_yyyy") + "='a' ").CopyToDataTable();
                
                Session["__dt__"] = dtMS;
                ViewState["__tr__"] = "TAS"; // TAL=Today Attendance Sheet  tr=today report
                AllTodaysReportGenerate();
                lblMonthName.Text = "Today Attendance Sheet (" + TimeZoneBD.getCurrentTimeBD().ToString("d-MMM-yyyy") + ")";
                lblDepName.Text = "Department : All";
                lblDesName.Text = "Designation : All";
                Session["__ReportType__"] = "Today Attendance Sheet (" + TimeZoneBD.getCurrentTimeBD().ToString("d-MMM-yyyy") + ")";
            }
            catch { lblMessage.InnerText = "warning->Any Record is not found"; }
        }

        protected void btnTodayAttendanceList_Click(object sender, EventArgs e)
        {
            try
            {
                sqlDB.fillDataTable("SELECT EmployeeInfo.EName,EmployeeInfo.ECardNo,Faculty_Staff_AttendanceSheet_" + DateTime.Today.ToString("MMMMyyyy") + ".*, "
                + "Departments_HR.DName, Designations.DesName FROM Faculty_Staff_AttendanceSheet_" + DateTime.Today.ToString("MMMMyyyy") + " INNER JOIN EmployeeInfo ON "
                +"Faculty_Staff_AttendanceSheet_" + DateTime.Today.ToString("MMMMyyyy") + ".EID = EmployeeInfo.EID INNER JOIN Designations ON EmployeeInfo.DesId = "
                + "dbo.Designations.DesId INNER JOIN Departments_HR ON EmployeeInfo.DId = Departments_HR.DId", dtView = new DataTable());
                DataTable dtMS;
                dtMS = dtView.DefaultView.ToTable(false, "ECardNo", "EName", "DName", "DesName", "D" + TimeZoneBD.getCurrentTimeBD().Date.ToString("d_M_yyyy") + "");
                dtMS = dtMS.Select(" D" + TimeZoneBD.getCurrentTimeBD().Date.ToString("d_M_yyyy") + "='p'").CopyToDataTable();

                Session["__dt__"] = dtMS;
                ViewState["__tr__"] = "TPL"; // TAL=Today Present List  tr=today report
                AllTodaysReportGenerate();
                Session["__ReportType__"] = "Today Attendance List (" + TimeZoneBD.getCurrentTimeBD().ToString("d-MMM-yyyy") + ")";

                lblMonthName.Text = "Today Attendance List (" + TimeZoneBD.getCurrentTimeBD().ToString("d-MMM-yyyy") + ")";
                lblDepName.Text = "Department : All";
                lblDesName.Text = "Designation : All";
            }
            catch { lblMessage.InnerText = "warning->Any Record is not found"; }
        }

        protected void btnTodayAbsentList_Click(object sender, EventArgs e)
        {
            try
            {
                sqlDB.fillDataTable("SELECT EmployeeInfo.EName,EmployeeInfo.ECardNo,Faculty_Staff_AttendanceSheet_" + TimeZoneBD.getCurrentTimeBD().ToString("MMMMyyyy") + ".*, "
                + "Departments_HR.DName, Designations.DesName FROM Faculty_Staff_AttendanceSheet_" + TimeZoneBD.getCurrentTimeBD().ToString("MMMMyyyy") + " INNER JOIN EmployeeInfo ON "
                +"Faculty_Staff_AttendanceSheet_" + TimeZoneBD.getCurrentTimeBD().ToString("MMMMyyyy") + ".EID = EmployeeInfo.EID INNER JOIN Designations ON EmployeeInfo.DesId = "
                + "dbo.Designations.DesId INNER JOIN Departments_HR ON EmployeeInfo.DId = Departments_HR.DId", dtView = new DataTable());
                DataTable dtMS;
                dtMS = dtView.DefaultView.ToTable(false, "ECardNo", "EName", "DName", "DesName", "D" + TimeZoneBD.getCurrentTimeBD().Date.ToString("d_M_yyyy") + "");
                dtMS = dtMS.Select(" D" + TimeZoneBD.getCurrentTimeBD().Date.ToString("d_M_yyyy") + "='a'").CopyToDataTable();
                Session["__dt__"] = dtMS;
                ViewState["__tr__"] = "TAL"; // TAL=Today Absent List  tr=today report
                AllTodaysReportGenerate();
                Session["__ReportType__"] = "Today Absent List (" + TimeZoneBD.getCurrentTimeBD().ToString("d-MMM-yyyy") + ")";

                lblMonthName.Text = "Today Absent List (" + TimeZoneBD.getCurrentTimeBD().ToString("d-MMM-yyyy") + ")";
                lblDepName.Text = "Department : All";
                lblDesName.Text = "Designation : All";
            }
            catch 
            {
                lblMessage.InnerText = "warning->Any Record is not found";
            }
        }

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
                        if (x==0)if (!status) dt.Columns.RemoveAt(4);
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

        protected void btnPrintPreview_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/Report/FacultyStaffAttendanceReport.aspx');", true);  //Open New Tab for Sever side code
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
                    dt= new DataTable();

                    if (dlName.SelectedItem.Text == "All")
                    {
                        string getMonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(b + int.Parse(getFromDate[1]));
                        sqlDB.fillDataTable("SELECT EmployeeInfo.EName,EmployeeInfo.ECardNo,Faculty_Staff_AttendanceSheet_" + getMonthName + getFromDate[2] + ".*, "
                        + "Departments_HR.DName, Designations.DesName FROM Faculty_Staff_AttendanceSheet_" + getMonthName + getFromDate[2] + " INNER JOIN EmployeeInfo ON "
                        +"Faculty_Staff_AttendanceSheet_" + getMonthName + getFromDate[2] + ".EID = EmployeeInfo.EID INNER JOIN Designations ON EmployeeInfo.DesId = "
                        + "dbo.Designations.DesId INNER JOIN Departments_HR ON EmployeeInfo.DId = Departments_HR.DId  ", dt);
                        dtMonth.Rows.Add(getMonthName);
                    }
                    else
                    {
                        string getMonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(b + int.Parse(getFromDate[1]));
                        sqlDB.fillDataTable("SELECT EmployeeInfo.EName,EmployeeInfo.ECardNo,Faculty_Staff_AttendanceSheet_" + getMonthName + getFromDate[2] + ".*, "
                        + "Departments_HR.DName, Designations.DesName FROM Faculty_Staff_AttendanceSheet_" + getMonthName + getFromDate[2] + " INNER JOIN EmployeeInfo ON "
                        +"Faculty_Staff_AttendanceSheet_" + getMonthName + getFromDate[2] + ".EID = EmployeeInfo.EID INNER JOIN Designations ON EmployeeInfo.DesId = "
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

                    for (byte i = 3; i < ds.Tables[j].Columns.Count -2; i++)
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

                        for (byte k = 0; k < ds.Tables[j].Columns.Count -2; k++)
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


     }

 }