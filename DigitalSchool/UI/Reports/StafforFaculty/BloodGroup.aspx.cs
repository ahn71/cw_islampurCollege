using DS.BLL.ControlPanel;
using DS.DAL.AdviitDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.Reports.StafforFaculty
{
    public partial class BloodGroup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.InnerText = "";
            if (!IsPostBack) 
            {
                if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "BloodGroup.aspx", "")) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
            }
        }
        protected void rdoWithImage_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                rdoWithImage.Checked = true;
                rdoNoImage.Checked = false;
            }
            catch { }
        }

        protected void rdoNoImage_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                rdoWithImage.Checked = false;
                rdoNoImage.Checked = true;
            }
            catch { }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (rdoWithImage.Checked)
            {
                Session["__Image__"] = "withimage";
            }
            else
            {
                Session["__Image__"] = "withoutimage";
            }
            string sqlcmd = "";
            if (ddlBloodGroup.SelectedItem.Text == "All")
            {
                sqlcmd = "Select ECardNo, EName,DName,DesName,EMobile,EGender,EBloodGroup,EReligion,EPictureName from v_EmployeeInfo";
            }           
            else
            {
                sqlcmd = "Select ECardNo, EName,EMobile,DName,DesName,EMobile,EGender,EBloodGroup,EReligion,EPictureName from v_EmployeeInfo where EBloodGroup='" + ddlBloodGroup.SelectedItem.Text + "'";
            }
            DataTable dt;
            sqlDB.fillDataTable(sqlcmd, dt = new DataTable());
            if (dt.Rows.Count > 0)
            {
                Session["__BloodGroup__"] = dt;
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=EmpBloodGroup');", true);  //Open New Tab for Sever side code
            }
            else
            {
                lblMessage.InnerText = "warning->Blood Group Not Found";
            }
        }
    }
}