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
    public partial class SubjectwiseTeacherList : System.Web.UI.Page
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
                    ddlDepartmentList.Items.Add("Techer List");
                    ddlDepartmentList.Items.Add("Staff List");
                    Classes.commonTask.LoadDepartmentList(ddlDepartmentList);
                    loadTeacherList("");
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
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
                            divInfo += "<td class='numeric_control' >" + "<img src='http://www.placehold.it/38x38/EFEFEF/AAAAAA&text=no+image' style='width:38px; height:38px' />";                                                                      
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
                       string sqlCmds = "Select EName,EMobile,ELastDegree,DName,EPictureName from v_EmployeeInfo where IsTeacher='True' and DName='"+dt.Rows[i]["DName"].ToString()+"'";
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
                            divInfo += "<td class='numeric_control' >" + "<img src='/Images/teacherProfileImage/" + ds.Tables[i].Rows[x]["EPictureName"].ToString() + "' style='width:38px; height:38px' />";
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
                            divInfo += "<td class='numeric_control' >" + "<img src='/Images/teacherProfileImage/" + ds.Tables[i].Rows[x]["EPictureName"].ToString() + "' style='width:38px; height:38px' />";
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
                        divInfo += "<td class='numeric_control' >" + "<img src='/Images/teacherProfileImage/" + dt.Rows[x]["EPictureName"].ToString() + "' style='width:38px; height:38px' />";
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
                //Response.Redirect("/Report/SubjectwiseTeacherReport.aspx");
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/Report/SubjectwiseTeacherReport.aspx');", true);  //Open New Tab for Sever side code
            }
            catch { }
        }      

        protected void rdoWithImage_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                rdoWithImage.Checked = true;
                rdoNoImage.Checked = false;
            }
            catch { }
        }

        protected void rdoNoImage_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                rdoWithImage.Checked = false;
                rdoNoImage.Checked = true;
            }
            catch { }
        }
    }
}