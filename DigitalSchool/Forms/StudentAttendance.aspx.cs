using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

using DS.DAL.AdviitDAL;

namespace DS.Forms
{
    public partial class StudentAttendance : System.Web.UI.Page
    {
        DataTable dt;
        SqlDataAdapter da;
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
                    Classes.commonTask.loadClass(dlClass);
                    Classes.commonTask.loadSection(dlSection);
                    Classes.commonTask.loadMonths(dlMonths);
                }
            }
        }

        private void loadAttendanceSheet()
        {
            try
            {
                lblMessage.InnerText = "";
                AttendanceSheetTitle.InnerText = "";
                string batch = new String(dlMonths.Text.Where(Char.IsNumber).ToArray());
                batch = dlClass.Text + batch;
                dt = new DataTable();
                da = new SqlDataAdapter();
                da = new SqlDataAdapter("SELECT StudentProfile.FullName, CurrentStudentInfo.RollNo,AttendanceSheet_" + dlClass.Text.Trim() + "_" + dlSection.Text.Trim() + "_" + dlMonths.Text.Trim() + ".* FROM StudentProfile INNER JOIN CurrentStudentInfo ON StudentProfile.StudentId = CurrentStudentInfo.StudentId INNER JOIN AttendanceSheet_" + dlClass.Text.Trim() + "_" + dlSection.Text.Trim() + "_" + dlMonths.Text.Trim() + " ON StudentProfile.StudentId = AttendanceSheet_" + dlClass.Text.Trim() + "_" + dlSection.Text.Trim() + "_" + dlMonths.Text.Trim() + ".StudentId Where CurrentStudentInfo.Shift='"+dlShift.SelectedItem.Text+"'  Order By CurrentStudentInfo.RollNo ", sqlDB.connection);
                da.Fill(dt=new DataTable());
                AttendanceSheetTitle.Style["Color"] = "green";
                AttendanceSheetTitle.InnerText = "Attendance sheet of " + dlClass.Text + "(" + dlSection.Text + ") " + dlMonths.Text + "";                
                System.Threading.Thread.Sleep( TimeSpan.FromSeconds(dt.Rows.Count));
                string tbl = "";
                string tblInputElement = "";
                for (byte b = 3; b < dt.Columns.Count; b++)
                {
                    string[] col = dt.Columns[b].ToString().Split('_');
                    string col1 = col[0];
                    col1 = new String(col1.Where(Char.IsNumber).ToArray());
                    tbl += "<th style='width: 76px;text-align:center'>" + col1 + "</th>";                  
                }

                string tableInfo = "";
                tableInfo = "<table id='tblStudentAttendance'   >";
                tableInfo += " <th style='width: 70px'>Roll</th> <th style='width: 280px'>Name</th>" + tbl + "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    tblInputElement = "";
                    int row = i+1;               
                    for (byte b =3;b < dt.Columns.Count;b++)   // this loop generate every student inputbox 
                    {
                        if (dt.Rows[i].ItemArray[b].ToString().Equals("w") || dt.Rows[i].ItemArray[b].ToString().Equals("h"))
                        {
                            tblInputElement += "<td  style='width: 50px'> <input autocomplete='off' style='background-color:#980000 ;color:White; text-align:center'  readonly='true' tabindex=" + row + " onchange='saveData(this)' type=text id='AttendanceSheet_" + dlClass.Text.Trim() + "_" + dlSection.Text.Trim() + "_" + dlMonths.Text.Trim() + ":" + dt.Columns[b].ToString() + ":" + dt.Rows[i]["StudentId"] + "' value=" + dt.Rows[i].ItemArray[b].ToString() + "> </td>";  // this line for hilight weekly liholyday 
                        }
                        else
                        {
                            if (dt.Rows[i].ItemArray[b].ToString().Trim().Length >= 1) tblInputElement += "<td style='width: 50px'> <input readonly='true' style='text-align:center' autocomplete='off' tabindex=" + row + " onchange='saveData(this)' type=text id='AttendanceSheet_" + dlClass.Text.Trim() + "_" + dlSection.Text.Trim() + "_" + dlMonths.Text.Trim() + ":" + dt.Columns[b].ToString() + ":" + dt.Rows[i]["StudentId"] + ":" + batch + "' value=" + dt.Rows[i].ItemArray[b].ToString() + "> </td>";
                            else tblInputElement += "<td style='width: 50px'> <input style='text-align:center' autocomplete='off' tabindex=" + row + " onchange='saveData(this)' onkeydown='return acceptValidCharacter(event, this)' MaxLength='1' type=text id='AttendanceSheet_" + dlClass.Text.Trim() + "_" + dlSection.Text.Trim() + "_" + dlMonths.Text.Trim() + ":" + dt.Columns[b].ToString() + ":" + dt.Rows[i]["StudentId"] + ":" + batch + "' value=" + dt.Rows[i].ItemArray[b].ToString() + "> </td>";
                        }
                        row += dt.Rows.Count;
                    }
                    tableInfo += "<tr> <td style='width: 80px'> " + dt.Rows[i]["RollNo"].ToString() + "</td>  <td style='width: 60px'>" + dt.Rows[i]["FullName"].ToString() + "</td>" + tblInputElement + "</tr>";
                }
                tableInfo += "</table>";
                divTable.Controls.Add(new LiteralControl(tableInfo));
                divTable.Visible = true;
            }
            catch 
            {
                   AttendanceSheetTitle.Style["Color"] ="Red";
                   AttendanceSheetTitle.InnerText = "Sorry this attendance sheet is not created";
                   divTable.Visible = false;
            }
        }

        protected void btnProcess_Click(object sender, EventArgs e)
        {
            loadAttendanceSheet();
        }

        private void processing()
        {
            try
            {
                loadAttendanceSheet();
            }
            catch { }

        }
        protected void btnPreview_Click(object sender, EventArgs e)
        {
            lblMessage.InnerText = "";
            if (loadAttendanceReport() == false)
            {
                lblMessage.InnerText = "warning->Attendance Not Available";
                return;
            }
            string batch = new String(dlMonths.Text.Where(Char.IsNumber).ToArray());
            batch = dlClass.Text + batch;
            Session["__Batch__"] = "Batch Name: " + batch;
            Session["__Shift__"] = "Shift Name: " + dlShift.SelectedItem.Text;
            Session["__Section__"] = "Section Name: " + dlSection.Text;     
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/Report/AttendanceDetailsReport.aspx');", true);  //Open New Tab for Sever side code
        }
        private Boolean loadAttendanceReport()
        {
            try
            {
                string batch = new String(dlMonths.Text.Where(Char.IsNumber).ToArray());
                batch = dlClass.Text + batch;
                string findTbl = "AttendanceSheet_" + dlClass.SelectedItem.Text + "_" + dlSection.SelectedItem.Text + "_" + dlMonths.SelectedItem.Text + "";
                string attendanceQuery = "SELECT StudentProfile.FullName,StudentProfile.AdmissionNo,CurrentStudentInfo.RollNo, " + findTbl + ".* FROM StudentProfile "
                +"INNER JOIN CurrentStudentInfo ON StudentProfile.StudentId = CurrentStudentInfo.StudentId INNER JOIN " + findTbl + " ON StudentProfile.StudentId = " 
                + findTbl + ".StudentId where CurrentStudentInfo.BatchName='" + batch + "' and CurrentStudentInfo.Shift='" + dlShift.SelectedItem.Text + "' and "
                +"CurrentStudentInfo.SectionName='" + dlSection.SelectedItem.Text + "' Order by CurrentStudentInfo.RollNo ";
                sqlDB.fillDataTable(attendanceQuery, dt=new DataTable());
              
                //..........................For Sheet ..............................

                int totalRows = dt.Rows.Count;
                string divInfo = "";
                string divInfoReport = "";
                if (totalRows == 0)
                {
                    divInfo = "<div class='noData'>No Student available</div>";
                    divInfo += "<div><div class='head'></div></div>";
                    return false;
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
                            else if (dt.Rows[x][dt.Columns[k].ToString()].ToString() == "w" || dt.Rows[x][dt.Columns[k].ToString()].ToString() == "h")
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
                Session["__AttendanceDetails__"] = divInfoReport;
                Session["__ReportType__"] = "Attendance Sheet at " + dlMonths.SelectedItem.Text;
                Session["__Month__"] = "Attendance Info at " + dlMonths.SelectedItem.Text;
                Session["__BatchAttendance__"] = batch;
                Session["__ShiftAttendance__"] = dlShift.SelectedItem.Text;
                Session["__SectionAttendance__"] = dlSection.SelectedItem.Text;
                Session["__MonthNameAttendance__"] = dlMonths.SelectedItem.Text;
                Session["__dataTableDateRange__"] = null;
                return true;
            }
            catch { return false; }
        }      
    }
}