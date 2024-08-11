using DS.DAL.AdviitDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DS.Forms
{
    public partial class IndividualAbsentDetails : System.Web.UI.Page
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
                    Classes.commonTask.loadAttendanceSheet(dlSheetName);
                    dlSheetName.Items.Add("---Select Month---");
                    dlSheetName.SelectedIndex = dlSheetName.Items.Count - 1;
                }
            }
        }

        protected void dlSheetName_SelectedIndexChanged(object sender, EventArgs e)
        {
            dlRollNo.Items.Clear();
            string[] value = dlSheetName.SelectedItem.Text.Split('_');
            string Class = value[1];
            string Section = value[2];
            string MonthYear = value[3];
            string Year = new String(MonthYear.Where(Char.IsNumber).ToArray());
            string Batch = Class + Year;
            sqlDB.loadDropDownList("Select RollNo From CurrentStudentInfo where ClassName='" + Class + "' and SectionName='" + Section + "' and BatchName='" + Batch + "'", dlRollNo);
            dlRollNo.Items.Add("---Select Roll---");
            dlRollNo.SelectedIndex = dlRollNo.Items.Count - 1;

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string divInfo = "";
            string[] value = dlSheetName.SelectedItem.Text.Split('_');
            string Class = value[1];
            string Section = value[2];
            string MonthYear = value[3];
            string Year = new String(MonthYear.Where(Char.IsNumber).ToArray());
            string Batch = Class + Year;
            DataTable dtall = new DataTable();
            DataTable dtDis = new DataTable();
            sqlDB.fillDataTable("Select * From v_AbsentDetails where BatchName='" + Batch + "' and SectionName='" + Section + "' and RollNo="+dlRollNo.SelectedItem.Text+" ", dtall); //For All Data
            if (dtall.Rows.Count == 0)
            {
                divIndividualAbsent.Controls.Clear();
                
                divInfo = "<div class='noData'>No Absent available</div>";
                divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                divIndividualAbsent.Controls.Add(new LiteralControl(divInfo));
                return;
            }
            sqlDB.fillDataTable("Select Distinct RollNo,FullName,ImageName From v_AbsentDetails where BatchName='" + Batch + "' and SectionName='" + Section + "' and RollNo=" + dlRollNo.SelectedItem.Text + " ", dtDis); // For Distinct Data
            string studentId = "";
            string AbsentDays = "";
            int numberOfRecords = 0;
            DataRow[] rows;
            dtDis.Columns.Add("Days");
            dtDis.Columns.Add("Total Days");
            int j = 0;
            for (int i = 0; i < dtall.Rows.Count; i++)
            {
                studentId = dtall.Rows[i]["StudentId"].ToString();
                rows = dtall.Select("StudentId=" + studentId + "");
                numberOfRecords = rows.Length;
                for (int x = i; x < i + numberOfRecords; x++)
                {
                    AbsentDays += DateTime.Parse(dtall.Rows[x]["AbsentDate"].ToString()).ToString("dd");
                    if (x < i + numberOfRecords - 1)
                    {
                        AbsentDays += ",";
                    }
                }
                dtDis.Rows[j]["Days"] = AbsentDays;
                string[] total = dtDis.Rows[0]["Days"].ToString().Split(',');
                int count = 0;
                foreach(string dt in total)
                {
                    count++;
                }
                dtDis.Rows[j]["Total Days"] = count;
                j++;
                AbsentDays = "";
                i += numberOfRecords - 1;
            }
            Session["__AttendanceSheet__"] = dlSheetName.Text;
            Session["__AbsentDetails__"] = dtDis;
            for (int x = 0; x < dtDis.Rows.Count; x++)
            {
                divInfo = " <table id='tblStudentInfo' class='display'  > ";
                divInfo += "<thead>";
                divInfo += "<tbody>";
                string id = "";
                divInfo += "<tr>";
                divInfo += "<td>Roll No</td>";
                divInfo += "<td >" + dtDis.Rows[x]["RollNo"].ToString() + "</td>";
                divInfo += "</tr>";
                divInfo += "<tr>";
                divInfo += "<td>Name</td>";
                divInfo += "<td >" + dtDis.Rows[x]["FullName"].ToString() + "</td>";
                divInfo += "</tr>";
                divInfo += "<tr>";
                divInfo += "<td>Days</td>";
                divInfo += "<td >" + dtDis.Rows[x]["Days"].ToString() + "</td>";
                divInfo += "</tr>";
                divInfo += "<tr>";
                divInfo += "<td>Total Days</td>";
                divInfo += "<td >" + dtDis.Rows[x]["Total Days"].ToString() + "</td>";
                divInfo += "</tr>";
                divInfo += "<tr id='r_" + id + "'>";
                divInfo += "</tbody>";
                divInfo += "<tfoot>";
                divInfo += "</table>";
                divIndividualAbsent.Controls.Add(new LiteralControl(divInfo));
            }
        }

        protected void btnPrintPreview_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/Report/IndividualStudentAbsentReport.aspx');", true);  //Open New Tab for Sever side code
        }
    }
}