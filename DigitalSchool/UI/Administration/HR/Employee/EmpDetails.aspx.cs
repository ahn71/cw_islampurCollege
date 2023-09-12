using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DS.DAL.AdviitDAL;
using DS.BLL.ControlPanel;

namespace DS.UI.Administration.HR.Employee
{
    public partial class EmpDetails : System.Web.UI.Page
    {
        string sqlCmd="";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                    lblMessage.InnerText = "";
                    if (!IsPostBack)
                    {
                    Classes.commonTask.loadEmployeeTypeWithAll(rblEmpType);
                        try
                        {
                            if (Request.QueryString["hasperm"].ToString() != null) lblMessage.InnerText = "warning->You need permission to perform this action.";
                        }
                        catch { }
                        if (!PrivilegeOperation.SetPrivilegeControl(float.Parse(Session["__UserTypeId__"].ToString()), "EmpDetails.aspx")) Response.Redirect(Request.UrlReferrer.ToString() + "?&hasperm=no"); 
                        loadTeacherInfo();
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

        private void loadTeacherInfo()// (IsTeacher Replease by IsFaculty)
        {
            try
            {
                if (rblEmpType.SelectedValue== "All")
                {
                    sqlCmd = "select EID,ECardNo,EName,EGender,EFathersName,EMobile,IsFaculty from v_EmployeeInfo where   IsActive=1";
                    
                }
              
                else 
                {
                    sqlCmd = "select EID,ECardNo,EName,EGender,EFathersName,EMobile,IsFaculty from v_EmployeeInfo where  IsActive=1 and EmployeeTypeID="+rblEmpType.SelectedValue;
                   
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
                divInfo += "<th>SL</th>";
                divInfo += "<th>ID No.</th>";
                divInfo += "<th>Full Name</th>";
                divInfo += "<th>Gender</th>";
                divInfo += "<th>Fathers Name</th>";
                divInfo += "<th>Mobile</th>";
                divInfo += "<th>Type</th>";
                if (Session["__View__"].ToString().Equals("true"))
                    divInfo += "<th style='text-align:center;'>View</th>";
                divInfo += "<th style='text-align:center;'>Edit</th>";
                //divInfo += "<th style='text-align:center;'>Salary</th>";
                divInfo += "</tr>";
                divInfo += "</thead>";
                divInfo += "<tbody>";
                if (totalRows == 0)
                {
                    divInfo += "</tbody></table>";                  
                    divTeacherInfo.Controls.Add(new LiteralControl(divInfo));
                    return;
                }
                string id = "";

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    string IsFaculty = (dt.Rows[x]["IsFaculty"].ToString() == "True") ? "Teacher" : "Staff"; 
                    id = dt.Rows[x]["EID"].ToString();
                    divInfo += "<tr id='r_" + id + "'>";
                    // divInfo += "<td>" + adviitScripting.getNameForFixedLength(dt.Rows[x]["EName"].ToString(), 12) + "</td>";
                    divInfo += "<td>" + (x + 1) + "</td>";
                    divInfo += "<td>" + dt.Rows[x]["ECardNo"].ToString()+ "</td>";
                    
                    divInfo += "<td>" + dt.Rows[x]["EName"].ToString()+ "</td>";
                    divInfo += "<td>" + dt.Rows[x]["EGender"].ToString() + "</td>";
                    divInfo += "<td>" + dt.Rows[x]["EFathersName"].ToString() + "</td>";
                    divInfo += "<td>" + dt.Rows[x]["EMobile"].ToString() + "</td>";
                    divInfo += "<td>" + IsFaculty + "</td>";
                    if (Session["__View__"].ToString().Equals("true"))
                        divInfo += "<td class='numeric control' >" + "<img src='/Images/gridImages/view.png' width='30px' onclick='viewTeacher(" + id + ");'  />";
                    //divInfo += "<td class='numeric control' >" + "<img src='/Images/gridImages/edit.png' width='30px' onclick='editTeacher(" + id + ");'  />";
                    divInfo += "<td class='numeric control' >" + "<a  href='/UI/Administration/HR/Employee/EmpRegForm.aspx?TeacherId=" + id + "&Edit=True'>Edit</a>";
                    //divInfo += "<td class='numeric control' >" + "<img src='/Images/gridImages/salaryset.png' width='30px' onclick='setSalary(" + id + ");'  />";
                }

                divInfo += "</tbody>";
                divInfo += "<tfoot>";
                divInfo += "</table>";
                //
                divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                divTeacherInfo.Controls.Add(new LiteralControl(divInfo));
            }
            catch { }
        }

        //protected void chkIsTeacher_CheckedChanged(object sender, EventArgs e)
        //{
        //    loadTeacherInfo("");
        //}

        protected void rblEmpType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "loaddatatable();", true);
            loadTeacherInfo();
        }
    }
}