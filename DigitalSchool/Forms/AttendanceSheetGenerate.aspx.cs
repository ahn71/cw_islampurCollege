using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DS.DAL.AdviitDAL;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using ComplexScriptingSystem;
using DS.BLL;

namespace DS.Forms
{
    public partial class AttendanceSheetGenerate : System.Web.UI.Page
    {
        SqlCommand cmd;
        DataTable dt;

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
                    Classes.commonTask.loadClass(dlClass);
                    Classes.commonTask.loadSection(dlSection);
                    loadAttendanceSheetList();
                }
            }
        }
        protected void btnGenerator_Click(object sender, EventArgs e)
        {
            int days = DateTime.DaysInMonth(TimeZoneBD.getCurrentTimeBD().Year,dlMonths.SelectedIndex);
            createTtable( days);          
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
                    dateFilds.Add("D"+b+"_"+dlMonths.SelectedIndex+"_"+getYear);
                    if (b == days) dateField += "D" + b + "_" + dlMonths.SelectedIndex + "_" + getYear + " varchar(16)";
                    else dateField += "D" + b + "_" + dlMonths.SelectedIndex + "_" + getYear + " varchar(16),";
                }

                dt = new DataTable();
                cmd = new SqlCommand("CREATE TABLE AttendanceSheet_"+ dlClass.Text.Trim() + "_" + dlSection.Text.Trim() + "_" + dlMonths.Text.Trim() + " ( StudentId bigint ," + dateField + ",Foreign Key (StudentId) REFERENCES StudentProfile(StudentId) On Update Cascade On Delete Cascade)", sqlDB.connection);
                int result=cmd.ExecuteNonQuery();
                dt=new  DataTable();
                sqlDB.fillDataTable("select StudentId from CurrentStudentInfo where ClassName='" + dlClass.Text.Trim() + "' AND SectionName ='" + dlSection.Text.Trim() + "'", dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cmd = new SqlCommand("insert into AttendanceSheet_" + dlClass.Text.Trim() + "_" + dlSection.Text.Trim() + "_" + dlMonths.Text.Trim() + " (StudentId) values (" + dt.Rows[i]["StudentId"] + ")", sqlDB.connection);
                    cmd.ExecuteNonQuery();
                }

                Read_N_Write_WH(days, getYear);
                saveAttendanceSheetName(getYear);  // for enterd sheet name in attendanceSheetInfo

                loadAttendanceSheetList();
                lblMessage.InnerText = "success-> Successfully Attendance Sheet Created";

            }
            catch { lblMessage.InnerText = "worning-> Already This Sheet Are Created"; }
        }

     
        private void Read_N_Write_WH(int days,string year)    // N=And,WH=Weekly Holiday
        {
            try
            {
                DateTime begin = new DateTime(int.Parse(year),dlMonths.SelectedIndex,1);
                DateTime end = new DateTime(int.Parse(year),dlMonths.SelectedIndex,days);
                dt = new DataTable();
                DataTable dtDate;
                sqlDB.fillDataTable("Select OffDate,Purpose From OffdaySettings where OffDate BETWEEN '" + begin.ToString("yyyy-MM-dd") + "' and '" + end.ToString("yyyy-MM-dd") + "'", dt);
                while(begin <= end)
                {
                    dtDate = new DataTable();
                    try
                    {
                        dtDate = dt.Select(" OffDate='" + begin.ToString("yyyy-MM-dd") + "'").CopyToDataTable();
                    }
                    catch { }
                    if (dtDate.Rows.Count>0)
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
                        cmd = new SqlCommand("update  AttendanceSheet_" + dlClass.Text.Trim() + "_" + dlSection.Text.Trim() + "_" + dlMonths.Text.Trim() + " set " + "D" + whd[0] + "_" + whd[1] + "_" + whd[2] + "='"+Offday+"'", sqlDB.connection);
                        cmd.ExecuteNonQuery();
                        
                    }
                    begin=begin.AddDays(1);

                }
            }
            catch { }
        }

        private void saveAttendanceSheetName(string getYear)
        {
            try
            {
                cmd = new SqlCommand("insert into AttendanceSheetInfo values ('AttendanceSheet_"+ dlClass.Text.Trim() + "_" + dlSection.Text.Trim() + "_" + dlMonths.Text.Trim() +"','"+getYear+"')",sqlDB.connection);
                cmd.ExecuteNonQuery();
            }
            catch { }
        }

        private void loadAttendanceSheetList()
        {
            try
            {
                
                DataTable dt = new DataTable();
                sqlDB.fillDataTable("Select ASName from AttendanceSheetInfo  Order by ASYear ",dt);

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
        private void loadSectionClassWise()
        {
            try
            {
                DataTable dt;
                SQLOperation.selectBySetCommandInDatatable("Select ClassOrder From Classes where ClassName='" + dlClass.SelectedItem.Text + "'", dt = new DataTable(), sqlDB.connection);
                if (byte.Parse(dt.Rows[0]["ClassOrder"].ToString()) >= 9)
                {
                    dlSection.Items.Clear();
                    dlSection.Items.Add("...Select...");
                    dlSection.Items.Add("Science");
                    dlSection.Items.Add("Commerce");
                    dlSection.Items.Add("Arts");
                    dlSection.SelectedIndex = dlSection.Items.Count - dlSection.Items.Count;
                }
                else
                {
                    dlSection.Items.Clear();
                    sqlDB.loadDropDownList("Select  SectionName from Sections where SectionName<>'Science' AND SectionName<>'Commerce' AND SectionName<>'Arts' Order by SectionName", dlSection);
                    dlSection.Items.Add("...Select...");
                    dlSection.SelectedIndex = dlSection.Items.Count - 1;
                }
            }
            catch { }
        }

        protected void dlClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadSectionClassWise();
        }
    }
}