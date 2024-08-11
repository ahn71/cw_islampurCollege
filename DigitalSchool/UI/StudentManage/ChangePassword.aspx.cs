using DS.BLL.Admission;
using DS.BLL.ControlPanel;
using DS.BLL.ManageUser;
using DS.DAL;
using DS.DAL.ComplexScripting;
using DS.PropertyEntities.Model.ManageUser;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.StudentManage
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                lblMessage.InnerText = "";
                if (!IsPostBack)
                {
                    lblName.Text = Session["__FullName__"].ToString() + "'s DashBoard";
                    LoadStudentAccount();
                    txtUserName.Enabled = false;
                    txtOldPassword.Enabled = false;
                    txtUserName.CssClass = "input controlLength";
                    txtOldPassword.CssClass = "input controlLength";
                }
        }
        private void LoadStudentAccount()
        {
            string getstudentID = Session["__StudentId__"].ToString();           
            DataTable dt = CRUD.ReturnTableNull("select * from v_UserAccount_Student where StudentId=" + getstudentID + "");
            txtUserName.Text = DS.DAL.ComplexScripting.ComplexLetters.getEntangledLetters(dt.Rows[0]["UserName"].ToString());
            txtOldPassword.Text = DS.DAL.ComplexScripting.ComplexLetters.getEntangledLetters(dt.Rows[0]["UserPassword"].ToString());
        }
        private StudentUserAccountEntities GetFormData()
        {
            StudentUserAccountEntities trEntry = new StudentUserAccountEntities();
            trEntry.StudentId = int.Parse(Session["__StudentId__"].ToString());
            trEntry.UserPassword = ComplexLetters.getTangledLetters(txtNewPassword.Text);                 
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
                if (StdUserAccountEntry.Chagepassword() == true)
                {
                    lblMessage.InnerText = "success->Successfully Your Password Changed";
                    Response.Redirect("~/UserLogin.aspx");
                }
                else
                {
                    lblMessage.InnerText = "error->Unable to Save";
                }
            }
        }
    }
}