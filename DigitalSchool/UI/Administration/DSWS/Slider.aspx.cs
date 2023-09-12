using DS.BLL.ControlPanel;
using DS.BLL.DSWS;
using DS.Classes;
using DS.DAL;
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
    public partial class Slider : System.Web.UI.Page
    {
        SliderEntry Entry;
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.InnerText = "";
            if (!IsPostBack)
            {
                if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "Slider.aspx", btnSaveAD)) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                //url bind->
                aDashboard.HRef = "~/" + Classes.Routing.DashboardRouteUrl;
                aAdministration.HRef = "~/" + Classes.Routing.AdministrationRouteUrl;
                aWebsite.HRef = "~/" + Classes.Routing.WSHomeRouteUrl;
                //url bind-<

                LoadData();

            }
        }
        private void save()
        {
            SliderEntities entitis = getCotrolValue();
            if (Entry == null)
            {
                Entry = new SliderEntry();
            }
            Entry.SetEntities = entitis;
            if (Entry.Insert())
            {
                saveImg();
                lblMessage.InnerText = "success->Successfully Save.";
                AllClear();
            }
            LoadData();
        }
        private void Update()
        {
            SliderEntities entitis = getCotrolValue();
            if (Entry == null)
            {
                Entry = new SliderEntry();
            }
            Entry.SetEntities = entitis;

            if (Entry.Update())
            {
                if (FileUpload1.HasFile == true)
                {
                    deleteImage(ViewState["__imgPath__"].ToString());
                    saveImg();
                }
                lblMessage.InnerText = "success->Successfully Update.";
                AllClear();
            }
            LoadData();
        }
        private SliderEntities getCotrolValue()
        {
            SliderEntities entities = new SliderEntities();
            if (btnSaveAD.Text == "Update")
                entities.SSL = int.Parse(ViewState["__SL__"].ToString());
            entities.Ordering = int.Parse(txtOrdering.Text);
            entities.Chosen = (chkChosen.Checked == true) ? true : false;
            if (FileUpload1.HasFile == true)
            {
                string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
                entities.Location = "~/Images/dsimages/slider/" + filename;
            }
            else
            {
                if (btnSaveAD.Text == "Save")
                    entities.Location = "";
                else entities.Location = ViewState["__imgPath__"].ToString();
            }
            return entities;
        }
       
        private void LoadData()
        {
            if (Entry == null)
                Entry = new SliderEntry();
            List<WSSlider> getdata = new List<WSSlider>();
            getdata = Entry.list();
            Session["__Sliderlist__"] = getdata;
            if (getdata != null)
            {
                gvSlider.DataSource = getdata;
                gvSlider.DataBind();
            }
            else
            {
                gvSlider.DataSource = null;
                gvSlider.DataBind();
            }
        }
        private void LoadDataOld()
        {
            if (Entry == null)
                Entry = new SliderEntry();
            List<SliderEntities> getdata = new List<SliderEntities>();
            getdata = Entry.getEntitiesData();
            Session["__Sliderlist__"] = getdata;
            if (getdata != null)
            {
                gvSlider.DataSource = getdata;
                gvSlider.DataBind();
            }
            else
            {
                gvSlider.DataSource = null;
                gvSlider.DataBind();
            }
        }
        private void AllClear()
        {
            txtOrdering.Text = "";           
            imgProfile.ImageUrl = "/Images/dsimages/blank-frame.png";
            chkChosen.Checked = false;
            btnSaveAD.Text = "Save";
        }

        protected void btnSaveAD_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile == true && Path.GetFileName(FileUpload1.PostedFile.FileName).Length > 20)
            {
                lblMessage.InnerText = "warning->Image Name Maximum 20 Character!";
                return;
            }
            if (btnSaveAD.Text == "Save")
                save();
            else Update();
        }

        protected void gvSlider_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rIndex = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "change")
            {
                int sl = int.Parse(gvSlider.DataKeys[rIndex].Values[0].ToString());
                string _sl = commonTask.Base64Encode(sl.ToString());
                Response.Redirect(GetRouteUrl("SliderEditRoute", new { id = _sl }));

                imgProfile.ImageUrl = gvSlider.DataKeys[rIndex].Values[1].ToString();
                ViewState["__SL__"] = gvSlider.DataKeys[rIndex].Values[0].ToString();
                txtOrdering.Text = gvSlider.Rows[rIndex].Cells[2].Text;
                if (gvSlider.DataKeys[rIndex].Values[2].ToString().Equals("True"))
                    chkChosen.Checked = true;
                else
                    chkChosen.Checked = false;
                btnSaveAD.Text = "Update";
                ViewState["__imgPath__"] = gvSlider.DataKeys[rIndex].Values[1].ToString();
            }
            else if (e.CommandName == "Delete")
            {
                if (Entry == null)
                    Entry = new SliderEntry();
                string a = gvSlider.DataKeys[rIndex].Values[0].ToString();
                //if (Entry.Delete(gvSlider.DataKeys[rIndex].Values[0].ToString()))
                if (Entry.delete(int.Parse( gvSlider.DataKeys[rIndex].Values[0].ToString())))
                {                  
                    deleteImage(gvSlider.DataKeys[rIndex].Values[1].ToString());
                    lblMessage.InnerText = "success-> Successfully Deleted";
                    gvSlider.Rows[rIndex].Visible = false;
                }
            }
        }
        private void saveImg()
        {
            try
            {
                //Get Filename from fileupload control
                string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
                //Save images into Images folder
                //System.Drawing.Image image = System.Drawing.Image.FromStream(FileUpload1.PostedFile.InputStream);
                //int width = 1034;
                //int height = 576;
                //using (System.Drawing.Image thumbnail = image.GetThumbnailImage(width, height, new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback), IntPtr.Zero))
                //{
                //    using (MemoryStream memoryStream = new MemoryStream())
                //    {
                //        thumbnail.Save(Server.MapPath("/Images/dsimages/slider/" + filename), System.Drawing.Imaging.ImageFormat.Png);
                //    }
                //}
                FileUpload1.SaveAs(Server.MapPath("/Images/dsimages/slider/" + filename));

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

        protected void gvSlider_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            LoadData();
        }

        protected void gvSlider_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            LoadData();
            gvSlider.PageIndex = e.NewPageIndex;
            gvSlider.DataBind();
        }
    }
}