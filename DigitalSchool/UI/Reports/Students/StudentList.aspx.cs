using DS.BLL;
using DS.BLL.Admission;
using DS.BLL.ControlPanel;
using DS.BLL.GeneralSettings;
using DS.BLL.ManagedBatch;
using DS.BLL.ManagedClass;
using DS.Classes;
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
    public partial class StudentList : System.Web.UI.Page
    {
        ClassGroupEntry clsgrpEntry;
        CurrentStdEntry currentstdEntry;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                {
                    if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "StudentList.aspx", "")) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");

                ShiftEntry.GetShiftListWithAll(dlShift);
                commonTask.loadYearFromBatch(ddlYear);
                ddlYear.SelectedValue = TimeZoneBD.getCurrentTimeBD().Year.ToString();

                ClassEntry.GetEntitiesDataWithAll(ddlClass);
                if (ddlClass != null && ddlClass.SelectedValue != "00")
                {
                    ViewState["__BatchId__"] = commonTask.get_batchid(ddlClass.SelectedValue, ddlYear.SelectedValue);
                    ClassGroupEntry.GetDropDownWithAll(dlGroup, int.Parse(ddlClass.SelectedValue.ToString()));
                    if (dlGroup != null && dlGroup.SelectedValue != "00")
                        ClassSectionEntry.GetSectionList(dlSection, int.Parse(ddlClass.SelectedValue), dlGroup.SelectedValue);
                }
            }
        }
       

        protected void dlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ClassGroupEntry.GetDropDownWithAll(dlGroup, int.Parse(ddlClass.SelectedValue.ToString()));
            if (dlGroup != null && dlGroup.SelectedValue != "00")
                ClassSectionEntry.GetSectionList(dlSection, int.Parse(ddlClass.SelectedValue), dlGroup.SelectedValue);
            else
            {
                if (dlSection != null)
                    dlSection.Items.Clear();
                dlSection.Items.Insert(0, new ListItem("All", "00"));
            }
        }
        private void loadStudentInfo(string sqlCmd)
        {
            try
            {

                

                DataTable dt = new DataTable();
                if (currentstdEntry == null)
                {
                    currentstdEntry = new CurrentStdEntry();
                }
                string conditions = " Where Year='" + ddlYear.SelectedValue + "'";
                if (dlShift.SelectedValue != "00")
                    conditions += " and ConfigId=" + dlShift.SelectedValue;
                if (ddlClass.SelectedValue != "00")
                {
                    conditions += " and ClassId=" + ddlClass.SelectedValue;

                    if (dlGroup.SelectedValue != "0" && dlGroup.SelectedValue != "00")
                        conditions += " and ClsGrpID=" + dlGroup.SelectedValue;
                    if (dlSection.SelectedValue != "00")
                        conditions += " and ClsSecID=" + dlSection.SelectedValue;

                }

                dt = currentstdEntry.GetCurrentStudentProfile(conditions);  
                
                int totalRows = dt.Rows.Count;
                string divInfo = "";
                if (totalRows == 0)
                {
                    divInfo = "<div class='noData'>No Student available</div>";
                    divInfo += "<div><div class='head'></div></div>";
                    divStudentList.Controls.Add(new LiteralControl(divInfo));
                    return;
                }


                divInfo = " <table id='tblStudentList' class='display'  style='width:100%;margin:0px auto;' > ";
                divInfo += "<thead>";
                divInfo += "<tr>";
                divInfo += "<th style='text-align:center'>Images</th>";
                divInfo += "<th style='text-align:center'>Roll No</th>";
                divInfo += "<th>Name</th>";
                divInfo += "<th style='text-align:center'>Gender</th>";
                divInfo += "<th>Mobile</th>";
                divInfo += "</tr>";
                divInfo += "</thead>";
                divInfo += "<tbody>";
                string id = "";

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    id = dt.Rows[x]["StudentId"].ToString();
                    divInfo += "<tr id='r_" + id + "'>";
                    divInfo += "<td  style='text-align:center;width:70px;'><img style='width:50px;height:50px;' src='/Images/profileImages/" + dt.Rows[x]["ImageName"].ToString() + "'/></td>";
                    divInfo += "<td style='text-align:center;width:70px'>" + dt.Rows[x]["RollNo"].ToString() + "</td>";
                    divInfo += "<td  >" + dt.Rows[x]["FullName"].ToString() + "</td>";
                    divInfo += "<td style='text-align:center;width:55px'>" + dt.Rows[x]["Gender"].ToString() + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["Mobile"].ToString() + "</td>";
                }

                divInfo += "</tbody>";
                divInfo += "<tfoot>";
                divInfo += "</table>";
                divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                divStudentList.Controls.Add(new LiteralControl(divInfo));
                Session["_StudentList_"] = dt;
            }
            catch { }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            loadStudentInfo("");
        }
        protected void btnPrintPreview_Click(object sender, EventArgs e)
        {
            

            DataTable dt = new DataTable();
            if (currentstdEntry == null)
            {
                currentstdEntry = new CurrentStdEntry();
            }
            string conditions = " Where Year='" + ddlYear.SelectedValue + "'";
            if (dlShift.SelectedValue != "00")
                conditions += " and ConfigId=" + dlShift.SelectedValue;
            if (ddlClass.SelectedValue != "00")
            {
                conditions += " and ClassId=" + ddlClass.SelectedValue;

                if (dlGroup.SelectedValue != "0" && dlGroup.SelectedValue != "00")
                    conditions += " and ClsGrpID=" + dlGroup.SelectedValue;
                if (dlSection.SelectedValue != "00")
                    conditions += " and ClsSecID=" + dlSection.SelectedValue;

            }
            dt = currentstdEntry.GetCurrentStudentProfile(conditions);  
            Session["_StudentList_"] = dt;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=StudentListAll');", true);  //Open New Tab for Sever side code
        }

        protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlClass.SelectedValue != "00")
            {
                ClassGroupEntry.GetDropDownWithAll(dlGroup, int.Parse(ddlClass.SelectedValue.ToString()));
                if (dlGroup != null && dlGroup.SelectedValue != "00")
                    ClassSectionEntry.GetSectionList(dlSection, int.Parse(ddlClass.SelectedValue), dlGroup.SelectedValue);
                else
                {
                    if (dlSection != null)
                        dlSection.Items.Clear();
                    dlSection.Items.Insert(0, new ListItem("All", "00"));
                }
            }
            else
            {
                if (dlGroup != null)
                    dlGroup.Items.Clear();
                dlGroup.Items.Insert(0, new ListItem("All", "00"));
                if (dlSection != null)
                    dlSection.Items.Clear();
                dlSection.Items.Insert(0, new ListItem("All", "00"));
            }
        }
    }
}