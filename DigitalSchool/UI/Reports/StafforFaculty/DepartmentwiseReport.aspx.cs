using DS.BLL.ControlPanel;
using DS.DAL.AdviitDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.Reports.StafforFaculty
{
    public partial class SubjectwiseTeacherList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                if (!IsPostBack)
                {
                    if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "DepartmentwiseReport.aspx", "")) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                    //ddlDepartmentList.Items.Add("Techer List");
                    //ddlDepartmentList.Items.Add("Staff List");
                    Classes.commonTask.LoadDepartmentList(ddlDepartmentList);
                    loadTeacherList("");
                }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load();", true);
            loadTeacherList("");
        }

        private void loadTeacherList(string sqlcmd)
        {
            if (rdoWithImage.Checked)
            {
                Session["__Image__"] = "withimage";
            }
            else
            {
                Session["__Image__"] = "withoutimage";
            }
            if (ddlDepartmentList.Text == "All")
            {
                Session["__Department__"] = "All";
                DataTable dt = new DataTable();
                sqlDB.fillDataTable("Select Distinct DName From v_EmployeeInfo ", dt);

                DataSet ds = new DataSet();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataTable Tables = new DataTable();
                    ds.Tables.Add(Tables);
                    string sqlCmds = "Select EName,EMobile,ELastDegree,DName,EPictureName from v_EmployeeInfo where DName='" + dt.Rows[i]["DName"] + "'  ";
                    sqlDB.fillDataTable(sqlCmds, ds.Tables[i]);
                }

                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    int totalRows = ds.Tables[i].Rows.Count;
                    string divInfo = "";
                    divInfo = " <table id='tblStudentInfo' class='display'  style='width:100%;margin:0px auto;' > ";
                    if (ds.Tables[i].Rows.Count > 0)
                    {
                        divInfo += "<div class='dataTables_wrapper'><div class='head' style='font-size:16px; margin-top:5px'>" + " Department : " + ds.Tables[i].Rows[0]["DName"].ToString() + "</div></div>";
                    }
                    divInfo += "<thead>";
                    divInfo += "<tr>";
                    divInfo += "<th style='text-align:center'>SL</th>";
                    divInfo += "<th style='width:400px'>Name</th>";
                    divInfo += "<th>Contact No</th>";
                    divInfo += "<th>Last Degree</th>";
                    if (rdoWithImage.Checked)
                    {
                        divInfo += "<th>Photo</th>";
                    }
                    divInfo += "</tr>";

                    divInfo += "</thead>";

                    divInfo += "<tbody>";
                    string id = "";

                    for (int x = 0; x < ds.Tables[i].Rows.Count; x++)
                    {
                        int Sl = x + 1;
                        divInfo += "<tr id='r_" + id + "'>";
                        divInfo += "<td style='text-align:center'>" + Sl + "</td>";
                        divInfo += "<td >" + ds.Tables[i].Rows[x]["EName"].ToString() + "</td>";
                        divInfo += "<td >" + ds.Tables[i].Rows[x]["EMobile"].ToString() + "</td>";
                        divInfo += "<td >" + ds.Tables[i].Rows[x]["ELastDegree"].ToString() + "</td>";
                        if (rdoWithImage.Checked)
                        {
                            string url = @"/Images/teacherProfileImage/" + Path.GetFileName(ds.Tables[i].Rows[x]["EPictureName"].ToString());
                            divInfo += "<td class='numeric_control' >" + "<img src='"+ url + "' style='width:38px; height:38px' />";
                        }
                    }

                    if (ds.Tables[i].Rows.Count > 0)
                    {
                        divInfo += "</tbody>";
                        divInfo += "<tfoot>";

                        divInfo += "</table><br>";
                        divTeacherList.Controls.Add(new LiteralControl(divInfo));
                    }

                    Session["__TeacherInfo__"] = ds;
                }
            }
            else if (ddlDepartmentList.Text == "Techer List")
            {
                Session["__Department__"] = "Techer List";
                DataTable dt = new DataTable();
                sqlDB.fillDataTable("Select Distinct DName from v_EmployeeInfo where IsTeacher='True'", dt);
                DataSet ds = new DataSet();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataTable Tables = new DataTable();
                    ds.Tables.Add(Tables);
                    string sqlCmds = "Select EName,EMobile,ELastDegree,DName,EPictureName from v_EmployeeInfo where IsTeacher='True' and DName='" + dt.Rows[i]["DName"].ToString() + "'";
                    sqlDB.fillDataTable(sqlCmds, ds.Tables[i]);
                }


                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    int totalRows = ds.Tables[i].Rows.Count;
                    string divInfo = "";
                    divInfo = " <table id='tblStudentInfo' class='display'  style='width:100%;margin:0px auto;' > ";
                    if (ds.Tables[i].Rows.Count > 0)
                    {
                        divInfo += "<div class='dataTables_wrapper'><div class='head' style='font-size:16px; margin-top:5px'>" + " Department : " + ds.Tables[i].Rows[0]["DName"].ToString() + "</div></div>";
                    }
                    divInfo += "<thead>";
                    divInfo += "<tr>";
                    divInfo += "<th style='text-align:center'>SL</th>";
                    divInfo += "<th style='width:400px'>Name</th>";
                    divInfo += "<th>Contact No</th>";
                    divInfo += "<th>Last Degree</th>";
                    if (rdoWithImage.Checked)
                    {
                        divInfo += "<th>Photo</th>";
                    }
                    divInfo += "</tr>";

                    divInfo += "</thead>";

                    divInfo += "<tbody>";
                    string id = "";

                    for (int x = 0; x < ds.Tables[i].Rows.Count; x++)
                    {
                        int Sl = x + 1;
                        divInfo += "<tr id='r_" + id + "'>";
                        divInfo += "<td style='text-align:center'>" + Sl + "</td>";
                        divInfo += "<td >" + ds.Tables[i].Rows[x]["EName"].ToString() + "</td>";
                        divInfo += "<td >" + ds.Tables[i].Rows[x]["EMobile"].ToString() + "</td>";
                        divInfo += "<td >" + ds.Tables[i].Rows[x]["ELastDegree"].ToString() + "</td>";
                        if (rdoWithImage.Checked)
                        {

                            string url = @"/Images/teacherProfileImage/" + Path.GetFileName(ds.Tables[i].Rows[x]["EPictureName"].ToString());
                            divInfo += "<td class='numeric_control' >" + "<img src='" + url + "' style='width:38px; height:38px' />";
                        }

                    }

                    if (ds.Tables[i].Rows.Count > 0)
                    {
                        divInfo += "</tbody>";
                        divInfo += "<tfoot>";

                        divInfo += "</table><br>";
                        divTeacherList.Controls.Add(new LiteralControl(divInfo));
                    }

                    Session["__TeacherInfo__"] = ds;
                }
            }
            else if (ddlDepartmentList.Text == "Staff List")
            {
                Session["__Department__"] = "Staff List";
                DataTable dt = new DataTable();
                sqlDB.fillDataTable("Select Distinct DName from v_EmployeeInfo where IsTeacher='False'", dt);
                DataSet ds = new DataSet();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataTable Tables = new DataTable();
                    ds.Tables.Add(Tables);
                    string sqlCmds = "Select EName,EMobile,ELastDegree,DName,EPictureName from v_EmployeeInfo where IsTeacher='False' and DName='" + dt.Rows[i]["DName"].ToString() + "'";
                    sqlDB.fillDataTable(sqlCmds, ds.Tables[i]);
                }


                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    int totalRows = ds.Tables[i].Rows.Count;
                    string divInfo = "";
                    divInfo = " <table id='tblStudentInfo' class='display'  style='width:100%;margin:0px auto;' > ";
                    if (ds.Tables[i].Rows.Count > 0)
                    {
                        divInfo += "<div class='dataTables_wrapper'><div class='head' style='font-size:16px; margin-top:5px'>" + " Department : " + ds.Tables[i].Rows[0]["DName"].ToString() + "</div></div>";
                    }
                    divInfo += "<thead>";
                    divInfo += "<tr>";
                    divInfo += "<th style='text-align:center'>SL</th>";
                    divInfo += "<th style='width:400px'>Name</th>";
                    divInfo += "<th>Contact No</th>";
                    divInfo += "<th>Last Degree</th>";
                    if (rdoWithImage.Checked)
                    {
                        divInfo += "<th>Photo</th>";
                    }
                    divInfo += "</tr>";

                    divInfo += "</thead>";

                    divInfo += "<tbody>";
                    string id = "";
                    for (int x = 0; x < ds.Tables[i].Rows.Count; x++)
                    {
                        int Sl = x + 1;
                        divInfo += "<tr id='r_" + id + "'>";
                        divInfo += "<td style='text-align:center'>" + Sl + "</td>";
                        divInfo += "<td >" + ds.Tables[i].Rows[x]["EName"].ToString() + "</td>";
                        divInfo += "<td >" + ds.Tables[i].Rows[x]["EMobile"].ToString() + "</td>";
                        divInfo += "<td >" + ds.Tables[i].Rows[x]["ELastDegree"].ToString() + "</td>";
                        if (rdoWithImage.Checked)
                        {
                            string url = @"/Images/teacherProfileImage/" + Path.GetFileName(ds.Tables[i].Rows[x]["EPictureName"].ToString());
                            divInfo += "<td class='numeric_control' >" + "<img src='" + url + "' style='width:38px; height:38px' />";
                        }

                    }

                    if (ds.Tables[i].Rows.Count > 0)
                    {
                        divInfo += "</tbody>";
                        divInfo += "<tfoot>";

                        divInfo += "</table><br>";
                        divTeacherList.Controls.Add(new LiteralControl(divInfo));
                    }

                    Session["__TeacherInfo__"] = ds;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(sqlcmd)) sqlcmd = "Select EName,EMobile,ELastDegree,EPictureName from v_EmployeeInfo where DId=" + ddlDepartmentList.SelectedValue + " ";
                DataTable dt = new DataTable();
                sqlDB.fillDataTable(sqlcmd, dt);

                int totalRows = dt.Rows.Count;
                string divInfo = "";

                if (totalRows == 0)
                {
                    divInfo = "<div class='noData'>No Teacher available</div>";
                    divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                    divTeacherList.Controls.Add(new LiteralControl(divInfo));
                    return;
                }
                divInfo = " <table id='tblClassList' class='display'  > ";
                divInfo += "<thead>";
                divInfo += "<tr>";
                divInfo += "<th style='text-align:center'>SL</th>";
                divInfo += "<th style='width:400px'>Name</th>";
                divInfo += "<th>Contact No</th>";
                divInfo += "<th>Last Degree</th>";
                if (rdoWithImage.Checked)
                {
                    divInfo += "<th>Photo</th>";
                }
                divInfo += "</tr>";

                divInfo += "</thead>";

                divInfo += "<tbody>";
                string id = "";

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    int Sl = x + 1;
                    divInfo += "<tr id='r_" + id + "'>";
                    divInfo += "<td style='text-align:center'>" + Sl + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["EName"].ToString() + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["EMobile"].ToString() + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["ELastDegree"].ToString() + "</td>";
                    if (rdoWithImage.Checked)
                    {
                        string url = @"/Images/teacherProfileImage/" + Path.GetFileName(dt.Rows[x]["EPictureName"].ToString());
                        divInfo += "<td class='numeric_control' >" + "<img src='" + url + "' style='width:38px; height:38px' />";
                    }
                }

                divInfo += "</tbody>";
                divInfo += "<tfoot>";

                divInfo += "</table>";
                Session["__TeacherInfo__"] = dt;
                Session["__Department__"] = ddlDepartmentList.SelectedItem.Text;
                DataTable dtisT = new DataTable();
                sqlDB.fillDataTable("Select IsTeacher from Departments_HR where DId=" + ddlDepartmentList.SelectedValue + " ", dtisT);
                if (dtisT.Rows[0]["IsTeacher"].ToString() == "1")
                {
                    Session["__CheckDepartment__"] = "Teacher";
                }
                else
                {
                    Session["__CheckDepartment__"] = "Staff";
                }

                divTeacherList.Controls.Add(new LiteralControl(divInfo));
            }
        }
        protected void btnPrintPreview_Click(object sender, EventArgs e)
        {
            try
            {
                if (rdoWithImage.Checked)
                {
                    Session["__Image__"] = "withimage";
                }
                else
                {
                    Session["__Image__"] = "withoutimage";
                }
                string sqlcmd = "";
                if (ddlDepartmentList.Text == "All")
                {
                    sqlcmd = "Select EName,EMobile,ELastDegree,DName,Format(EJoiningDate,'dd-MM-yyyy') as EJoiningDate,DesName,EPictureName from v_EmployeeInfo";
                }
                else if (ddlDepartmentList.Text == "Techer List")
                {
                    sqlcmd = "Select EName,EMobile,ELastDegree,DName,Format(EJoiningDate,'dd-MM-yyyy') as EJoiningDate,DesName,EPictureName from v_EmployeeInfo where IsTeacher='True'";
                }
                else if (ddlDepartmentList.Text == "Staff List")
                {
                    sqlcmd = "Select EName,EMobile,ELastDegree,DName,Format(EJoiningDate,'dd-MM-yyyy') as EJoiningDate,DesName,EPictureName from v_EmployeeInfo where IsTeacher='false'";
                }
                else
                {
                    sqlcmd = "Select EName,EMobile,ELastDegree,DName,Format(EJoiningDate,'dd-MM-yyyy') as EJoiningDate,DesName,EPictureName from v_EmployeeInfo where DId=" + ddlDepartmentList.SelectedValue + "";
                }
                DataTable dt;
                sqlDB.fillDataTable(sqlcmd,dt=new DataTable());
                Session["__DptwiseEmpList__"] = dt;
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=DptwiseEmpList');", true);  //Open New Tab for Sever side code

            }
            catch { }
        }

        protected void rdoWithImage_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load();", true);
                rdoWithImage.Checked = true;
                rdoNoImage.Checked = false;
            }
            catch { }
        }

        protected void rdoNoImage_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load();", true);
                rdoWithImage.Checked = false;
                rdoNoImage.Checked = true;
            }
            catch { }
        }
    }
}