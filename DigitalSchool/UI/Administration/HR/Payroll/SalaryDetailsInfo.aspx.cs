using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DS.DAL.AdviitDAL;
using DS.BLL.ControlPanel;

namespace DS.UI.Administration.HR.Payroll
{
    public partial class SalaryDetailsInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                    lblMessage.InnerText = "";
                    if (!IsPostBack)
                    {
                        LoadDepartment(dlDepartment);
                        if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "SalaryDetailsInfo.aspx", "")) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                        Session["__SalarySheet__"] = "";
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
                //if (string.IsNullOrEmpty(sqlCmd) && dlDepartment.SelectedItem.Text != "All" && dlTeacher.SelectedItem.Text != "All") sqlCmd = "select EID,EName,DName,"
                //+ "DesName,SaGovtOrBasic,SaSchool,SaTotal,SaStaus from v_SalarySetDetails Where DId='" + dlDepartment.SelectedValue + "' And EName='"
                //+ dlTeacher.SelectedItem.Text + "'  ";
                if (string.IsNullOrEmpty(sqlCmd) && dlTeacher.SelectedItem.Text != "All") sqlCmd = "select EID,EName,DName,"
            + "DesName,SaGovtOrBasic,SaSchool,SaTotal,SaStaus from v_SalarySetDetails  Where EId='" + dlTeacher.SelectedValue + "' ";
                else if (string.IsNullOrEmpty(sqlCmd) && dlDepartment.SelectedItem.Text == "All" && dlTeacher.SelectedItem.Text == "All") sqlCmd = "select EID,EName,DName,DesName,SaGovtOrBasic,SaSchool,SaTotal,"
                + "SaStaus from v_SalarySetDetails Order By DName ";
                else if (string.IsNullOrEmpty(sqlCmd) && dlDepartment.SelectedItem.Text != "All" && dlTeacher.SelectedItem.Text == "All") sqlCmd = "select EID,EName,DName,"
                + "DesName,SaGovtOrBasic,SaSchool,SaTotal,SaStaus from v_SalarySetDetails  Where DId='" + dlDepartment.SelectedValue + "' ";
               
                else if (sqlCmd == "SalaryRange" && dlSalaryType.SelectedItem.Text == "Basic Salary") sqlCmd = "select EID,EName,DName,DesName,SaGovtOrBasic,SaSchool,"
                + "SaTotal,SaStaus from v_SalarySetDetails Where SaGovtOrBasic BETWEEN " + txtFromSalary.Text.Trim() + " And " + txtToSalary.Text.Trim() + " Order By "
                + "DName  ";
                else if (sqlCmd == "SalaryRange" && dlSalaryType.SelectedItem.Text == "School Salary") sqlCmd = "select EID,EName,DName,DesName,SaGovtOrBasic,SaSchool,"
                + "SaTotal,SaStaus from v_SalarySetDetails Where SaSchool BETWEEN " + txtFromSalary.Text.Trim() + " And " + txtToSalary.Text.Trim() + " Order By DName  ";
                else if (sqlCmd == "SalaryRange" && dlSalaryType.SelectedItem.Text == "Total Salary") sqlCmd = "select EID,EName,DName,DesName,SaGovtOrBasic,SaSchool,"
                + "SaTotal,SaStaus from v_SalarySetDetails Where SaTotal BETWEEN " + txtFromSalary.Text.Trim() + " And " + txtToSalary.Text.Trim() + " Order By DName  ";
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
                   // divInfo += "<td >" + adviitScripting.getNameForFixedLength(dt.Rows[x]["EName"].ToString(), 12) + "</td>";
                    divInfo += "<td >" +dt.Rows[x]["EName"].ToString()+ "</td>";
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
                if (Session["__SalarySheet__"].ToString() == "") { lblMessage.InnerText = "warning->Please, first search then preview"; return; }
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "report call", "goToNewTab('/Report/SalarySheetReport.aspx');", true);
            }
            catch { }
        }

        protected void dlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {if(dlDepartment.SelectedValue!="00")
                sqlDB.bindDropDownList("Select EId,EName From v_EmployeeInfo Where DId='" + dlDepartment.SelectedValue + "' ", "EId", "EName", dlTeacher);
            else sqlDB.bindDropDownList("Select EId,EName From v_EmployeeInfo ", "EId", "EName", dlTeacher);
            dlTeacher.Items.Insert(0, new ListItem("...Select...", "0"));
            if (dlTeacher.Items.Count > 2)
                dlTeacher.Items.Insert(dlTeacher.Items.Count, new ListItem("All", "00"));
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
                dl.Items.Insert(0, new ListItem("...Select...", "0"));
                if (dl.Items.Count > 2)
                    dl.Items.Insert(dl.Items.Count, new ListItem("All", "00"));
             
            }
            catch { }
        }

        protected void btnSalaryRange_Click(object sender, EventArgs e)
        {
            loadSalarySetDetails("SalaryRange");
        }
    }
}