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
    public partial class SliderAdd : System.Web.UI.Page
    {
        SliderEntry Entry;
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.InnerText = "";
            if (!IsPostBack)
            {
                if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "SliderAdd.aspx", btnSaveAD)) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                //url bind->
                aDashboard.HRef = "~/" + Classes.Routing.DashboardRouteUrl;
                aAdministration.HRef = "~/" + Classes.Routing.AdministrationRouteUrl;
                aWebsite.HRef = "~/" + Classes.Routing.WSHomeRouteUrl;
                aSlider.HRef = "~/" + Classes.Routing.WSHomeRouteUrl;
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
                string stid = RouteData.Values["id"].ToString();
                int SL = int.Parse(commonTask.Base64Decode(stid));
                ViewState["__SL__"] = SL.ToString();               
                List<WSSlider> list = (List<WSSlider>)Session["__Sliderlist__"];

                WSSlider slider = list.Find(s => s.SL == SL);
                imgProfile.ImageUrl = slider.Location;               
                txtOrdering.Text = slider.Ordering.ToString();
                chkChosen.Checked = slider.Chosen;
                ViewState["__imgPath__"] = slider.Location;

                btnSaveAD.Text = "Update";
                btnClearAD.Visible = false;



            }
            catch (Exception ex) { }
        }
        private void saveOld()
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
            
        }
        private void save()
        {
            WSSlider entitis = getCotrolValue_orm();
            if (Entry == null)
            {
                Entry = new SliderEntry();
            }
            Entry.SetSlider = entitis;
            if (Entry.save())
            {
                saveImg();
                Response.Redirect("~/" + Routing.SliderListRouteUrl);
            }

        }
        private void Update()
        {
            WSSlider entitis = getCotrolValue_orm();
            if (Entry == null)
            {
                Entry = new SliderEntry();
            }
            Entry.SetSlider = entitis;

            if (Entry.update())
            {
                if (FileUpload1.HasFile == true)
                {
                    deleteImage(ViewState["__imgPath__"].ToString());
                    saveImg();
                }
                Response.Redirect("~/"+Routing.SliderListRouteUrl);
            }
           
        }
        private void UpdateOld()
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
        private WSSlider getCotrolValue_orm()
        {
            WSSlider slider = new WSSlider {
                SL = (btnSaveAD.Text == "Update") ? int.Parse(ViewState["__SL__"].ToString()) : 0,
                Ordering = int.Parse(txtOrdering.Text),
                Chosen = chkChosen.Checked,
                Location = (FileUpload1.HasFile) ? "~/Images/dsimages/slider/" + Path.GetFileName(FileUpload1.PostedFile.FileName) : (btnSaveAD.Text == "Save") ? "" : ViewState["__imgPath__"].ToString()
            };           
            
            return slider;
        }


        private void AllClear()
        {
            txtOrdering.Text = "";
            imgProfile.ImageUrl = "/Images/dsimages/blank-frame.png";
            chkChosen.Checked = true;            
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

       
        private void saveImg()
        {
            try
            {
                
                string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);                
                FileUpload1.SaveAs(Server.MapPath("/Images/dsimages/slider/" + filename));

            }
            catch { }
        }
        private void deleteImage(string filename)
        {
            try
            {
               
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

        

       
    }
}