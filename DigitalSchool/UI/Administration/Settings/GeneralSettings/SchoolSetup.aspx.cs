using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DS.DAL.AdviitDAL;
using DS.BLL.ControlPanel;
using DS.DAL;

namespace DS.UI.Administration.Settings.GeneralSettings
{
    public partial class SchoolSetup : System.Web.UI.Page
    {
        DataTable dt;
        static string imageName;
        string msg = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
                if (!IsPostBack)
                {
                    if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "SchoolSetup.aspx", btnSave)) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");                   
                    loadSchoolInfo();
                    if ((btnSave.Text == "Update" && Session["__Update__"].ToString().Equals("false"))||(btnSave.Text == "Save" && Session["__Save__"].ToString().Equals("false"))) btnSave.Enabled = false;                   
                }
        }
        private void loadSchoolInfo()
        {
            try
            {
                msg = sqlDB.fillDataTable("Select * From School_Setup where " +
                    " DSId=(Select Max(DSId) as DSId From School_Setup)", dt = new DataTable());
                if (msg == string.Empty)
                {
                    if (dt.Rows.Count > 0)
                    {
                        btnSave.Text = "Update";
                        txtSchoolName.Text = dt.Rows[0]["SchoolName"].ToString();
                        txtAddress.Text = dt.Rows[0]["Address"].ToString();
                        txtCountry.Text = dt.Rows[0]["Country"].ToString();
                        txtTelephone.Text = dt.Rows[0]["Telephone"].ToString();
                        txtFax.Text = dt.Rows[0]["Fax"].ToString();
                        txtRegistrationNo.Text = dt.Rows[0]["RegistrationNo"].ToString();
                        txtEmail.Text = dt.Rows[0]["Email"].ToString();
                        ckbIsOnline.Checked =bool.Parse(dt.Rows[0]["IsOnline"].ToString());
                        // txtEmailPassword.Text=adviitSecurity.crypto(dt.Rows[0]["EmailPassword"].ToString(),false);
                        txtEmailPassword.Attributes.Add("value", adviitSecurity.crypto(dt.Rows[0]["EmailPassword"].ToString(), false));
                        imageName = dt.Rows[0]["LogoName"].ToString();
                        string url = @"/Images/Logo/" + Path.GetFileName(dt.Rows[0]["LogoName"].ToString());
                        imgProfile.ImageUrl = url;                        
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
                string[] getColumns = { "SchoolName", "Address", "Country", "Telephone", "Fax", "RegistrationNo", "Email", "EmailPassword", "LogoName","IsOnline" };
                string[] getValues = { txtSchoolName.Text.Trim(), txtAddress.Text, txtCountry.Text.Trim(), 
                                            txtTelephone.Text.Trim(), txtFax.Text.Trim(), txtRegistrationNo.Text.Trim(), 
                                            txtEmail.Text.Trim(), adviitSecurity.crypto(txtEmailPassword.Text.Trim(), true), LogoName,ckbIsOnline.Checked.ToString() };
                if (ComplexScriptingSystem.SQLOperation.forSaveValue("School_Setup", getColumns, getValues, DbConnection.Connection) == true)
                {
                    saveImg();
                    lblMessage.InnerText = "success->Save Successfully";
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
                    string[] getColumns = { "SchoolName", "Address", "Country", "Telephone", "Fax", "RegistrationNo", "Email", "EmailPassword", "LogoName", "IsOnline" };
                    string getIdentifierValue = lblDSId.Value.ToString();
                    string[] getValues = { txtSchoolName.Text.Trim(), txtAddress.Text, txtCountry.Text.Trim(), txtTelephone.Text.Trim(), 
                                             txtFax.Text.Trim(), txtRegistrationNo.Text.Trim(), txtEmail.Text.Trim(), 
                                             adviitSecurity.crypto(txtEmailPassword.Text.Trim(), true), LogoName,ckbIsOnline.Checked.ToString() };
                    if (ComplexScriptingSystem.SQLOperation.forUpdateValue("School_Setup", getColumns, getValues, "DSId", getIdentifierValue, DbConnection.Connection) == true)
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
                        lblMessage.InnerText = "success->Update Successfully";
                    }
                }
                else
                {
                    string[] getColumns = { "SchoolName", "Address", "Country", "Telephone", "Fax", "RegistrationNo", "Email", "EmailPassword", "IsOnline" };
                    string getIdentifierValue = lblDSId.Value.ToString();
                    string[] getValues = { txtSchoolName.Text.Trim(), txtAddress.Text, txtCountry.Text.Trim(), txtTelephone.Text.Trim(), txtFax.Text.Trim(), txtRegistrationNo.Text.Trim(), txtEmail.Text.Trim(), adviitSecurity.crypto(txtEmailPassword.Text.Trim(), true),ckbIsOnline.Checked.ToString() };
                    if (ComplexScriptingSystem.SQLOperation.forUpdateValue("School_Setup", getColumns, getValues, "DSId", getIdentifierValue, DbConnection.Connection) == true)
                    {
                        loadSchoolInfo();
                        lblMessage.InnerText = "success->Update Successfully";
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
                        thumbnail.Save(Server.MapPath("/Images/Logo/" + filename), System.Drawing.Imaging.ImageFormat.Png);
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