using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DS.DAL.AdviitDAL;


namespace DS.Admin
{
    public partial class CreateUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {      
              if (Session["__UserId__"] == null)
            {
                Response.Redirect("~/UserLogin.aspx");
            }
        }

        private void checkUserPrivilegeInfo()
        {
            try
            {
                string userId = Session["__UserId__"].ToString();
                string userType = Session["__UserType__"].ToString();
                if (userType != "Super Admin")
                {
                    DataTable dt = new DataTable();
                    SqlParameter[] prms = { new SqlParameter("@UserId", userId) };

                    sqlDB.fillDataTable("Select  ModuleName, ViewAction, AddAction, EditAction from UserPrivilege where " +
                        " UserId=@UserId and ModuleName='Register' ", prms, dt);
                    // txtUserId.Text = dt.Rows[0]["UserId"].ToString();
                    if (dt.Rows.Count == 0) Response.Redirect("/Default.aspx");
                    else
                    {
                        
                    }
                }

            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
            }
        }

        private Boolean saveUserAccount()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("saveUserAccount", sqlDB.connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Title", txtTitle.Text.Trim());
                cmd.Parameters.AddWithValue("@FirstName", txtFirstName.Text.Trim());
                cmd.Parameters.AddWithValue("@MiddleName", txtMiddleName.Text.Trim());
                cmd.Parameters.AddWithValue("@LastName", txtLastName.Text.Trim());
                cmd.Parameters.AddWithValue("@Initial", txtInitial.Text.Trim());
                cmd.Parameters.AddWithValue("@UserName", txtUserName.Text.Trim());
                cmd.Parameters.AddWithValue("@UserPassword", adviitSecurity.crypto(txtUserPassword.Text.Trim(), true));
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                cmd.Parameters.AddWithValue("@UserType", ddlUserType.Text);
                cmd.Parameters.AddWithValue("@CreatedBy", "0");

                int result = (int)cmd.ExecuteScalar();
                if (result > 0)
                {
                    clearText();
                    lblMessage.InnerText = "success->Successfully saved";
                }
                else lblMessage.InnerText = "error->Unable to save";
                return true;
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
                return false;
            }
        }

        private void clearText()
        {
            try
            {
                txtFirstName.Text = "";
                txtMiddleName.Text = "";
                txtInitial.Text = "";
                txtEmail.Text = "";
                txtLastName.Text = "";
                txtTitle.Text = "";
                txtUserName.Text = "";
            }
            catch { }
        }

        protected void btnSaveUserAccount_Click(object sender, EventArgs e)
        {
            sqlDB.connectDB();
            saveUserAccount();
        }
    }
}