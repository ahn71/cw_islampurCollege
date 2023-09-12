using DS.BLL.ControlPanel;
using DS.BLL.DSWS;
using DS.Classes;
using DS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.Administration.DSWS
{
    public partial class QuickLink : System.Web.UI.Page
    {
        QuickLinkEntry quickLinkEntry;
        List<WSQuickLink> list;
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.InnerText = "";
            if (!IsPostBack)
            {
                if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "AddQuickLink.aspx", btnAdd)) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                //url bind->
                aDashboard.HRef = "~/" + Classes.Routing.DashboardRouteUrl;
                aAdministration.HRef = "~/" + Classes.Routing.AdministrationRouteUrl;
                aWebsite.HRef = "~/" + Classes.Routing.WSHomeRouteUrl;
                //url bind-<

                loadList();
            }
        }
        
        private void loadList()
        {
            if (quickLinkEntry == null)
            {
                quickLinkEntry = new QuickLinkEntry();
            }
            list = new List<WSQuickLink>();
            list= quickLinkEntry.list();
            Session["__QuickLinkLlist__"] = list;
            gvSlider.DataSource = list;
            gvSlider.DataBind();
        }
    

        protected void gvSlider_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "change")
            {
                int rIndex = int.Parse(e.CommandArgument.ToString());
                int sl = int.Parse(gvSlider.DataKeys[rIndex].Values[0].ToString());          
                string _sl = commonTask.Base64Encode(sl.ToString());                
                Response.Redirect(GetRouteUrl("QuickLinkEditRoute", new { id = _sl }));
            }
            else if(e.CommandName== "remove")
            {
                int rIndex = int.Parse(e.CommandArgument.ToString());
                int sl = int.Parse(gvSlider.DataKeys[rIndex].Values[0].ToString());
                delete(sl, rIndex);
            }
        }
        private void delete(int sl,int rIndex)
        {
            if (quickLinkEntry == null)
            {
                quickLinkEntry = new QuickLinkEntry();
            }           
            if (quickLinkEntry.delete(sl))
            {
                lblMessage.InnerText = "success->Successfully Delete.";
                gvSlider.Rows[rIndex].Visible = false;
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/"+Routing.QuickLinkAddRouteUrl);
        }
    }
}