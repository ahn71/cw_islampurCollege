using DS.DAL.AdviitDAL;
using ComplexScriptingSystem;
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
using DS.BLL;

namespace DS.Forms.machine_attendance
{
    public partial class AbsentDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["__UserId__"] == null)
                {
                    Response.Redirect("~/UserLogin.aspx");
                }
                else
                {
                    if (!IsPostBack)
                    {
                        Classes.commonTask.loadMonths(dlMonth);
                        Classes.commonTask.loadBatch(dlBatchName);
                        Classes.commonTask.loadSection(ddlSection);
                    }
                }
            }
            catch { }
        }

        protected void dlBatchName_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadSectionClayseWise();
        }
        private void loadSectionClayseWise()
        {
            try
            {

                DataTable dt;
                SQLOperation.selectBySetCommandInDatatable("Select ClassOrder From Classes where ClassName='" + new String(dlBatchName.Text.Where(Char.IsLetter).ToArray()) + "'", dt = new DataTable(), sqlDB.connection);
                if ((dt.Rows[0]["ClassOrder"].ToString().Equals("9") || (dt.Rows[0]["ClassOrder"].ToString().Equals("10"))))
                {

                    ddlSection.Items.Clear();
                    ddlSection.Items.Add("...Select...");
                    ddlSection.Items.Add("Science");
                    ddlSection.Items.Add("Commerce");
                    ddlSection.Items.Add("Arts");
                    ddlSection.SelectedIndex = ddlSection.Items.Count - ddlSection.Items.Count;
                }
                else
                {
                    ddlSection.Items.Clear();
                    sqlDB.loadDropDownList("Select  SectionName from Sections where SectionName<>'Science' AND SectionName<>'Commerce' AND SectionName<>'Arts' Order by SectionName", ddlSection);
                    ddlSection.Items.Add("...Select...");
                    ddlSection.SelectedIndex = ddlSection.Items.Count - 1;
                }
            }
            catch { }
        }

        protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dlRoll.Items.Clear();
                sqlDB.bindDropDownList("select RollNo from CurrentStudentInfo where BatchName='" + dlBatchName.SelectedItem.Text + "' and Shift='" + dlShiftName.SelectedItem.Text + "' and SectionName='" + ddlSection.SelectedItem.Text + "' Order by RollNo ", "RollNo", dlRoll);
                dlRoll.Items.Add("All");
                dlRoll.SelectedIndex = dlRoll.Items.Count - 1;
            }
            catch { }
        }
        static string reportType;
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            loadAttendanceReport();
            reportType = "list";
        }
        private void loadAttendanceReport()
        {
            try
            {
               string checktable= "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Machine_AttendanceSheet]') AND type in (N'U')) BEGIN Drop Table Machine_AttendanceSheet END";
               SqlCommand cmdMA = new SqlCommand(checktable, sqlDB.connection);
                cmdMA.ExecuteNonQuery();
               

                string getYear = new String(dlMonth.Text.Where(Char.IsNumber).ToArray());
                string getMonth = new String(dlMonth.Text.Where(Char.IsLetter).ToArray());
                string date = getMonth+", 01 "+getYear;
                DateTime d = DateTime.ParseExact(date, "MMMM, dd yyyy",CultureInfo.InvariantCulture, DateTimeStyles.None);
                int days = DateTime.DaysInMonth(d.Year,d.Month);

                createTtable(days,d);
                DataTable dt = new DataTable();
               
               string Year = new String(dlMonth.SelectedItem.Text.Where(Char.IsNumber).ToArray());
                DateTime begin=new DateTime(int.Parse(Year),dlMonth.SelectedIndex,1);
                DateTime End=begin.AddMonths(1);
                End=End.AddDays(-1);
               
              
                if (dlRoll.SelectedItem.Text == "All")
                {
                    sqlDB.fillDataTable("Select Format(DailyAttendanceRecord.AttDate,'yyyy-MM-dd') as AttDate,DailyAttendanceRecord.AttStatus,CurrentStudentInfo.StudentId From DailyAttendanceRecord INNER Join CurrentStudentInfo On DailyAttendanceRecord.StudentId=CurrentStudentInfo.StudentId where CurrentStudentInfo.BatchName='" + dlBatchName.SelectedItem.Text + "' and CurrentStudentInfo.SectionName='" + ddlSection.SelectedItem.Text + "' and Shift='" + dlShiftName.SelectedItem.Text + "' and DailyAttendanceRecord.AttDate between '" + begin.ToString("yyyy-MM-dd") + "' and '" + End.ToString("yyyy-MM-dd") + "' ", dt);
                    for (int m = 0; m < dt.Rows.Count; m++)
                    {
                        string[] getdate = dt.Rows[m]["AttDate"].ToString().Split('-');
                        int day = Convert.ToInt32(getdate[2]);
                        int Month = Convert.ToInt32(getdate[1]);
                        SqlCommand cmdUMA = new SqlCommand("Update Machine_AttendanceSheet set D" + day + "_" + Month + "_" + getdate[0] + "='" + dt.Rows[m]["AttStatus"].ToString() + "' where StudentId=" + dt.Rows[m]["StudentId"].ToString() + "", sqlDB.connection);
                        cmdUMA.ExecuteNonQuery();
                    }
                    
                   // string findTbl = "AttendanceSheet_" + new String(dlBatchName.Text.Where(Char.IsLetter).ToArray()) + "_" + ddlSection.SelectedItem.Text + "_" + dlMonth.SelectedItem.Text + "";
                    string attendanceQuery = "SELECT Distinct StudentProfile.FullName,StudentProfile.AdmissionNo,CurrentStudentInfo.RollNo, Machine_AttendanceSheet.* FROM StudentProfile INNER JOIN CurrentStudentInfo ON StudentProfile.StudentId = CurrentStudentInfo.StudentId INNER JOIN DailyAttendanceRecord ON StudentProfile.StudentId = DailyAttendanceRecord.StudentId Inner join Machine_AttendanceSheet On Machine_AttendanceSheet.StudentId=CurrentStudentInfo.StudentId where CurrentStudentInfo.BatchName='" + dlBatchName.SelectedItem.Text + "' and CurrentStudentInfo.Shift='" + dlShiftName.SelectedItem.Text + "' and CurrentStudentInfo.SectionName='" + ddlSection.SelectedItem.Text + "' Order by CurrentStudentInfo.RollNo ";
                    sqlDB.fillDataTable(attendanceQuery, dt=new DataTable());
                }
                else
                {
                    if (btnByRollAndName.Text == "By Name")
                    {
                        sqlDB.fillDataTable("Select Format(DailyAttendanceRecord.AttDate,'yyyy-MM-dd') as AttDate,DailyAttendanceRecord.AttStatus,CurrentStudentInfo.StudentId From DailyAttendanceRecord INNER Join CurrentStudentInfo On DailyAttendanceRecord.StudentId=CurrentStudentInfo.StudentId where CurrentStudentInfo.BatchName='" + dlBatchName.SelectedItem.Text + "' and CurrentStudentInfo.SectionName='" + ddlSection.SelectedItem.Text + "' and Shift='" + dlShiftName.SelectedItem.Text + "' and DailyAttendanceRecord.AttDate between '" + begin.ToString("yyyy-MM-dd") + "' and '" + End.ToString("yyyy-MM-dd") + "' and CurrentStudentInfo.RollNo='" + dlRoll.SelectedItem.Text + "' Order by CurrentStudentInfo.RollNo ", dt);
                        for (int m = 0; m < dt.Rows.Count; m++)
                        {
                            string[] getdate = dt.Rows[m]["AttDate"].ToString().Split('-');
                            int day = Convert.ToInt32(getdate[2]);
                            int Month = Convert.ToInt32(getdate[1]);
                            SqlCommand cmdUMA = new SqlCommand("Update Machine_AttendanceSheet set D" + day + "_" + Month + "_" + getdate[0] + "='" + dt.Rows[m]["AttStatus"].ToString() + "' where StudentId=" + dt.Rows[m]["StudentId"].ToString() + "", sqlDB.connection);
                            cmdUMA.ExecuteNonQuery();
                        }

                        // string findTbl = "AttendanceSheet_" + new String(dlBatchName.Text.Where(Char.IsLetter).ToArray()) + "_" + ddlSection.SelectedItem.Text + "_" + dlMonth.SelectedItem.Text + "";
                        string attendanceQuery = "SELECT Distinct StudentProfile.FullName,StudentProfile.AdmissionNo,CurrentStudentInfo.RollNo, Machine_AttendanceSheet.* FROM StudentProfile INNER JOIN CurrentStudentInfo ON StudentProfile.StudentId = CurrentStudentInfo.StudentId INNER JOIN DailyAttendanceRecord ON StudentProfile.StudentId = DailyAttendanceRecord.StudentId Inner join Machine_AttendanceSheet On Machine_AttendanceSheet.StudentId=CurrentStudentInfo.StudentId where CurrentStudentInfo.BatchName='" + dlBatchName.SelectedItem.Text + "' and CurrentStudentInfo.Shift='" + dlShiftName.SelectedItem.Text + "' and CurrentStudentInfo.SectionName='" + ddlSection.SelectedItem.Text + "' and CurrentStudentInfo.RollNo='" + dlRoll.SelectedItem.Text + "' Order by StudentProfile.FullName ";
                        sqlDB.fillDataTable(attendanceQuery, dt = new DataTable());                    
                    }
                    else
                    {
                        sqlDB.fillDataTable("Select Format(DailyAttendanceRecord.AttDate,'yyyy-MM-dd') as AttDate,DailyAttendanceRecord.AttStatus,CurrentStudentInfo.StudentId,StudentProfile.FullName From DailyAttendanceRecord INNER Join CurrentStudentInfo On DailyAttendanceRecord.StudentId=CurrentStudentInfo.StudentId INNER JOIN StudentProfile ON CurrentStudentInfo.StudentId=StudentProfile.StudentId where CurrentStudentInfo.BatchName='" + dlBatchName.SelectedItem.Text + "' and CurrentStudentInfo.SectionName='" + ddlSection.SelectedItem.Text + "' and CurrentStudentInfo.Shift='" + dlShiftName.SelectedItem.Text + "' and DailyAttendanceRecord.AttDate between '" + begin.ToString("yyyy-MM-dd") + "' and '" + End.ToString("yyyy-MM-dd") + "' and StudentProfile.FullName='" + dlRoll.SelectedItem.Text + "' Order by CurrentStudentInfo.RollNo ", dt);
                        for (int m = 0; m < dt.Rows.Count; m++)
                        {
                            string[] getdate = dt.Rows[m]["AttDate"].ToString().Split('-');
                            int day = Convert.ToInt32(getdate[2]);
                            int Month = Convert.ToInt32(getdate[1]);
                            SqlCommand cmdUMA = new SqlCommand("Update Machine_AttendanceSheet set D" + day + "_" + Month + "_" + getdate[0] + "='" + dt.Rows[m]["AttStatus"].ToString() + "' where StudentId=" + dt.Rows[m]["StudentId"].ToString() + "", sqlDB.connection);
                            cmdUMA.ExecuteNonQuery();
                        }

                        // string findTbl = "AttendanceSheet_" + new String(dlBatchName.Text.Where(Char.IsLetter).ToArray()) + "_" + ddlSection.SelectedItem.Text + "_" + dlMonth.SelectedItem.Text + "";
                        string attendanceQuery = "SELECT Distinct StudentProfile.FullName,StudentProfile.AdmissionNo,CurrentStudentInfo.RollNo, Machine_AttendanceSheet.* FROM StudentProfile INNER JOIN CurrentStudentInfo ON StudentProfile.StudentId = CurrentStudentInfo.StudentId INNER JOIN DailyAttendanceRecord ON StudentProfile.StudentId = DailyAttendanceRecord.StudentId Inner join Machine_AttendanceSheet On Machine_AttendanceSheet.StudentId=CurrentStudentInfo.StudentId where CurrentStudentInfo.BatchName='" + dlBatchName.SelectedItem.Text + "' and CurrentStudentInfo.Shift='" + dlShiftName.SelectedItem.Text + "' and CurrentStudentInfo.SectionName='" + ddlSection.SelectedItem.Text + "' and StudentProfile.FullName='" + dlRoll.SelectedItem.Text + "' Order by StudentProfile.FullName ";
                        sqlDB.fillDataTable(attendanceQuery, dt = new DataTable());  

                        //string findTbl = "AttendanceSheet_" + new String(dlBatchName.Text.Where(Char.IsLetter).ToArray()) + "_" + ddlSection.SelectedItem.Text + "_" + dlMonth.SelectedItem.Text + "";
                        //string attendanceQuery = "SELECT StudentProfile.FullName,StudentProfile.AdmissionNo,CurrentStudentInfo.RollNo, " + findTbl + ".* FROM StudentProfile INNER JOIN CurrentStudentInfo ON StudentProfile.StudentId = CurrentStudentInfo.StudentId INNER JOIN " + findTbl + " ON StudentProfile.StudentId = " + findTbl + ".StudentId where CurrentStudentInfo.BatchName='" + dlBatchName.SelectedItem.Text + "' and CurrentStudentInfo.Shift='" + dlShiftName.SelectedItem.Text + "' and CurrentStudentInfo.SectionName='" + ddlSection.SelectedItem.Text + "' and StudentProfile.FullName='" + dlRoll.SelectedItem.Text + "' Order by CurrentStudentInfo.RollNo ";
                        //sqlDB.fillDataTable(attendanceQuery, dt);
                    }
                }
                //..........................For Sheet ..............................

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

                divInfo += "<th style='text-align:center;  width:8%'>Roll No</th>";
                divInfoReport += "<th style='text-align:center;  width:8%'>Roll No</th>";
                divInfo += "<th style=' width:25%'>Name</th>";
                divInfoReport += "<th style=' width:25%'>Name</th>";
                for (byte i = 4; i < dt.Columns.Count; i++)
                {
                    string[] columnname = dt.Columns[i].ToString().Split('_');
                    string val = columnname[0];
                    string col = new String(val.Where(Char.IsNumber).ToArray());
                    //string Month = new String(MonthYear.Where(Char.IsLetter).ToArray());
                    divInfo += "<th style='text-align:center; padding:05px'>" + col + "</th>";
                    divInfoReport += "<th style='text-align:center; padding:05px'>" + col + "</th>";
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
                    id = dt.Rows[x]["StudentId"].ToString();
                    divInfo += "<tr id='r_" + id + "'>";
                    divInfoReport += "<tr id='r_" + id + "'>";
                    for (byte k = 0; k < dt.Columns.Count; k++)
                    {
                        if (k == 0)
                        {
                            divInfo += "<td style='width:13%; text-align:center' >" + dt.Rows[x]["RollNo"].ToString() + "</td>";
                            divInfoReport += "<td style='width:13%; text-align:center' >" + dt.Rows[x]["RollNo"].ToString() + "</td>";
                            divInfo += "<td style='width:13%' >" + dt.Rows[x]["FullName"].ToString() + "</td>";
                            divInfoReport += "<td style='width:13%' >" + dt.Rows[x]["FullName"].ToString() + "</td>";
                        }
                        else
                        {
                            if (dt.Rows[x][dt.Columns[k].ToString()].ToString() == "a")
                            {
                                TA += 1;
                                divInfo += "<td style='width:3%; color:red ; text-align:center' >" + dt.Rows[x][dt.Columns[k].ToString()].ToString() + "</td>";
                                divInfoReport += "<td style='width:3%; color:red ; text-align:center' >" + dt.Rows[x][dt.Columns[k].ToString()].ToString() + "</td>";
                            }
                            else if (dt.Rows[x][dt.Columns[k].ToString()].ToString() == "p")
                            {
                                TP += 1;
                                divInfo += "<td style='width:3% ; text-align:center' >" + dt.Rows[x][dt.Columns[k].ToString()].ToString() + "</td>";
                                divInfoReport += "<td style='width:3% ; text-align:center' >" + dt.Rows[x][dt.Columns[k].ToString()].ToString() + "</td>";
                            }
                            else if (dt.Rows[x][dt.Columns[k].ToString()].ToString() == "w")
                            {
                                divInfo += "<td style='width:3% ; text-align:center ; padding-left:0px; border-bottom:0px solid white' >" + dt.Rows[x][dt.Columns[k].ToString()].ToString().ToLower() + "</td>";
                                divInfoReport += "<td style='width:3% ; text-align:center ; padding-left:0px; border-bottom:0px solid white' >" + dt.Rows[x][dt.Columns[k].ToString()].ToString().ToLower() + "</td>";
                            }
                            else if (dt.Rows[x][dt.Columns[k].ToString()].ToString() == "")
                            {
                                divInfo += "<td style='width:3% ; text-align:center;' >" + dt.Rows[x][dt.Columns[k].ToString()].ToString() + "</td>";
                                divInfoReport += "<td style='width:3% ; text-align:center;' >" + dt.Rows[x][dt.Columns[k].ToString()].ToString() + "</td>";
                            }
                        }
                        if (dt.Columns.Count - 1 == k)
                        {
                            divInfo += "<td style='width:15% ; text-align:center ; font-weight:bold ; color:green; padding-left:0px' >" + TP + "</td>";
                            divInfoReport += "<td style='width:15% ; text-align:center ; font-weight:bold ; color:green; padding-left:0px' >" + TP + "</td>";
                            divInfo += "<td style='width:15% ; text-align:center ; font-weight:bold ; color:red; padding-left:0px ' >" + TA + "</td>";
                            divInfoReport += "<td style='width:15% ; text-align:center ; font-weight:bold ; color:red; padding-left:0px ' >" + TA + "</td>";
                            divInfo += "<td style='max-width:20px;' class='numeric control' >" + "<img src='/Images/gridImages/view.png' onclick='viewStudent(" + id + ");'  />";
                        }
                    }

                    TA = 0;
                    TP = 0;
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
                Session["__AttendanceDetails__"] = divInfoReport;
                Session["__ReportType__"] = "Attendance Sheet at " + dlMonth.SelectedItem.Text;
                lblMonthName.Text = "Attendance Sheet at " + dlMonth.SelectedItem.Text;
                lblBatchName.Text = "Batch : " + dlBatchName.SelectedItem.Text;
                lblShiftName.Text = "Shift : " + dlShiftName.SelectedItem.Text;
                lblSectionName.Text = "Section : " + ddlSection.SelectedItem.Text;


                Session["__Month__"] = "Attendance Info at " + dlMonth.SelectedItem.Text;
                Session["__BatchAttendance__"] = dlBatchName.SelectedItem.Text;
                Session["__ShiftAttendance__"] = dlShiftName.SelectedItem.Text;
                Session["__SectionAttendance__"] = ddlSection.SelectedItem.Text;
                Session["__MonthNameAttendance__"] = dlMonth.SelectedItem.Text;

                Session["__dataTableDateRange__"] = null;
            }
            catch { }
        }
        private void createTtable(int days,DateTime ddMMyyyy)
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
                cmd = new SqlCommand("CREATE TABLE Machine_AttendanceSheet ( StudentId bigint ," + dateField + ",Foreign Key (StudentId) REFERENCES StudentProfile(StudentId) On Update Cascade On Delete Cascade)", sqlDB.connection);
                int result = cmd.ExecuteNonQuery();
                dt = new DataTable();
                if (dlRoll.SelectedItem.Text == "All")
                {
                    sqlDB.fillDataTable("select StudentId from CurrentStudentInfo where Shift='" + dlShiftName.SelectedItem.Text + "' AND BatchName ='" + dlBatchName.SelectedItem.Text + "'", dt);
                }
                else
                {
                    sqlDB.fillDataTable("select StudentId from CurrentStudentInfo where Shift='" + dlShiftName.SelectedItem.Text + "' AND BatchName ='" + dlBatchName.SelectedItem.Text + "' and SectionName='"+ddlSection.SelectedItem.Text+"'", dt);
                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cmd = new SqlCommand("insert into Machine_AttendanceSheet (StudentId) values (" + dt.Rows[i]["StudentId"] + ")", sqlDB.connection);
                    cmd.ExecuteNonQuery();
                }

                Read_N_Write_WH(days, getYear,ddMMyyyy.Month);
                

            }
            catch { }
        }
        private void Read_N_Write_WH(int days, string year,int Month)    // N=And,WH=Weekly Holiday
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

        protected void btnByRollAndName_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnByRollAndName.Text == "By Roll")
                {
                    dlRoll.Items.Clear();
                    sqlDB.bindDropDownList("select RollNo from CurrentStudentInfo where BatchName='" + dlBatchName.SelectedItem.Text + "' and Shift='" + dlShiftName.SelectedItem.Text + "' and SectionName='" + ddlSection.SelectedItem.Text + "' Order by RollNo ", "RollNo", dlRoll);
                    dlRoll.Items.Add("All");
                    dlRoll.SelectedIndex = dlRoll.Items.Count - 1;
                    btnByRollAndName.Text = "By Name";
                }
                else
                {
                    dlRoll.Items.Clear();
                    sqlDB.bindDropDownList("SELECT StudentProfile.FullName FROM StudentProfile INNER JOIN CurrentStudentInfo ON StudentProfile.StudentId = CurrentStudentInfo.StudentId where CurrentStudentInfo.BatchName='" + dlBatchName.SelectedItem.Text + "' and CurrentStudentInfo.Shift='" + dlShiftName.SelectedItem.Text + "' and CurrentStudentInfo.SectionName='" + ddlSection.SelectedItem.Text + "' ", "FullName", dlRoll);
                    dlRoll.Items.Add("All");
                    dlRoll.SelectedIndex = dlRoll.Items.Count - 1;
                    btnByRollAndName.Text = "By Roll";
                }

            }
            catch { }
        }

        protected void btnTodayAttendanceSheet_Click(object sender, EventArgs e)
        {
            loadTodayAttendanceSheetReport();
        }
        private void loadTodayAttendanceSheetReport()
        {
            try
            {
                string monthName = TimeZoneBD.getCurrentTimeBD().ToString("MMMM") + TimeZoneBD.getCurrentTimeBD().Year.ToString();
                string checktable = "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Machine_AttendanceSheet]') AND type in (N'U')) BEGIN Drop Table Machine_AttendanceSheet END";
                SqlCommand cmdMA = new SqlCommand(checktable, sqlDB.connection);
                cmdMA.ExecuteNonQuery();
                int days = DateTime.DaysInMonth(TimeZoneBD.getCurrentTimeBD().Year, TimeZoneBD.getCurrentTimeBD().Month);
                createTtable(days, TimeZoneBD.getCurrentTimeBD());
                DataTable dt = new DataTable();

                sqlDB.fillDataTable("Select Format(DailyAttendanceRecord.AttDate,'yyyy-MM-dd') as AttDate,DailyAttendanceRecord.AttStatus,CurrentStudentInfo.StudentId From DailyAttendanceRecord INNER Join CurrentStudentInfo On DailyAttendanceRecord.StudentId=CurrentStudentInfo.StudentId  Where  CurrentStudentInfo.Shift='" + dlShiftName.SelectedItem.Text + "' and CurrentStudentInfo.BatchName='" + dlBatchName.SelectedItem.Text + "' and CurrentStudentInfo.SectionName='" + ddlSection.SelectedItem.Text + "' Order by CurrentStudentInfo.RollNo ", dt);
                for (int m = 0; m < dt.Rows.Count; m++)
                {
                    string[] getdate = dt.Rows[m]["AttDate"].ToString().Split('-');
                    int day = Convert.ToInt32(getdate[2]);
                    int Month = Convert.ToInt32(getdate[1]);
                    SqlCommand cmdUMA = new SqlCommand("Update Machine_AttendanceSheet set D" + day + "_" + Month + "_" + getdate[0] + "='" + dt.Rows[m]["AttStatus"].ToString() + "' where StudentId=" + dt.Rows[m]["StudentId"].ToString() + "", sqlDB.connection);
                    cmdUMA.ExecuteNonQuery();
                }
                dt = new DataTable();

                string attendanceQuery = "SELECT StudentProfile.FullName,StudentProfile.AdmissionNo,CurrentStudentInfo.RollNo,CurrentStudentInfo.Shift, CurrentStudentInfo.BatchName, StudentProfile.FathersName,CurrentStudentInfo.Mobile, D" + TimeZoneBD.getCurrentTimeBD().Day + "_" + TimeZoneBD.getCurrentTimeBD().Month + "_" + TimeZoneBD.getCurrentTimeBD().Year + "  FROM StudentProfile INNER JOIN CurrentStudentInfo ON StudentProfile.StudentId = CurrentStudentInfo.StudentId INNER JOIN Machine_AttendanceSheet ON StudentProfile.StudentId = Machine_AttendanceSheet.StudentId  Where (D" + TimeZoneBD.getCurrentTimeBD().Day + "_" + TimeZoneBD.getCurrentTimeBD().Month + "_" + TimeZoneBD.getCurrentTimeBD().Year + " ='a' or D" + TimeZoneBD.getCurrentTimeBD().Day + "_" + TimeZoneBD.getCurrentTimeBD().Month + "_" + TimeZoneBD.getCurrentTimeBD().Year + " ='p') and CurrentStudentInfo.Shift='" + dlShiftName.SelectedItem.Text + "' and CurrentStudentInfo.BatchName='" + dlBatchName.SelectedItem.Text + "' and CurrentStudentInfo.SectionName='" + ddlSection.SelectedItem.Text + "' Order by CurrentStudentInfo.RollNo ";
                sqlDB.fillDataTable(attendanceQuery, dt);

                //..........................For Sheet ..............................

                int totalRows = dt.Rows.Count;
                string divInfo = "";

                if (totalRows == 0)
                {
                    divInfo = "<div class='noData'>No Student available</div>";
                    divInfo += "<div><div class='head'></div></div>";
                    divMonthWiseAttendaceSheet.Controls.Add(new LiteralControl(divInfo));
                    return;
                }


                divInfo = " <table id='tblStudentList' class='display'  style='width:100%;margin:0px auto;' > ";
                divInfo += "<thead>";
                divInfo += "<tr>";

                divInfo += "<th style='text-align:center; width:10%'>Admission No</th>";
                divInfo += "<th style='text-align:center;  width:8%'>Roll No</th>";
                divInfo += "<th style=' width:25%'>Name</th>";
                divInfo += "<th style=' width:25%'>Status</th>";
                divInfo += "<th >Father’s Name</th>";
                divInfo += "<th >Cell No</th>";


                divInfo += "</tr>";

                divInfo += "</thead>";

                divInfo += "<tbody>";

                string id = "";
                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    divInfo += "<tr id='r_" + id + "'>";
                    divInfo += "<td style='width:13%; text-align:center' >" + dt.Rows[x]["AdmissionNo"].ToString() + "</td>";
                    divInfo += "<td style='width:13%; text-align:center' >" + dt.Rows[x]["RollNo"].ToString() + "</td>";
                    divInfo += "<td style='width:13%' >" + dt.Rows[x]["FullName"].ToString() + "</td>";

                    string clum = "D" + TimeZoneBD.getCurrentTimeBD().Date.ToString("d_M_yyyy");
                    if (dt.Rows[x][clum].ToString() == "a") divInfo += "<td style='width:13%; ' >Absent</td>";
                    else divInfo += "<td style='width:13%; ' >Present</td>";
                    divInfo += "<td style='width:13%' >" + dt.Rows[x]["FathersName"].ToString() + "</td>";

                    divInfo += "<td style='width:13%; ' >" + dt.Rows[x]["Mobile"].ToString() + "</td>";
                }

                divInfo += "</tbody>";
                divInfo += "<tfoot>";

                divInfo += "</table>";
                divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";

                divMonthWiseAttendaceSheet.Controls.Add(new LiteralControl(divInfo));
                Session["__AttendanceDetails__"] = divInfo;

                Session["__ReportType__"] = "Today Attendance Sheet (" + TimeZoneBD.getCurrentTimeBD().ToString("d-MMM-yyyy") + ")";
                lblMonthName.Text = "Today Attendance Sheet (" + TimeZoneBD.getCurrentTimeBD().ToString("d-MMM-yyyy") + ")";
                lblBatchName.Text = "Batch : " + dlBatchName.SelectedItem.Text;
                lblShiftName.Text = "Shift : " + dlShiftName.SelectedItem.Text;
                lblSectionName.Text = "Section : " + ddlSection.SelectedItem.Text;

            }
            catch { }
        }

        protected void btnTodayAttendanceList_Click(object sender, EventArgs e)
        {
            loadTodayAttendanceReport();
        }
        private void loadTodayAttendanceReport()
        {
            try
            {
                string checktable = "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Machine_AttendanceSheet]') AND type in (N'U')) BEGIN Drop Table Machine_AttendanceSheet END";
                SqlCommand cmdMA = new SqlCommand(checktable, sqlDB.connection);
                cmdMA.ExecuteNonQuery();
                int days = DateTime.DaysInMonth(TimeZoneBD.getCurrentTimeBD().Year, TimeZoneBD.getCurrentTimeBD().Month);
                createTtable(days, TimeZoneBD.getCurrentTimeBD());
                DataTable dt = new DataTable();

                sqlDB.fillDataTable("Select Format(DailyAttendanceRecord.AttDate,'yyyy-MM-dd') as AttDate,DailyAttendanceRecord.AttStatus,CurrentStudentInfo.StudentId From DailyAttendanceRecord INNER Join CurrentStudentInfo On DailyAttendanceRecord.StudentId=CurrentStudentInfo.StudentId  Where  CurrentStudentInfo.Shift='" + dlShiftName.SelectedItem.Text + "' and CurrentStudentInfo.BatchName='" + dlBatchName.SelectedItem.Text + "' and CurrentStudentInfo.SectionName='" + ddlSection.SelectedItem.Text + "' Order by CurrentStudentInfo.RollNo ", dt);
                for (int m = 0; m < dt.Rows.Count; m++)
                {
                    string[] getdate = dt.Rows[m]["AttDate"].ToString().Split('-');
                    int day = Convert.ToInt32(getdate[2]);
                    int Month = Convert.ToInt32(getdate[1]);
                    SqlCommand cmdUMA = new SqlCommand("Update Machine_AttendanceSheet set D" + day + "_" + Month + "_" + getdate[0] + "='" + dt.Rows[m]["AttStatus"].ToString() + "' where StudentId=" + dt.Rows[m]["StudentId"].ToString() + "", sqlDB.connection);
                    cmdUMA.ExecuteNonQuery();
                }
                dt = new DataTable();

                string attendanceQuery = "SELECT StudentProfile.FullName,StudentProfile.AdmissionNo,CurrentStudentInfo.RollNo,CurrentStudentInfo.Shift, CurrentStudentInfo.BatchName, StudentProfile.FathersName,CurrentStudentInfo.Mobile  FROM StudentProfile INNER JOIN CurrentStudentInfo ON StudentProfile.StudentId = CurrentStudentInfo.StudentId INNER JOIN Machine_AttendanceSheet ON StudentProfile.StudentId = Machine_AttendanceSheet.StudentId  Where D" + TimeZoneBD.getCurrentTimeBD().Day + "_" + TimeZoneBD.getCurrentTimeBD().Month + "_" + TimeZoneBD.getCurrentTimeBD().Year + " ='p' and CurrentStudentInfo.Shift='" + dlShiftName.SelectedItem.Text + "' and CurrentStudentInfo.BatchName='" + dlBatchName.SelectedItem.Text + "' and CurrentStudentInfo.SectionName='" + ddlSection.SelectedItem.Text + "' Order by CurrentStudentInfo.RollNo ";
                sqlDB.fillDataTable(attendanceQuery, dt);

                //..........................For Sheet ..............................

                int totalRows = dt.Rows.Count;
                string divInfo = "";

                if (totalRows == 0)
                {
                    divInfo = "<div class='noData'>No Student available</div>";
                    divInfo += "<div><div class='head'></div></div>";
                    divMonthWiseAttendaceSheet.Controls.Add(new LiteralControl(divInfo));
                    return;
                }


                divInfo = " <table id='tblStudentList' class='display'  style='width:100%;margin:0px auto;' > ";
                divInfo += "<thead>";
                divInfo += "<tr>";

                divInfo += "<th style='text-align:center; width:10%'>Admission No</th>";
                divInfo += "<th style='text-align:center;  width:8%'>Roll No</th>";
                divInfo += "<th style='text-align:center;  width:25%'>Name</th>";

                divInfo += "<th >Father’s Name</th>";
                divInfo += "<th >Cell No</th>";


                divInfo += "</tr>";

                divInfo += "</thead>";

                divInfo += "<tbody>";

                string id = "";
                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    divInfo += "<tr id='r_" + id + "'>";
                    divInfo += "<td style='width:13%; text-align:center' >" + dt.Rows[x]["AdmissionNo"].ToString() + "</td>";
                    divInfo += "<td style='width:13%; text-align:center' >" + dt.Rows[x]["RollNo"].ToString() + "</td>";
                    divInfo += "<td style='width:13%' >" + dt.Rows[x]["FullName"].ToString() + "</td>";

                    divInfo += "<td style='width:13%; ' >" + dt.Rows[x]["FathersName"].ToString() + "</td>";
                    divInfo += "<td style='width:13%; ' >" + dt.Rows[x]["Mobile"].ToString() + "</td>";
                }

                divInfo += "</tbody>";
                divInfo += "<tfoot>";

                divInfo += "</table>";
                divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";

                divMonthWiseAttendaceSheet.Controls.Add(new LiteralControl(divInfo));
                Session["__AttendanceDetails__"] = divInfo;
                Session["__ReportType__"] = "Today Absents Report";

                Session["__ReportType__"] = "Today Absent List (" + TimeZoneBD.getCurrentTimeBD().ToString("d-MMM-yyyy") + ")";
                lblMonthName.Text = "Today Absent List (" + TimeZoneBD.getCurrentTimeBD().ToString("d-MMM-yyyy") + ")";
                lblBatchName.Text = "Batch : " + dlBatchName.SelectedItem.Text;
                lblShiftName.Text = "Shift : " + dlShiftName.SelectedItem.Text;
                lblSectionName.Text = "Section : " + ddlSection.SelectedItem.Text;
            }
            catch { }
        }

        protected void btnTodayAbsentList_Click(object sender, EventArgs e)
        {
            loadTodayAbsentReport();
        }
        private void loadTodayAbsentReport()
        {
            try
            {
                string checktable = "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Machine_AttendanceSheet]') AND type in (N'U')) BEGIN Drop Table Machine_AttendanceSheet END";
                SqlCommand cmdMA = new SqlCommand(checktable, sqlDB.connection);
                cmdMA.ExecuteNonQuery();
                int days = DateTime.DaysInMonth(TimeZoneBD.getCurrentTimeBD().Year, TimeZoneBD.getCurrentTimeBD().Month);
                createTtable(days, TimeZoneBD.getCurrentTimeBD());
                DataTable dt = new DataTable();

                sqlDB.fillDataTable("Select Format(DailyAttendanceRecord.AttDate,'yyyy-MM-dd') as AttDate,DailyAttendanceRecord.AttStatus,CurrentStudentInfo.StudentId From DailyAttendanceRecord INNER Join CurrentStudentInfo On DailyAttendanceRecord.StudentId=CurrentStudentInfo.StudentId  Where  CurrentStudentInfo.Shift='" + dlShiftName.SelectedItem.Text + "' and CurrentStudentInfo.BatchName='" + dlBatchName.SelectedItem.Text + "' and CurrentStudentInfo.SectionName='" + ddlSection.SelectedItem.Text + "' Order by CurrentStudentInfo.RollNo ", dt);
                for (int m = 0; m < dt.Rows.Count; m++)
                {
                    string[] getdate = dt.Rows[m]["AttDate"].ToString().Split('-');
                    int day = Convert.ToInt32(getdate[2]);
                    int Month = Convert.ToInt32(getdate[1]);
                    SqlCommand cmdUMA = new SqlCommand("Update Machine_AttendanceSheet set D" + day + "_" + Month + "_" + getdate[0] + "='" + dt.Rows[m]["AttStatus"].ToString() + "' where StudentId=" + dt.Rows[m]["StudentId"].ToString() + "", sqlDB.connection);
                    cmdUMA.ExecuteNonQuery();
                }
                dt = new DataTable();

                string attendanceQuery = "SELECT StudentProfile.FullName,StudentProfile.AdmissionNo,CurrentStudentInfo.RollNo,CurrentStudentInfo.Shift, CurrentStudentInfo.BatchName, StudentProfile.FathersName,CurrentStudentInfo.Mobile  FROM StudentProfile INNER JOIN CurrentStudentInfo ON StudentProfile.StudentId = CurrentStudentInfo.StudentId INNER JOIN Machine_AttendanceSheet ON StudentProfile.StudentId = Machine_AttendanceSheet.StudentId  Where D" + TimeZoneBD.getCurrentTimeBD().Day + "_" + TimeZoneBD.getCurrentTimeBD().Month + "_" + TimeZoneBD.getCurrentTimeBD().Year + " ='a' and CurrentStudentInfo.Shift='" + dlShiftName.SelectedItem.Text + "' and CurrentStudentInfo.BatchName='" + dlBatchName.SelectedItem.Text + "' and CurrentStudentInfo.SectionName='" + ddlSection.SelectedItem.Text + "' Order by CurrentStudentInfo.RollNo ";
                sqlDB.fillDataTable(attendanceQuery, dt);

                //..........................For Sheet ..............................

                int totalRows = dt.Rows.Count;
                string divInfo = "";

                if (totalRows == 0)
                {
                    divInfo = "<div class='noData'>No Student available</div>";
                    divInfo += "<div><div class='head'></div></div>";
                    divMonthWiseAttendaceSheet.Controls.Add(new LiteralControl(divInfo));
                    return;
                }


                divInfo = " <table id='tblStudentList' class='display'  style='width:100%;margin:0px auto;' > ";
                divInfo += "<thead>";
                divInfo += "<tr>";

                divInfo += "<th style='text-align:center; width:10%'>Admission No</th>";
                divInfo += "<th style='text-align:center;  width:8%'>Roll No</th>";
                divInfo += "<th style='text-align:center;  width:25%'>Name</th>";

                divInfo += "<th >Father’s Name</th>";
                divInfo += "<th >Cell No</th>";


                divInfo += "</tr>";

                divInfo += "</thead>";

                divInfo += "<tbody>";

                string id = "";
                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    divInfo += "<tr id='r_" + id + "'>";
                    divInfo += "<td style='width:13%; text-align:center' >" + dt.Rows[x]["AdmissionNo"].ToString() + "</td>";
                    divInfo += "<td style='width:13%; text-align:center' >" + dt.Rows[x]["RollNo"].ToString() + "</td>";
                    divInfo += "<td style='width:13%' >" + dt.Rows[x]["FullName"].ToString() + "</td>";

                    divInfo += "<td style='width:13%; ' >" + dt.Rows[x]["FathersName"].ToString() + "</td>";
                    divInfo += "<td style='width:13%; ' >" + dt.Rows[x]["Mobile"].ToString() + "</td>";
                }

                divInfo += "</tbody>";
                divInfo += "<tfoot>";

                divInfo += "</table>";
                divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";

                divMonthWiseAttendaceSheet.Controls.Add(new LiteralControl(divInfo));
                Session["__AttendanceDetails__"] = divInfo;
                Session["__ReportType__"] = "Today Absents Report";

                Session["__ReportType__"] = "Today Absent List (" + TimeZoneBD.getCurrentTimeBD().ToString("d-MMM-yyyy") + ")";
                lblMonthName.Text = "Today Absent List (" + TimeZoneBD.getCurrentTimeBD().ToString("d-MMM-yyyy") + ")";
                lblBatchName.Text = "Batch : " + dlBatchName.SelectedItem.Text;
                lblShiftName.Text = "Shift : " + dlShiftName.SelectedItem.Text;
                lblSectionName.Text = "Section : " + ddlSection.SelectedItem.Text;
            }
            catch { }
        }

        protected void btnPrintPreview_Click(object sender, EventArgs e)
        {
            Session["__Batch__"] = "Batch Name: " + dlBatchName.Text;
            Session["__Shift__"] = "Shift Name: " + dlShiftName.Text;
            Session["__Section__"] = "Section Name: " + ddlSection.Text;
            //if (reportType == "DateRange") { }
            //else loadAttendanceReportForPrint();

            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/Report/AttendanceDetailsReport.aspx');", true);  //Open New Tab for Sever side code
        }
        protected void btnDateRangeSearch_Click(object sender, EventArgs e)
        {
            generateAttendanceSheetByDateRange();
            reportType = "DateRange";
        }
        string attendanceQuery;
        DataTable dtMonth = new DataTable();
        string allReport;
        private void generateAttendanceSheetByDateRange()
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
                DataTable dt;

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
                  
                    if (dlRoll.SelectedItem.Text == "All")
                    {
                        sqlDB.fillDataTable("Select Format(DailyAttendanceRecord.AttDate,'yyyy-MM-dd') as AttDate,DailyAttendanceRecord.AttStatus,CurrentStudentInfo.StudentId From DailyAttendanceRecord INNER Join CurrentStudentInfo On DailyAttendanceRecord.StudentId=CurrentStudentInfo.StudentId where CurrentStudentInfo.BatchName='" + dlBatchName.SelectedItem.Text + "' and CurrentStudentInfo.SectionName='" + ddlSection.SelectedItem.Text + "' and Shift='" + dlShiftName.SelectedItem.Text + "' and DailyAttendanceRecord.AttDate between '" + begin.ToString("yyyy-MM-dd") + "' and '" + End.ToString("yyyy-MM-dd") + "' ", dt);
                        for (int m = 0; m < dt.Rows.Count; m++)
                        {
                            string[] getdate = dt.Rows[m]["AttDate"].ToString().Split('-');
                            int day = Convert.ToInt32(getdate[2]);
                            int Month = Convert.ToInt32(getdate[1]);
                            SqlCommand cmdUMA = new SqlCommand("Update Machine_AttendanceSheet set D" + day + "_" + Month + "_" + getdate[0] + "='" + dt.Rows[m]["AttStatus"].ToString() + "' where StudentId=" + dt.Rows[m]["StudentId"].ToString() + "", sqlDB.connection);
                            cmdUMA.ExecuteNonQuery();
                        }
                        string getMonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(b + int.Parse(getFromDate[1]));
                       // string findTbl = "AttendanceSheet_" + new String(dlBatchName.Text.Where(Char.IsLetter).ToArray()) + "_" + ddlSection.SelectedItem.Text + "_" + getMonthName + "" + getFromDate[2];
                        attendanceQuery = "SELECT StudentProfile.FullName,StudentProfile.AdmissionNo,CurrentStudentInfo.RollNo, Machine_AttendanceSheet.* FROM StudentProfile INNER JOIN CurrentStudentInfo ON StudentProfile.StudentId = CurrentStudentInfo.StudentId INNER JOIN Machine_AttendanceSheet ON StudentProfile.StudentId = Machine_AttendanceSheet.StudentId where CurrentStudentInfo.BatchName='" + dlBatchName.SelectedItem.Text + "' and CurrentStudentInfo.Shift='" + dlShiftName.SelectedItem.Text + "' and CurrentStudentInfo.SectionName='" + ddlSection.SelectedItem.Text + "' Order by CurrentStudentInfo.RollNo ";
                        dtMonth.Rows.Add(getMonthName);
                    }
                    else
                    {
                        if (btnByRollAndName.Text == "By Name")
                        {
                            sqlDB.fillDataTable("Select Format(DailyAttendanceRecord.AttDate,'yyyy-MM-dd') as AttDate,DailyAttendanceRecord.AttStatus,CurrentStudentInfo.StudentId From DailyAttendanceRecord INNER Join CurrentStudentInfo On DailyAttendanceRecord.StudentId=CurrentStudentInfo.StudentId where CurrentStudentInfo.BatchName='" + dlBatchName.SelectedItem.Text + "' and CurrentStudentInfo.SectionName='" + ddlSection.SelectedItem.Text + "' and Shift='" + dlShiftName.SelectedItem.Text + "' and DailyAttendanceRecord.AttDate between '" + begin.ToString("yyyy-MM-dd") + "' and '" + End.ToString("yyyy-MM-dd") + "' and CurrentStudentInfo.RollNo='" + dlRoll.SelectedItem.Text + "' Order by CurrentStudentInfo.RollNo ", dt);
                            for (int m = 0; m < dt.Rows.Count; m++)
                            {
                                string[] getdate = dt.Rows[m]["AttDate"].ToString().Split('-');
                                int day = Convert.ToInt32(getdate[2]);
                                int Month = Convert.ToInt32(getdate[1]);
                                SqlCommand cmdUMA = new SqlCommand("Update Machine_AttendanceSheet set D" + day + "_" + Month + "_" + getdate[0] + "='" + dt.Rows[m]["AttStatus"].ToString() + "' where StudentId=" + dt.Rows[m]["StudentId"].ToString() + "", sqlDB.connection);
                                cmdUMA.ExecuteNonQuery();
                            }
                            string getMonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(b + int.Parse(getFromDate[1]));

                            attendanceQuery = "SELECT StudentProfile.FullName,StudentProfile.AdmissionNo,CurrentStudentInfo.RollNo, Machine_AttendanceSheet.* FROM StudentProfile INNER JOIN CurrentStudentInfo ON StudentProfile.StudentId = CurrentStudentInfo.StudentId INNER JOIN Machine_AttendanceSheet ON StudentProfile.StudentId = Machine_AttendanceSheet.StudentId where CurrentStudentInfo.BatchName='" + dlBatchName.SelectedItem.Text + "' and CurrentStudentInfo.Shift='" + dlShiftName.SelectedItem.Text + "' and CurrentStudentInfo.SectionName='" + ddlSection.SelectedItem.Text + "' and CurrentStudentInfo.RollNo=" + dlRoll.SelectedItem.Text + " Order by CurrentStudentInfo.RollNo ";
                            dtMonth.Rows.Add(getMonthName);
                        }
                        else
                        {
                            sqlDB.fillDataTable("Select Format(DailyAttendanceRecord.AttDate,'yyyy-MM-dd') as AttDate,DailyAttendanceRecord.AttStatus,CurrentStudentInfo.StudentId,StudentProfile.FullName From DailyAttendanceRecord INNER Join CurrentStudentInfo On DailyAttendanceRecord.StudentId=CurrentStudentInfo.StudentId INNER JOIN StudentProfile ON CurrentStudentInfo.StudentId=StudentProfile.StudentId where CurrentStudentInfo.BatchName='" + dlBatchName.SelectedItem.Text + "' and CurrentStudentInfo.SectionName='" + ddlSection.SelectedItem.Text + "' and CurrentStudentInfo.Shift='" + dlShiftName.SelectedItem.Text + "' and DailyAttendanceRecord.AttDate between '" + begin.ToString("yyyy-MM-dd") + "' and '" + End.ToString("yyyy-MM-dd") + "' and StudentProfile.FullName='" + dlRoll.SelectedItem.Text + "' Order by CurrentStudentInfo.RollNo ", dt);
                            for (int m = 0; m < dt.Rows.Count; m++)
                            {
                                string[] getdate = dt.Rows[m]["AttDate"].ToString().Split('-');
                                int day = Convert.ToInt32(getdate[2]);
                                int Month = Convert.ToInt32(getdate[1]);
                                SqlCommand cmdUMA = new SqlCommand("Update Machine_AttendanceSheet set D" + day + "_" + Month + "_" + getdate[0] + "='" + dt.Rows[m]["AttStatus"].ToString() + "' where StudentId=" + dt.Rows[m]["StudentId"].ToString() + "", sqlDB.connection);
                                cmdUMA.ExecuteNonQuery();
                            }
                            string getMonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(b + int.Parse(getFromDate[1]));
                           // string findTbl = "AttendanceSheet_" + new String(dlBatchName.Text.Where(Char.IsLetter).ToArray()) + "_" + ddlSection.SelectedItem.Text + "_" + getMonthName + "" + getFromDate[2];
                            attendanceQuery = "SELECT StudentProfile.FullName,StudentProfile.AdmissionNo,CurrentStudentInfo.RollNo, Machine_AttendanceSheet.* FROM StudentProfile INNER JOIN CurrentStudentInfo ON StudentProfile.StudentId = CurrentStudentInfo.StudentId INNER JOIN Machine_AttendanceSheet ON StudentProfile.StudentId = Machine_AttendanceSheet.StudentId where CurrentStudentInfo.BatchName='" + dlBatchName.SelectedItem.Text + "' and CurrentStudentInfo.Shift='" + dlShiftName.SelectedItem.Text + "' and CurrentStudentInfo.SectionName='" + ddlSection.SelectedItem.Text + "' and StudentProfile.FullName='" + dlRoll.SelectedItem.Text + "' Order by CurrentStudentInfo.RollNo ";
                            dtMonth.Rows.Add(getMonthName);
                        }
                    }


                    string[] getFields = { "FullName", "AdmissionNo", "RollNo", "StudentId" };
                    sqlDB.fillDataTable(attendanceQuery, dt=new DataTable());
                    int getDays = dt.Columns.Count - (4 + byte.Parse(getFromDate[0].ToString()));

                    if (b == 0)
                    {
                        for (byte f = 0; f < int.Parse(getFromDate[0]) - 1; f++)
                        {
                            dt.Columns.RemoveAt(4);
                        }
                    }
                    int tDays = DateTime.DaysInMonth(int.Parse(getFromDate[2]), b + int.Parse(getFromDate[1]));     // b+getToDate[0] for count from select month number  ,getToDate[1] for get year

                    if ((b + int.Parse(getFromDate[1])).ToString() == getToDate[1])
                    {
                        for (int f = int.Parse(getToDate[0]); f < tDays; f++)
                        {
                            if (b == 0) dt.Columns.RemoveAt(int.Parse(getToDate[0]) + 5 - int.Parse(getFromDate[0]));
                            else dt.Columns.RemoveAt(int.Parse(getToDate[0]) + 4);
                        }
                    }
                    ds.Tables.Add(dt);
                }

                Session["__dataTableDateRange__"] = ds;

                for (int j = 0; j < ds.Tables.Count; j++)
                {
                    int totalRows = ds.Tables[j].Rows.Count;
                    string divInfo = "";
                    string divInfoReport = "";

                    if (totalRows == 0)
                    {
                        divInfo = "<div class='noData'>No Student available</div>";
                        divInfo += "<div><div class='head'></div></div>";
                        //divMonthWiseAttendaceSheet.Controls.Add(new LiteralControl(divInfo));
                        goto Outer;
                        // return;
                    }

                    divInfo = "<h4> Month Name : " + dtMonth.Rows[j]["MonthName"] + " </h4>";
                    divInfoReport = "<h4> Month Name : " + dtMonth.Rows[j]["MonthName"] + " </h4>";
                    divInfo += " <table id='tblStudentList' class='display'  style='width:100%;margin:0px auto;' > ";
                    divInfoReport += " <table id='tblStudentList' class='display'  style='width:100%;margin:0px auto;' > ";
                    divInfo += "<thead>";
                    divInfoReport += "<thead>";
                    divInfo += "<tr>";
                    divInfoReport += "<tr>";

                    divInfo += "<th style='text-align:center; width:10%'>Admission No</th>";
                    divInfoReport += "<th style='text-align:center; width:10%'>Admission No</th>";
                    divInfo += "<th style='text-align:center;  width:8%'>Roll No</th>";
                    divInfoReport += "<th style='text-align:center;  width:8%'>Roll No</th>";
                    divInfo += "<th style=' width:25%'>Name</th>";
                    divInfoReport += "<th style=' width:25%'>Name</th>";
                    for (byte i = 4; i < ds.Tables[j].Columns.Count; i++)
                    {
                        string[] columnname = ds.Tables[j].Columns[i].ToString().Split('_');
                        string val = columnname[0];
                        string col = new String(val.Where(Char.IsNumber).ToArray());
                        divInfo += "<th style='text-align:center; padding:05px'>" + col + "</th>";
                        divInfoReport += "<th style='text-align:center; padding:05px'>" + col + "</th>";
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
                        id = ds.Tables[j].Rows[x]["StudentId"].ToString();
                        divInfo += "<tr id='r_" + id + "'>";
                        divInfoReport += "<tr id='r_" + id + "'>";
                        for (byte k = 0; k < ds.Tables[j].Columns.Count; k++)
                        {
                            if (k == 0)
                            {
                                divInfo += "<td style='width:13%; text-align:center' >" + ds.Tables[j].Rows[x]["AdmissionNo"].ToString() + "</td>";
                                divInfoReport += "<td style='width:13%; text-align:center' >" + ds.Tables[j].Rows[x]["AdmissionNo"].ToString() + "</td>";
                                divInfo += "<td style='width:13%; text-align:center' >" + ds.Tables[j].Rows[x]["RollNo"].ToString() + "</td>";
                                divInfoReport += "<td style='width:13%; text-align:center' >" + ds.Tables[j].Rows[x]["RollNo"].ToString() + "</td>";
                                divInfo += "<td style='width:13%' >" + ds.Tables[j].Rows[x]["FullName"].ToString() + "</td>";
                                divInfoReport += "<td style='width:13%' >" + ds.Tables[j].Rows[x]["FullName"].ToString() + "</td>";
                            }
                            else
                            {
                                if (ds.Tables[j].Rows[x][ds.Tables[j].Columns[k].ToString()].ToString() == "a")
                                {
                                    TA += 1;
                                    divInfo += "<td style='width:3%; color:red ; text-align:center' >" + ds.Tables[j].Rows[x][ds.Tables[j].Columns[k].ToString()].ToString() + "</td>";
                                    divInfoReport += "<td style='width:3%; color:red ; text-align:center' >" + ds.Tables[j].Rows[x][ds.Tables[j].Columns[k].ToString()].ToString() + "</td>";
                                }
                                else if (ds.Tables[j].Rows[x][ds.Tables[j].Columns[k].ToString()].ToString() == "p")
                                {
                                    TP += 1;
                                    divInfo += "<td style='width:3% ; text-align:center' >" + ds.Tables[j].Rows[x][ds.Tables[j].Columns[k].ToString()].ToString() + "</td>";
                                    divInfoReport += "<td style='width:3% ; text-align:center' >" + ds.Tables[j].Rows[x][ds.Tables[j].Columns[k].ToString()].ToString() + "</td>";
                                }
                                else if (ds.Tables[j].Rows[x][ds.Tables[j].Columns[k].ToString()].ToString() == "w" || ds.Tables[j].Rows[x][ds.Tables[j].Columns[k].ToString()].ToString() == "h")
                                {
                                    divInfo += "<td style='width:3% ; text-align:center ; padding-left:0px; border-bottom:0px solid white' >" + ds.Tables[j].Rows[x][ds.Tables[j].Columns[k].ToString()].ToString().ToLower() + "</td>";
                                    divInfoReport += "<td style='width:3% ; text-align:center ; padding-left:0px; border-bottom:0px solid white' >" + ds.Tables[j].Rows[x][ds.Tables[j].Columns[k].ToString()].ToString().ToLower() + "</td>";
                                }
                                else if (ds.Tables[j].Rows[x][ds.Tables[j].Columns[k].ToString()].ToString() == "")
                                {
                                    divInfo += "<td style='width:3% ; text-align:center;' >" + ds.Tables[j].Rows[x][ds.Tables[j].Columns[k].ToString()].ToString() + "</td>";
                                    divInfoReport += "<td style='width:3% ; text-align:center;' >" + ds.Tables[j].Rows[x][ds.Tables[j].Columns[k].ToString()].ToString() + "</td>";
                                }
                            }
                            if (ds.Tables[j].Columns.Count - 1 == k)
                            {
                                divInfo += "<td style='width:3% ; text-align:center ; font-weight:bold ; color:green; padding-left:0px' >" + TP + "</td>";
                                divInfoReport += "<td style='width:3% ; text-align:center ; font-weight:bold ; color:green; padding-left:0px' >" + TP + "</td>";
                                divInfo += "<td style='width:3% ; text-align:center ; font-weight:bold ; color:red; padding-left:0px ' >" + TA + "</td>";
                                divInfoReport += "<td style='width:3% ; text-align:center ; font-weight:bold ; color:red; padding-left:0px ' >" + TA + "</td>";
                                divInfo += "<td style='width:3%;' class='numeric control' >" + "<img src='/Images/gridImages/view.png' onclick='viewStudent(" + id + ");'  />";
                            }
                        }

                        TA = 0;
                        TP = 0;
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
                    allReport += divInfoReport;


                Outer:
                    continue;
                }

                Session["__AttendanceDetails__"] = allReport;

                Session["__ReportType__"] = "Attendance Sheet at " + dlMonth.SelectedItem.Text;
                lblMonthName.Text = "Attendance Sheet at " + dlMonth.SelectedItem.Text;
                lblBatchName.Text = "Batch : " + dlBatchName.SelectedItem.Text;
                lblShiftName.Text = "Shift : " + dlShiftName.SelectedItem.Text;
                lblSectionName.Text = "Section : " + ddlSection.SelectedItem.Text;


                Session["__Month__"] = "Attendance Info at " + dlMonth.SelectedItem.Text;
                Session["__BatchAttendance__"] = dlBatchName.SelectedItem.Text;
                Session["__ShiftAttendance__"] = dlShiftName.SelectedItem.Text;
                Session["__SectionAttendance__"] = ddlSection.SelectedItem.Text;
                Session["__MonthNameAttendance__"] = dlMonth.SelectedItem.Text;
                Session["__MonthName__"] = dtMonth;
            }
            catch { }
        }
    }
}