using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DS.BLL.ControlPanel;
using DS.DAL.AdviitDAL;
using DS.PropertyEntities.Model.ControlPanel;
using DS.DAL;

namespace DS.UI.Administration.Users
{
    public partial class UserRegister : System.Web.UI.Page
    {
        UserAccountEntry uae;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (Request.QueryString["hasperm"].ToString() != null) lblMessage.InnerText = "warning->You need permission to view user account list.";
                }
                catch { }
                if (!PrivilegeOperation.SetPrivilegeControl(Session["__UserTypeId__"].ToString(), "UserRegister.aspx")) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                UserTypeInfoEntry.GetUserTypeList_inDropdownList(ddlUserTypeList);
            }
        }      

        private UserAccount GetData()
        {
            try
            {
                UserAccount getUAList=new UserAccount ();
                getUAList.FirstName = txtFirstName.Text.Trim();
                getUAList.LastName = txtLastName.Text.Trim();
                getUAList.Email = txtEmail.Text.Trim();
                getUAList.UserName = ComplexScriptingSystem.ComplexLetters.getTangledLetters(txtUserName.Text.Trim());
                getUAList.UserPassword = ComplexScriptingSystem.ComplexLetters.getTangledLetters(txtUserPassword.Text.Trim());
                getUAList.Status = (rblAccountStatus.SelectedItem.Value.ToString() == "1") ? true : false;
                getUAList.CreatedOn = DateTime.Now;
                getUAList.CreatedBy = int.Parse(Session["__UserId__"].ToString());
                getUAList.ModifiedOn = DateTime.Now;
                getUAList.ModifiedBy = int.Parse(Session["__UserId__"].ToString());
                getUAList.UserTypeId = int.Parse(ddlUserTypeList.SelectedItem.Value);
                if (ViewState["EID"] == null)
                {
                    getUAList.EID = 0;
                }
                else
                {
                    getUAList.EID = int.Parse(ViewState["EID"].ToString());
                }
                if (rdbAdviser.SelectedValue == "1")
                {
                    getUAList.IsAdviser = true;
                }
                else
                {
                    getUAList.IsAdviser = false;
                }
                return getUAList;
            }
            catch { return null; }
        }

        private bool InputValidationBasket()
        {
            try
            {
                if (rdbAdviser.SelectedValue == "1")
                {
                    if (txtCardNo.Text == "")
                    {
                        lblMessage.InnerText = "warning->Please Type Adviser Card No";
                        return false;
                    }
                }
                if (txtFirstName.Text.Trim().Length <3)
                {
                    lblMessage.InnerText = "warning->Please type first name.";
                    txtFirstName.Focus();
                    return false;
                }
                else if (txtLastName.Text.Trim().Length <3)
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

                else if (txtUserPassword.Text.Trim().Length < 3)
                {
                    lblMessage.InnerText = "warning->User name required minimum 3 letters.";
                    txtUserPassword.Focus();
                    return false;
                }

                else if (txtUserPassword.Text.Trim() != txtConfirmPassword.Text.Trim())
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
                rblAccountStatus.SelectedIndex = 0;
                ddlUserTypeList.SelectedIndex = 0;
                txtCardNo.Text = "";

            }
            catch { }
        }

        protected void btnSaveUserAccount_Click(object sender, EventArgs e)
        {
            if (!InputValidationBasket()) return;
            
            using (UserAccount ua = GetData())
            {
                if (uae == null) uae = new UserAccountEntry();
                uae.SetValues = ua;
                if (uae.Insert())
                {
                    clearText();
                    lblMessage.InnerText = "success->Successfully created";  

                    //if (uae.InsertDetails(ddlUserTypeList))
                    //{
                                   
                    //    Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('SetUserPrivilege.aspx','_newtab');", true);
                    //}
                }
                else lblMessage.InnerText = "error->Unable to create";
                
            }
            
        }

        protected void rdbAdviser_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdbAdviser.SelectedValue == "1")
            {
                txtCardNo.Visible = true;
                btnFind.Visible = true;
            }
            else
            {
                txtCardNo.Visible = false;
                btnFind.Visible = false;
            }
        }

        protected void btnFind_Click(object sender, EventArgs e)
        {
            DataTable dt = CRUD.ReturnTableNull("SELECT EID,EName,EEmail FROM EmployeeInfo WHERE ECardNo='"+txtCardNo.Text+"'");
            if (dt.Rows.Count > 0)
            {
                ViewState["EID"] = dt.Rows[0]["EID"].ToString();
                txtFirstName.Text = dt.Rows[0]["EName"].ToString();
                txtEmail.Text = dt.Rows[0]["EEmail"].ToString();
                txtConfirmEmail.Text = dt.Rows[0]["EEmail"].ToString();
            }
            else
            {
                lblMessage.InnerText = "warning->Please Type Valid Card No";
                txtCardNo.Focus();
            }
        }
    }
}