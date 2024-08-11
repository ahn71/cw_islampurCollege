using DS.BLL;
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
    public partial class GuardianPhoneList : System.Web.UI.Page
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
                    Classes.commonTask.loadClass(dlClass);
                    sqlDB.loadDropDownList("Select  SectionName from Sections  Order by SectionName", dlSection);
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            loadGuardianContactList("");
        }

        private void loadGuardianContactList(string sqlCmd) // for load student Guardian Contact List information
        {
            try
            {
                btnPrintPreview.Visible = true;
                Session["__Section__"] = dlSection.Text;
                if (dlSection.Text == "All")
                {
                    DataSet ds = new DataSet();
                    for (int i = 0; i < dlSection.Items.Count - 1; i++)
                    {
                        DataTable  Tables = new DataTable();
                        ds.Tables.Add(Tables);
                        if(dlShift.Text=="All") sqlCmd = "select ClassName,SectionName,FullName,RollNo,Shift,GuardianName,GuardianRelation,GuardianMobileNo "
                        +"from v_CurrentStudentInfo where ClassName='" + dlClass.Text + "' and SectionName='" + dlSection.Items[i].Text + "'  ";
                        if (dlShift.Text != "All") sqlCmd = "select ClassName,SectionName,FullName,RollNo,Shift,GuardianName,GuardianRelation,GuardianMobileNo from "
                        +"v_CurrentStudentInfo where ClassName='" + dlClass.Text + "' and SectionName='" + dlSection.Items[i].Text + "' and Shift='"+dlShift.Text+"' ";
                        sqlDB.fillDataTable(sqlCmd, ds.Tables[i]);
                    }

                    Session["__GuardianInfo__"] = ds;
                    Session["__Batch__"] = "Batch : " + dlClass.Text + TimeZoneBD.getCurrentTimeBD().Year.ToString() + " ( " + dlSection.Text + " )";

                    for (int i = 0; i < ds.Tables.Count; i++)
                    {

                        int totalRows = ds.Tables[i].Rows.Count;
                        string divInfo = "";

                        divInfo = " <table id='tblStudentInfo' class='display'  style='width:100%;margin:0px auto;' > ";
                        if (ds.Tables[i].Rows.Count > 0)
                        {
                            divInfo += "<div class='dataTables_wrapper'><div class='head' style='font-size:16px; margin-top:5px'>" + " Section : " 
                            + ds.Tables[i].Rows[0]["SectionName"].ToString() + "</div></div>";
                        }
                        divInfo += "<thead>";
                        divInfo += "<tr>";

                        divInfo += "<th class='numeric' style='width:50px;'>SL</th>";
                        divInfo += "<th style='width:600px'>Student Name</th>";
                        divInfo += "<th style='width:120px' class='numeric'>Roll</th>";
                        divInfo += "<th style='width:120px' class='numeric'>Shift</th>";
                        divInfo += "<th>Guardian Mobile</th>";

                        divInfo += "</tr>";

                        divInfo += "</thead>";

                        divInfo += "<tbody>";

                        for (int x = 0; x < ds.Tables[i].Rows.Count; x++)
                        {
                            int sl = x + 1;

                            divInfo += "<tr >";
                            divInfo += "<td class='numeric'>" + sl + "</td>";
                            divInfo += "<td >" + ds.Tables[i].Rows[x]["FullName"].ToString() + "</td>";
                            divInfo += "<td class='numeric'>" + ds.Tables[i].Rows[x]["RollNo"].ToString() + "</td>";
                            divInfo += "<td class='numeric'>" + ds.Tables[i].Rows[x]["Shift"].ToString() + "</td>";
                            divInfo += "<td >" + ds.Tables[i].Rows[x]["GuardianMobileNo"].ToString() + "</td>";

                        }

                        if (ds.Tables[i].Rows.Count > 0)
                        {
                            divInfo += "</tbody>";
                            divInfo += "<tfoot>";

                            divInfo += "</table><br>";
                            divGuardianContractList.Controls.Add(new LiteralControl(divInfo));
                        }
                    }
                }
                else
                {
                    if(dlShift.Text=="All") sqlCmd = "select ClassName,SectionName,FullName,RollNo,Shift,GuardianMobileNo from v_CurrentStudentInfo where ClassName='" 
                    + dlClass.Text + "'  and SectionName='" + dlSection.Text + "' ";
                    if (dlShift.Text != "All") sqlCmd = "select ClassName,SectionName,FullName,RollNo,Shift,GuardianMobileNo from v_CurrentStudentInfo where ClassName='" 
                    + dlClass.Text + "'  and SectionName='" + dlSection.Text + "' and Shift='"+dlShift.Text+"' ";
                    DataTable dt = new DataTable();
                    sqlDB.fillDataTable(sqlCmd, dt);

                    Session["__Batch__"] = "Batch : " + dlClass.Text + TimeZoneBD.getCurrentTimeBD().Year.ToString() + " ( " + dlSection.Text + " )";
                    Session["__GuardianInfoWithFixtSection__"] = dt;
                    int totalRows = dt.Rows.Count;
                    string divInfo = "";

                    if (totalRows == 0)
                    {
                        divInfo = "<div class='noData'>No Guardian available</div>";
                        divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                        divGuardianContractList.Controls.Add(new LiteralControl(divInfo));
                        btnPrintPreview.Visible = false;
                        return;
                    }


                    divInfo = " <table id='tblStudentInfo' class='display'  style='width:100%;margin:0px auto;' > ";
                    divInfo += "<thead>";
                    divInfo += "<tr>";

                    divInfo += "<th class='numeric' style='width:50px;'>SL</th>";
                    divInfo += "<th style='width:600px'>Student Name</th>";
                    divInfo += "<th class='numeric'>Roll</th>";
                    divInfo += "<th class='numeric'>Shift</th>";
                    divInfo += "<th>Guardian Mobile</th>";

                    divInfo += "</tr>";

                    divInfo += "</thead>";

                    divInfo += "<tbody>";

                    for (int x = 0; x < dt.Rows.Count; x++)
                    {
                        int sl = x + 1;

                        divInfo += "<tr >";
                        divInfo += "<td class='numeric'>" + sl + "</td>";
                        divInfo += "<td >" + dt.Rows[x]["FullName"].ToString() + "</td>";
                        divInfo += "<td class='numeric'>" + dt.Rows[x]["RollNo"].ToString() + "</td>";
                        divInfo += "<td class='numeric'>" + dt.Rows[x]["Shift"].ToString() + "</td>";
                        divInfo += "<td >" + dt.Rows[x]["GuardianMobileNo"].ToString() + "</td>";
                    }

                    divInfo += "</tbody>";
                    divInfo += "<tfoot>";

                    divInfo += "</table>";
                    divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";

                    divGuardianContractList.Controls.Add(new LiteralControl(divInfo));
                }

                btnPrintPreview.Visible = true;
            }
            catch { }
        }

        protected void btnPrintPreview_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/Report/GuardianContactListReport.aspx');", true);  //Open New Tab for Sever side code
        }

    }
}