using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DS.DAL.AdviitDAL;
using DS.BLL.ControlPanel;
namespace DS.UI.Reports.StafforFaculty
{
    public partial class EmployeeList : System.Web.UI.Page
    {
        string sqlCmd = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.InnerText = "";
            if (!IsPostBack)
            {
                if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "EmployeeList.aspx", "")) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                Classes.commonTask.loadEmployeeTypeWithAll(rdblEmpList);
                loadTeacherInfo("");
            }
        }

        private void loadTeacherInfo(string condition)
        {
            try
            {
                
                if (string.IsNullOrEmpty(sqlCmd)) sqlCmd = "select EID,ECardNo,Format(EJoiningDate,'dd-MM-yyyy') as EJoiningDate,EName,DName,DesName,EMobile,EExaminer from v_EmployeeInfo " + condition + " order by ECardNo";

                DataTable dt = new DataTable();
                sqlDB.fillDataTable(sqlCmd, dt);
                Session["__EmployeeList__"] = dt;

                int totalRows = dt.Rows.Count;
                string divInfo = "";

                if (totalRows == 0)
                {
                    divInfo = "<div class='noData'>No Teacher available</div>";
                    divInfo += "<div><div class='head'></div></div>";
                    employeeList.Controls.Add(new LiteralControl(divInfo));
                    return;
                }


                divInfo = " <table id='tblTeacherList' class='display'  style='width:100%;margin:0px auto;' > ";
                divInfo += "<thead>";
                divInfo += "<tr>";

                divInfo += "<th>Card No</th>";
                divInfo += "<th>Joining Date</th>";
                divInfo += "<th>Name</th>";

                divInfo += "<th>Department</th>";
                divInfo += "<th>Designation</th>";
                divInfo += "<th>Mobile</th>";
                divInfo += "<th>Examiner</th>";



                divInfo += "</tr>";

                divInfo += "</thead>";

                divInfo += "<tbody>";

                string id = "";

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    id = dt.Rows[x]["EID"].ToString();
                    divInfo += "<tr id='r_" + id + "'>";
                    divInfo += "<td >" + dt.Rows[x]["ECardNo"].ToString() + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["EJoiningDate"].ToString() + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["EName"].ToString() + "</td>";

                    divInfo += "<td >" + dt.Rows[x]["DName"].ToString() + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["DesName"].ToString() + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["EMobile"].ToString() + "</td>";

                    if (dt.Rows[x]["EExaminer"].ToString() == "True")
                    {
                        divInfo += "<td >" + "Yes" + "</td>";
                    }
                    else
                    {
                        divInfo += "<td >" + "No" + "</td>";
                    }
                }

                divInfo += "</tbody>";
                divInfo += "<tfoot>";

                divInfo += "</table>";
                divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";

                employeeList.Controls.Add(new LiteralControl(divInfo));
            }
            catch { }
        }

        protected void btnPrintPreview_Click(object sender, EventArgs e)
        {
            string Reporttitle= rdblEmpList.SelectedItem.Text+" List";            
            DataTable dt = new DataTable();
            dt = (DataTable)Session["__EmployeeList__"];
            if (dt == null || dt.Rows.Count < 1) 
            {
                lblMessage.InnerText = "warning->Data not foud."; return;
            }
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=EmployeeList-" + Reporttitle + "');", true);  //Open New Tab for Sever side code
        }

        protected void rdblEmpList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string condition = "";
            if (rdblEmpList.SelectedValue != "All")
            {
                condition = " WHERE EmployeeTypeID="+ rdblEmpList.SelectedValue;
            }          
            loadTeacherInfo(condition);
        }
    }
}