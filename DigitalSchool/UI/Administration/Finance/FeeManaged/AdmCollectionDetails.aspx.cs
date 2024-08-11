using DS.BLL.Admission;
using DS.BLL.ControlPanel;
using DS.BLL.Finance;
using DS.BLL.GeneralSettings;
using DS.BLL.ManagedClass;
using DS.DAL.AdviitDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.Administration.Finance.FeeManaged
{
    public partial class AdmCollectionDetails : System.Web.UI.Page
    {
        AdmFeesCategoresEntry AdmFeescat;
        FeesCollectionEntry FeeCollection;
        AdmStdInfoEntry admstdEntry;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                    if (!IsPostBack)
                    {
                        if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "AdmCollectionDetails.aspx", "")) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                        //..........Collection Details................
                        ShiftEntry.GetDropDownList(ddlCShift);
                        ddlCShift.Items.RemoveAt(0);
                        ddlCShift.Items.Insert(0, new ListItem("...All...", "All"));                        
                        ClassEntry.GetEntitiesData(ddlCClass);
                        ddlCClass.Items.RemoveAt(0);
                        ddlCClass.Items.Insert(0,new ListItem("...All...","All"));  
                    
                        //..............Unpaid List........................
                        ShiftEntry.GetDropDownList(dlShiftDueList);
                        dlShiftDueList.Items.RemoveAt(0);
                        dlShiftDueList.Items.Insert(0, new ListItem("...All...", "All"));
                        ClassEntry.GetEntitiesData(dlClassDueList);
                        dlClassDueList.Items.RemoveAt(0);
                        dlClassDueList.Items.Insert(0, new ListItem("...All...", "All"));     
                        string index = Request.QueryString["back"];
                        if (index == "csr")
                        {
                            TabContainer.ActiveTabIndex = 1;
                        }
                        if (index == "ftd")
                        {
                            TabContainer.ActiveTabIndex = 2;
                        }                        
                    }
                }
            catch { }

        }
        protected void ddlCClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            AdmFeesCategoresEntry.GetDropDownList(ddlCFeeCat, ddlCClass.SelectedValue);
            ddlCFeeCat.Items.RemoveAt(0);
            ddlCFeeCat.Items.Insert(0, new ListItem("...All...", "All"));
        }
        protected void btnCSearch_Click(object sender, EventArgs e)
        {
            if (AdmFeescat == null)
            {
                AdmFeescat = new AdmFeesCategoresEntry();
            }
            string condition = "";
            string date = "";
            condition = AdmFeescat.SearchCondition(ddlCShift.SelectedValue,ddlCClass.SelectedValue,ddlCFeeCat.SelectedValue);
            if (chkCTodayCollect.Checked)
            {
                if (condition != "")
                {
                    condition += " AND Convert(datetime,DateOfPayment,105)=Convert(datetime,'" + DateTime.Now.ToString("dd-MM-yyyy") + "',105)";
                }
                else
                {
                    condition = " WHERE Convert(datetime,DateOfPayment,105)=Convert(datetime,'" + DateTime.Now.ToString("dd-MM-yyyy")
                        + "',105) AND PayStatus='True' AND BatchID='0' ";
                }
                date = "Date:"+DateTime.Now.ToString("dd/MM/yyyy")+"";
            }
            else
            {
                if (condition != "")
                {
                    condition += " AND Convert(datetime,DateOfPayment,105) between Convert(datetime,'" + txtCFrom.Text 
                        + "',105) AND Convert(datetime,'" + txtCTo.Text + "',105)";
                }
                else
                {
                    condition = " WHERE Convert(datetime,DateOfPayment,105) between Convert(datetime,'" + txtCFrom.Text
                        + "',105) AND Convert(datetime,'" + txtCTo.Text + "',105) AND PayStatus='True' AND BatchID='0'";
                }
                date = "From:"+txtCFrom.Text+"  To:"+txtCTo.Text+"";
                date = date.Replace("-","/");
            }

            if (FeeCollection == null)
            {
                FeeCollection = new FeesCollectionEntry();
            }
            DataTable dt = new DataTable();
            dt = FeeCollection.LoadAdmCollection(condition);
            if (dt.Rows.Count > 0)
            {
                Session["__AdmCollectionDetails__"] = dt;
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports"
                    + "/CrystalReport/ReportViewer.aspx?for=AdmCollectionDetails-"+date+"');", true);  //Open New Tab for Sever side code
            }
            else
            {
                lblMessage.InnerText = "warning->Admission Collection Not Found";
            }
        }     

        protected void btnPrintPreviewDueList_Click(object sender, EventArgs e)
        {
            string condition = "";
           
            if (dlClassDueList.SelectedValue == "All" && dlShiftDueList.SelectedValue != "All")
            {
                condition = " AND ConfigId='" + dlShiftDueList.SelectedValue + "'";
            }
            else if (dlClassDueList.SelectedValue != "All" && dlShiftDueList.SelectedValue == "All")
            {
                condition = " AND ClassID='" + dlClassDueList.SelectedValue + "'";
            }
            else if (dlClassDueList.SelectedValue != "All" && dlShiftDueList.SelectedValue != "All")
            {
                condition = " AND ConfigId='" + dlShiftDueList.SelectedValue + "' AND ClassID='" + dlClassDueList.SelectedValue + "'";
            }
            if (admstdEntry == null)
            {
                admstdEntry = new AdmStdInfoEntry();
            }
            dt = new DataTable();
            dt = admstdEntry.GetUnpaidStdList(condition);
            if (dt.Rows.Count > 0)
            {
                Session["__AdmUnpaidList__"] = dt;
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(),
                    "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=AdmUnpaidList');", true);  //Open New Tab for Sever side code
            }
            else
            {
                lblMessage.InnerText = "warning->No Unpaid Student";
            }
        }      
       
    }
}