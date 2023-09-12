using ComplexScriptingSystem;
using DS.BLL.Admission;
using DS.BLL.ControlPanel;
using DS.BLL.ManageUser;
using DS.DAL;
using DS.PropertyEntities.Model.ManageUser;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.Administration.Users
{
    public partial class ChangeStudentAccount : System.Web.UI.Page
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
                if (!PrivilegeOperation.SetPrivilegeControl(Session["__UserTypeId__"].ToString(), "ChangeStudentAccount.aspx")) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                AdmStdInfoEntry.GetAdmissionNo(ddlAdmissionNo);
                LoadStudentAccount();
            }
        }
        private void LoadStudentAccount()
        {
            string getstudentAccID = Request.QueryString["Id"].ToString();
            ViewState["__GetUserId__"] = getstudentAccID;
            DataTable dt = CRUD.ReturnTableNull("select * from v_UserAccount_Student where StudentAccId=" + getstudentAccID + "");
            ddlAdmissionNo.SelectedValue= dt.Rows[0]["StudentId"].ToString();
            txtOldPassword.Text = DS.DAL.ComplexScripting.ComplexLetters.getEntangledLetters(dt.Rows[0]["UserPassword"].ToString());            
            if (dt.Rows[0]["Status"].ToString() == "True")
            {
                rblAccountStatus.SelectedValue = "1";
            }
            else
            {
                rblAccountStatus.SelectedValue = "0";
            }

        }
        private StudentUserAccountEntities GetFormData()
        {
            StudentUserAccountEntities trEntry = new StudentUserAccountEntities();
            trEntry.StudentAccId = int.Parse(ViewState["__GetUserId__"].ToString());
            trEntry.StudentId = int.Parse(ddlAdmissionNo.SelectedValue);           
            trEntry.UserPassword = ComplexLetters.getTangledLetters(txtNewPassword.Text);
            trEntry.CreatedBy = int.Parse(Session["__UserId__"].ToString());
            trEntry.CreatedOn = convertDateTime.getCertainCulture(DateTime.Now.ToString("dd-MM-yyyy"));
            if (rblAccountStatus.SelectedValue == "1")
                trEntry.Status = true;
            else
                trEntry.Status = false;
            return trEntry;
        }

        protected void btnSaveUserAccount_Click(object sender, EventArgs e)
        {
            if (txtNewPassword.Text.Length < 4)
            {
                lblMessage.InnerText = "warning->Password Must be Minimum 4 Character";
                return;
            }
            using (StudentUserAccountEntities entities = GetFormData())
            {
                StudentUserAccountEntry StdUserAccountEntry = new StudentUserAccountEntry();
                StdUserAccountEntry.AddEntities = entities;
                if (StdUserAccountEntry.Update() == true)
                {
                    lblMessage.InnerText = "success->Successfully Student Account Change";
                    Response.Redirect("/UI/Administration/Users/StudentAccountList.aspx");
                }
                else
                {
                    lblMessage.InnerText = "error->Unable to Save";
                }
            }
        }
    }
}