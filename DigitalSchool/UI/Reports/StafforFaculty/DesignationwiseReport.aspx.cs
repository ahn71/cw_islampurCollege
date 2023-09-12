using DS.BLL.ControlPanel;
using DS.BLL.HR;
using DS.DAL.AdviitDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.Reports.StafforFaculty
{
    public partial class DesignationwiseReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                if (!IsPostBack)
                {
                    if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "DesignationwiseReport.aspx", "")) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                    DesignationEntry.GetDropdownlist(ddlDesignation);
                    ddlDesignation.Items.RemoveAt(0);
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
            if (ddlDesignation.SelectedItem.Text == "All")
            {
                Session["__Department__"] = "All";
                DataTable dt = new DataTable();
                sqlDB.fillDataTable("Select Distinct DesId, DesName From v_EmployeeInfo ", dt);

                DataSet ds = new DataSet();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataTable Tables = new DataTable();
                    ds.Tables.Add(Tables);
                    string sqlCmds = "Select EName,EMobile,ELastDegree,DName,DesName,EPictureName from v_EmployeeInfo where DesId='" + dt.Rows[i]["DesId"] + "'  ";
                    sqlDB.fillDataTable(sqlCmds, ds.Tables[i]);
                }

                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    int totalRows = ds.Tables[i].Rows.Count;
                    string divInfo = "";
                    divInfo = " <table id='tblStudentInfo' class='display'  style='width:100%;margin:0px auto;' > ";
                    if (ds.Tables[i].Rows.Count > 0)
                    {
                        divInfo += "<div class='dataTables_wrapper'><div class='head' style='font-size:16px; margin-top:5px'>" + " Designation : " + ds.Tables[i].Rows[0]["DesName"].ToString() + "</div></div>";
                    }
                    divInfo += "<thead>";
                    divInfo += "<tr>";
                    divInfo += "<th style='text-align:center'>SL</th>";
                    divInfo += "<th style='width:400px'>Name</th>";
                    divInfo += "<th style='width:400px'>Department</th>";
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
                        divInfo += "<td >" + ds.Tables[i].Rows[x]["DName"].ToString() + "</td>";
                        divInfo += "<td >" + ds.Tables[i].Rows[x]["EMobile"].ToString() + "</td>";
                        divInfo += "<td >" + ds.Tables[i].Rows[x]["ELastDegree"].ToString() + "</td>";
                        if (rdoWithImage.Checked)
                        {
                            if (ds.Tables[i].Rows[x]["EPictureName"].ToString() == "")
                            {
                                divInfo += "<td class='numeric_control' >" + "<img src='http://www.placehold.it/38x38/EFEFEF/AAAAAA&text=no+image' style='width:38px; height:38px' />";
                            }
                            else
                            {
                                divInfo += "<td class='numeric_control' >" + "<img src='/Images/teacherProfileImage/" + ds.Tables[i].Rows[x]["EPictureName"].ToString() + "' style='width:38px; height:38px' />";
                            }
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
                if (string.IsNullOrEmpty(sqlcmd)) sqlcmd = "Select EName,DName,EMobile,ELastDegree,EPictureName from v_EmployeeInfo where DesId=" + ddlDesignation.SelectedValue + " ";
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
                divInfo += "<th style='width:400px'>Department</th>";
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
                    divInfo += "<td >" + dt.Rows[x]["DName"].ToString() + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["EMobile"].ToString() + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["ELastDegree"].ToString() + "</td>";
                    if (rdoWithImage.Checked)
                    {
                        if (dt.Rows[x]["EPictureName"].ToString() == "")
                        {
                            divInfo += "<td class='numeric_control' >" + "<img src='http://www.placehold.it/38x38/EFEFEF/AAAAAA&text=no+image' style='width:38px; height:38px' />";
                        }
                        else
                        {
                            divInfo += "<td class='numeric_control' >" + "<img src='/Images/teacherProfileImage/" + dt.Rows[x]["EPictureName"].ToString() + "' style='width:38px; height:38px' />";
                        }                        
                    }
                }

                divInfo += "</tbody>";
                divInfo += "<tfoot>";

                divInfo += "</table>";                         

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
                if (ddlDesignation.SelectedValue == "All")
                {
                    sqlcmd = "Select ECardNo,EName,EGender,EReligion,Format(EJoiningDate,'dd-MM-yyyy') as EJoiningDate,EMobile,ELastDegree,DName,DesName,EPictureName from v_EmployeeInfo";
                }               
                else
                {
                    sqlcmd = "Select ECardNo,EName,EGender,EReligion,Format(EJoiningDate,'dd-MM-yyyy') as EJoiningDate,EMobile,ELastDegree,DName,DesName,EPictureName from v_EmployeeInfo where DesId=" + ddlDesignation.SelectedValue + "";
                }
                DataTable dt;
                sqlDB.fillDataTable(sqlcmd, dt = new DataTable());
                Session["__DesignationwiseEmpList__"] = dt;
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=DesignationwiseEmpList');", true);  //Open New Tab for Sever side code

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