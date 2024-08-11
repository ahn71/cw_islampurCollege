using DS.DAL.AdviitDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.Forms
{
    public partial class SalaryDetailsInfo : System.Web.UI.Page
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
                        LoadDepartment(dlDepartment);
                    }
                }
            }
            catch { }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            loadSalarySetDetails("");
        }

        private void loadSalarySetDetails(string sqlCmd)
        {
            try
            {
                if (string.IsNullOrEmpty(sqlCmd) && dlDepartment.SelectedItem.Text != "All" && dlTeacher.SelectedItem.Text != "All") sqlCmd = "select EID,EName,DName,"
                +"DesName,SaGovtOrBasic,SaSchool,SaTotal,SaStaus from v_SalarySetDetails Where DName='" + dlDepartment.SelectedItem.Text + "' And EName='" 
                + dlTeacher.SelectedItem.Text + "'  ";
                else if (string.IsNullOrEmpty(sqlCmd) && dlDepartment.SelectedItem.Text == "All") sqlCmd = "select EID,EName,DName,DesName,SaGovtOrBasic,SaSchool,SaTotal,"
                +"SaStaus from v_SalarySetDetails Order By DName ";
                else if (string.IsNullOrEmpty(sqlCmd) && dlDepartment.SelectedItem.Text != "All" && dlTeacher.SelectedItem.Text == "All") sqlCmd = "select EID,EName,DName,"
                +"DesName,SaGovtOrBasic,SaSchool,SaTotal,SaStaus from v_SalarySetDetails  Where DName='" + dlDepartment.SelectedItem.Text + "' ";
                else if (sqlCmd == "SalaryRange" && dlSalaryType.SelectedItem.Text == "Basic Salary") sqlCmd = "select EID,EName,DName,DesName,SaGovtOrBasic,SaSchool,"
                +"SaTotal,SaStaus from v_SalarySetDetails Where SaGovtOrBasic BETWEEN " + txtFromSalary.Text.Trim() + " And " + txtToSalary.Text.Trim() + " Order By "
                +"DName  ";
                else if (sqlCmd == "SalaryRange" && dlSalaryType.SelectedItem.Text == "School Salary") sqlCmd = "select EID,EName,DName,DesName,SaGovtOrBasic,SaSchool,"
                +"SaTotal,SaStaus from v_SalarySetDetails Where SaSchool BETWEEN " + txtFromSalary.Text.Trim() + " And " + txtToSalary.Text.Trim() + " Order By DName  ";
                else if (sqlCmd == "SalaryRange" && dlSalaryType.SelectedItem.Text == "Total Salary") sqlCmd = "select EID,EName,DName,DesName,SaGovtOrBasic,SaSchool,"
                +"SaTotal,SaStaus from v_SalarySetDetails Where SaTotal BETWEEN " + txtFromSalary.Text.Trim() + " And " + txtToSalary.Text.Trim() + " Order By DName  ";
                DataTable dt = new DataTable();
                sqlDB.fillDataTable(sqlCmd, dt);
                int totalRows = dt.Rows.Count;
                string divInfo = "";
                if (totalRows == 0)
                {
                    divInfo = "<div class='noData'>Employee Salary Not Set</div>";
                    divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                    divSalaryDetailsInfo.Controls.Add(new LiteralControl(divInfo));
                    return;
                }

                divInfo = " <table id='tblEmployeeSalary' class='display'  style='width:100%;margin:0px auto;' > ";
                divInfo += "<thead>";
                divInfo += "<tr>";
                divInfo += "<th>Name</th>";
                divInfo += "<th>Department </th>";
                divInfo += "<th>Designation </th>";
                divInfo += "<th class='numeric'>Basic</th>";
                divInfo += "<th class='numeric' >School</th>";
                divInfo += "<th class='numeric' >Total</th>";
                divInfo += "<th >Staus</th>";
                divInfo += "</tr>";
                divInfo += "</thead>";
                divInfo += "<tbody>";
                string id = "";
                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    id = dt.Rows[x]["EID"].ToString();
                    divInfo += "<tr id='r_" + id + "'>";
                    divInfo += "<td >" + adviitScripting.getNameForFixedLength(dt.Rows[x]["EName"].ToString(), 12) + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["DName"].ToString() + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["DesName"].ToString() + "</td>";
                    divInfo += "<td class='numeric'>" + dt.Rows[x]["SaGovtOrBasic"].ToString() + "</td>";
                    divInfo += "<td class='numeric' >" + dt.Rows[x]["SaSchool"].ToString() + "</td>";
                    divInfo += "<td class='numeric' >" + dt.Rows[x]["SaTotal"].ToString() + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["SaStaus"].ToString() + "</td>";
                }

                divInfo += "</tbody>";
                divInfo += "<tfoot>";
                divInfo += "</table>";
                divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                divSalaryDetailsInfo.Controls.Add(new LiteralControl(divInfo));
                Session["__SalarySheet__"] = divInfo;
            }
            catch { }
        }

        protected void btnPrintPreview_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "report call", "goToNewTab('/Report/SalarySheetReport.aspx');", true);
            }
            catch { }
        }

        protected void dlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                sqlDB.bindDropDownList("Select TCodeNo,EName From v_EmployeeInfo Where DName='" + dlDepartment.SelectedItem.Text + "' ", "TCodeNo", "EName", dlTeacher);
                dlTeacher.Items.Add("All");
                dlTeacher.Items.Add("--Select--");
                dlTeacher.Text = "--Select--";
            }
            catch { }
        }

        private void LoadDepartment(DropDownList dl)
        {
            try
            {
                dl.Items.Clear();
                DataTable dt = new DataTable();
                sqlDB.fillDataTable("Select DId,DName From Departments_HR where DStatus='True' and DName!='MLS'", dt);
                dl.DataSource = dt;
                dl.DataTextField = "DName";
                dl.DataValueField = "DId";
                dl.DataBind();
                dl.Items.Add("All");
                dl.Items.Add("--Select--");
                dl.Text = "--Select--";
            }
            catch { }
        }

        protected void btnSalaryRange_Click(object sender, EventArgs e)
        {
            loadSalarySetDetails("SalaryRange");
        }
    }
}