using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DS.DAL.AdviitDAL;
using System.Data.SqlClient;

namespace DS.Admin
{
    public partial class UserPrivilege : System.Web.UI.Page
    {
        DataTable dt;
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
                    loadAllUser();
                    loadAllPages();
                }
            }            
        }

        private void loadAllUser()
        {
            try
            {
                lbUserList.Items.Clear();
                dt = new DataTable();
                msg = sqlDB.fillDataTable("select UserName from UserAccount ", dt);
                if (msg == string.Empty)
                {
                    if(dt != null)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++) lbUserList.Items.Add(dt.Rows[i].ItemArray[0].ToString());
                        return;
                    }
                    else
                    {
                        msg = "Users are not found!";
                    }
                }
                lblMessage.InnerText = "error->" + msg;                          
            }
            catch { }
        }

        private void loadAllPages()
        {
            try
            {
                dt = new DataTable();
                msg = sqlDB.fillDataTable("select FormsName from FormsInfo", dt);
                if (msg == string.Empty)
                {
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++) chkUserPrivilegeList.Items.Add(dt.Rows[i].ItemArray[0].ToString());
                        return;
                    }
                    else
                    {
                        msg = "Pages are not found!";
                    }
                }
                lblMessage.InnerText = "error->" + msg;       
                
            }
            catch { }
        }
        protected void lbUserList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtUserName.Text = lbUserList.SelectedItem.Value.ToString(); ;
            }
            catch { }
        }


        protected void btnAllSelect_Click(object sender, EventArgs e)
        {
            try
            {
                
                for (int i = 0; i < chkUserPrivilegeList.Items.Count; i++)
                {
                    if (btnAllSelect.Text =="Select All")
                    chkUserPrivilegeList.Items[i].Selected = true;
                    else chkUserPrivilegeList.Items[i].Selected = false;
                    if (i == chkUserPrivilegeList.Items.Count - 1)
                    {
                        if (btnAllSelect.Text == "Select All") btnAllSelect.Text = "Unselect All";
                        else btnAllSelect.Text = "Select All";
                    }
                }
            }
            catch { }
        }

        protected void btnSet_Click(object sender, EventArgs e)
        {
            if (btnSet.Text == "Set") saveUserPrivilege();
        }

        private Boolean saveUserPrivilege()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlParameter[] prms = { new SqlParameter("@UserName", txtUserName.Text.Trim()) };
                sqlDB.fillDataTable("Select UserId,UserType from UserAccount where UserName=@UserName ", prms, dt);

                for (int i = 0; i < chkUserPrivilegeList.Items.Count; i++)
                {
                    if (chkUserPrivilegeList.Items[i].Selected == true)
                    {
                        SqlCommand cmd = new SqlCommand("Insert into  UserPrivilege (UserId, ModuleName, MenuId, ViewAction, " +
                            " AddAction, EditAction, HideAction)  values (@UserId, @ModuleName, @MenuId, @ViewAction, @AddAction, " +
                            " @EditAction, @HideAction) ", sqlDB.connection);

                        cmd.Parameters.AddWithValue("@UserId", dt.Rows[0]["UserId"].ToString());
                        cmd.Parameters.AddWithValue("@ModuleName", chkUserPrivilegeList.Items[i].Text);
                        cmd.Parameters.AddWithValue("@MenuId", "0");

                        if (chkRead.Checked) cmd.Parameters.AddWithValue("@ViewAction", "1");
                        else cmd.Parameters.AddWithValue("@ViewAction", "0");

                        if (chkWrite.Checked) cmd.Parameters.AddWithValue("@AddAction", "1");
                        else cmd.Parameters.AddWithValue("@AddAction", "0");

                        if (chkUpdate.Checked) cmd.Parameters.AddWithValue("@EditAction", "1");
                        else cmd.Parameters.AddWithValue("@EditAction", "0");

                        cmd.Parameters.AddWithValue("@HideAction", "0");

                        int result = (int)cmd.ExecuteNonQuery();
                    }
                }

                //if (result > 0) lblMessage.InnerText = "success->Successfully saved";
              //  else lblMessage.InnerText = "error->Unable to save";

                return true;

            }
            catch (Exception ex)
            {
               // lblMessage.InnerText = "error->" + ex.Message;
                return false;
            }
        }
    }
}