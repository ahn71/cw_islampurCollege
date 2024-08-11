using ComplexScriptingSystem;
using DS.BLL.ControlPanel;
using DS.BLL.DSWS;
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
    public partial class AddSpesialDescription : System.Web.UI.Page
    {
        AddSpecialDescriptionEntry Entry;
        string imageName = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.InnerText = "";
            if (!IsPostBack)
            {
                if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "AddSpesialDescription.aspx", btnSubmit)) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                LoadData();

            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (btnSubmit.Text == "Save")
                save();           
            else Update(); 
        }
        private void save()
        {
            AddSecialDescriptionEntities entitis = getCotrolValue();
            if (Entry == null)
            {
                Entry = new AddSpecialDescriptionEntry();
            }
            Entry.AddEntities = entitis;
            if (Entry.IsItExist())
            {
                lblMessage.InnerText = "warning-> This type of description is already exist";
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
            AddSecialDescriptionEntities entitis = getCotrolValueForUpdate();
            if (Entry == null)
            {
                Entry = new AddSpecialDescriptionEntry();
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
        private void saveImg()
        {
            try
            {
                //Get Filename from fileupload control
                string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
                //Save images into Images folder
                System.Drawing.Image image = System.Drawing.Image.FromStream(FileUpload1.PostedFile.InputStream);
                int width = 100;
                int height = 100;
                using (System.Drawing.Image thumbnail = image.GetThumbnailImage(width, height, new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback), IntPtr.Zero))
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        thumbnail.Save(Server.MapPath("/Images/dsimages/" + filename), System.Drawing.Imaging.ImageFormat.Png);
                    }
                }
                //FileUpload1.SaveAs(Server.MapPath("/Images/profileImages/" + txtAdmissionNo.Text.Trim() + filename));

            }
            catch { }
        }
        public bool ThumbnailCallback()
        {
            return false;
        }
        private void deleteImage(string filename)
        {
            try
            {
                //Get Filename from fileupload control                
                string path = Server.MapPath("/Images/dsimages/" + filename);
                FileInfo file = new FileInfo(path);
                if (file.Exists)
                {
                    file.Delete();
                }
            }
            catch { }
        }  
        private AddSecialDescriptionEntities getCotrolValue()
        {
            AddSecialDescriptionEntities entities = new AddSecialDescriptionEntities();
            entities.Type=ddlSDType.SelectedValue;
            entities.Subject = txtNSubject.Text;
            entities.Details = txtNDetails.Text;
            entities.EntryDate = DateTime.Now;        
            entities.PublishedDate = convertDateTime.getCertainCulture(txtPublishdate.Text);
            if (FileUpload1.HasFile == true)
            {
                string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
                entities.Image = filename;
            }
            else
                entities.Image = "";  
            return entities;
        }
        private AddSecialDescriptionEntities getCotrolValueForUpdate()
        {
            AddSecialDescriptionEntities entities = new AddSecialDescriptionEntities();
            entities.DSL = int.Parse(ViewState["__DSL__"].ToString());
            entities.Type = ddlSDType.SelectedValue;
            entities.Subject = txtNSubject.Text;
            entities.Details = txtNDetails.Text;
            entities.PublishedDate = convertDateTime.getCertainCulture(txtPublishdate.Text);
            if (FileUpload1.HasFile == true)
            {
                string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
                entities.Image = filename;
            }
            else
                entities.Image = ViewState["__ImgName__"].ToString();  
            return entities;           
        }
        private void LoadData()
        {
            if (Entry == null)
                Entry = new AddSpecialDescriptionEntry();
            List<AddSecialDescriptionEntities> getdata = new List<AddSecialDescriptionEntities>();
            getdata = Entry.getEntitiesData();
            if (getdata != null)
            {
                gvNoticeList.DataSource = getdata;
                gvNoticeList.DataBind();
            }
            else
            {
                gvNoticeList.DataSource = null;
                gvNoticeList.DataBind();
            }
        }
        private void AllClear()
        {
            txtNSubject.Text = "";
            txtNDetails.Text = "";
            txtPublishdate.Text = "";
            btnSubmit.Text = "Save";
            ddlSDType.Enabled = true;
            imgProfile.ImageUrl = "~/Images/dsimages/blank-frame.png";
        }

        protected void gvNoticeList_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            int rIndex = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "change")
            {
                ddlSDType.SelectedValue = gvNoticeList.Rows[rIndex].Cells[1].Text;
                txtNSubject.Text = gvNoticeList.Rows[rIndex].Cells[2].Text;
                txtNDetails.Text = gvNoticeList.Rows[rIndex].Cells[3].Text;
                Label lv = new Label();
                lv = (Label)gvNoticeList.Rows[rIndex].Cells[3].FindControl("lblPublishDate");
                txtPublishdate.Text = lv.Text;                      
                ViewState["__DSL__"] = gvNoticeList.DataKeys[rIndex].Values[0].ToString();
                ViewState["__ImgName__"] = imageName = gvNoticeList.DataKeys[rIndex].Values[1].ToString();
                if (imageName != "")
                {
                    string url = @"/Images/dsimages/" + Path.GetFileName(imageName);
                    imgProfile.ImageUrl = url;
                }  
                btnSubmit.Text = "Update";
                ddlSDType.Enabled = false;             
            }
            else if (e.CommandName == "Delete")
            {
                if (Entry == null)
                    Entry = new AddSpecialDescriptionEntry();
                if (Entry.Delete(gvNoticeList.DataKeys[rIndex].Values[0].ToString()))
                {
                    deleteImage(gvNoticeList.DataKeys[rIndex].Values[1].ToString());
                    lblMessage.InnerText = "success->Successfully Deleted";
                }
            }
        }

        protected void gvNoticeList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            LoadData();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            AllClear();
        }

        protected void gvNoticeList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            LoadData();
            gvNoticeList.PageIndex = e.NewPageIndex;
            gvNoticeList.DataBind();
        }     

    }
}