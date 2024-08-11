using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DS.DAL.AdviitDAL;

namespace DS.UI.Academics.Attendance.StafforFaculty.Machine
{
    public partial class Machine_Staff_Atten_Details : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                if (!IsPostBack)
                {
                    Classes.commonTask.loadMonths(dlMonth);
                    Classes.commonTask.LoadDeprtmentAtttedence(dlDepartment);
                    Classes.commonTask.LoadDesignation(dlDesignation);
                    dlName.Items.Add("All");
                }
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
                dlDesignation.Items.Add("All");
                dlDesignation.SelectedIndex = dlDesignation.Items.Count - 1;
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //sqlDB.fillDataTable("Select * From Faculty_Staff_AttendanceSheet_" + dlSheetName.SelectedItem.Text + "", dt);

            string checktable = "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Machine_AttendanceSheet]') AND type in (N'U')) BEGIN Drop Table Machine_AttendanceSheet END";
            SqlCommand cmdMA = new SqlCommand(checktable, sqlDB.connection);
            cmdMA.ExecuteNonQuery();


            string getYear = new String(dlMonth.Text.Where(Char.IsNumber).ToArray());
            string getMonth = new String(dlMonth.Text.Where(Char.IsLetter).ToArray());
            string date = getMonth + ", 01 " + getYear;
            DateTime d = DateTime.ParseExact(date, "MMMM, dd yyyy", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None);
            int days = DateTime.DaysInMonth(d.Year, d.Month);

            createTtable(days, d);
            DataTable dt = new DataTable();
            if ((dlDepartment.SelectedItem.Text.Trim().Equals("All") && (dlDesignation.SelectedItem.Text.Trim().Equals("All")) && (dlName.SelectedItem.Text.Trim().Equals("All"))))
                sqlDB.fillDataTable("Select Format(DailyAttendanceRecord.AttDate,'yyyy-MM-dd') as AttDate,DailyAttendanceRecord.AttStatus,EmployeeInfo.EID From DailyAttendanceRecord INNER Join EmployeeInfo On DailyAttendanceRecord.EID=EmployeeInfo.EID ", dt);

            else if ((!dlDepartment.SelectedItem.Text.Trim().Equals("All") && (dlDesignation.SelectedItem.Text.Trim().Equals("All")) && (dlName.SelectedItem.Text.Trim().Equals("All"))))
                sqlDB.fillDataTable("Select Format(DailyAttendanceRecord.AttDate,'yyyy-MM-dd') as AttDate,DailyAttendanceRecord.AttStatus,EmployeeInfo.EID From DailyAttendanceRecord INNER Join EmployeeInfo On DailyAttendanceRecord.EID=EmployeeInfo.EID INNER Join Departments_HR ON EmployeeInfo.DId=Departments_HR.DId where Departments_HR.DName='" + dlDepartment.SelectedItem.Text + "'", dt);

            else if ((!dlDepartment.SelectedItem.Text.Trim().Equals("All") && (dlDesignation.SelectedItem.Text.Trim().Equals("All")) && (!dlName.SelectedItem.Text.Trim().Equals("All"))))

                sqlDB.fillDataTable("Select Format(DailyAttendanceRecord.AttDate,'yyyy-MM-dd') as AttDate,DailyAttendanceRecord.AttStatus,EmployeeInfo.EID From DailyAttendanceRecord INNER Join EmployeeInfo On DailyAttendanceRecord.EID=EmployeeInfo.EID INNER Join Departments_HR ON EmployeeInfo.DId=Departments_HR.DId where Departments_HR.DName='" + dlDepartment.SelectedItem.Text + "' and EmployeeInfo.EName='" + dlName.SelectedItem.Text + "'", dt);

            else if ((!dlDepartment.SelectedItem.Text.Trim().Equals("All") && (!dlDesignation.SelectedItem.Text.Trim().Equals("All")) && (dlName.SelectedItem.Text.Trim().Equals("All"))))

