using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using adviitRuntimeScripting;

namespace DigitalSchool.Admin
{
    public partial class UserPrivilege : System.Web.UI.Page
    {
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            sqlDB.connectionString = SiteMaster.getConnectionString();
            if (!IsPostBack)
            loadAllUser();
            loadAllPages();
        }

        private void loadAllUser()
        {
            try
            {
                lbUserList.Items.Clear();
                 dt = new DataTable();
                sqlDB.fillDataTable("select UserName from UserAccount ",dt);
                for (int i = 0; i < dt.Rows.Count;i++) lbUserList.Items.Add(dt.Rows[i].ItemArray[0].ToString());
               
               
            }
            catch { }
        }

        private void loadAllPages()
        {
            try
            {
                dt = new DataTable();
                sqlDB.fillDataTable("select FormsName from FormsInfo",dt);
                for (int i = 0; i < dt.Rows.Count; i++) chkUserPrivilegeList.Items.Add(dt.Rows[i].ItemArray[0].ToString());
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

        protected void btnSelectAll_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i <chkUserPrivilegeList.Items.Count; i++)
                {
                    chkUserPrivilegeList.Items[i].Selected = true;
                }
            }
            catch { }
        }
    }
}