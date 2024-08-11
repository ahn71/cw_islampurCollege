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
    public partial class AddQuickLink : System.Web.UI.Page
    {
        QuickLinkEntry quickLinkEntry;
       
      
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.InnerText = "";
            if (!IsPostBack)
            {
                if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "AddQuickLink.aspx", btnSave)) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                //url bind->
                aDashboard.HRef = "~/" + Classes.Routing.DashboardRouteUrl;
                aAdministration.HRef = "~/" + Classes.Routing.AdministrationRouteUrl;
                aWebsite.HRef = "~/" + Classes.Routing.WSHomeRouteUrl;
                aQuickLink.HRef = "~/" + Classes.Routing.QuickLinkListRouteUrl;
                liAddEdit.InnerText = "Add";

                string[] path = HttpContext.Current.Request.Url.AbsolutePath.Split('/');
                if (path.Contains("edit"))
                {
                    liAddEdit.InnerText = "Edit";
                    loadEditData();
                }
                    
            }
        }
        private void loadEditData()
        {
            try {
             string  stid = RouteData.Values["id"].ToString();
                int SL = int.Parse(commonTask.Base64Decode(stid));
                ViewState["__SL__"] = SL.ToString();
                if (quickLinkEntry == null)
                    quickLinkEntry = new QuickLinkEntry();

                List<WSQuickLink> list =(List<WSQuickLink>) Session["__QuickLinkLlist__"];

                WSQuickLink quickLink = list.Find(s=>s.SL== SL);
                txtTitle.Text = quickLink.Title;
                txtUrl.Text = quickLink.Url;
                txtOrdering.Text = quickLink.Ordering.ToString();
                ckbIsActive.Checked = quickLink.IsActive;
                btnSave.Text = "Update";



            } catch (Exception ex) { }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (btnSave.Text.Trim() == "Save")
            {
                save();
            }
            else
            {
                update();
            }

        }
      
        private void save()
        {

            if (quickLinkEntry == null)
            {
                quickLinkEntry = new QuickLinkEntry();
            }
            quickLinkEntry.SetEntities = getCotrolValue();
            if (quickLinkEntry.save())
            {
                Response.Redirect("~/" + Classes.Routing.QuickLinkListRouteUrl);
                //lblMessage.InnerText = "success->Successfully Save.";
                //clear();               
            }

        }
        private void update()
        {

            if (quickLinkEntry == null)
            {
                quickLinkEntry = new QuickLinkEntry();
            }
            quickLinkEntry.SetEntities = getCotrolValue();
            if (quickLinkEntry.update())
            {
                Response.Redirect("~/" + Classes.Routing.QuickLinkListRouteUrl);
                //lblMessage.InnerText = "success->Successfully Update.";
                //clear();               
            }

        }
        private WSQuickLink getCotrolValue()
        {
            WSQuickLink entities = new WSQuickLink
            {
                SL = (btnSave.Text == "Update") ? int.Parse(ViewState["__SL__"].ToString()) : 0,
                Title = txtTitle.Text.Trim(),
                Url = txtUrl.Text.Trim(),
                Ordering = int.Parse(txtOrdering.Text.Trim()),
                IsActive = ckbIsActive.Checked
            };
            return entities;
        }
        private void clear()
        {
            txtTitle.Text = "";
            txtUrl.Text = "";
            txtOrdering.Text = "";
            ckbIsActive.Checked = true;            
        }

        

        protected void btnClear_Click(object sender, EventArgs e)
        {
            clear();
        }
    }
}