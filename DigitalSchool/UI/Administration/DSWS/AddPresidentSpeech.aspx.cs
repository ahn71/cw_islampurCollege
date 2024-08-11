using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DS.PropertyEntities.Model.DSWS;
using DS.BLL.DSWS;
using System.IO;
using DS.BLL.ControlPanel;

namespace DS.UI.Administration.DSWS
{
    public partial class AddPresidentSpeech : System.Web.UI.Page
    {
       
        AddPresidentSpeechEntry Entry;
        string imageName = "";
        bool result;
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.InnerText = "";
            if (!IsPostBack) 
            {
                if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "AddPresidentSpeech.aspx", btnSubmit)) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                //url bind->
                aDashboard.HRef = "~/" + Classes.Routing.DashboardRouteUrl;
                aAdministration.HRef = "~/" + Classes.Routing.AdministrationRouteUrl;
                aWebsite.HRef = "~/" + Classes.Routing.WSHomeRouteUrl;
                //url bind-<

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
            AddPresidentEntities entitis = getCotrolValue();
            if (Entry == null)
            {
                Entry = new AddPresidentSpeechEntry();
            }
            Entry.AddEntitis = entitis;
            if(rblType.SelectedValue=="0")
                result=Entry.Insert();
            else result=Entry.InsertPr();
            if (result)
            {
                saveImg();
                lblMessage.InnerText = "success-> Successfully Save";
                allClear();
            }
            LoadData();
        }
        private void Update() 
        {
            AddPresidentEntities entitis = getCotrolValueForUpdate();
            if (Entry == null)
            {
                Entry = new AddPresidentSpeechEntry();
            }
            Entry.AddEntitis = entitis;
             if(rblType.SelectedValue=="0")
                result=Entry.Update();
             else result = Entry.UpdatePr();
            if (result)
            {
                deleteImage(ViewState["__ImgName__"].ToString());
                saveImg();                       
                lblMessage.InnerText = "success-> Successfully Updated";
                allClear();
            }
            LoadData();
        }
        private AddPresidentEntities getCotrolValue() 
        {
            AddPresidentEntities entities = new AddPresidentEntities();
            entities.PresidentName = txtPresidentName.Text;
            entities.Speech = txtSpeech.Text;
            if (FileUpload1.HasFile == true)
            {
                string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
                entities.ImgPath =filename;
            }
            else                      
            entities.ImgPath = "";  
            return entities;
        }
        private AddPresidentEntities getCotrolValueForUpdate()
        {
            AddPresidentEntities entities = new AddPresidentEntities();
            entities.SPId = int.Parse(ViewState["__SPId__"].ToString());
            entities.PresidentName = txtPresidentName.Text;
            entities.Speech = txtSpeech.Text;
            if (FileUpload1.HasFile == true)
            {
                string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
                entities.ImgPath = filename;
            }
            else
                entities.ImgPath = imageName;            
            return entities;
        }
        private void LoadData() 
        {
            if (Entry == null)
                Entry = new AddPresidentSpeechEntry();
            List<AddPresidentEntities> getdata = new List<AddPresidentEntities>();
           if(rblType.SelectedValue=="0")
            getdata = Entry.getEntitiesData();
           else getdata = Entry.getEntitiesDataPr();
            if (getdata != null)
            {
                gvSpeechList.DataSource = getdata;
                gvSpeechList.DataBind();
            }
            else {
                gvSpeechList.DataSource = null;
                gvSpeechList.DataBind();
            }
        }

        protected void gvSpeechList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rIndex = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "change")
            {      
                txtPresidentName.Text = gvSpeechList.Rows[rIndex].Cells[1].Text.ToString();
                txtSpeech.Text = gvSpeechList.Rows[rIndex].Cells[2].Text.ToString();
                ViewState["__SPId__"] = gvSpeechList.DataKeys[rIndex].Values[0].ToString();
                btnSubmit.Text = "Update";
              ViewState["__ImgName__"] = imageName = gvSpeechList.DataKeys[rIndex].Values[1].ToString();
                if (imageName != "") { 
                string url = @"/Images/dsimages/" + Path.GetFileName(imageName);
                imgProfile.ImageUrl = url;
                }     
                
            }
            else if(e.CommandName=="Delete")
            {
                if (Entry == null)
                {
                    Entry = new AddPresidentSpeechEntry();
                }
                if(rblType.SelectedValue=="0")
                    result=Entry.Delete(gvSpeechList.DataKeys[rIndex].Values[0].ToString());
                else
                    result = Entry.DeletePr(gvSpeechList.DataKeys[rIndex].Values[0].ToString());
                if (result)
                {
                    deleteImage(gvSpeechList.DataKeys[rIndex].Values[1].ToString());
                    lblMessage.InnerText = "success->Succsessfully Deleted";
                }
            }
        }

        protected void gvSpeechList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            LoadData();
        }
        private void saveImg()
        {
            try
            {
                //Get Filename from fileupload control
                string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
                //Save images into Images folder

                //System.Drawing.Image image = System.Drawing.Image.FromStream(FileUpload1.PostedFile.InputStream);
                //int width = 65;
                //int height = 51;
                //using (System.Drawing.Image thumbnail = image.GetThumbnailImage(width, height, new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback), IntPtr.Zero))
                //{
                //    using (MemoryStream memoryStream = new MemoryStream())
                //    {
                //        thumbnail.Save(Server.MapPath("/Images/dsimages/"+ filename), System.Drawing.Imaging.ImageFormat.Png);
                //    }
                //}
                FileUpload1.SaveAs(Server.MapPath("/Images/dsimages/" + filename));

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
                //FileUpload1.SaveAs(Server.MapPath("/Images/dsimages/" + filename));
                //if (System.IO.File.Exists(filename))
                //{
                //    System.IO.File.Delete(filename);
                //}
                string path = Server.MapPath("/Images/dsimages/" + filename);
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

        private void allClear() 
        {
            txtPresidentName.Text = "";
            txtSpeech.Text = "";
            btnSubmit.Text = "Save";
            imgProfile.ImageUrl = "/Images/profileImages/noProfileImage.jpg";
        }

        protected void rblType_SelectedIndexChanged(object sender, EventArgs e)
        {
            allClear();
            LoadData();
        }

        protected void gvSpeechList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            LoadData();
            gvSpeechList.PageIndex = e.NewPageIndex;
            gvSpeechList.DataBind();
        }
       
    }
}