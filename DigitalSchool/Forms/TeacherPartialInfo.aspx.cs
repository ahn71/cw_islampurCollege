using DS.DAL.AdviitDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.Forms
{
    public partial class TeacherPartialInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Session["__UserId__"] = "oitl";
                if (Session["__UserId__"] == null)
                {
                    Response.Redirect("~/UserLogin.aspx");
                }
                else
                {
                    if (!IsPostBack)
                    {
                        loadTeacherInfo("");
                        checkExitingSalary();
                    }
                }
               
            }
            catch { }
        }

        private void checkExitingSalary()
        {
            try
            {
                string  EmployeeId = Request.QueryString["TeacherId"];
                if (EmployeeId == null) return;

                DataTable dt = new DataTable();
                SqlParameter[] prms = { new SqlParameter("@EId", EmployeeId) };
                sqlDB.fillDataTable("Select SaID, SaGovtOrBasic, SaSchool, SaTotal from Salaryset where EId=@EId ", prms, dt);

                if (dt.Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page,this.GetType(),"call me","existData();",true);
                   // lblMessage.InnerText = "warning-> Salary Allrady Set";
                }
                else
                {
                    Response.Redirect("/Forms/SetSalary.aspx?TeacherId=" + EmployeeId );
                }
            }
            catch { }
        }

        private void loadTeacherInfo(string sqlCmd)
        {
            try
            {
                if (chkIsTeacher.Checked)
                {
                    sqlCmd = "select EID,EName,EGender,EFathersName,EMobile,IsTeacher from v_EmployeeInfo where IsTeacher='True' ";
                    lblTitle.Text = "Employee";
                }
                else
                {
                    sqlCmd = "select EID,EName,EGender,EFathersName,EMobile,IsTeacher from v_EmployeeInfo where IsTeacher='False' ";
                    lblTitle.Text = "Staff";
                }

                DataTable dt = new DataTable();
                sqlDB.fillDataTable(sqlCmd, dt);

                int totalRows = dt.Rows.Count;
                string divInfo = "";

                if (totalRows == 0)
                {
                    divInfo = "<div class='noData'></div>";
                    divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                    divTeacherInfo.Controls.Add(new LiteralControl(divInfo));
                    return;
                }


                divInfo = " <table id='tblTeacherInfo' class='display'  style='width:100%; margin:0px auto;' > ";
                divInfo += "<thead>";
                divInfo += "<tr>";
                divInfo += "<th>Full Name</th>";
                divInfo += "<th>Gender</th>";
                divInfo += "<th>Fathers Name</th>";
                divInfo += "<th>Mobile</th>";
                divInfo += "<th>IsTeacher</th>";
                divInfo += "<th class='numeric control'>View</th>";
                divInfo += "<th class='numeric control'>Edit</th>";
                divInfo += "<th class='numeric control'>Salary</th>";
                divInfo += "</tr>";
                divInfo += "</thead>";
                divInfo += "<tbody>";
                string id = "";

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    id = dt.Rows[x]["EID"].ToString();
                    divInfo += "<tr id='r_" + id + "'>";
                    divInfo += "<td>" + adviitScripting.getNameForFixedLength(dt.Rows[x]["EName"].ToString(), 12) + "</td>";
                    divInfo += "<td>" + dt.Rows[x]["EGender"].ToString() + "</td>";
                    divInfo += "<td>" + dt.Rows[x]["EFathersName"].ToString() + "</td>";
                    divInfo += "<td>" + dt.Rows[x]["EMobile"].ToString() + "</td>";
                    divInfo += "<td>" + dt.Rows[x]["IsTeacher"].ToString() + "</td>";
                    divInfo += "<td style='max-width:20px;' class='numeric control' >" + "<img src='/Images/gridImages/view.png' onclick='viewTeacher(" + id + ");'  />";
                    divInfo += "<td style='max-width:20px;' class='numeric control' >" + "<img src='/Images/gridImages/edit.png'  onclick='editTeacher(" + id + ");'  />";
                    divInfo += "<td style='max-width:20px;' class='numeric control' >" + "<img src='/Images/gridImages/salaryset.png'  onclick='setSalary(" + id + ");'  />";
                }

                divInfo += "</tbody>";
                divInfo += "<tfoot>";
                divInfo += "</table>";
                divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                divTeacherInfo.Controls.Add(new LiteralControl(divInfo));
            }
            catch { }
        }

        protected void chkIsTeacher_CheckedChanged(object sender, EventArgs e)
        {
             loadTeacherInfo("");
        }


    }
}