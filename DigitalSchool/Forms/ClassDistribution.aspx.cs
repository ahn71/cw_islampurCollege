using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DS.DAL.AdviitDAL;

namespace DS.Forms
{
    public partial class ClassDistribution : System.Web.UI.Page
    {
        DataSet ds;
        string totalTable;
        int clm = 0;
        static string reportType;
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
                        Classes.commonTask.LoadDepartment(dlDepartment);
                        Classes.commonTask.LoadDepartment(dlDepartmentWeek);

                        sqlDB.bindDropDownList("Select RoutineId From RoutineInfo ", "RoutineId", "RoutineId", dlRoutineId);
                        dlRoutineId.Items.Add("--Select--");
                        dlRoutineId.Text = "--Select--";
                        sqlDB.bindDropDownList("Select RoutineId From RoutineInfo ", "RoutineId", "RoutineId", dlRoutineIdDay);
                        sqlDB.bindDropDownList("Select RoutineId From RoutineInfo ", "RoutineId", "RoutineId", dlRoutineIdWeek);
                    }
                }
            }
            catch { }
        }

        protected void dlDepartmentWeek_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                sqlDB.bindDropDownList("Select TCodeNo,EName From v_EmployeeInfo Where DName='" + dlDepartmentWeek.SelectedItem.Text + "'  ", "TCodeNo", "EName", dlTeacherWeek);
            }
            catch { }
        }

        protected void dlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                sqlDB.bindDropDownList("Select TCodeNo,EName From v_EmployeeInfo Where DName='" + dlDepartment.SelectedItem.Text + "' ", "TCodeNo", "EName", dlTeacher);
            }
            catch { }
        }

        protected void btnSearchDayWise_Click(object sender, EventArgs e)
        {
            reportType = "DayWise";
            loadTeacherClassDayWise("DayWise");
        }
        private void loadTeacherClassDayWise(string command)
        {
            try
            {
                DataTable dt = new DataTable();

                if(command=="DayWise")
                {
                    if (txtIndexNo.Text == "")
                    {
                        DataTable dtEmId = new DataTable();
                        sqlDB.fillDataTable("Select EID From EmployeeInfo Where TCodeNo='" + dlTeacher.SelectedValue + "' ", dtEmId);

                        sqlDB.fillDataTable("Select Day, SubName, Convert(char(5), StartTime,108) AS StartTime, Convert(char(5),EndTime,108) AS EndTime, TCodeNo, RoutineId, EName, Shift From v_ClassRoutine Where EID='" + dtEmId.Rows[0]["EID"] + "' And Day='" + dlDay.SelectedItem.Text + "' And RoutineId='" + dlRoutineIdDay.SelectedItem.Text + "' Order By StartTime  ", dt);
                    }
                    else
                    {
                        sqlDB.fillDataTable("Select Day, SubName, Convert(char(5), StartTime,108) AS StartTime, Convert(char(5),EndTime,108) AS EndTime, TCodeNo, RoutineId, EName, Shift From v_ClassRoutine Where ECardNo='" + txtIndexNo.Text.Trim() + "' And Day='" + dlDay.SelectedItem.Text + "' And RoutineId='" + dlRoutineIdDay.SelectedItem.Text + "' Order By StartTime  ", dt);
                    }

                }
                else if (command == "Weekly")
                {
                    if (txtIndexNoWeek.Text == "")
                    {
                        DataTable dtEmId = new DataTable();
                        sqlDB.fillDataTable("Select EID From EmployeeInfo Where TCodeNo='" + dlTeacherWeek.SelectedValue + "' ", dtEmId);

                        sqlDB.fillDataTable("Select Day, SubName, Convert(char(5), StartTime,108) AS StartTime, Convert(char(5),EndTime,108) AS EndTime, TCodeNo, RoutineId, EName, Shift From v_ClassRoutine Where EID='" + dtEmId.Rows[0]["EID"] + "' And RoutineId='" + dlRoutineIdWeek.SelectedItem.Text + "' Order By OrderNo, StartTime   ", dt);
                    }
                    else
                    {
                        sqlDB.fillDataTable("Select Day, SubName, Convert(char(5), StartTime,108) AS StartTime, Convert(char(5),EndTime,108) AS EndTime, TCodeNo, RoutineId, EName, Shift From v_ClassRoutine Where ECardNo='" + txtIndexNoWeek.Text.Trim() + "' And RoutineId='" + dlRoutineIdWeek.SelectedItem.Text + "' Order By OrderNo,StartTime   ", dt);
                    }
                }

                string divInfo = "";

                divInfo = " <table id='tblClassDistribution' class='display'> ";
                divInfo += "<thead>";
                divInfo += "<tr>";
                divInfo += "<th class='numeric'>SL</th>";
                divInfo += "<th>Class</th>";
                divInfo += "<th>Class Time</th>";
                divInfo += "<th>Subject</th>";
                if(command == "Weekly") divInfo += "<th>Day</th>";

                divInfo += "</tr>";
                divInfo += "</thead>";

                divInfo += "<thead>";

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string [] className = dt.Rows[i]["RoutineId"].ToString().Split('_');
                    int sl = i + 1;

                    divInfo += "<tr>";
                    divInfo += "<td class='numeric' style='width:50px;'>" + sl + " </td>";
                    divInfo += "<td>  " + new String(className[0].Where(Char.IsLetter).ToArray()) + "</td>";
                    divInfo += "<td>  " + dt.Rows[i]["StartTime"] + "-" + dt.Rows[i]["EndTime"] + " </td>";
                    divInfo += "<td>  " + dt.Rows[i]["SubName"] + "</td>";
                    if (command == "Weekly") divInfo += "<td>  " + dt.Rows[i]["Day"] + "</td>";
                    divInfo += "</tr>";
                }
                divInfo += "</thead>";

                divInfo += "</table>";
                divClassInfo.Controls.Add(new LiteralControl(divInfo));

                Session["__ReportClassDis__"] = divInfo;
                Session["__TeacherName__"] = dt.Rows[0]["EName"].ToString();
                Session["__Shift__"] = dt.Rows[0]["Shift"].ToString();
            }
            catch { }
        }

        protected void btnSearchWeek_Click(object sender, EventArgs e)
        {
            reportType = "Weekly";
            loadTeacherClassDayWise("Weekly");
        }

        protected void btnPrintPreview_Click(object sender, EventArgs e)
        {
            if (reportType == "ClassRoutine") ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/Report/RoutinePrint.aspx');", true); 
            else ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/Report/ClassDistributionReport.aspx');", true);  //Open New Tab for Sever side code
        }

        protected void dlClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            reportType = "ClassRoutine";
            loadClassRoutine();
        }

        private void loadClassRoutine()
        {
            try
            {
                DataTable dtday = new DataTable();
                sqlDB.fillDataTable("Select distinct Day, OrderNo, Shift From v_ClassRoutine where RoutineId='" + dlRoutineId.SelectedItem.Text + "' ", dtday);

                DataTable dtDays = dtday.Select("", "OrderNo ASC").CopyToDataTable();
                dtday = dtDays;

                ds = new DataSet();

                for (int j = 0; j < dtday.Rows.Count; j++)
                {
                    DataTable dt = new DataTable();
                    sqlDB.fillDataTable("Select Day, SubName, Convert(char(5), StartTime,108) AS StartTime, Convert(char(5),EndTime,108) AS EndTime, TCodeNo From v_ClassRoutine where RoutineId='" + dlRoutineId.SelectedItem.Text + "' and Day='" + dtday.Rows[j]["Day"] + "' Order By StartTime ", dt);

                    ds.Tables.Add(dt);
                }
                Session["__Shift__"] = dtday.Rows[0]["Shift"].ToString();
                int tableColumn = 0;
                for (byte y = 0; y < ds.Tables.Count; y++)
                {
                    if (ds.Tables[y].Rows.Count > tableColumn)
                    {
                        tableColumn = ds.Tables[y].Rows.Count;
                    }
                }

                string divInfo = "";
                divInfo += "<div style='width:100%'>";//s
                divInfo = " <table id='tblClassRoutine' class='displayRoutine'  > ";
                divInfo += "<thead>";

                for (int x = 0; x < ds.Tables.Count; x++) //Main Loop
                {
                    divInfo += "<tr>";
                    for (byte b = 0; b < tableColumn; b++)
                    {
                        if (b == 0) divInfo += "<th>" + ds.Tables[x].Rows[b]["Day"] + "<br/> (" + ds.Tables[x].Rows[b]["StartTime"] + ")</th>";

                        if (ds.Tables[x].Rows.Count > clm)
                        {
                            divInfo += "<th>" + ds.Tables[x].Rows[b]["StartTime"] + "-" + ds.Tables[x].Rows[b]["EndTime"] + "<br/>" + ds.Tables[x].Rows[b]["SubName"] + " <br/>(" + ds.Tables[x].Rows[b]["TCodeNo"] + ")</th>";
                            clm++;
                        }
                        else divInfo += "<th> &nbsp; </th>";
                    }
                    clm = 0;
                    divInfo += "</tr>";
                }

                divInfo += "</thead>"; 
                divInfo += "</table>";
                divClassInfo.Controls.Add(new LiteralControl(divInfo));
                totalTable += divInfo;

                Session["__ClassRoutine__"] = totalTable;
                string[] classN = dlRoutineId.SelectedItem.Text.Split('_');
                Session["__ClassName__"] = classN[0].ToString();
            }
            catch { }
        }

    }
}