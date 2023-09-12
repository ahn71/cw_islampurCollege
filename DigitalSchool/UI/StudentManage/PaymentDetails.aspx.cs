using DS.BLL.Admission;
using DS.BLL.Finance;
using DS.BLL.ManagedBatch;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.StudentManage
{
    public partial class PaymentDetails : System.Web.UI.Page
    {
        FeesCollectionEntry fc;
        protected void Page_Load(object sender, EventArgs e)
        {
                if (!IsPostBack)
                {
                    lblName.Text = Session["__FullName__"].ToString() + "'s DashBoard";                    
                    BatchEntry.GetStdBatch(ddlBatch, Session["__StudentId__"].ToString());
                    if(ddlBatch.SelectedValue=="All"||ddlBatch.Items.Count==1)
                    {
                        FeesCollectionEntry fc = new FeesCollectionEntry();
                        fc.LoadStudentFessCategory(ddlFeesCat, Session["__StudentId__"].ToString());
                        ddlFeesCat.Items.RemoveAt(0);
                        ddlFeesCat.Items.Insert(0,new ListItem("...All...","All"));
                    }
                    else
                    {
                        ddlFeesCat.Items.Insert(0,new ListItem("..Select...","0"));
                    }
                }
        }      

        protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            FeesCollectionEntry fc = new FeesCollectionEntry();
            fc.LoadStudentFessCategory(ddlFeesCat, Session["__StudentId__"].ToString(), ddlBatch.SelectedValue);
            ddlFeesCat.Items.RemoveAt(0);
            ddlFeesCat.Items.Insert(0, new ListItem("...All...", "All"));
        }
        protected void A4_ServerClick(object sender, EventArgs e)
        {
            LoadPaymentDetails();
        }
        private void LoadPaymentDetails()
        {
            try
            {
                string condition = "";
                if(ddlBatch.SelectedValue=="All" && ddlFeesCat.SelectedValue!="All")
                {
                    condition = " WHERE StudentId='" + Session["__StudentId__"].ToString()
                        + "' AND  FeeCatId='" + ddlFeesCat.SelectedValue + "' AND PayStatus='True'";
                }
                else if (ddlBatch.SelectedValue != "All" && ddlFeesCat.SelectedValue == "All")
                {
                    condition = " WHERE StudentId='" + Session["__StudentId__"].ToString()
                        + "' AND PayStatus='True'";
                }
                else if(ddlBatch.SelectedValue!="All" && ddlFeesCat.SelectedValue!="All")
                {
                    condition = " WHERE StudentId='" + Session["__StudentId__"].ToString()
                        + "'  AND BatchId='" + ddlBatch.SelectedValue + "' AND FeeCatId='"
                        + ddlFeesCat.SelectedValue + "' AND PayStatus='True'";
                }
                DataTable dt;
                dt = new DataTable();
                if(fc==null)
                {
                    fc = new FeesCollectionEntry();
                }
                dt = fc.LoadFeeCollection(condition);
                if (dt.Rows.Count > 0)
                {
                    Session["__StdCollectionDetails__"] = dt;
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", 
                        "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=StdCollectionDetails-" + Session["__FullName__"].ToString() + "');", true);  //Open New Tab for Sever side code
                }
                else lblMessage.InnerText = "warning->No Payment";
            }
            catch { }
        }
      
    }
}