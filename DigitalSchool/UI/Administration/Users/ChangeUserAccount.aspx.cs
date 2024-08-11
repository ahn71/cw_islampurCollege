using DS.BLL.ControlPanel;
using DS.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DS.PropertyEntities.Model.ControlPanel;

namespace DS.UI.Administration.Users
{
    public partial class ChangeUserAccount : System.Web.UI.Page
    {
        UserAccountEntry uae;
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.InnerText = "";
            if (!IsPostBack)
            {
                try
                {
                    if (Request.QueryString["hasperm"].ToString() != null) lblMessage.InnerText = "warning->You need permission to view user account list.";
                }
                catch { }
                if (!PrivilegeOperation.SetPrivilegeControl(Session["__UserTypeId__"].ToString(), "ChangeUserAccount.aspx")) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                UserTypeInfoEntry.GetUserTypeList_inDropdownList(ddlUserTypeList);
                loadUserAccountInfo();
            }
        }
        private void loadUserAccountInfo()
        {
            string getUserId = Request.QueryString["Id"].ToString();
            ViewState["__GetUserId__"] = getUserId;
            DataTable dt = CRUD.ReturnTableNull("select * from v_UserAccount where UserId=" + getUserId + "");

            txtFirstName.Text = dt.Rows[0]["FirstName"].ToString();
            txtLastName.Text = dt.Rows[0]["LastName"].ToString();
            txtEmail.Text = dt.Rows[0]["Email"].ToString();
            txtConfirmEmail.Text = dt.Rows[0]["Email"].ToString();
            txtUserName.Text =DS.DAL.ComplexScripting.ComplexLetters.getEntangledLetters(dt.Rows[0]["UserName"].ToString());
            txtOldPassword.Text = DS.DAL.ComplexScripting.ComplexLetters.getEntangledLetters(dt.Rows[0]["UserPassword"].ToString());


         //   rblAccountStatus.SelectedIndex = (bool.Parse(dt.Rows[0]["Status"].ToString()).Equals(true)) ? 0 : 1;
            if (bool.Parse(dt.Rows[0]["Status"].ToString()).Equals(true))
            {
                rblAccountStatus.SelectedIndex = 0;
                btnActivation.Text = "Inactive";
                btnActivation.BackColor = System.Drawing.Color.Red;
            }
            else
            {
                rblAccountStatus.SelectedIndex = 1;
                btnActivation.Text ="Active";
                btnActivation.BackColor = System.Drawing.Color.Green;
            }
            
            ddlUserTypeList.SelectedValue = dt.Rows[0]["UserTypeId"].ToString();
            
            //for (byte b = 0; b <chkUserTypeList.Items.Count; b++)
            //{
            //    for (byte c = 0; c < dt.Rows.Count; c++)
            //    {
            //        if (chkUserTypeList.Items[b].Value.ToString().Equals(dt.Rows[c]["UserTypeId"].ToString()))
            //        {
            //            chkUserTypeList.Items[b].Selected = true;
            //            dt.Rows.RemoveAt(c); break;
            //        }
            //    }
            //}
            
        }

        private UserAccount GetData()
        {
            try
            {
                UserAccount getUAList = new UserAccount();
                getUAList.FirstName = txtFirstName.Text.Trim();
                getUAList.LastName = txtLastName.Text.Trim();
                getUAList.Email = txtEmail.Text.Trim();
                getUAList.UserName = ComplexScriptingSystem.ComplexLetters.getTangledLetters(txtUserName.Text.Trim());
                getUAList.UserPassword = ComplexScriptingSystem.ComplexLetters.getTangledLetters(txtNewPassword.Text.Trim());
                getUAList.Status = (rblAccountStatus.SelectedItem.Value.ToString() == "1") ? true : false;
                getUAList.ModifiedOn = DateTime.Now;
                getUAList.ModifiedBy = int.Parse(Session["__UserId__"].ToString());
                getUAList.UserTypeId =int.Parse(ddlUserTypeList.SelectedValue.ToString());
                return getUAList;
            }
            catch { return null; }
        }

        protected void btnChangeUserAccount_Click(object sender, EventArgs e)
        {
            if (!InputValidationBasket()) return;
            using (UserAccount ua = GetData())
            {
                if (uae == null) uae = new UserAccountEntry();
                uae.SetValues = ua;
                if (uae.Update(ViewState["__GetUserId__"].ToString()))
                {
                    clearText();
                    lblMessage.InnerText = "success->Successfully Changed User Account !";

                    //if (uae.UpdateAccountDetails(chkUserTypeList, ViewState["__GetUserId__"].ToString()))
                    //{
                       
                    //}
                
                }
            }
        }

        private bool InputValidationBasket()
        {
            try
            {
                if (txtFirstName.Text.Trim().Length < 3)
                {
                    lblMessage.InnerText = "warning->Please type first name.";
                    txtFirstName.Focus();
                    return false;
                }
                else if (txtLastName.Text.Trim().Length < 3)
                {
                    lblMessage.InnerText = "warning->Please type last name.";
                    txtLastName.Focus();
                    return false;
                }
                else if (txtEmail.Text.Trim().Length <= 5)
                {
                    lblMessage.InnerText = "warning->Please type valid email address.";
                    txtEmail.Focus();
                    return false;
                }
                else if (txtEmail.Text.Trim() != txtConfirmEmail.Text.Trim())
                {
                    lblMessage.InnerText = "warning->Confirm email address does not match with above email address";
                    txtConfirmEmail.Focus();
                    return false;
                }
                else if (txtUserName.Text.Trim().Length < 3)
                {
                    lblMessage.InnerText = "warning->User name required minimum 3 letters.";
                    txtUserName.Focus();
                    return false;
                }

                else if (txtNewPassword.Text.Trim().Length < 3)
                {
                    lblMessage.InnerText = "warning->User name required minimum 3 letters.";
                    txtNewPassword.Focus();
                    return false;
                }

                else if (txtNewPassword.Text.Trim() != txtConfirmPassword.Text.Trim())
                {
                    lblMessage.InnerText = "warning->Confirm password does not matcha with above password.";
                    txtConfirmPassword.Focus();
                    return false;
                }
                else if (ddlUserTypeList.SelectedIndex == 0)
                {
                    lblMessage.InnerText = "warning->Minimum one type of user are required.";
                    ddlUserTypeList.Focus();
                    return false;
                }
                return true;
            }
            catch { return false; }
        }

        private void clearText()
        {
            try
            {
                txtFirstName.Text = "";
                txtEmail.Text = "";
                txtLastName.Text = "";
                txtUserName.Text = "";
                txtConfirmEmail.Text = "";
                txtNewPassword.Text = "";
                txtConfirmPassword.Text = "";
                rblAccountStatus.SelectedIndex = 0;
                ddlUserTypeList.SelectedIndex = 0;

            }
            catch { }
        }

        protected void btnActivation_Click(object sender, EventArgs e)
        {
            string status = (btnActivation.Text == "Inactive") ? "0" : "1";
            if (uae == null) uae = new UserAccountEntry();           
            if (uae.UpdateStatus(ViewState["__GetUserId__"].ToString(), status))
            {
                
                lblMessage.InnerText = "success->Successfully Changed Status !";
                Response.Redirect("/UI/Administration/Users/UserAccountList.aspx");
            }
        }
    }
}