using DS.BLL.Admission;
using DS.BLL.Finance;
using DS.BLL.GeneralSettings;
using DS.BLL.ManagedBatch;
using DS.BLL.ManagedClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DS.BLL.Finance;
using System.Data;

namespace DS.UI.Reports.Finance
{
    public partial class FineReports : System.Web.UI.Page
    {
        ClassGroupEntry clsgrpEntry;
        CurrentStdEntry currentstdEntry;
        FeesCollectionEntry fc;
        StudentFine stdFine;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
                if (!IsPostBack)
                {
                    //........Discount List.............
                    BatchEntry.GetDropdownlist(ddlBatch, "True");
                    ddlBatch.Items.Insert(1, new ListItem("All", "All"));
                    ddlBatch.SelectedValue = "All";
                    ddlSection.Items.Insert(0, new ListItem("All", "All"));
                    ddlSection.SelectedValue = "All";
                    ddlgroup.Items.Insert(0, new ListItem("All", "All"));
                    ddlgroup.SelectedValue = "All";
                    ShiftEntry.GetDropDownList(ddlShift);
                    ddlShift.Items.Insert(1, new ListItem("All", "All"));
                    ddlShift.SelectedValue = "All";
                    ddlRoll.Items.Insert(0, new ListItem("All", "All"));
                    ddlRoll.SelectedValue = "All";                   
                }
        }
        protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] BatchClsID = ddlBatch.SelectedValue.Split('_');
            if (clsgrpEntry == null)
            {
                clsgrpEntry = new ClassGroupEntry();
            }
            clsgrpEntry.GetDropDownListClsGrpId(int.Parse(BatchClsID[1]), ddlgroup);
            ClassSectionEntry.GetEntitiesData(ddlSection, int.Parse(BatchClsID[1]), ddlgroup.SelectedValue);
            ddlgroup.Items.Insert(1, new ListItem("All", "All"));
            if (ddlgroup.Enabled == true)
            {
                ddlgroup.SelectedValue = "All";
                divGroup.Visible = true;
            }
            else
            {
                divGroup.Visible = false;
            }
            ddlSection.Items.Insert(1, new ListItem("All", "All"));
        }

        protected void ddlgroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] BatchClsID = ddlBatch.SelectedValue.Split('_');
            string GroupId = "0";
            if (ddlgroup.SelectedValue != "All")
            {
                GroupId = ddlgroup.SelectedValue;
            }
            ClassSectionEntry.GetEntitiesData(ddlSection, int.Parse(BatchClsID[1]), GroupId);
            ddlSection.Items.Insert(1, new ListItem("All", "All"));
        }

        protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlSection.SelectedValue == "All")
                {
                    ddlRoll.Items.Clear();
                    ddlRoll.Items.Insert(0, new ListItem("All", "All"));
                    return;
                }
                string[] BatchClsID = ddlBatch.SelectedValue.Split('_');
                if (currentstdEntry == null)
                {
                    currentstdEntry = new CurrentStdEntry();
                }
                currentstdEntry.GetRollNo(ddlRoll, ddlShift.SelectedValue,
                    BatchClsID[0], ddlgroup.SelectedValue, ddlSection.SelectedValue);
            }
            catch
            { }
        }

        protected void btnPreview_Click(object sender, EventArgs e)
        {
            LoadFineList();
        }
        private void LoadFineList()
        {
            try
            {
                string condition = "", Categorycondition = "", AbsentCondition="";

                dt = new DataTable();
                if (fc == null)
                {
                    fc = new FeesCollectionEntry();
                }
                condition = fc.GetSearchCondition(ddlShift.SelectedValue, ddlBatch.SelectedValue, ddlgroup.SelectedValue, ddlSection.SelectedValue);
                if (ddlRoll.SelectedValue != "All")
                {
                    if (condition != "")
                    {
                        condition += " AND StudentID='" + ddlRoll.SelectedValue + "'";
                    }
                    else
                    {
                        condition = " WHERE StudentID='" + ddlRoll.SelectedValue + "'";
                    }
                }
                if (condition == "")
                {
                    Categorycondition = " WHERE (FineamountPaid is null or FineamountPaid=0) ";
                    AbsentCondition = " WHERE IsPaid='False' ";
                }
                else
                {
                    Categorycondition = condition + " AND (FineamountPaid is null or FineamountPaid=0)";
                    AbsentCondition = condition + " AND IsPaid='False'";
                }
                if (stdFine == null)
                {
                    stdFine = new StudentFine();
                }
                dt = new DataTable();

                dt = stdFine.GetStudentFineList(Categorycondition, AbsentCondition);        //....Load StudentFineList
                if (dt.Rows.Count > 0)
                {
                    Session["__FineListReport__"] = dt;
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=FineListReport');", true);  //Open New Tab for Sever side code
                }
                else lblMessage.InnerText = "warning->No Fine List ";
            }
            catch { }
        }

        protected void btnFineCollectionSummary_Click(object sender, EventArgs e)
        {
            LoadFineCollectionSummary();
        }
        private void LoadFineCollectionSummary()
        {
            try
            {
                string condition = "", Categorycondition = "", AbsentCondition = "";

                dt = new DataTable();
                if (fc == null)
                {
                    fc = new FeesCollectionEntry();
                }
                condition = fc.GetSearchCondition(ddlShift.SelectedValue, ddlBatch.SelectedValue, ddlgroup.SelectedValue, ddlSection.SelectedValue);
                if (ddlRoll.SelectedValue != "All")
                {
                    if (condition != "")
                    {
                        condition += " AND StudentID='" + ddlRoll.SelectedValue + "'";
                    }
                    else
                    {
                        condition = " WHERE StudentID='" + ddlRoll.SelectedValue + "'";
                    }
                }
                if (condition == "")
                {
                    Categorycondition = " WHERE (FineamountPaid!=0 and FineamountPaid is not null) ";
                    AbsentCondition = " WHERE IsPaid='True' ";
                }
                else
                {
                    Categorycondition = condition + " AND (FineamountPaid!=0 and FineamountPaid is not null)";
                    AbsentCondition = condition + " AND IsPaid='True'";
                }
                if (stdFine == null)
                {
                    stdFine = new StudentFine();
                }
                dt = new DataTable();

                dt = stdFine.GetStudentFineList(Categorycondition, AbsentCondition);        //....Load StudentFineList
                if (dt.Rows.Count > 0)
                {
                    Session["__FineCollectionSummary__"] = dt;
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=FineCollectionSummary');", true);  //Open New Tab for Sever side code
                }
                else lblMessage.InnerText = "warning->No Fine List ";
            }
            catch { }
        }
    }
}