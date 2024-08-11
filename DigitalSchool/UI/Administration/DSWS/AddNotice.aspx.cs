using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DS.BLL.DSWS;
using DS.PropertyEntities.Model.DSWS;
using ComplexScriptingSystem;
using System.IO;
using System.Data;
using DS.BLL;
using DS.Classes;
using DS.DAL;
using DS.BLL.ControlPanel;

namespace DS.UI.Administration.DSWS
{
    public partial class AddNotice : System.Web.UI.Page
    {
        AddNoticeEntry Entry;
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.InnerText = "";
            if (!IsPostBack)
            {
                Button btnSave = new Button();
                if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "AddNotice.aspx", btnSave)) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                //url bind->
                aDashboard.HRef = "~/" + Classes.Routing.DashboardRouteUrl;
                aAdministration.HRef = "~/" + Classes.Routing.AdministrationRouteUrl;
                aWebsite.HRef = "~/" + Classes.Routing.WSHomeRouteUrl;
                aNotice.HRef = "~/" + Classes.Routing.NoticeListRouteUrl;                
                liAddEdit.InnerText = "Add";
                //url bind-<

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
            try
            {
                string id = RouteData.Values["id"].ToString();
                id = commonTask.Base64Decode(id);
                if (Entry == null)
                    Entry = new AddNoticeEntry();
                DataTable dt = new DataTable();
                dt = Entry.getNoticeWithAttachmentData(id);
                if (dt != null && dt.Rows.Count > 0)
                {
                    ViewState["__FileName__"] = dt.Rows[0]["onlyFileName"].ToString(); 
                    txtNSubject.Text = dt.Rows[0]["NSubject"].ToString();
                    txtNDetails.Text = dt.Rows[0]["NDetails"].ToString(); 
                    txtPublishdate.Text = dt.Rows[0]["PublishdDate"].ToString();
                    if (dt.Rows[0]["IsActive"].ToString().Equals("True")) 
                        chkIsActive.Checked = true;
                    else
                        chkIsActive.Checked = false;
                    if (dt.Rows[0]["IsImportantNews"].ToString().Equals("True"))
                        chkIsImportantNews.Checked = true;
                    else
                        chkIsImportantNews.Checked = false;
                    ViewState["__NSL__"] = id;
                    btnSubmit.Text = "Update";
                }
               



            }
            catch (Exception ex) { }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try {
                if (btnSubmit.Text == "Save")
                    save_orm();
                else update_orm();
            }
            catch (Exception ex)            
            {
                lblMessage.InnerText = "error-> unable to save or update"+ex.Message;
            }
            
        }
        private WSNoticeAttach getCotrolValue_orm()
        {
            string FileName = "";
            if (fileAttachment.HasFile)
                FileName = Path.GetFileName(fileAttachment.PostedFile.FileName);
            WSNoticeAttach entities = new WSNoticeAttach
            {
                NSL = (btnSubmit.Text == "Update") ? int.Parse(ViewState["__NSL__"].ToString()) : 0,
                FileName = FileName,
                Title = txtNSubject.Text.Trim(),
                Status = chkIsActive.Checked,
                PublishdDate = DateTime.Parse(commonTask.ddMMyyyyToyyyyMMdd(txtPublishdate.Text.Trim())),
                NDetails = txtNDetails.Text.Trim(),
                NEntryDate = TimeZoneBD.getCurrentTimeBD(),
                pinTop = false,
                IsImportantNews= chkIsImportantNews.Checked
            };
            return entities;
        }
        private void save_orm()
        {

            if (Entry == null)
            {
                Entry = new AddNoticeEntry();
            }
            Entry.AddwSNoticeAttach = getCotrolValue_orm();
           int result  =Entry.save();
            if (result != 0)
            {
                if (fileAttachment.HasFile)
                {
                  string   FileName = Path.GetFileName(fileAttachment.PostedFile.FileName);
                    saveFileToFolder(result.ToString() + "_" + FileName);
                }
                Response.Redirect("~/"+Routing.NoticeListRouteUrl);
            }

        }
        private void save()
        {
          
            if (Entry == null)
            {
                Entry = new AddNoticeEntry();
            }
            string FileName = "";
            if(fileAttachment.HasFile)
                FileName= Path.GetFileName(fileAttachment.PostedFile.FileName);
            string[] pDate = txtPublishdate.Text.Trim().Split('-');
            string PublishDate= pDate[2]+"-"+ pDate[1]+"-"+pDate[0];
            int result = Entry.InsertNoticeWithAttachment(FileName, txtNSubject.Text.Trim(), chkIsActive.Checked.ToString(), PublishDate, txtNDetails.Text.Trim(),TimeZoneBD.getCurrentTimeBD().ToString("yyyy-MM-dd HH:mm:ss"), "0",chkIsImportantNews.Checked.ToString());
            if (result != 0)
            {
                if (fileAttachment.HasFile)
                {
                    FileName = Path.GetFileName(fileAttachment.PostedFile.FileName);
                    saveFileToFolder(result.ToString()+"_"+FileName);
                }
                   
                AllCleal();
                //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "clearIt();", true);
                lblMessage.InnerText = "success-> Successfully Save";
            }
            
        }
       private void saveOld()
        {
            AddNoticeEntities entitis = getCotrolValue();
            if (Entry == null)
            {
                Entry = new AddNoticeEntry();
            }
            Entry.AddEntities = entitis;
            if (Entry.Insert())
            {
                Response.Redirect("~/" + Routing.NoticeListRouteUrl);
            }
           
        }
        private void update_orm()
        {

            if (Entry == null)
            {
                Entry = new AddNoticeEntry();
            }
            Entry.AddwSNoticeAttach = getCotrolValue_orm();
            bool result = Entry.update();
            if (result)
            {
                if (fileAttachment.HasFile)
                {
                    string FileName = Path.GetFileName(fileAttachment.PostedFile.FileName);
                    saveFileToFolder(result.ToString() + "_" + FileName);
                }
                Response.Redirect("~/" + Routing.NoticeListRouteUrl);
            }

        }
        private void Update()
        {
            
            if (Entry == null)
            {
                Entry = new AddNoticeEntry();
            }
            string FileName = ViewState["__FileName__"].ToString();
            if (fileAttachment.HasFile)
                FileName = Path.GetFileName(fileAttachment.PostedFile.FileName);
            string[] pDate = txtPublishdate.Text.Trim().Split('-');
            string PublishDate = pDate[2] + "-" + pDate[1] + "-" + pDate[0];
            if (Entry.UpdateNoticeWithAttachment(ViewState["__NSL__"].ToString(), FileName, txtNSubject.Text.Trim(), chkIsActive.Checked.ToString(), PublishDate, txtNDetails.Text.Trim(), TimeZoneBD.getCurrentTimeBD().ToString("yyyy-MM-dd HH:mm:ss"), chkIsImportantNews.Checked.ToString())) {
                if (fileAttachment.HasFile)
                {

                    FileName = Path.GetFileName(fileAttachment.PostedFile.FileName);
                    deleteFileToFolder(ViewState["__NSL__"].ToString() + "_" + ViewState["__FileName__"].ToString());
                    saveFileToFolder(ViewState["__NSL__"].ToString() + "_" + FileName);
                }
                Response.Redirect("~/"+Routing.NoticeListRouteUrl);    
            }                
            
        }
        private void UpdateOld()
        {
            AddNoticeEntities entitis = getCotrolValueForUpdate();
            if (Entry == null)
            {
                Entry = new AddNoticeEntry();
            }
            Entry.AddEntities = entitis;

            if (Entry.Update())
            {
                AllCleal();
                // ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "clearIt();", true);
                lblMessage.InnerText = "success-> Successfully Updated";
            }
            
        }
        private AddNoticeEntities getCotrolValue()
        {
            AddNoticeEntities entities = new AddNoticeEntities();
          
            entities.NSubject = txtNSubject.Text;
            entities.NDetails = txtNDetails.Text;
            entities.NEntryDate = TimeZoneBD.getCurrentTimeBD();
            entities.NOrdering =int.Parse(txtOrder.Text);
            entities.IsActive = chkIsActive.Checked;
            entities.IsImportantNews = chkIsImportantNews.Checked;
            entities.NPublishedDate = DateTime.Parse(Classes.commonTask.ddMMyyyyToyyyyMMdd(txtPublishdate.Text.Trim())); 
            return entities;
        }
        private AddNoticeEntities getCotrolValueForUpdate()
        {
            AddNoticeEntities entities = new AddNoticeEntities();
            entities.NSL = int.Parse(ViewState["__NSL__"].ToString());
            entities.NSubject = txtNSubject.Text;
            entities.NDetails = txtNDetails.Text;
            entities.NOrdering = int.Parse(txtOrder.Text);
          //  entities.NEntryDate = DateTime.Now;
            entities.IsActive = chkIsActive.Checked;
            entities.IsImportantNews = chkIsImportantNews.Checked;
            entities.NPublishedDate =DateTime.Parse(Classes.commonTask.ddMMyyyyToyyyyMMdd(txtPublishdate.Text.Trim())); 
            return entities;
        }
       
        private void AllCleal() 
        {
            txtNSubject.Text = "";
            txtNDetails.Text = "";
            txtPublishdate.Text = "";
            txtOrder.Text = "";
            chkIsActive.Checked = false;
            chkIsImportantNews.Checked = false;
            fileAttachment.Dispose();
            
        }
        private void saveFileToFolder(string filename)
        {
            try
            {
                
                fileAttachment.SaveAs(Server.MapPath("/Images/dsimages/Notice/" + filename));

            }
            catch { }
        }
        private void deleteFileToFolder(string filename)
        {
            try
            {

                File.Delete(Server.MapPath("/Images/dsimages/Notice/" + filename));

            }
            catch { }
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            AllCleal();
        }
    }
}