using DS.BLL.ControlPanel;
using DS.PropertyEntities.Model.ControlPanel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.Adviser
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        DataTable dt;
        UserAccountEntry uae;
        protected void Page_Load(object sender, EventArgs e)
        {
                if (!IsPostBack)
                {
                    LoadAdviserAccount();
                }
        }
        private void LoadAdviserAccount()
        {
            try
            {
                dt = UserAccountEntry.GetAdviserUserAccount(Session["__EID__"].ToString());
                if (dt.Rows.Count > 0)
                {
                    txtUserName.Text = DS.DAL.ComplexScripting.ComplexLetters.getEntangledLetters(dt.Rows[0]["UserName"].ToString());
                    txtOldPassword.Text = DS.DAL.ComplexScripting.ComplexLetters.getEntangledLetters(dt.Rows[0]["UserPassword"].ToString());
                }
            }
            catch { }
        }

        protected void btnChangeUserAccount_Click(object sender, EventArgs e)
        {
            using (UserAccount ua = GetData())
            {
                if (uae == null) uae = new UserAccountEntry();
                uae.SetValues = ua;
                if (uae.UpdatePassword(Session["__EID__"].ToString()))
                {                   
                    lblMessage.InnerText = "success->Successfully Change Your Password !";
                    txtNewPassword.Text = "";
                    txtConfirmPassword.Text = "";
                    LoadAdviserAccount();
                }
            }
        }
        private UserAccount GetData()
        {
            try
            {
                UserAccount getUAList = new UserAccount();   
                getUAList.UserPassword = ComplexScriptingSystem.ComplexLetters.getTangledLetters(txtNewPassword.Text.Trim());               
                return getUAList;
            }
            catch { return null; }
        }

    }
}