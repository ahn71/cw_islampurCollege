using DS.BLL.ControlPanel;
using DS.BLL.DSWS;
using DS.Classes;
using DS.PropertyEntities.Model.DSWS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.Administration.DSWS
{
    public partial class AddPageContent : System.Web.UI.Page
    {
        AddPageContentEntry Entry;
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.InnerText = "";
            if (!IsPostBack)
            {
                if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "AddPageContent.aspx", btnSubmit)) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                commonTask.loadWsPagseList(ddlPageList);
                LoadData();
            }
                
        }
        private void LoadData()
        {
            if (Entry == null)
                Entry = new AddPageContentEntry();
            List<AddPageContentEntities> entities = new List<AddPageContentEntities>();
            entities = Entry.getEntitiesData();
            if (entities != null)
            {
                gvNoticeList.DataSource = entities;
                gvNoticeList.DataBind();
            }
            else
            {
                gvNoticeList.DataSource = null;
                gvNoticeList.DataBind();
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (ddlPageList.SelectedValue == "0")
            {
                lblMessage.InnerText = "warning-> Please select Page.";
                ddlPageList.Focus();
                return;
            }
            if (txtNSubject.Text.Trim() == "")
            {
                lblMessage.InnerText = "warning-> Please enter Title.";
                txtNSubject.Focus();
                return;
            }
            if (txtNDetails.Text.Trim() == "")
            {
                lblMessage.InnerText = "warning-> Please enter Details.";
                txtNDetails.Focus();
                return;
            }

            if (btnSubmit.Text == "Save")
                save();
            else Update();
        }
        private void save()
        {
            AddPageContentEntities entitis = getCotrolValue();
            if (Entry == null)
            {
                Entry = new AddPageContentEntry();
            }
            Entry.AddEntities = entitis;
            if (Entry.IsItExist())
            {
                lblMessage.InnerText = "warning-> This page content is already exist.";
                return;
            }
            if (Entry.Insert())
            {
                saveImg();
                AllClear();
                //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "clearIt();", true);
                lblMessage.InnerText = "success-> Successfully Save";
            }
            LoadData();
        }
        private void Update()
        {
            AddPageContentEntities entitis = getCotrolValueForUpdate();
            if (Entry == null)
            {
                Entry = new AddPageContentEntry();
            }
            Entry.AddEntities = entitis;

            if (Entry.Update())
            {
                saveImg();
                AllClear();
                // ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "clearIt();", true);
                lblMessage.InnerText = "success-> Successfully Updated";
            }
            LoadData();
        }
       
        private AddPageContentEntities getCotrolValue()
        {
            AddPageContentEntities entities = new AddPageContentEntities();
            entities.PageID = ddlPageList.SelectedValue;
            entities.Title = txtNSubject.Text;
            entities.TextContent = txtNDetails.Text;
            entities.EntryTime = DateTime.Now;
            entities.Status = ckbStatus.Checked;
            if (FileUpload1.HasFile == true)
            {
                string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
                string imageNameAs = ddlPageList.SelectedValue + "." + filename.Substring(filename.Length - 3);
                entities.Image = imageNameAs;
            }
            else
                entities.Image = "";
            return entities;
        }
        private AddPageContentEntities getCotrolValueForUpdate()
        {
            AddPageContentEntities entities = new AddPageContentEntities();
            entities.PageID = ViewState["__PageId__"].ToString();
            
            entities.Title = txtNSubject.Text;
            entities.TextContent = txtNDetails.Text;
            entities.EntryTime = DateTime.Now;
            if (FileUpload1.HasFile == true)
            {
                string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
                string imageNameAs = ddlPageList.SelectedValue + "." + filename.Substring(filename.Length - 3);
                entities.Image = imageNameAs;
            }
            else
                entities.Image = ViewState["__ImgName__"].ToString();
            return entities;
        }
        private void AllClear()
        {
            ddlPageList.SelectedValue = "0";
            txtNSubject.Text = "";
            txtNDetails.Text = "";           
            btnSubmit.Text = "Save";
            ddlPageList.Enabled = true;
            imgProfile.ImageUrl = "~/Images/dsimages/blank-frame.png";
        }
        private void saveImg()
        {
            try
            {
                //Get Filename from fileupload control
                string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
                
                ////Save images into Images folder
                ////System.Drawing.Image image = System.Drawing.Image.FromStream(FileUpload1.PostedFile.InputStream);
                ////int width = 100;
                ////int height = 100;
                ////using (System.Drawing.Image thumbnail = image.GetThumbnailImage(width, height, new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback), IntPtr.Zero))
                ////{
                ////    using (MemoryStream memoryStream = new MemoryStream())
                ////    {
                ////        thumbnail.Save(Server.MapPath("UI/DSWS/PageImages/" + filename), System.Drawing.Imaging.ImageFormat.Png);
                ////    }
                ////}

                string imageNameAs= ddlPageList.SelectedValue+"." + filename.Substring(filename.Length - 3);
                if(btnSubmit.Text.Trim()=="Update")
                deleteImage(ViewState["__ImgName__"].ToString());
                FileUpload1.SaveAs(Server.MapPath("/UI/DSWS/PageImages/" +imageNameAs));

            }
            catch { }
        }
        public bool ThumbnailCallback()
        {
            return false;
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {

        }

        protected void gvNoticeList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rIndex = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "Change")
            {
                ViewState["__PageId__"] = ddlPageList.SelectedValue = gvNoticeList.DataKeys[rIndex].Values[0].ToString();
                txtNSubject.Text = gvNoticeList.Rows[rIndex].Cells[2].Text;
                txtNDetails.Text = gvNoticeList.Rows[rIndex].Cells[3].Text;

                string imageName="";
                ViewState["__ImgName__"] = imageName = gvNoticeList.DataKeys[rIndex].Values[1].ToString();
                if (imageName != "")
                {
                    string url = @"/UI/DSWS/PageImages/" + imageName;
                    imgProfile.ImageUrl = url;
                }
                ckbStatus.Checked = bool.Parse(gvNoticeList.DataKeys[rIndex].Values[2].ToString());
                btnSubmit.Text = "Update";
                ddlPageList.Enabled = false;
            }
            else if (e.CommandName == "Remove")
            {
                if (Entry == null)
                    Entry = new AddPageContentEntry();
                if (Entry.Delete(gvNoticeList.DataKeys[rIndex].Values[0].ToString()))
                {
                    deleteImage(gvNoticeList.DataKeys[rIndex].Values[1].ToString());
                    gvNoticeList.Rows[rIndex].Visible = false;
                    lblMessage.InnerText = "success->Successfully Deleted";
                }
            }
        }
        private void deleteImage(string filename)
        {
            try
            {
                //Get Filename from fileupload control                
                string path = Server.MapPath("/UI/DSWS/PageImages/" + filename);
                FileInfo file = new FileInfo(path);
                if (file.Exists)
                {
                    file.Delete();
                }
            }
            catch { }
        }
    }
}