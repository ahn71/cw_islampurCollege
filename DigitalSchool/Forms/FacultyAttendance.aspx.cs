using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ComplexScriptingSystem;
using DS.DAL.AdviitDAL;
using System.Data.SqlClient;
using System.Data;

namespace DS.Forms
{
    public partial class FacultyAttendance : System.Web.UI.Page
    {
        DataTable dt;
        SqlDataAdapter da;
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
                    getDepartmentsList();
                    Classes.commonTask.loadMonths(dlMonths);
                }
            }
        }

        private void getDepartmentsList()
        {
            try
            {
                dlDepartments.Items.Clear();

                SQLOperation.selectBySetCommandInDatatable("Select DName,DId from Departments_HR   Order by DName ", dt = new DataTable(), sqlDB.connection);
                dlDepartments.DataValueField = "DId";
                dlDepartments.DataTextField = "DName";
                dlDepartments.DataSource = dt;
                dlDepartments.DataBind();
                dlDepartments.Items.Add("All");
                dlDepartments.SelectedIndex = dlDepartments.Items.Count;

            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
            }
        }

        protected void btnProcess_Click(object sender, EventArgs e)
        {
            lblMessage.InnerText = "";
            loadAttendanceSheet();
        }

        private void loadAttendanceSheet()
        {
            try
            {
                AttendanceSheetTitle.InnerText = "";
                string batch = new String(dlMonths.Text.Where(Char.IsNumber).ToArray());
                
                dt = new DataTable();
                da = new SqlDataAdapter();
                da = new SqlDataAdapter("SELECT Departments_HR.DName,EmployeeInfo.EName, EmployeeInfo.ECardNo,Faculty_Staff_AttendanceSheet_" + dlMonths.Text.Trim() 
                    + ".* FROM EmployeeInfo INNER JOIN Faculty_Staff_AttendanceSheet_" + dlMonths.Text.Trim() + " ON EmployeeInfo.EId = Faculty_Staff_AttendanceSheet_"
                    + dlMonths.Text.Trim() + ".EId INNER JOIN Departments_HR ON EmployeeInfo.DId = Departments_HR.DId", sqlDB.connection);
                da.Fill(dt = new DataTable());
                AttendanceSheetTitle.Style["Color"] = "green";
                AttendanceSheetTitle.InnerText = "Attendance sheet of Faculty and staff " + dlMonths.Text + "";

                if (dlDepartments.SelectedItem.Text != "All") dt = dt.Select("DName='" + dlDepartments.SelectedItem.Text.Trim() + "'").CopyToDataTable();

                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(dt.Rows.Count));
                string tbl = "";
                string tblInputElement = "";

                for (byte b = 4; b < dt.Columns.Count; b++)
                {
                    tbl += "<th style='width: 76px'>" + dt.Columns[b].ToString() + "</th>";
                }

                string tableInfo = "";
                tableInfo = "<table id='tblStudentAttendance'   >";
                tableInfo += " <th style='width: 70px'>Card No</th> <th style='width: 280px'>Name</th>" + tbl + "";

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    tblInputElement = "";
                    int row = i + 1;

                    for (byte b = 4; b < dt.Columns.Count; b++)   // this loop generate every employee inputbox 
                    {
                        if (dt.Rows[i].ItemArray[b].ToString().Equals("w") || dt.Rows[i].ItemArray[b].ToString().Equals("h")) tblInputElement 
                            += "<td  style='width: 50px'> <input autocomplete='off' style='background-color:red;text-align:center' readonly='true' tabindex=" + row 
                            + " onchange='saveData(this)' type=text id='Faculty_Staff_AttendanceSheet_" + "_" + dlMonths.Text.Trim() + ":" + dt.Columns[b].ToString() 
                            + ":" + dt.Rows[i]["EId"] + "' value=" + dt.Rows[i].ItemArray[b].ToString() + "> </td>";  // this line for hilight weekly liholyday 
                        else
                        {
                            if (dt.Rows[i].ItemArray[b].ToString().Trim().Length >= 1) tblInputElement += "<td style='width: 50px'> <input readonly='true' "
                            +"style='text-align:center' autocomplete='off' tabindex=" + row + " onchange='saveData(this)' type=text id='Faculty_Staff_AttendanceSheet_"
                            + dlMonths.Text.Trim() + ":" + dt.Columns[b].ToString() + ":" + dt.Rows[i]["EId"] + ":" + batch + "' value=" 
                            + dt.Rows[i].ItemArray[b].ToString() + "> </td>";
                            else tblInputElement += "<td style='width: 50px'> <input style='text-align:center' autocomplete='off' tabindex=" + row 
                                + " onchange='saveData(this)' type=text id='Faculty_Staff_AttendanceSheet_" + dlMonths.Text.Trim() + ":" + dt.Columns[b].ToString() 
                                + ":" + dt.Rows[i]["EId"] + ":" + batch + "' value=" + dt.Rows[i].ItemArray[b].ToString() + "> </td>";
                        }
                        row += dt.Rows.Count;
                    }
                    tableInfo += "<tr> <td style='width: 80px'> " + dt.Rows[i]["ECardNo"].ToString() + "</td>  <td style='width: 60px'>" + dt.Rows[i]["EName"].ToString() 
                        + "</td>" + tblInputElement + "</tr>";
                }
                tableInfo += "</table>";
                divTable.Controls.Add(new LiteralControl(tableInfo));
                divTable.Visible = true;
            }
            catch
            {
                AttendanceSheetTitle.Style["Color"] = "Red";
                AttendanceSheetTitle.InnerText = "Sorry this attendance sheet is not created";
                divTable.Visible = false;
            }
        }
        protected void btnPreview_Click(object sender, EventArgs e)
        {
            AttendanceSheetTitle.InnerText = "";
            lblMessage.InnerText = "";
            if (reportGenerateForFiltering() == true)
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/Report/FacultyStaffAttendanceReport.aspx');", true);  //Open New Tab for Sever side code
            else
            {
               // AttendanceSheetTitle.Style["Color"] = "Red";
                lblMessage.InnerText = "warning->Sorry this attendance sheet is not created";
              //  divTable.Visible = false;
            }
        }
        private Boolean reportGenerateForFiltering()
        {
            try
            {
                DataTable dt = new DataTable();
                //dt = (DataTable)Session["__dt__"];
                if (dlDepartments.SelectedItem.Text == "All") sqlDB.fillDataTable("SELECT EmployeeInfo.EName,EmployeeInfo.ECardNo,Faculty_Staff_AttendanceSheet_"
                    + dlMonths.SelectedItem.Text + ".*, Departments_HR.DName, Designations.DesName FROM Faculty_Staff_AttendanceSheet_" + dlMonths.SelectedItem.Text 
                    + " INNER JOIN EmployeeInfo ON Faculty_Staff_AttendanceSheet_" + dlMonths.SelectedItem.Text + ".EID = EmployeeInfo.EID INNER JOIN Designations "
                + "ON EmployeeInfo.DesId = dbo.Designations.DesId INNER JOIN Departments_HR ON EmployeeInfo.DId = Departments_HR.DId ", dt);
                else
                sqlDB.fillDataTable("SELECT EmployeeInfo.EName,EmployeeInfo.ECardNo,Faculty_Staff_AttendanceSheet_" + dlMonths.SelectedItem.Text
                    + ".*, Departments_HR.DName, Designations.DesName FROM Faculty_Staff_AttendanceSheet_" + dlMonths.SelectedItem.Text + " INNER JOIN EmployeeInfo ON "
                +"Faculty_Staff_AttendanceSheet_" + dlMonths.SelectedItem.Text + ".EID = EmployeeInfo.EID INNER JOIN Designations ON EmployeeInfo.DesId = "
                + "dbo.Designations.DesId INNER JOIN Departments_HR ON EmployeeInfo.DId = Departments_HR.DId where Departments_HR.DName='" + dlDepartments.SelectedItem.Text + "'", dt);
                int totalRows = dt.Rows.Count;
                string divInfo = "";
                string divInfoReport = "";

                if (totalRows == 0)
                {
                    divInfo = "<div class='noData'>No Student available</div>";
                    divInfo += "<div><div class='head'></div></div>";
                    AttendanceSheetTitle.InnerText = "Sorry this attendance sheet is not created";
                    return false;
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

               // divMonthWiseAttendaceSheet.Controls.Add(new LiteralControl(divInfo));
                Session["__FacultyReport__"] = divInfoReport;
                Session["__ReportType__"] = "Attendance Sheet at " + dlMonths.SelectedItem.Text;

                Session["__Department__"] = "Department : " + dlDepartments.SelectedItem.Text;
                Session["Designation"] = "Designation : " + "All";
                Session["__MonthName__"] = dlMonths.SelectedItem.Text;
                return true;
            }
            catch { return false; }
        }
    }
}