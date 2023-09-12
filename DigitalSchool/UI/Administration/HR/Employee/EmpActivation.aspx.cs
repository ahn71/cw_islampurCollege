
using DS.BLL.ControlPanel;
using DS.BLL.HR;
using DS.DAL.AdviitDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.Administration.HR.Employee
{
    public partial class EmpActivation : System.Web.UI.Page
    {
        EmployeeEntry employeeEntry;
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                lblMessage.InnerText = "";
                if (!IsPostBack)
                {
                    try
                    {
                        if (Request.QueryString["hasperm"].ToString() != null) lblMessage.InnerText = "warning->You need permission to perform this action.";
                    }
                    catch { }

                    if (!PrivilegeOperation.SetPrivilegeControl(float.Parse(Session["__UserTypeId__"].ToString()), "EmpDetails.aspx")) Response.Redirect(Request.UrlReferrer.ToString() + "?&hasperm=no");
                    Classes.commonTask.loadEmployeeTypeWithAll(rblEmpType);
                    loadTeacherInfo("");
                    checkExitingSalary();
                }
            }
            catch { }
        }

        private void checkExitingSalary()
        {
            try
            {
                string EmployeeId = Request.QueryString["TeacherId"];
                if (EmployeeId == null) return;

                DataTable dt = new DataTable();
                SqlParameter[] prms = { new SqlParameter("@EId", EmployeeId) };
                sqlDB.fillDataTable("Select SaID, SaGovtOrBasic, SaSchool, SaTotal from Salaryset where EId=@EId ", prms, dt);

                if (dt.Rows.Count > 0)
                {
                    //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "call me", "existData();", true);
                    lblMessage.InnerText = "warning-> Salary Already Set";
                }
                else
                {
                    Response.Redirect("~/UI/Administration/HR/Payroll/SetSalary.aspx?TeacherId=" + EmployeeId);
                }
            }
            catch { }
        }
        private void loadTeacherInfo1(string sqlCmd)// (IsTeacher Replease by IsFaculty)
        {
            try
            {
                if (rblEmpType.SelectedValue == "All")
                {
                    sqlCmd = "select EID,EName,EGender,EFathersName,EMobile,IsFaculty from v_EmployeeInfo  where IsActive="+rblEmpActivation.SelectedValue;
                }                
                else
                {
                    sqlCmd = "select EID,EName,EGender,EFathersName,EMobile,IsFaculty from v_EmployeeInfo  where IsActive=" + rblEmpActivation.SelectedValue+" and EmployeeTypeID ="+rblEmpType.SelectedValue;                    
                }
                lblTitle.Text = rblEmpType.SelectedItem.Text;
                DataTable dt = new DataTable();
                sqlDB.fillDataTable(sqlCmd, dt);
                int totalRows = dt.Rows.Count;
                string divInfo = "";
                //
                divInfo = "<table id='tblTeacherInfo' class='table table-striped table-bordered dt-responsive nowrap'cellspacing='0' Width='100%'> ";
                divInfo += "<thead>";
                divInfo += "<tr>";
                divInfo += "<th>Full Name</th>";
                divInfo += "<th>Gender</th>";
                divInfo += "<th>Fathers Name</th>";
                divInfo += "<th>Mobile</th>";
                divInfo += "<th>IsFaculty</th>";
                if (Session["__View__"].ToString().Equals("true"))
                    divInfo += "<th style='text-align:center;'>View</th>";
                divInfo += "<th style='text-align:center;'>Edit</th>";
                divInfo += "<th style='text-align:center;'>Salary</th>";
                divInfo += "</tr>";
                divInfo += "</thead>";
                divInfo += "<tbody>";
                if (totalRows == 0)
                {
                    divInfo += "</tbody></table>";
                    //divTeacherInfo.Controls.Add(new LiteralControl(divInfo));
                    return;
                }
                string id = "";

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    string IsFaculty = (dt.Rows[x]["IsFaculty"].ToString() == "True") ? "Yes" : "No";
                    id = dt.Rows[x]["EID"].ToString();
                    divInfo += "<tr id='r_" + id + "'>";
                    // divInfo += "<td>" + adviitScripting.getNameForFixedLength(dt.Rows[x]["EName"].ToString(), 12) + "</td>";
                    divInfo += "<td>" + dt.Rows[x]["EName"].ToString() + "</td>";
                    divInfo += "<td>" + dt.Rows[x]["EGender"].ToString() + "</td>";
                    divInfo += "<td>" + dt.Rows[x]["EFathersName"].ToString() + "</td>";
                    divInfo += "<td>" + dt.Rows[x]["EMobile"].ToString() + "</td>";
                    divInfo += "<td>" + IsFaculty + "</td>";
                    if (Session["__View__"].ToString().Equals("true"))
                        divInfo += "<td class='numeric control' >" + "<img src='/Images/gridImages/view.png' width='30px' onclick='viewTeacher(" + id + ");'  />";
                    //divInfo += "<td class='numeric control' >" + "<img src='/Images/gridImages/edit.png' width='30px' onclick='editTeacher(" + id + ");'  />";
                    divInfo += "<td class='numeric control' >" + "<a  href='/UI/Administration/HR/Employee/EmpRegForm.aspx?TeacherId=" + id + "&Edit=True'>Edit</a>";
                    divInfo += "<td class='numeric control' >" + "<img src='/Images/gridImages/salaryset.png' width='30px' onclick='setSalary(" + id + ");'  />";
                }

                divInfo += "</tbody>";
                divInfo += "<tfoot>";
                divInfo += "</table>";
                //
                divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                //  divTeacherInfo.Controls.Add(new LiteralControl(divInfo));
            }
            catch { }
        }

        private void loadTeacherInfo(string sqlCmd)// (IsTeacher Replease by IsFaculty)
        {
            try
            {
                if (rblEmpType.SelectedValue == "1")
                {
                    sqlCmd = "select EID,EName,EGender,EFathersName,EMobile,IsFaculty,case when IsFaculty=1 then 'Yes' else 'No' end as IsFacultyText from v_EmployeeInfo where IsFaculty='True' and IsActive=" + rblEmpActivation.SelectedValue; ;
                    lblTitle.Text = "Teacher";
                }
                else if (rblEmpType.SelectedValue == "0")
                {
                    sqlCmd = "select EID,EName,EGender,EFathersName,EMobile,IsFaculty,case when IsFaculty=1 then 'Yes' else 'No' end as IsFacultyText from v_EmployeeInfo where IsFaculty='False' and IsActive=" + rblEmpActivation.SelectedValue; ;
                    lblTitle.Text = "Staff";
                }
                else
                {
                    sqlCmd = "select EID,EName,EGender,EFathersName,EMobile,IsFaculty,case when IsFaculty=1 then 'Yes' else 'No' end as IsFacultyText from v_EmployeeInfo where IsActive=" + rblEmpActivation.SelectedValue;
                    lblTitle.Text = "Teacher and Staff";
                }
               
                DataTable dt = new DataTable();
                sqlDB.fillDataTable(sqlCmd, dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    gvEmployeeList.DataSource = dt;
                    gvEmployeeList.DataBind();
                    if (rblEmpActivation.SelectedValue == "0")
                    {
                        gvEmployeeList.Columns[7].Visible = false;
                        gvEmployeeList.Columns[8].Visible = true;

                    }
                    else
                    {
                        gvEmployeeList.Columns[8].Visible = false;
                        gvEmployeeList.Columns[7].Visible = true;
                    }
                }
                else
                {
                    gvEmployeeList.DataSource = null;
                    gvEmployeeList.DataBind();
                }
                
            }
            catch { }
        }

        //protected void chkIsTeacher_CheckedChanged(object sender, EventArgs e)
        //{
        //    loadTeacherInfo("");
        //}

        protected void rblEmpType_SelectedIndexChanged(object sender, EventArgs e)
        {
          
            loadTeacherInfo("");
        }

        protected void rblEmpActivation_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadTeacherInfo("");
        }

        protected void gvEmployeeList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "InActive")
            {
                int rIndex = int.Parse(e.CommandArgument.ToString());
                string EID = gvEmployeeList.DataKeys[rIndex].Values[0].ToString();
                
                TextBox txtNote = (TextBox)gvEmployeeList.Rows[rIndex].FindControl("txtNote");

                if (employeeEntry == null)
                    employeeEntry = new EmployeeEntry();
                if (employeeEntry.UpdateCurrentEmpStatus(EID, "0"))
                {
                    employeeEntry.InsertToActivationLog(EID, txtNote.Text.Trim(), "0");
                    lblMessage.InnerText = "success-> Successfully Inactivated.";
                    gvEmployeeList.Rows[rIndex].Visible = false;
                }




            }
            else if (e.CommandName == "Active")
            {
                int rIndex = int.Parse(e.CommandArgument.ToString());
                string EID = gvEmployeeList.DataKeys[rIndex].Values[0].ToString();

                TextBox txtNote = (TextBox)gvEmployeeList.Rows[rIndex].FindControl("txtNote");

                if (employeeEntry == null)
                    employeeEntry = new EmployeeEntry();
                if (employeeEntry.UpdateCurrentEmpStatus(EID, "1"))
                {
                    employeeEntry.InsertToActivationLog(EID, txtNote.Text.Trim(), "1");
                    lblMessage.InnerText = "success-> Successfully Activated.";
                    gvEmployeeList.Rows[rIndex].Visible = false;
                }
            
            }
        }
    }
}