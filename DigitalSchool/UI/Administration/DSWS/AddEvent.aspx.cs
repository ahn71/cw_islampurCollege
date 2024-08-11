using DS.BLL.ControlPanel;
using DS.BLL.DSWS;
using DS.DAL.ComplexScripting;
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
    public partial class AddEvent : System.Web.UI.Page
    {

        EventInfoEntry EEntry;
        EventDetailsEntry EdEntry;
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.InnerText = "";
            if (!IsPostBack)
            {

                if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "AddEvent.aspx", btnSubmit)) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                LoadAlbumInfo();
                if (EdEntry == null)
                    EdEntry = new EventDetailsEntry();
                loadAlbumDetails();
                EdEntry.bindAlbumlistInDropdownlist(ddlAlbumName);

            }
        }
        private EventInfoEntities getAlbumInfoControlvalues()
        {
            EventInfoEntities entities = new EventInfoEntities();
            if (btnSubmit.Text == "Update")
                entities.ESL = int.Parse(ViewState["__ESL__"].ToString());
            entities.EventName = txtEventName.Text;
            entities.Notes = txtNotes.Text;
            entities.IsActive = (chkIsActive.Checked == true) ? true : false;
            return entities;
        }
        private EventDetailsEntities getAlbumDetailsControlvalues()
        {
            EventDetailsEntities entities = new EventDetailsEntities();
            if (btnSaveAD.Text == "Update")
                entities.SL = int.Parse(ViewState["__SL__"].ToString());
            entities.ESL = int.Parse(ddlAlbumName.SelectedValue);
            entities.Title = txtTitle.Text;
            entities.Description = txtShortDes.Text;
            entities.EventDate = convertDateTime.getCertainCulture( txtEventDate.Text.Trim()); 
            if (FileUpload1.HasFile == true)
            {
                string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
                entities.imgLocation = "~/Images/dsimages/event/" + filename;
            }
            else
            {
                if (btnSaveAD.Text == "Save")
                    entities.imgLocation = "";
                else entities.imgLocation = ViewState["__imgPath__"].ToString();
            }
            return entities;
        }
        private void SaveAlbumInfo()
        {
            EventInfoEntities entities = getAlbumInfoControlvalues();
            if (EEntry == null)
                EEntry = new EventInfoEntry();
            EEntry.AddEntities = entities;
            if (EEntry.Insert())
            {
                lblMessage.InnerText = "success->Successfully Saved.";
                AllClearAlbumInfo();
            }
            else
            {
                lblMessage.InnerText = "error->Unable to Save.";
            }
        }

        private void UpdateAlbumInfo()
        {
            EventInfoEntities entities = getAlbumInfoControlvalues();
            if (EEntry == null)
                EEntry = new EventInfoEntry();
            EEntry.AddEntities = entities;
            if (EEntry.Update())
            {
                lblMessage.InnerText = "success->Successfully Updated.";
                AllClearAlbumInfo();
            }
            else
            {
                lblMessage.InnerText = "error->Unable to Update.";
            }
        }
        private void LoadAlbumInfo()
        {
            List<EventInfoEntities> entities = new List<EventInfoEntities>();
            if (EEntry == null)
                EEntry = new EventInfoEntry();
            entities = EEntry.getEntitiesData();
            if (entities != null)
            {
                gvAlbumInfo.DataSource = entities;
                gvAlbumInfo.DataBind();
            }

        }
        private void AllClearAlbumInfo()
        {
            txtEventName.Text = "";
            txtNotes.Text = "";
            chkIsActive.Checked = false;
            btnSubmit.Text = "Save";
        }

        private void AllClearAD()
        {
            txtTitle.Text = "";
            txtShortDes.Text = "";
            imgProfile.ImageUrl = "/Images/dsimages/blank-frame.png";
            btnSaveAD.Text = "Save";
        }
        private void saveAlbumDetails()
        {
            EventDetailsEntities entities = getAlbumDetailsControlvalues();
            if (EdEntry == null)
                EdEntry = new EventDetailsEntry();
            EdEntry.AddEntities = entities;
            if (EdEntry.Insert())
            {
                saveImg();
                lblMessage.InnerText = "success->Successfully Save.";
                AllClearAD();
            }
        }
        private void updateAlbumDetails()
        {
            EventDetailsEntities entities = getAlbumDetailsControlvalues();
            if (EdEntry == null)
                EdEntry = new EventDetailsEntry();
            EdEntry.AddEntities = entities;
            if (EdEntry.Update())
            {
                if (FileUpload1.HasFile == true)
                {
                    deleteImage(ViewState["__imgPath__"].ToString());
                    saveImg();
                }
                lblMessage.InnerText = "success->Successfully Update.";
                AllClearAD();
            }
        }
        private void saveImg()
        {
            try
            {
                //Get Filename from fileupload control
                string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
                //Save images into Images folder
                System.Drawing.Image image = System.Drawing.Image.FromStream(FileUpload1.PostedFile.InputStream);
                int width = 261;
                int height = 151;
                using (System.Drawing.Image thumbnail = image.GetThumbnailImage(width, height, new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback), IntPtr.Zero))
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        thumbnail.Save(Server.MapPath("/Images/dsimages/event/" + filename), System.Drawing.Imaging.ImageFormat.Png);
                    }
                }
                //FileUpload1.SaveAs(Server.MapPath("/Images/profileImages/" + txtAdmissionNo.Text.Trim() + filename));

            }
            catch { }
        }
        private void deleteImage(string filename)
        {
            try
            {
                //Get Filename from fileupload control
                // string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
                //Save images into Images folder
                //  FileUpload1.SaveAs(Server.MapPath(filename));
                string path = Server.MapPath(filename);
                FileInfo file = new FileInfo(path);
                if (file.Exists)
                {
                    file.Delete();
                }
            }
            catch { }
        }
        public bool ThumbnailCallback()
        {
            return false;
        }
        private void loadAlbumDetails()
        {
            List<EventDetailsEntities> entitiesList = new List<EventDetailsEntities>();
            if (EdEntry == null)
                EdEntry = new EventDetailsEntry();
            entitiesList = EdEntry.getEntitiesData();
            if (entitiesList != null)
            {
                gvAlbumDetails.DataSource = entitiesList;
                gvAlbumDetails.DataBind();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (btnSubmit.Text == "Save")
                SaveAlbumInfo();
            else UpdateAlbumInfo();
            LoadAlbumInfo();
            if (EdEntry == null)
                EdEntry = new EventDetailsEntry();
            EdEntry.bindAlbumlistInDropdownlist(ddlAlbumName);
        }

        protected void gvAlbumInfo_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            int rIndex = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "change")
            {
                txtEventName.Text = gvAlbumInfo.Rows[rIndex].Cells[1].Text;

                txtNotes.Text = (gvAlbumInfo.Rows[rIndex].Cells[2].Text.Equals("&nbsp;")) ? " " : gvAlbumInfo.Rows[rIndex].Cells[2].Text;
                if (gvAlbumInfo.DataKeys[rIndex].Values[1].ToString().Equals("True"))
                    chkIsActive.Checked = true;
                else
                    chkIsActive.Checked = false;
                ViewState["__ESL__"] = gvAlbumInfo.DataKeys[rIndex].Values[0].ToString();
                btnSubmit.Text = "Update";
            }
            else if (e.CommandName == "Delete")
            {
                if (EEntry == null)
                    EEntry = new EventInfoEntry();
                if (EEntry.Delete(gvAlbumInfo.DataKeys[rIndex].Values[0].ToString()))
                {
                    deleteImage(gvAlbumInfo.DataKeys[rIndex].Values[1].ToString());
                    lblMessage.InnerText = "success-> Successfully Deleted";
                    gvAlbumInfo.Rows[rIndex].Visible = false;
                }
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            AllClearAlbumInfo();
        }

        protected void gvAlbumInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            LoadAlbumInfo();
        }

        protected void btnSaveAD_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile == true && Path.GetFileName(FileUpload1.PostedFile.FileName).Length > 20)
            {
                lblMessage.InnerText = "warning->Image Name Maximum 20 Character!";
                return;
            }
            if (btnSaveAD.Text == "Save")
                saveAlbumDetails();
            else updateAlbumDetails();
            loadAlbumDetails();
        }

        protected void gvAlbumDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rIndex = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "change")
            {
                ddlAlbumName.SelectedValue = gvAlbumDetails.DataKeys[rIndex].Values[2].ToString();
                txtTitle.Text = (gvAlbumDetails.Rows[rIndex].Cells[2].Text.Equals("&nbsp;")) ? " " : gvAlbumDetails.Rows[rIndex].Cells[2].Text;
                txtShortDes.Text = (gvAlbumDetails.Rows[rIndex].Cells[3].Text.Equals("&nbsp;")) ? " " : gvAlbumDetails.Rows[rIndex].Cells[3].Text;
                imgProfile.ImageUrl = gvAlbumDetails.DataKeys[rIndex].Values[1].ToString();
                ViewState["__SL__"] = gvAlbumDetails.DataKeys[rIndex].Values[0].ToString();

                Label lblEventDate = new Label();
                lblEventDate = (Label)gvAlbumDetails.Rows[rIndex].Cells[4].FindControl("lblEventDate");
                txtEventDate.Text = lblEventDate.Text;
                btnSaveAD.Text = "Update";
                ViewState["__imgPath__"] = gvAlbumDetails.DataKeys[rIndex].Values[1].ToString();
            }
            else if (e.CommandName == "Delete")
            {
                if (EdEntry == null)
                    EdEntry = new EventDetailsEntry();
                if (EdEntry.Delete(gvAlbumDetails.DataKeys[rIndex].Values[0].ToString()))
                {
                    deleteImage(gvAlbumDetails.DataKeys[rIndex].Values[1].ToString());
                    lblMessage.InnerText = "success-> Successfully Deleted";
                    gvAlbumDetails.Rows[rIndex].Visible = false;
                }
            }
        }

        protected void gvAlbumDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            loadAlbumDetails();
        }

        protected void btnClearAD_Click(object sender, EventArgs e)
        {
            AllClearAD();
        }

        protected void gvAlbumInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            LoadAlbumInfo();
            gvAlbumInfo.PageIndex = e.NewPageIndex;
            gvAlbumInfo.DataBind();
        }  
        protected void gvAlbumDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            loadAlbumDetails();
            gvAlbumDetails.PageIndex = e.NewPageIndex;
            gvAlbumDetails.DataBind();
        }
    }
}