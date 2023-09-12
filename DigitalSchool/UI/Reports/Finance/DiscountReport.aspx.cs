using DS.BLL.Admission;
using DS.BLL.Finance;
using DS.BLL.GeneralSettings;
using DS.BLL.ManagedBatch;
using DS.BLL.ManagedClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.Reports.Finance
{
    public partial class DiscountReport : System.Web.UI.Page
    {
        ClassGroupEntry clsgrpEntry;
        CurrentStdEntry currentstdEntry;
        FeesCollectionEntry fc;      
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
                    ddlRoll.Items.Insert(0,new ListItem("All","All"));
                    ddlRoll.SelectedValue = "All";
                    //..........Discount Summary...........
                    BatchEntry.GetDropdownlist(ddlBatch_S, "True");
                    ddlBatch_S.Items.Insert(1, new ListItem("All", "All"));
                    ddlBatch_S.SelectedValue = "All";
                    ddlSection_S.Items.Insert(0, new ListItem("All", "All"));
                    ddlSection_S.SelectedValue = "All";
                    ddlgroup_S.Items.Insert(0, new ListItem("All", "All"));
                    ddlgroup_S.SelectedValue = "All";
                    ShiftEntry.GetDropDownList(ddlShift_S);
                    ddlShift_S.Items.Insert(1, new ListItem("All", "All"));
                    ddlShift_S.SelectedValue = "All";
                    ddlCategory.Items.Insert(0,new ListItem("All","All"));
                    txtFromDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
                    txtToDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
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
            try
            {
                string condition = "";

                DataTable dt = new DataTable();
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
                DiscountEntry dcentry = new DiscountEntry();
                dt = new DataTable();
                dt = dcentry.LoadDiscountList(condition);
                if (dt.Rows.Count > 0)
                {
                    Session["__DiscountList__"] = dt;
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=DiscountList');", true);  //Open New Tab for Sever side code
                }
                else lblMessage.InnerText = "warning->No Discount Student";
            }
            catch { }
        }

        protected void ddlBatch_S_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] BatchClsID = ddlBatch_S.SelectedValue.Split('_');
            if (clsgrpEntry == null)
            {
                clsgrpEntry = new ClassGroupEntry();
            }
            clsgrpEntry.GetDropDownListClsGrpId(int.Parse(BatchClsID[1]), ddlgroup_S);
            ClassSectionEntry.GetEntitiesData(ddlSection_S, int.Parse(BatchClsID[1]), ddlgroup_S.SelectedValue);
            ddlgroup_S.Items.Insert(1, new ListItem("All", "All"));
            if (ddlgroup_S.Enabled == true)
            {
                ddlgroup_S.SelectedValue = "All";
                divgroup_S.Visible = true;
            }
            else
            {
                divgroup_S.Visible = false;
            }
            ddlSection_S.Items.Insert(1, new ListItem("All", "All"));
            if (fc == null)
            {
                fc = new FeesCollectionEntry();
            }
            fc.LoadFeesCategory(ddlCategory, BatchClsID[0]);
            ddlCategory.Items.Insert(1,new ListItem("All","All"));
        }
        protected void btnDiscountSummary_Click(object sender, EventArgs e)
        {
            LoadDiscountSummary();
        }
        private void LoadDiscountSummary()
        {
            try
            {
                string condition = "";

                DataTable dt = new DataTable();
                if (fc == null)
                {
                    fc = new FeesCollectionEntry();
                }
                condition = fc.GetSearchCondition(ddlShift_S.SelectedValue, ddlBatch_S.SelectedValue, ddlgroup_S.SelectedValue, ddlSection_S.SelectedValue);
                if (ddlCategory.SelectedValue != "All")
                {
                    if (condition != "")
                    {
                        condition += " AND FeeCatId='" + ddlCategory.SelectedValue + "'";
                    }
                    else
                    {
                        condition = " WHERE FeeCatId='" + ddlCategory.SelectedValue + "'";
                    }
                }
                if (condition != "")
                {
                    condition += " AND BatchID!='0' AND PayStatus='True' AND DiscountStatus='True' AND Convert(datetime,DateOfPayment,105) between Convert(datetime,'" + txtFromDate.Text + "',105) AND Convert(datetime,'" + txtToDate.Text + "',105)";
                }
                else
                {
                    condition = " WHERE BatchID!='0' AND PayStatus='True' AND DiscountStatus='True' AND Convert(datetime,DateOfPayment,105) between Convert(datetime,'" + txtFromDate.Text + "',105) AND Convert(datetime,'" + txtToDate.Text + "',105)";
                }
                DiscountEntry dcentry = new DiscountEntry();
                dt = new DataTable();
                dt = dcentry.LoadDiscountSummary(condition);
                if (dt.Rows.Count > 0)
                {
                    Session["__DiscountSummary__"] = dt;
                    string FromDate = txtFromDate.Text.Replace('-','/');
                    string todate = txtToDate.Text.Replace('-','/');
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=DiscountSummary-From Date:"+FromDate+" To Date:"+todate+"');", true);  //Open New Tab for Sever side code
                }
                else lblMessage.InnerText = "warning->No Discount Student";
            }
            catch { }
        }
    }
}