                sqlDB.fillDataTable("Select Format(DailyAttendanceRecord.AttDate,'yyyy-MM-dd') as AttDate,DailyAttendanceRecord.AttStatus,EmployeeInfo.EID From DailyAttendanceRecord INNER Join EmployeeInfo On DailyAttendanceRecord.EID=EmployeeInfo.EID INNER Join Departments_HR ON EmployeeInfo.DId=Departments_HR.DId INNER JOIN Designations ON EmployeeInfo.DesId=Designations.DesId where Departments_HR.DName='" + dlDepartment.SelectedItem.Text + "' and Designations.DesName='" + dlDesignation.SelectedItem.Text + "'", dt);

            else if ((!dlDepartment.SelectedItem.Text.Trim().Equals("All") && (!dlDesignation.SelectedItem.Text.Trim().Equals("All")) && (!dlName.SelectedItem.Text.Trim().Equals("All"))))
                sqlDB.fillDataTable("Select Format(DailyAttendanceRecord.AttDate,'yyyy-MM-dd') as AttDate,DailyAttendanceRecord.AttStatus,EmployeeInfo.EID From DailyAttendanceRecord INNER Join EmployeeInfo On DailyAttendanceRecord.EID=EmployeeInfo.EID INNER Join Departments_HR ON EmployeeInfo.DId=Departments_HR.DId INNER JOIN Designations ON EmployeeInfo.DesId=Designations.DesId where Departments_HR.DName='" + dlDepartment.SelectedItem.Text + "' and Designations.DesName='" + dlDesignation.SelectedItem.Text + "' and EmployeeInfo.EName='" + dlName.SelectedItem.Text + "'", dt);

            for (int m = 0; m < dt.Rows.Count; m++)
            {
                string[] getdate = dt.Rows[m]["AttDate"].ToString().Split('-');
                int day = Convert.ToInt32(getdate[2]);
                int Month = Convert.ToInt32(getdate[1]);
                SqlCommand cmdUMA = new SqlCommand("Update Machine_AttendanceSheet set D" + day + "_" + Month + "_" + getdate[0] + "='" + dt.Rows[m]["AttStatus"].ToString() + "' where EID=" + dt.Rows[m]["EID"].ToString() + "", sqlDB.connection);
                cmdUMA.ExecuteNonQuery();
            }
            dt = new DataTable();
            FilteringByDepartmentDesignationName(dt);
            reportGenerateForFiltering();
            lblMonthName.Text = "Attendance Sheet at " + dlMonth.SelectedItem.Text;
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
                Session["__ReportType__"] = "Attendance Sheet at " + dlMonth.SelectedItem.Text;

