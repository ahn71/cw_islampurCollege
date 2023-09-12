using DS.BLL.Admission;
using DS.BLL.ControlPanel;
using DS.BLL.ManageUser;
using DS.DAL.ComplexScripting;
using DS.PropertyEntities.Model.ManageUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.Administration.Users
{
    public partial class StudentAccount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.InnerText = "";
            if(!IsPostBack)
            {
                try
                {
                    if (Request.QueryString["hasperm"].ToString() != null) lblMessage.InnerText = "warning->You need permission to view student's account list.";
                }
                catch { }
                if (!PrivilegeOperation.SetPrivilegeControl(Session["__UserTypeId__"].ToString(), "StudentAccount.aspx")) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                AdmStdInfoEntry.GetAdmissionNo(ddlAdmissionNo);
            }
        }      

        protected void btnSaveUserAccount_Click(object sender, EventArgs e)
        {
            if(txtUserPassword.Text.Length<4)
            {
                lblMessage.InnerText = "warning->Password Must be Minimum 4 Character";
                return;
            }
            using (StudentUserAccountEntities entities = GetFormData())
            {
                StudentUserAccountEntry StdUserAccountEntry = new StudentUserAccountEntry();
                StdUserAccountEntry.AddEntities = entities;
                if (StdUserAccountEntry.Insert() == true)
                {
                    lblMessage.InnerText = "success->Successfully Student Account Created";
                }
                else
                {
                    lblMessage.InnerText = "error->Unable to Save";
                }
            }
        }
        private StudentUserAccountEntities GetFormData()
        {
            StudentUserAccountEntities trEntry = new StudentUserAccountEntities();
            trEntry.StudentId = int.Parse(ddlAdmissionNo.SelectedValue);
            string[] username = ddlAdmissionNo.SelectedItem.Text.Split('_');
            trEntry.UserName = ComplexLetters.getTangledLetters(username[0]);
            trEntry.UserPassword = ComplexLetters.getTangledLetters(txtUserPassword.Text);
            trEntry.CreatedBy = int.Parse(Session["__UserId__"].ToString());
            trEntry.CreatedOn = convertDateTime.getCertainCulture(DateTime.Now.ToString("dd-MM-yyyy"));
            if (rblAccountStatus.SelectedValue == "1")
                trEntry.Status = true;
            else
                trEntry.Status = false;
            return trEntry;
        }
    }
}