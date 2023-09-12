using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DS.DAL.AdviitDAL;
using DS.DAL.ComplexScripting;

namespace DS.UI.Academics.Attendance.Student.Manually
{
    public partial class AttendanceSheetGenerate : System.Web.UI.Page
    {
        SqlCommand cmd;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
                if (!IsPostBack)
                {
                    Classes.commonTask.loadMonths(dlMonths);
                    Classes.commonTask.loadClass(dlClass);
                    Classes.commonTask.loadSection(dlSection);
                    loadAttendanceSheetList();
                }
        }
        private void loadAttendanceSheetList()
        {
            try
            {
                DataTable dt = new DataTable();
                sqlDB.fillDataTable("Select ASName,Batch,Class,Section,Month,Year from AttendanceSheetInfo  Order by Year ", dt);
                int totalRows = dt.Rows.Count;
                string divInfo = "";
                divInfo = " <table id='tblSectionList' class='display'  > ";
                divInfo += "<thead>";
                divInfo += "<tr>";
                divInfo += "<th>SL</th>";
                divInfo += "<th>Sheet Name</th>";
                divInfo += "<th>Batch</th>";
                divInfo += "<th>Class</th>";
                divInfo += "<th>Section</th>";
                divInfo += "<th>Month</th>";
                divInfo += "<th>Year</th>";
                divInfo += "</thead>";
                divInfo += "<tbody>";
                if (totalRows == 0)
                {
                    divInfo += "<tr><td colspan='7'>Attendance sheet has not found</td></tr></table>";                    
                    divAttendanceSheetList.Controls.Add(new LiteralControl(divInfo));
                    return;
                }                
                int i = 0;
                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    i++;
                    divInfo += "<tr><td class='numeric_control'>" + i + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["ASName"].ToString() + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["Batch"].ToString() + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["Class"].ToString() + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["Section"].ToString() + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["Month"].ToString() + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["Year"].ToString() + "</td></tr>";
                }
                divInfo += "</tbody>";
                divInfo += "<tfoot>";
                divInfo += "</table>";
                divAttendanceSheetList.Controls.Add(new LiteralControl(divInfo));
            }
            catch { }
        }
        protected void btnGenerator_Click(object sender, EventArgs e)
        {
            if(!ChkSheetCreation())
            {
                string year = new String(dlMonths.Text.Trim().Where(Char.IsNumber).ToArray());
                int days = DateTime.DaysInMonth(DateTime.Now.Year, dlMonths.SelectedIndex);                
                if(createTtable(days))
                {
                    Read_N_Write_WH(days, year);
                }                
            }
            else
            {
                lblMessage.InnerText = "warning-> Already This Sheet Are Created"; 
            }            
        }
        private bool ChkSheetCreation()
        {
            bool result = false;
            string monthYear = dlMonths.Text.Trim();
            string year = new String(monthYear.Where(Char.IsNumber).ToArray());
            string month = new String(monthYear.Where(Char.IsLetter).ToArray());
            string className = dlClass.Text.Trim();
            string sectionName = dlSection.Text.Trim();      
            DataTable dt = new DataTable();
            sqlDB.fillDataTable("SELECT ASName FROM AttendanceSheetInfo " +
                                "WHERE Batch='" + className + year + "' AND " +
                                "Class='" + className + "' AND Section = '" + sectionName + "'" +
                                "AND Month = '" + month + "' AND Year='" + year + "'", dt);
            if(dt.Rows.Count > 0)
            {
                return result = true;
            }
            else
            {
                return result;
            }
        }
        private bool createTtable(int days)
        {
            bool output = true;
            try
            {
                ArrayList dateFilds = new ArrayList();
                string getYear = new String(dlMonths.Text.Where(Char.IsNumber).ToArray());
                string dateField = "";
                DataTable sdt = new DataTable();
                sqlDB.fillDataTable("SELECT StudentId FROM CurrentStudentInfo WHERE ClassName='" + dlClass.Text.Trim() + "' " +
                                    "AND SectionName ='" + dlSection.Text.Trim() + "' AND BatchName = '" + dlClass.Text.Trim() + getYear + "'", sdt);
                if(sdt.Rows.Count > 0)
                {
                    for (byte b = 1; b <= days; b++)
                    {
                        dateFilds.Add("D" + b + "_" + dlMonths.SelectedIndex + "_" + getYear);
                        if (b == days) dateField += "D" + b + "_" + dlMonths.SelectedIndex + "_" + getYear + " varchar(16)";
                        else dateField += "D" + b + "_" + dlMonths.SelectedIndex + "_" + getYear + " varchar(16),";
                    }

                    dt = new DataTable();
                    cmd = new SqlCommand("CREATE TABLE AttendanceSheet_" + dlClass.Text.Trim() + "_" + dlSection.Text.Trim() + "_" + dlMonths.Text.Trim() + " " +
                                         "( StudentId bigint ," + dateField + ",Foreign Key (StudentId) REFERENCES CurrentStudentInfo(StudentId) On Update Cascade " +
                                         "On Delete Cascade)", sqlDB.connection);
                    int result = cmd.ExecuteNonQuery();
                    for (int i = 0; i < sdt.Rows.Count; i++)
                    {
                        cmd = new SqlCommand("INSERT INTO AttendanceSheet_" + dlClass.Text.Trim() + "_" + dlSection.Text.Trim() + "_" + dlMonths.Text.Trim() + " (StudentId) values (" + sdt.Rows[i]["StudentId"] + ")", sqlDB.connection);
                        cmd.ExecuteNonQuery();
                    }                    
                    saveAttendanceSheetName(getYear);  // for enterd sheet name in attendanceSheetInfo
                    loadAttendanceSheetList();
                    lblMessage.InnerText = "success-> Successfully Attendance Sheet Created";
                    return output;
                } 
                else
                {
                    lblMessage.InnerText = "warning-> Batch: " + dlClass.Text.Trim() + getYear + ", Class: " + dlClass.Text.Trim() + ", " +
                                           "Section: " + dlSection.Text.Trim() + " have no student";
                    return output = false;
                }
            }
            catch { 
                lblMessage.InnerText = "warning-> Already This Sheet Are Created";
                return output = false;
            }
        }
        private void saveAttendanceSheetName(string getYear)
        {
            try
            {
                string className = dlClass.Text.Trim();
                string sectionName = dlSection.Text.Trim();
                string monthYear = dlMonths.Text.Trim();
                string month = new String(monthYear.Where(Char.IsLetter).ToArray());
                cmd = new SqlCommand("INSERT INTO AttendanceSheetInfo(ASName, Batch, Class, Section, Month, Year) values " +
                                     "('AttendanceSheet_" + className + "_" + sectionName + "_" + monthYear + "'," +
                                     "'" + className + getYear + "', '" + className + "', '" + sectionName + "', " +
                                     "'" + month + "','" + getYear + "')", sqlDB.connection);
                cmd.ExecuteNonQuery();
            }
            catch { }
        }
        private void Read_N_Write_WH(int days, string year)    // N=And,WH=Weekly Holiday
        {
            try
            {
                DateTime begin = new DateTime(int.Parse(year), dlMonths.SelectedIndex, 1);
                DateTime end = new DateTime(int.Parse(year), dlMonths.SelectedIndex, days);
                dt = new DataTable();
                DataTable dtDate;
                sqlDB.fillDataTable("SELECT OffDate,Purpose FROM OffdaySettings WHERE OffDate BETWEEN '" + begin.ToString("yyyy-MM-dd") + "' " +
                                    "AND '" + end.ToString("yyyy-MM-dd") + "'", dt);
                if(dt.Rows.Count > 0)
                {
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
                            cmd = new SqlCommand("UPDATE  AttendanceSheet_" + dlClass.Text.Trim() + "_" + dlSection.Text.Trim() + "_" + dlMonths.Text.Trim() + " SET " + "D" + whd[0] + "_" + whd[1] + "_" + whd[2] + "='" + Offday + "'", sqlDB.connection);
                            cmd.ExecuteNonQuery();
                        }
                        begin = begin.AddDays(1);
                    }
                }                
            }
            catch { }
        }       
        protected void dlClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadSectionClassWise();
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
                    dlSection.Items.Add("Science");
                    dlSection.Items.Add("Commerce");
                    dlSection.Items.Add("Arts");
                    dlSection.Items.Add(new ListItem("...Select...", "0"));
                    dlSection.SelectedIndex = dlSection.Items.Count - 1;
                }
                else
                {
                    dlSection.Items.Clear();
                    sqlDB.loadDropDownList("Select  SectionName from Sections where SectionName<>'Science' AND SectionName<>'Commerce' AND SectionName<>'Arts' Order by SectionName", dlSection);
                    dlSection.Items.Add(new ListItem("...Select...", "0"));
                    dlSection.SelectedIndex = dlSection.Items.Count - 1;
                }
            }
            catch { }
        }        
    }
}