                Session["__Department__"] = "Department : " + dlDepartment.SelectedItem.Text;
                Session["Designation"] = "Designation : " + dlDesignation.SelectedItem.Text;
                Session["__MonthName__"] = dlMonth.SelectedItem.Text;
            }
            catch { }
        }

        DataTable dtView;
        private void FilteringByDepartmentDesignationName(DataTable dt)
        {
            try
            {

                sqlDB.fillDataTable("SELECT EmployeeInfo.EName,EmployeeInfo.ECardNo,Machine_AttendanceSheet.*, Departments_HR.DName, Designations.DesName FROM Machine_AttendanceSheet INNER JOIN EmployeeInfo ON Machine_AttendanceSheet.EID = EmployeeInfo.EID INNER JOIN Designations ON EmployeeInfo.DesId = dbo.Designations.DesId INNER JOIN Departments_HR ON EmployeeInfo.DId = Departments_HR.DId", dtView = new DataTable());

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
        private void createTtable(int days, DateTime ddMMyyyy)
        {
            try
            {
                SqlCommand cmd;
                DataTable dt;
                ArrayList dateFilds = new ArrayList();
                string getYear = ddMMyyyy.Year.ToString();
                string dateField = "";

                for (byte b = 1; b <= days; b++)
                {
                    dateFilds.Add("D" + b + "_" + ddMMyyyy.Month + "_" + getYear);
                    if (b == days) dateField += "D" + b + "_" + ddMMyyyy.Month + "_" + getYear + " varchar(16)";
                    else dateField += "D" + b + "_" + ddMMyyyy.Month + "_" + getYear + " varchar(16),";
                }

                dt = new DataTable();
                cmd = new SqlCommand("CREATE TABLE Machine_AttendanceSheet ( EID bigint ," + dateField + ",Foreign Key (EID) REFERENCES EmployeeInfo(EID) On Update Cascade On Delete Cascade)", sqlDB.connection);
                int result = cmd.ExecuteNonQuery();
                dt = new DataTable();

                if ((dlDepartment.SelectedItem.Text.Trim().Equals("All") && (dlDesignation.SelectedItem.Text.Trim().Equals("All")) && (dlName.SelectedItem.Text.Trim().Equals("All"))))
                    sqlDB.fillDataTable("select EID from v_EmployeeInfo ", dt);

                else if ((!dlDepartment.SelectedItem.Text.Trim().Equals("All") && (dlDesignation.SelectedItem.Text.Trim().Equals("All")) && (dlName.SelectedItem.Text.Trim().Equals("All"))))
                    sqlDB.fillDataTable("select EID from v_EmployeeInfo where DName='" + dlDepartment.SelectedItem.Text + "'", dt);

                else if ((!dlDepartment.SelectedItem.Text.Trim().Equals("All") && (dlDesignation.SelectedItem.Text.Trim().Equals("All")) && (!dlName.SelectedItem.Text.Trim().Equals("All"))))

                    sqlDB.fillDataTable("select EID from v_EmployeeInfo where DName='" + dlDepartment.SelectedItem.Text + "'AND EName='" + dlName.SelectedItem.Text.Trim() + "'", dt);

                else if ((!dlDepartment.SelectedItem.Text.Trim().Equals("All") && (!dlDesignation.SelectedItem.Text.Trim().Equals("All")) && (dlName.SelectedItem.Text.Trim().Equals("All"))))

                    sqlDB.fillDataTable("select EID from v_EmployeeInfo where DName='" + dlDepartment.SelectedItem.Text.Trim() + "' AND DesName='" + dlDesignation.SelectedItem.Text.Trim() + "'", dt);

                else if ((!dlDepartment.SelectedItem.Text.Trim().Equals("All") && (!dlDesignation.SelectedItem.Text.Trim().Equals("All")) && (!dlName.SelectedItem.Text.Trim().Equals("All"))))
                    sqlDB.fillDataTable("select EID from v_EmployeeInfo where DName='" + dlDepartment.SelectedItem.Text.Trim() + "'AND DesName='" + dlDesignation.SelectedItem.Text.Trim() + "'  AND EName='" + dlName.SelectedItem.Text.Trim() + "'", dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cmd = new SqlCommand("insert into Machine_AttendanceSheet (EID) values (" + dt.Rows[i]["EID"] + ")", sqlDB.connection);
                    cmd.ExecuteNonQuery();
                }

                Read_N_Write_WH(days, getYear, ddMMyyyy.Month);


            }
            catch { }
        }
        private void Read_N_Write_WH(int days, string year, int Month)    // N=And,WH=Weekly Holiday
        {
            try
            {
                SqlCommand cmd;
                DateTime begin = new DateTime(int.Parse(year), Month, 1);
                DateTime end = new DateTime(int.Parse(year), Month, days);
                DataTable dt = new DataTable();
                DataTable dtDate;
                sqlDB.fillDataTable("Select OffDate,Purpose From OffdaySettings where OffDate BETWEEN '" + begin.ToString("yyyy-MM-dd") + "' and '" + end.ToString("yyyy-MM-dd") + "'", dt);
                while (begin <= end)
                {
                    dtDate = new DataTable();
                    try
                    {
                        dtDate = dt.Select(" OffDate='" + begin.ToString("yyyy-MM-dd") + "'").CopyToDataTable();
                    }
                    catch { }
                    if (dtDate.Rows.Count > 0)
                    {
                        string Offday = "";
                        if (dtDate.Rows[0]["Purpose"].ToString() == "Weekly Holiday")
                        {
                            Offday = "w";
                        }
                        else
                        {
                            Offday = "h";
                        }

                        string wh = begin.ToString("d-M-yyyy");
                        string[] whd = wh.Split('-');
                        cmd = new SqlCommand("update  Machine_AttendanceSheet set " + "D" + whd[0] + "_" + whd[1] + "_" + whd[2] + "='" + Offday + "'", sqlDB.connection);
                        cmd.ExecuteNonQuery();

                    }
                    begin = begin.AddDays(1);

                }
            }
            catch { }
        }

        protected void btnTodayAttendanceSheet_Click(object sender, EventArgs e)
        {
            try
            {

                string checktable = "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Machine_AttendanceSheet]') AND type in (N'U')) BEGIN Drop Table Machine_AttendanceSheet END";
                SqlCommand cmdMA = new SqlCommand(checktable, sqlDB.connection);
                cmdMA.ExecuteNonQuery();
                int days = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);

                createTtable(days, DateTime.Now);
                DataTable dt = new DataTable();
                sqlDB.fillDataTable("Select Format(DailyAttendanceRecord.AttDate,'yyyy-MM-dd') as AttDate,DailyAttendanceRecord.AttStatus,EmployeeInfo.EID From DailyAttendanceRecord INNER Join EmployeeInfo On DailyAttendanceRecord.EID=EmployeeInfo.EID ", dt);

                for (int m = 0; m < dt.Rows.Count; m++)
                {
                    string[] getdate = dt.Rows[m]["AttDate"].ToString().Split('-');
                    int day = Convert.ToInt32(getdate[2]);
                    int Month = Convert.ToInt32(getdate[1]);
                    SqlCommand cmdUMA = new SqlCommand("Update Machine_AttendanceSheet set D" + day + "_" + Month + "_" + getdate[0] + "='" + dt.Rows[m]["AttStatus"].ToString() + "' where EID=" + dt.Rows[m]["EID"].ToString() + "", sqlDB.connection);
                    cmdUMA.ExecuteNonQuery();
                }

                sqlDB.fillDataTable("SELECT EmployeeInfo.EName,EmployeeInfo.ECardNo,Machine_AttendanceSheet.*, Departments_HR.DName, Designations.DesName FROM Machine_AttendanceSheet INNER JOIN EmployeeInfo ON Machine_AttendanceSheet.EID = EmployeeInfo.EID INNER JOIN Designations ON EmployeeInfo.DesId = dbo.Designations.DesId INNER JOIN Departments_HR ON EmployeeInfo.DId = Departments_HR.DId", dtView = new DataTable());
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
                Session["__Department__"] = "Department : All";
                Session["Designation"] = "Designation : All";
                // Session["__MonthName__"] = dlMonth.SelectedItem.Text;

            }
            catch { }
        }

        protected void btnTodayAttendanceList_Click(object sender, EventArgs e)
        {
            try
            {
                string checktable = "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Machine_AttendanceSheet]') AND type in (N'U')) BEGIN Drop Table Machine_AttendanceSheet END";
                SqlCommand cmdMA = new SqlCommand(checktable, sqlDB.connection);
                cmdMA.ExecuteNonQuery();
                int days = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);

                createTtable(days, DateTime.Now);
                DataTable dt = new DataTable();
                sqlDB.fillDataTable("Select Format(DailyAttendanceRecord.AttDate,'yyyy-MM-dd') as AttDate,DailyAttendanceRecord.AttStatus,EmployeeInfo.EID From DailyAttendanceRecord INNER Join EmployeeInfo On DailyAttendanceRecord.EID=EmployeeInfo.EID ", dt);

                for (int m = 0; m < dt.Rows.Count; m++)
                {
                    string[] getdate = dt.Rows[m]["AttDate"].ToString().Split('-');
                    int day = Convert.ToInt32(getdate[2]);
                    int Month = Convert.ToInt32(getdate[1]);
                    SqlCommand cmdUMA = new SqlCommand("Update Machine_AttendanceSheet set D" + day + "_" + Month + "_" + getdate[0] + "='" + dt.Rows[m]["AttStatus"].ToString() + "' where EID=" + dt.Rows[m]["EID"].ToString() + "", sqlDB.connection);
                    cmdUMA.ExecuteNonQuery();
                }
                sqlDB.fillDataTable("SELECT EmployeeInfo.EName,EmployeeInfo.ECardNo,Machine_AttendanceSheet.*, Departments_HR.DName, Designations.DesName FROM Machine_AttendanceSheet INNER JOIN EmployeeInfo ON Machine_AttendanceSheet.EID = EmployeeInfo.EID INNER JOIN Designations ON EmployeeInfo.DesId = dbo.Designations.DesId INNER JOIN Departments_HR ON EmployeeInfo.DId = Departments_HR.DId", dtView = new DataTable());
                DataTable dtMS;
                dtMS = dtView.DefaultView.ToTable(false, "ECardNo", "EName", "DName", "DesName", "D" + DateTime.Now.Date.ToString("d_M_yyyy") + "");
                dtMS = dtMS.Select(" D" + DateTime.Now.Date.ToString("d_M_yyyy") + "='p'").CopyToDataTable();
                Session["__dt__"] = dtMS;
                ViewState["__tr__"] = "TAL"; // TAL=Today Absent List  tr=today report
                AllTodaysReportGenerate();
                Session["__ReportType__"] = "Today Absent List (" + DateTime.Now.ToString("d-MMM-yyyy") + ")";

                lblMonthName.Text = "Today Attendance List (" + DateTime.Now.ToString("d-MMM-yyyy") + ")";
                lblDepName.Text = "Department : All";
                lblDesName.Text = "Designation : All";
            }
            catch
            {
                lblMessage.InnerText = "warning->Any Record is not found";
            }
        }

        protected void btnTodayAbsentList_Click(object sender, EventArgs e)
        {
            try
            {
                string checktable = "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Machine_AttendanceSheet]') AND type in (N'U')) BEGIN Drop Table Machine_AttendanceSheet END";
                SqlCommand cmdMA = new SqlCommand(checktable, sqlDB.connection);
                cmdMA.ExecuteNonQuery();
                int days = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);

                createTtable(days, DateTime.Now);
                DataTable dt = new DataTable();
                sqlDB.fillDataTable("Select Format(DailyAttendanceRecord.AttDate,'yyyy-MM-dd') as AttDate,DailyAttendanceRecord.AttStatus,EmployeeInfo.EID From DailyAttendanceRecord INNER Join EmployeeInfo On DailyAttendanceRecord.EID=EmployeeInfo.EID ", dt);

                for (int m = 0; m < dt.Rows.Count; m++)
                {
                    string[] getdate = dt.Rows[m]["AttDate"].ToString().Split('-');
                    int day = Convert.ToInt32(getdate[2]);
                    int Month = Convert.ToInt32(getdate[1]);
                    SqlCommand cmdUMA = new SqlCommand("Update Machine_AttendanceSheet set D" + day + "_" + Month + "_" + getdate[0] + "='" + dt.Rows[m]["AttStatus"].ToString() + "' where EID=" + dt.Rows[m]["EID"].ToString() + "", sqlDB.connection);
                    cmdUMA.ExecuteNonQuery();
                }
                sqlDB.fillDataTable("SELECT EmployeeInfo.EName,EmployeeInfo.ECardNo,Machine_AttendanceSheet.*, Departments_HR.DName, Designations.DesName FROM Machine_AttendanceSheet INNER JOIN EmployeeInfo ON Machine_AttendanceSheet.EID = EmployeeInfo.EID INNER JOIN Designations ON EmployeeInfo.DesId = dbo.Designations.DesId INNER JOIN Departments_HR ON EmployeeInfo.DId = Departments_HR.DId", dtView = new DataTable());
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
                DateTime dtFromdate = new DateTime(int.Parse(getFromDate[2]), int.Parse(getFromDate[1]), int.Parse(getFromDate[0]));
                DateTime dtTodate = new DateTime(int.Parse(getToDate[2]), int.Parse(getToDate[1]), int.Parse(getToDate[0]));
                getMonth = (dtTodate.Year * 12 + dtTodate.Month + 1) - (dtFromdate.Year * 12 + dtFromdate.Month);

                DataSet ds = new DataSet();

                for (byte b = 0; b < getMonth; b++)
                {
                    string checktable = "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Machine_AttendanceSheet]') AND type in (N'U')) BEGIN Drop Table Machine_AttendanceSheet END";
                    SqlCommand cmdMA = new SqlCommand(checktable, sqlDB.connection);
                    cmdMA.ExecuteNonQuery();
                    DateTime FD;
                    if (b == 0)
                    {
                        FD = dtFromdate;
                    }
                    else
                    {
                        FD = dtFromdate.AddMonths(1);
                    }
                    int days = DateTime.DaysInMonth(FD.Year, FD.Month);
                    createTtable(days, FD);

                    DateTime begin = new DateTime(FD.Year, FD.Month, 1);
                    DateTime End = begin.AddMonths(1);
                    End = End.AddDays(-1);
                    dt = new DataTable();
                    string getMonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(b + int.Parse(getFromDate[1]));
                    if ((dlDepartment.SelectedItem.Text.Trim().Equals("All") && (dlDesignation.SelectedItem.Text.Trim().Equals("All")) && (dlName.SelectedItem.Text.Trim().Equals("All"))))
                    {
                        sqlDB.fillDataTable("Select Format(DailyAttendanceRecord.AttDate,'yyyy-MM-dd') as AttDate,DailyAttendanceRecord.AttStatus,EmployeeInfo.EID From DailyAttendanceRecord INNER Join EmployeeInfo On DailyAttendanceRecord.EID=EmployeeInfo.EID where DailyAttendanceRecord.AttDate between '" + begin.ToString("yyyy-MM-dd") + "' and '" + End.ToString("yyyy-MM-dd") + "' ", dt);
                        for (int m = 0; m < dt.Rows.Count; m++)
                        {
                            string[] getdate = dt.Rows[m]["AttDate"].ToString().Split('-');
                            int day = Convert.ToInt32(getdate[2]);
                            int Month = Convert.ToInt32(getdate[1]);
                            SqlCommand cmdUMA = new SqlCommand("Update Machine_AttendanceSheet set D" + day + "_" + Month + "_" + getdate[0] + "='" + dt.Rows[m]["AttStatus"].ToString() + "' where EID=" + dt.Rows[m]["EID"].ToString() + "", sqlDB.connection);
                            cmdUMA.ExecuteNonQuery();
                        }
                        sqlDB.fillDataTable("SELECT EmployeeInfo.EName,EmployeeInfo.ECardNo,Machine_AttendanceSheet.*, Departments_HR.DName, Designations.DesName FROM Machine_AttendanceSheet INNER JOIN EmployeeInfo ON Machine_AttendanceSheet.EID = EmployeeInfo.EID INNER JOIN Designations ON EmployeeInfo.DesId = dbo.Designations.DesId INNER JOIN Departments_HR ON EmployeeInfo.DId = Departments_HR.DId  ", dt = new DataTable());
                        dtMonth.Rows.Add(getMonthName);

                    }

                    else if ((!dlDepartment.SelectedItem.Text.Trim().Equals("All") && (dlDesignation.SelectedItem.Text.Trim().Equals("All")) && (dlName.SelectedItem.Text.Trim().Equals("All"))))
                    {
                        sqlDB.fillDataTable("Select Format(DailyAttendanceRecord.AttDate,'yyyy-MM-dd') as AttDate,DailyAttendanceRecord.AttStatus,EmployeeInfo.EID From DailyAttendanceRecord INNER Join EmployeeInfo On DailyAttendanceRecord.EID=EmployeeInfo.EID INNER JOIN Departments_HR ON Departments_HR.DId=EmployeeInfo.DId  where Departments_HR.DName='" + dlDepartment.SelectedItem.Text + "' and DailyAttendanceRecord.AttDate between '" + begin.ToString("yyyy-MM-dd") + "' and '" + End.ToString("yyyy-MM-dd") + "'", dt);

                        for (int m = 0; m < dt.Rows.Count; m++)
                        {
                            string[] getdate = dt.Rows[m]["AttDate"].ToString().Split('-');
                            int day = Convert.ToInt32(getdate[2]);
                            int Month = Convert.ToInt32(getdate[1]);
                            SqlCommand cmdUMA = new SqlCommand("Update Machine_AttendanceSheet set D" + day + "_" + Month + "_" + getdate[0] + "='" + dt.Rows[m]["AttStatus"].ToString() + "' where EID=" + dt.Rows[m]["EID"].ToString() + "", sqlDB.connection);
                            cmdUMA.ExecuteNonQuery();
                        }
                        sqlDB.fillDataTable("SELECT EmployeeInfo.EName,EmployeeInfo.ECardNo,Machine_AttendanceSheet.*, Departments_HR.DName, Designations.DesName FROM Machine_AttendanceSheet INNER JOIN EmployeeInfo ON Machine_AttendanceSheet.EID = EmployeeInfo.EID INNER JOIN Designations ON EmployeeInfo.DesId = dbo.Designations.DesId INNER JOIN Departments_HR ON EmployeeInfo.DId = Departments_HR.DId  where Departments_HR.DName='" + dlDepartment.SelectedItem.Text + "'  ", dt = new DataTable());
                        dtMonth.Rows.Add(getMonthName);
                    }

                    else if ((!dlDepartment.SelectedItem.Text.Trim().Equals("All") && (dlDesignation.SelectedItem.Text.Trim().Equals("All")) && (!dlName.SelectedItem.Text.Trim().Equals("All"))))
                    {
                        sqlDB.fillDataTable("Select Format(DailyAttendanceRecord.AttDate,'yyyy-MM-dd') as AttDate,DailyAttendanceRecord.AttStatus,EmployeeInfo.EID From DailyAttendanceRecord INNER Join EmployeeInfo On DailyAttendanceRecord.EID=EmployeeInfo.EID INNER JOIN Departments_HR ON Departments_HR.DId=EmployeeInfo.DId  where Departments_HR.DName='" + dlDepartment.SelectedItem.Text + "' AND EName='" + dlName.SelectedItem.Text.Trim() + "' and DailyAttendanceRecord.AttDate between '" + begin.ToString("yyyy-MM-dd") + "' and '" + End.ToString("yyyy-MM-dd") + "'", dt);

                        for (int m = 0; m < dt.Rows.Count; m++)
                        {
                            string[] getdate = dt.Rows[m]["AttDate"].ToString().Split('-');
                            int day = Convert.ToInt32(getdate[2]);
                            int Month = Convert.ToInt32(getdate[1]);
                            SqlCommand cmdUMA = new SqlCommand("Update Machine_AttendanceSheet set D" + day + "_" + Month + "_" + getdate[0] + "='" + dt.Rows[m]["AttStatus"].ToString() + "' where EID=" + dt.Rows[m]["EID"].ToString() + "", sqlDB.connection);
                            cmdUMA.ExecuteNonQuery();
                        }
                        sqlDB.fillDataTable("SELECT EmployeeInfo.EName,EmployeeInfo.ECardNo,Machine_AttendanceSheet.*, Departments_HR.DName, Designations.DesName FROM Machine_AttendanceSheet INNER JOIN EmployeeInfo ON Machine_AttendanceSheet.EID = EmployeeInfo.EID INNER JOIN Designations ON EmployeeInfo.DesId = dbo.Designations.DesId INNER JOIN Departments_HR ON EmployeeInfo.DId = Departments_HR.DId where Departments_HR.DName='" + dlDepartment.SelectedItem.Text + "' AND EName='" + dlName.SelectedItem.Text.Trim() + "' ", dt = new DataTable());
                        dtMonth.Rows.Add(getMonthName);
                    }
                    else if ((!dlDepartment.SelectedItem.Text.Trim().Equals("All") && (!dlDesignation.SelectedItem.Text.Trim().Equals("All")) && (dlName.SelectedItem.Text.Trim().Equals("All"))))
                    {
                        sqlDB.fillDataTable("Select Format(DailyAttendanceRecord.AttDate,'yyyy-MM-dd') as AttDate,DailyAttendanceRecord.AttStatus,EmployeeInfo.EID From DailyAttendanceRecord INNER Join EmployeeInfo On DailyAttendanceRecord.EID=EmployeeInfo.EID INNER JOIN Departments_HR ON Departments_HR.DId=EmployeeInfo.DId  INNER JOIN Designations ON Designations.DesId=EmployeeInfo.DesId where DName='" + dlDepartment.SelectedItem.Text.Trim() + "' AND DesName='" + dlDesignation.SelectedItem.Text.Trim() + "' and DailyAttendanceRecord.AttDate between '" + begin.ToString("yyyy-MM-dd") + "' and '" + End.ToString("yyyy-MM-dd") + "'", dt);

                        for (int m = 0; m < dt.Rows.Count; m++)
                        {
                            string[] getdate = dt.Rows[m]["AttDate"].ToString().Split('-');
                            int day = Convert.ToInt32(getdate[2]);
                            int Month = Convert.ToInt32(getdate[1]);
                            SqlCommand cmdUMA = new SqlCommand("Update Machine_AttendanceSheet set D" + day + "_" + Month + "_" + getdate[0] + "='" + dt.Rows[m]["AttStatus"].ToString() + "' where EID=" + dt.Rows[m]["EID"].ToString() + "", sqlDB.connection);
                            cmdUMA.ExecuteNonQuery();
                        }
                        sqlDB.fillDataTable("SELECT EmployeeInfo.EName,EmployeeInfo.ECardNo,Machine_AttendanceSheet.*, Departments_HR.DName, Designations.DesName FROM Machine_AttendanceSheet INNER JOIN EmployeeInfo ON Machine_AttendanceSheet.EID = EmployeeInfo.EID INNER JOIN Designations ON EmployeeInfo.DesId = dbo.Designations.DesId INNER JOIN Departments_HR ON EmployeeInfo.DId = Departments_HR.DId where DName='" + dlDepartment.SelectedItem.Text.Trim() + "' AND DesName='" + dlDesignation.SelectedItem.Text.Trim() + "' ", dt = new DataTable());
                        dtMonth.Rows.Add(getMonthName);
                    }
                    else if ((!dlDepartment.SelectedItem.Text.Trim().Equals("All") && (!dlDesignation.SelectedItem.Text.Trim().Equals("All")) && (!dlName.SelectedItem.Text.Trim().Equals("All"))))
                    {
                        sqlDB.fillDataTable("Select Format(DailyAttendanceRecord.AttDate,'yyyy-MM-dd') as AttDate,DailyAttendanceRecord.AttStatus,EmployeeInfo.EID From DailyAttendanceRecord INNER Join EmployeeInfo On DailyAttendanceRecord.EID=EmployeeInfo.EID INNER JOIN Departments_HR ON Departments_HR.DId=EmployeeInfo.DId  INNER JOIN Designations ON Designations.DesId=EmployeeInfo.DesId where DName='" + dlDepartment.SelectedItem.Text.Trim() + "'AND DesName='" + dlDesignation.SelectedItem.Text.Trim() + "'  AND EName='" + dlName.SelectedItem.Text.Trim() + "' and DailyAttendanceRecord.AttDate between '" + begin.ToString("yyyy-MM-dd") + "' and '" + End.ToString("yyyy-MM-dd") + "'", dt);

                        for (int m = 0; m < dt.Rows.Count; m++)
                        {
                            string[] getdate = dt.Rows[m]["AttDate"].ToString().Split('-');
                            int day = Convert.ToInt32(getdate[2]);
                            int Month = Convert.ToInt32(getdate[1]);
                            SqlCommand cmdUMA = new SqlCommand("Update Machine_AttendanceSheet set D" + day + "_" + Month + "_" + getdate[0] + "='" + dt.Rows[m]["AttStatus"].ToString() + "' where EID=" + dt.Rows[m]["EID"].ToString() + "", sqlDB.connection);
                            cmdUMA.ExecuteNonQuery();
                        }
                        sqlDB.fillDataTable("SELECT EmployeeInfo.EName,EmployeeInfo.ECardNo,Machine_AttendanceSheet.*, Departments_HR.DName, Designations.DesName FROM Machine_AttendanceSheet INNER JOIN EmployeeInfo ON Machine_AttendanceSheet.EID = EmployeeInfo.EID INNER JOIN Designations ON EmployeeInfo.DesId = dbo.Designations.DesId INNER JOIN Departments_HR ON EmployeeInfo.DId = Departments_HR.DId where DName='" + dlDepartment.SelectedItem.Text.Trim() + "'AND DesName='" + dlDesignation.SelectedItem.Text.Trim() + "'  AND EName='" + dlName.SelectedItem.Text.Trim() + "' ", dt = new DataTable());
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
                    Session["__ReportType__"] = "Today Attendance Sheet at ";

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

        protected void btnPrintPreview_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/Report/FacultyStaffAttendanceReport.aspx');", true);  //Open New Tab for Sever side code
        }
    }
}