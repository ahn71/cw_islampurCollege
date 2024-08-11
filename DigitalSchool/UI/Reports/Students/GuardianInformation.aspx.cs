﻿using DS.BLL.Admission;
using DS.BLL.ControlPanel;
using DS.BLL.GeneralSettings;
using DS.BLL.ManagedBatch;
using DS.BLL.ManagedClass;
using DS.DAL.AdviitDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.Reports.Students
{
    public partial class GuardianInformation : System.Web.UI.Page
    {
        ClassGroupEntry clsgrpEntry;
        CurrentStdEntry currentstdEntry;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                    if (!IsPostBack)
                    {
                        if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "GuardianInformation.aspx", "")) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no"); 
                        BatchEntry.GetDropdownlist(dlBatch, "True");
                        dlBatch.Items.Insert(1, new ListItem("All", "All"));
                        dlBatch.SelectedValue = "All";
                        dlSection.Items.Insert(0, new ListItem("All", "All"));
                        dlSection.SelectedValue = "All";
                        ShiftEntry.GetDropDownList(dlShift);
                        dlShift.Items.Insert(1, new ListItem("All", "All"));
                        dlShift.SelectedValue = "All";
                    }
            }
            catch { }
        }
        protected void dlBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] BatchClsID = dlBatch.SelectedValue.Split('_');
            if (clsgrpEntry == null)
            {
                clsgrpEntry = new ClassGroupEntry();
            }
            clsgrpEntry.GetDropDownListClsGrpId(int.Parse(BatchClsID[1]), dlGroup);
            ClassSectionEntry.GetEntitiesData(dlSection, int.Parse(BatchClsID[1]), dlGroup.SelectedValue);
            dlGroup.Items.Insert(1, new ListItem("All", "All"));
            if (dlGroup.Enabled == true)
                dlGroup.SelectedValue = "All";
            dlSection.Items.Insert(1, new ListItem("All", "All"));
        }

        protected void dlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] BatchClsID = dlBatch.SelectedValue.Split('_');
            string GroupId = "0";
            if (dlGroup.SelectedValue != "All")
            {
                GroupId = dlGroup.SelectedValue;
            }
            ClassSectionEntry.GetEntitiesData(dlSection, int.Parse(BatchClsID[1]), GroupId);
            dlSection.Items.Insert(1, new ListItem("All", "All"));
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            loadStudentInfo("");
        }

        DataTable Tables;
        private void loadStudentInfo(string sqlCmd) // for load student Guardian information
        {
            try
            {
                    btnPrintPreview.Visible = true;
                    string condition = "";

                    DataTable dt = new DataTable();
                    if (currentstdEntry == null)
                    {
                        currentstdEntry = new CurrentStdEntry();
                    }
                    condition = currentstdEntry.GetSearchCondition(dlShift.SelectedValue, dlBatch.SelectedValue, dlGroup.SelectedValue, dlSection.SelectedValue);

                    dt = currentstdEntry.GetGuardianInfo(condition); 
                    int totalRows = dt.Rows.Count;
                    string divInfo = "";
                    if (totalRows == 0)
                    {
                        divInfo = "<div class='noData'>No Guardian available</div>";
                        divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                        divGuardianList.Controls.Add(new LiteralControl(divInfo));
                        btnPrintPreview.Visible = false;
                        return;
                    }
                    divInfo = " <table id='tblStudentInfo' class='display'  style='width:100%;margin:0px auto;' > ";
                    divInfo += "<thead>";
                    divInfo += "<tr>";
                    divInfo += "<th class='numeric' style='width:50px;'>SL</th>";
                    divInfo += "<th class='numeric' style='width:120px'>Roll</th>";
                    divInfo += "<th style='width:190px'>Student Full Name</th>";
                    //divInfo += "<th class='numeric' style='width:120px'>Shift</th>";
                    divInfo += "<th style='width:260px'>Guardian Name</th>";
                    divInfo += "<th>Guardian Relation</th>";
                    divInfo += "<th class='numeric'>Guardian Mobile</th>";
                    divInfo += "<th>Guardian Address</th>";
                    divInfo += "</tr>";
                    divInfo += "</thead>";
                    divInfo += "<tbody>";
                    for (int x = 0; x < dt.Rows.Count; x++)
                    {
                        int sl = x + 1;
                        divInfo += "<tr >";
                        divInfo += "<td class='numeric'>" + sl + "</td>";
                        divInfo += "<td class='numeric'>" + dt.Rows[x]["RollNo"].ToString() + "</td>";
                        divInfo += "<td >" + dt.Rows[x]["FullName"].ToString() + "</td>";
                        //divInfo += "<td class='numeric'>" + dt.Rows[x]["Shift"].ToString() + "</td>";
                        divInfo += "<td >" + dt.Rows[x]["GuardianName"].ToString() + "</td>";
                        divInfo += "<td >" + dt.Rows[x]["GuardianRelation"].ToString() + "</td>";
                        divInfo += "<td >" + dt.Rows[x]["GuardianMobileNo"].ToString() + "</td>";
                        divInfo += "<td >" + dt.Rows[x]["GuardianAddress"].ToString() + "</td>";
                    }
                    divInfo += "</tbody>";
                    divInfo += "<tfoot>";
                    divInfo += "</table>";
                    divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                    divGuardianList.Controls.Add(new LiteralControl(divInfo));
                
                btnPrintPreview.Visible = true;
            }
            catch { }
        }
        protected void btnPrintPreview_Click(object sender, EventArgs e)
        {
            try
            {
                string condition = "";

                DataTable dt = new DataTable();
                if (currentstdEntry == null)
                {
                    currentstdEntry = new CurrentStdEntry();
                }
                condition = currentstdEntry.GetSearchCondition(dlShift.SelectedValue, dlBatch.SelectedValue, dlGroup.SelectedValue, dlSection.SelectedValue);

                dt = currentstdEntry.GetGuardianInfo(condition); 
                Session["__GuardianInformation__"] = dt;
                //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/Report/GuardianInformationReport.aspx');", true);  //Open New Tab for Sever side code
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=GuardianInformation');", true);  //Open New Tab for Sever side code
            }
            catch { }
        }
    }
}