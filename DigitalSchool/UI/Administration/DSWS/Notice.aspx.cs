using DS.BLL.ControlPanel;
using DS.BLL.DSWS;
using DS.Classes;
using DS.PropertyEntities.Model.DSWS;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.Administration.DSWS
{
    public partial class Notice : System.Web.UI.Page
    {
        AddNoticeEntry Entry;
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.InnerText = "";
            if (!IsPostBack)
            {
                if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "AddNotice.aspx", btnAdd)) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                //url bind->
                aDashboard.HRef = "~/" + Classes.Routing.DashboardRouteUrl;
                aAdministration.HRef = "~/" + Classes.Routing.AdministrationRouteUrl;
                aWebsite.HRef = "~/" + Classes.Routing.WSHomeRouteUrl;
                //url bind-<

                LoadData();

            }
        }

     
    
  
        private void LoadData()
        {
            if (Entry == null)
                Entry = new AddNoticeEntry();
            DataTable dt = new DataTable();
            dt = Entry.getNoticeWithAttachmentData();
            if (dt != null && dt.Rows.Count > 0)
            {
                gvNoticeList.DataSource = dt;
                gvNoticeList.DataBind();
                gvNoticeList.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            else
            {
                gvNoticeList.DataSource = null;
                gvNoticeList.DataBind();
            }
        }
        private void LoadDataOld()
        {
            if (Entry == null)
                Entry = new AddNoticeEntry();
            List<AddNoticeEntities> getdata = new List<AddNoticeEntities>();
            getdata = Entry.getEntitiesData();
            if (getdata != null)
            {
                gvNoticeList.DataSource = getdata;
                gvNoticeList.DataBind();
                gvNoticeList.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            else
            {
                gvNoticeList.DataSource = null;
                gvNoticeList.DataBind();
            }
        }


        protected void gvNoticeList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            LoadData();
            gvNoticeList.PageIndex = e.NewPageIndex;
            gvNoticeList.DataBind();
        }

        protected void gvNoticeList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rIndex = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "change")
            {              
               
                string sl = gvNoticeList.DataKeys[rIndex].Values[0].ToString();
                sl= commonTask.Base64Encode(sl.ToString());
                Response.Redirect(GetRouteUrl("NoticeEditRoute", new { id = sl }));

            }
            else if (e.CommandName == "Delete")
            {
                ViewState["__FileName__"] = gvNoticeList.DataKeys[rIndex].Values[4].ToString();
                ViewState["__NSL__"] = gvNoticeList.DataKeys[rIndex].Values[0].ToString();
                if (Entry == null)
                    Entry = new AddNoticeEntry();
                if (Entry.DeleteNoticeWithAttachment(gvNoticeList.DataKeys[rIndex].Values[0].ToString()))
                {
                    deleteFileToFolder(ViewState["__NSL__"].ToString() + "_" + ViewState["__FileName__"].ToString());
                    lblMessage.InnerText = "success->Successfully Deleted";
                    LoadData();
                }

            }
            else if (e.CommandName == "Pin")
            {
                try
                {
                    Button btnPin = new Button();
                    btnPin = (Button)gvNoticeList.Rows[rIndex].Cells[7].FindControl("btnPin");
                    if (Entry == null)
                        Entry = new AddNoticeEntry();
                    if (btnPin.Text == "Pin")
                    {
                        if (Entry.UpdatePinToTop(gvNoticeList.DataKeys[rIndex].Values[0].ToString()))
                            LoadData();
                    }
                    else
                    {
                        if (Entry.UpdateToUnpin(gvNoticeList.DataKeys[rIndex].Values[0].ToString()))
                            LoadData();

                    }
                }
                catch (Exception ex) { }


            }
        }

        protected void gvNoticeList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            LoadData();
        }
      
        
        private void deleteFileToFolder(string filename)
        {
            try
            {

                File.Delete(Server.MapPath("/Images/dsimages/Notice/" + filename));

            }
            catch { }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/"+Routing.NoticeAddRouteUrl);
        }
    }
}