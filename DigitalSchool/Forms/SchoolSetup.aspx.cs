using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using DS.DAL.AdviitDAL;

namespace DS.Forms
{
    public partial class SchoolSetup : System.Web.UI.Page
    {
        DataTable dt;
        static string imageName;
        string msg = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["__UserId__"] == null)
            {
                Response.Redirect("~/UserLogin.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    loadSchoolInfo();
                }
            }            
        }
        private void loadSchoolInfo()
        {
            try
            {
                msg = sqlDB.fillDataTable("Select * From School_Setup where " +
                    " DSId=(Select Max(DSId) as DSId From School_Setup)", dt = new DataTable());
                if(msg == string.Empty)
                {
                    if (dt.Rows.Count > 0)
                    {
                        txtSchoolName.Text = dt.Rows[0]["SchoolName"].ToString();
                        txtAddress.Text = dt.Rows[0]["Address"].ToString();
                        txtCountry.Text = dt.Rows[0]["Country"].ToString();
                        txtTelephone.Text = dt.Rows[0]["Telephone"].ToString();
                        txtFax.Text = dt.Rows[0]["Fax"].ToString();
                        txtRegistrationNo.Text = dt.Rows[0]["RegistrationNo"].ToString();
                        txtEmail.Text = dt.Rows[0]["Email"].ToString();
                        // txtEmailPassword.Text=adviitSecurity.crypto(dt.Rows[0]["EmailPassword"].ToString(),false);
                        txtEmailPassword.Attributes.Add("value", adviitSecurity.crypto(dt.Rows[0]["EmailPassword"].ToString(), false));
                        imageName = dt.Rows[0]["LogoName"].ToString();
                        string url = @"/Images/Logo/" + Path.GetFileName(dt.Rows[0]["LogoName"].ToString());
                        imgProfile.ImageUrl = url;
                        btnSave.Text = "Update";
                        lblDSId.Value = dt.Rows[0]["DSId"].ToString();
                        txtEmailPassword.TextMode = TextBoxMode.Password;
                        return;
                    }                    
                }
                lblMessage.InnerText = "error->" + msg;
                
            }
            catch { }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (lblDSId.Value == "0")
            {
                saveSchoolSetup();
            }
            else UpdateSchoolSetup();            
        }
        private void saveSchoolSetup()
        {
            try
            {
                string LogoName = "";
                LogoName = FileUpload1.FileName;
                string[] getColumns = { "SchoolName", "Address", "Country", "Telephone", "Fax", "RegistrationNo", "Email","EmailPassword", "LogoName" };
                string[] getValues = { txtSchoolName.Text.Trim(), txtAddress.Text, txtCountry.Text.Trim(), 
                                            txtTelephone.Text.Trim(), txtFax.Text.Trim(), txtRegistrationNo.Text.Trim(), 
                                            txtEmail.Text.Trim(), adviitSecurity.crypto(txtEmailPassword.Text.Trim(), true), LogoName };
                if (ComplexScriptingSystem.SQLOperation.forSaveValue("School_Setup", getColumns, getValues, sqlDB.connection) == true)
                {
                    saveImg();
                    lblMessage.InnerText = "success->Successfully save";
                }                
            }
            catch { }
        }
        private void UpdateSchoolSetup()
        {
            try
            {
                string LogoName = "";
                if (FileUpload1.HasFile == true)
                {
                    LogoName = FileUpload1.FileName;
                    string[] getColumns = { "SchoolName", "Address", "Country", "Telephone", "Fax", "RegistrationNo", "Email", "EmailPassword", "LogoName" };
                    string getIdentifierValue = lblDSId.Value.ToString();
                    string[] getValues = { txtSchoolName.Text.Trim(), txtAddress.Text, txtCountry.Text.Trim(), txtTelephone.Text.Trim(), 
                                             txtFax.Text.Trim(), txtRegistrationNo.Text.Trim(), txtEmail.Text.Trim(), 
                                             adviitSecurity.crypto(txtEmailPassword.Text.Trim(), true), LogoName };
                    if (ComplexScriptingSystem.SQLOperation.forUpdateValue("School_Setup", getColumns, getValues, "DSId", getIdentifierValue, sqlDB.connection) == true)
                    {
                        if (imageName != "")
                        {
                            System.IO.File.Delete(Request.PhysicalApplicationPath + "/Images/Logo/" + imageName);
                        }
                        string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);

                        System.Drawing.Image image = System.Drawing.Image.FromStream(FileUpload1.PostedFile.InputStream);
                        int width = 100;
                        int height = 100;
                        using (System.Drawing.Image thumbnail = image.GetThumbnailImage(width, height, new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback), IntPtr.Zero))
                        {
                            using (MemoryStream memoryStream = new MemoryStream())
                            {
                                thumbnail.Save(Server.MapPath("/Images/Logo/" + filename), System.Drawing.Imaging.ImageFormat.Png);
                            }
                        }
                        loadSchoolInfo(); 
                        lblMessage.InnerText = "success->Successfully save";
                    }
                }
                else
                {
                    string[] getColumns = { "SchoolName", "Address", "Country", "Telephone", "Fax", "RegistrationNo", "Email", "EmailPassword" };
                    string getIdentifierValue = lblDSId.Value.ToString();
                    string[] getValues = { txtSchoolName.Text.Trim(), txtAddress.Text, txtCountry.Text.Trim(), txtTelephone.Text.Trim(), txtFax.Text.Trim(), txtRegistrationNo.Text.Trim(), txtEmail.Text.Trim(), adviitSecurity.crypto(txtEmailPassword.Text.Trim(), true)};
                    if (ComplexScriptingSystem.SQLOperation.forUpdateValue("School_Setup", getColumns, getValues, "DSId", getIdentifierValue, sqlDB.connection) == true)
                    {
                        loadSchoolInfo();
                        lblMessage.InnerText = "success->Successfully save";
                    }
                }
               
            }
            catch { }
        }
        private void saveImg()
        {
            try
            {
                string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);

                System.Drawing.Image image = System.Drawing.Image.FromStream(FileUpload1.PostedFile.InputStream);
                int width = 100;
                int height = 100;
                using (System.Drawing.Image thumbnail = image.GetThumbnailImage(width, height, new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback), IntPtr.Zero))
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        thumbnail.Save(Server.MapPath("/Images/Logo/"+ filename), System.Drawing.Imaging.ImageFormat.Png);
                    }
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