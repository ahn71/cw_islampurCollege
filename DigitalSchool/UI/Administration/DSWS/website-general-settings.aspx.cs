using DS.BLL.Admission;
using DS.BLL.ControlPanel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.Administration.DSWS
{
    public partial class website_general_settings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.InnerText = "";
            if (!IsPostBack)
            {
                if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "website_general_settings.aspx", btnSubmit)) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                //url bind->
                aDashboard.HRef = "~/" + Classes.Routing.DashboardRouteUrl;
                aAdministration.HRef = "~/" + Classes.Routing.AdministrationRouteUrl;
                aWebsite.HRef = "~/" + Classes.Routing.WSHomeRouteUrl;
                //url bind-<

                StdAdmFormEntry.bindAdmissionInfo(ckbIsAdmissionOpen, txtAdmissionMsg);
            }
        }

        protected void ckbIsAdmissionOpen_CheckedChanged(object sender, EventArgs e)
        {
            submitInfo();
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            submitInfo();
        }
        private void submitInfo()
        {
            try {
                if (txtAdmissionMsg.Text.Trim() == "")
                {
                    lblMessage.InnerText = "warning-> Please enter the admission message";
                    txtAdmissionMsg.Focus();
                    return;
                }
                if (StdAdmFormEntry.setIsAdmissionOpen(ckbIsAdmissionOpen.Checked, txtAdmissionMsg.Text.Trim()))
                {
                    lblMessage.InnerText = "success-> Successfully Submited";
                }
                else
                    lblMessage.InnerText = "error-> Unable to submit.";
            } catch (Exception ex)
            {
                lblMessage.InnerText = "error->"+ex.Message;
            }
        }
    }
}