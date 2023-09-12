using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ComplexScriptingSystem;
using DS.DAL.AdviitDAL;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using DS.BLL;

namespace DS.Forms
{
    public partial class FacultyAttendanceSheetGenarate : System.Web.UI.Page
    {
        DataTable dt;
        SqlCommand cmd;
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
                    Classes.commonTask.loadMonths(dlMonths);
                    loadAttendanceSheetList();
                }
            }
        }
        protected void btnGenerator_Click(object sender, EventArgs e)
        {
            int days = DateTime.DaysInMonth(TimeZoneBD.getCurrentTimeBD().Year, dlMonths.SelectedIndex);
            createTtable(days);
        }
        private void createTtable(int days)
        {
            try
            {
                ArrayList dateFilds = new ArrayList();
                string getYear = new String(dlMonths.Text.Where(Char.IsNumber).ToArray());
                string dateField = "";

                for (byte b = 1; b <= days; b++)
                {
                    dateFilds.Add("D" + b + "_" + dlMonths.SelectedIndex + "_" + getYear);
                    if (b == days) dateField += "D" + b + "_" + dlMonths.SelectedIndex + "_" + getYear + " varchar(16)";
                    else dateField += "D" + b + "_" + dlMonths.SelectedIndex + "_" + getYear + " varchar(16),";
                }

                dt = new DataTable();
                cmd = new SqlCommand("CREATE TABLE Faculty_Staff_AttendanceSheet_" + dlMonths.Text.Trim() + " ( EID bigint ," + dateField + ",Foreign Key (EID) "
                +"REFERENCES EmployeeInfo(EID) On Update Cascade On Delete Cascade)", sqlDB.connection);
                int result = cmd.ExecuteNonQuery();
                dt = new DataTable();
                sqlDB.fillDataTable("select EID from EmployeeInfo where IsActive='True'", dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cmd = new SqlCommand("insert into Faculty_Staff_AttendanceSheet_" + dlMonths.Text.Trim() + " (EID) values (" + dt.Rows[i]["EID"] + ")", sqlDB.connection);
                    cmd.ExecuteNonQuery();
                }

                Read_N_Write_WH(days, getYear);
                saveAttendanceSheetName(getYear);  // for enterd sheet name in attendanceSheetInfo

                loadAttendanceSheetList();
                lblMessage.InnerText = "success-> Successfully Attendance Sheet Created";

            }
            catch { lblMessage.InnerText = "worning-> Already This Sheet Are Created"; }
        }
        private void Read_N_Write_WH(int days, string year)    // N=And,WH=Weekly Holiday
        {
            try
            {
                DateTime begin = new DateTime(int.Parse(year), dlMonths.SelectedIndex, 1);
                DateTime end = new DateTime(int.Parse(year), dlMonths.SelectedIndex, days);
                DataTable dtDate;
                sqlDB.fillDataTable("Select OffDate,Purpose From OffdaySettings where OffDate BETWEEN '" + begin.ToString("yyyy-MM-dd") + "' and '" + end.ToString("yyyy-MM-dd")
                    + "'", dt=new DataTable());
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
                        cmd = new SqlCommand("update Faculty_Staff_AttendanceSheet_" + dlMonths.Text.Trim() + " set " + "D" + whd[0] + "_" + whd[1] + "_" + whd[2] + Offday + "'", sqlDB.connection);
                        cmd.ExecuteNonQuery();

                    }
                    //string wh = begin.ToString("d-M-yyyy");
                    //string[] whd = wh.Split('-');
                    //cmd = new SqlCommand("update  Faculty_Staff_AttendanceSheet_" + dlMonths.Text.Trim() + " set " + "D" + whd[0] + "_" + whd[1] + "_" + whd[2] + "='WH'", sqlDB.connection);
                    //cmd.ExecuteNonQuery();
                    begin = begin.AddDays(1);
                }
            }
            catch { }
        }
        private void saveAttendanceSheetName(string getYear)
        {
            try
            {
                cmd = new SqlCommand("insert into FacultyNStaffAttendenceSheetInfo values ('Faculty_Staff_AttendanceSheet_" + dlMonths.Text.Trim() + "','" + getYear + "')", sqlDB.connection);
                cmd.ExecuteNonQuery();
            }
            catch { }
        }
        private void loadAttendanceSheetList()
        {
            try
            {

                DataTable dt = new DataTable();
                sqlDB.fillDataTable("Select ASName from FacultyNStaffAttendenceSheetInfo  Order by ASYear ", dt);

                int totalRows = dt.Rows.Count;
                string divInfo = "";

                if (totalRows == 0)
                {
                    divInfo = "<div class='noData'>No attendance sheet available</div>";
                    divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                    divAttendanceSheetList.Controls.Add(new LiteralControl(divInfo));
                    return;
                }

                divInfo = " <table id='tblSectionList' class='display'  > ";
                divInfo += "<thead>";
                divInfo += "<tr>";
                divInfo += "<th>SL</th>";
                divInfo += "<th>Sheet Name</th>";
                divInfo += "</tr>";
                divInfo += "</thead>";
                divInfo += "<tbody>";
                int i = 0;
                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    i++;
                    divInfo += "<tr><td class='numeric_control'>" + i + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["ASName"].ToString() + "</td></tr>";
                }
                divInfo += "</tbody>";
                divInfo += "<tfoot>";

                divInfo += "</table>";
                divAttendanceSheetList.Controls.Add(new LiteralControl(divInfo));
            }
            catch { }
        }
    }
}